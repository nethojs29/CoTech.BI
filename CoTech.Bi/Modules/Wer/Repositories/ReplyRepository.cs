using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Notifications.Models;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Models.Requests;
using CoTech.Bi.Modules.Wer.Models.Responses;
using EntityFrameworkCore.Rx;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Remotion.Linq.Clauses;

namespace CoTech.Bi.Modules.Wer.Repositories
{
    public class ReplyRepository
    {
        private BiContext context;
        
        private DbSet<MessageEntity> _Message {
            get { return context.Set<MessageEntity>(); }
        }

        private DbSet<GroupEntity> _group
        {
            get { return context.Set<GroupEntity>(); }
        }
        
        private DbSet<PartyEntity> _party
        {
            get { return context.Set<PartyEntity>(); }
        }
        
        private DbSet<PermissionEntity> _Permission
        {
            get { return context.Set<PermissionEntity>(); }
        }
        
        public ReplyRepository(BiContext context){
            this.context = context;
        }
        

        public IObservable<MessageEntity> UserMessages(long userId) {
            
            var obs = DbObservable<BiContext>.FromInserted<MessageEntity>()
                .Where(m => m.Entity.Group.UsersList.Any(u => u.UserId == m.Entity.UserId))
                .Select(n => n.Entity);
            return obs;
        }

        public async Task<List<GroupResponse>> SearchGroups(long user)
        {
            var groupsFound = new List<GroupResponse>();
            var groups = await _group.Include(g => g.UsersList)
                .ThenInclude(u => u.User)
                .Where(g => g.UsersList.Any(u => u.UserId == user))
                .ToListAsync();
            foreach (var group in groups)
            {
                groupsFound.Add(new GroupResponse(group,this.messagesNotView(user,group.Id)));
            }
            return groupsFound;
        }

        public GroupResponse GetGroup(long idGroup)
        {
            return _group
                .Where(g => g.Id == idGroup)
                .Include(g => g.User)
                .Include(g => g.UsersList)
                .ThenInclude(u => u.User)
                .Select(g => new GroupResponse(g))
                .FirstOrDefault();
        }

        public Task<List<MessageResponse>> GetMessage(long user,long idGroup, long idMessage, int count)
        {
            if (idMessage == 0)
            {
                return _Message.Include(m => m.User).Include(m => m.Group).ThenInclude(g =>g.UsersList)
                    .Include(m => m.Group).ThenInclude(g => g.User)
                    .Where(m => m.GroupId == idGroup)
                    .Where(m => m.Group.UsersList.Any(u => u.UserId == user))
                    .Select(m => new MessageResponse(m))
                    .OrderByDescending(m => m.CreatedAt)
                    .Take(30)
                    .ToListAsync();
            }
            return _Message.Include(m => m.User).Include(m => m.Group).ThenInclude(g =>g.UsersList)
                .Include(m => m.Group).ThenInclude(g => g.User)
                .Where(m => m.GroupId == idGroup && m.Id > idMessage)
                .Where(m => m.Group.UsersList.Any(u => u.UserId == user))
                .Select(m => new MessageResponse(m))
                .OrderByDescending(m => m.CreatedAt)
                .Take(30)
                .ToListAsync();
        }

        public PartyEntity UpdateParty(long company, long user, long creator,
            int type)
        {
            var group = _group.Include(g => g.UsersList)
                .Where( g=>
                    g.UsersList.Any(u => u.UserId == user) &&
                    g.UsersList.Any(u => u.UserId == creator))
                .FirstOrDefault( g => 
                    g.Category == type && 
                    g.CompanyId == company
                );
            if (group != null)
            {
                var usr = group.UsersList.FirstOrDefault(u => u.UserId == creator);
                if (usr != null)
                {
                    var entity = _party.Include(g => g.Group)
                        .ThenInclude(g => g.UsersList).ThenInclude(p => p.User)
                        .First(u => u.Id == usr.Id);
                    var aux = entity;
                    aux.DateIn = DateTime.Now;
                    context.Entry(entity).CurrentValues.SetValues(aux);
                    context.SaveChanges();
                    return entity;
                }
            }
            return null;
        }
        public PartyEntity FindParty(long company, long user, long creator,
            int type)
        {
            var group = _group.Include(g => g.UsersList)
                .Where( g=>
                    g.UsersList.Any(u => u.UserId == user) &&
                    g.UsersList.Any(u => u.UserId == creator))
                .FirstOrDefault( g => 
                    g.Category == type && 
                    g.CompanyId == company
                );
            if (group != null)
            {
                var usr = group.UsersList.FirstOrDefault(u => u.UserId == creator);
                if (usr != null)
                {
                    var entity = _party
                        .First(u => u.Id == usr.Id);
                    return entity;
                }
            }
            return null;
        }

        public List<MessageResponse> messagesNotView(long user, long group)
        {
            var party = _party.FirstOrDefault(p => p.UserId == user && p.GroupId == group);
            if (party != null)
            {
                var message = _Message
                    .Include(m => m.Group)
                    .Include(m => m.User)
                    .Where(m => m.CreatedAt.CompareTo(party.DateIn) >= 0 && m.GroupId == group)
                    .Select(m => new MessageResponse(m))
                    .ToList();
                return message;
            }
            return null;
        }

        public async Task<bool> isResUser(long userId, long companyId) {
            var companyDb = context.Set<CompanyEntity>();
            var company = await companyDb.FindAsync(companyId);
            var lowerBound = 600;
            while(company != null) {
                var permission = await _Permission
                    .FirstOrDefaultAsync(p => p.UserId == userId 
                        && p.CompanyId == company.Id 
                        && p.RoleId > lowerBound
                        && p.RoleId < 604
                    );
                if (permission != null) return true;
                if (!company.ParentId.HasValue) return false;
                company = await companyDb.FindAsync(company.ParentId.Value);
                lowerBound = 601;
            }
            return false;
        }

        public async Task<GroupResponse> CreateGroup(GroupRequest group, long creator)
        {
            if (await isResUser(group.UserId, group.CompanyId))
            {
                var groupExists = await _group
                    .AnyAsync(g => g.Category == group.Type 
                        && g.CompanyId == group.CompanyId 
                        && g.UserId == group.UserId
                        && g.UsersList.Any(u => u.Id == creator)
                    );
                if (groupExists)
                {
                    return null;
                }
                var entity = new GroupEntity
                {
                    Category = group.Type,
                    UserId = group.UserId,
                    CompanyId = group.CompanyId,
                    UsersList = new List<PartyEntity>
                    {
                        new PartyEntity()
                        {
                            UserId = creator
                        },
                        new PartyEntity()
                        {
                            UserId = group.UserId
                        }
                    }
                };
                _group.Add(entity);
                await context.SaveChangesAsync();
                return await _group
                        .Include(g => g.User)
                        .Include(g => g.UsersList)
                        .ThenInclude(g => g.User)
                    .Where(g => g.Id == entity.Id).Select(g => new GroupResponse(g))
                    .FirstOrDefaultAsync();
            }
            return null;
        }
        
        public async Task<MessageEntity> SearchOrCreateGroup(long company, long user, long creator,
            int type, MessageEntity message){
            if (_group.Include(g => g.UsersList).Where(g => g.UsersList.Any(u => u.UserId == creator) &&
                    g.UsersList.Any(u => u.UserId == user)).Any(g =>
                g.Category == type && g.CompanyId == company) == false)
            {
                var group = new GroupEntity()
                {
                    Category = type,
                    CompanyId = company,
                    UserId = user
                };
                var party = new List<PartyEntity>
                {
                    new PartyEntity()
                    {
                        Group = @group,
                        UserId = user
                    },
                    new PartyEntity()
                    {
                        Group = @group,
                        UserId = creator
                    }
                };
                group.UsersList = party;
                _group.Add(group);
                context.SaveChanges();
                if (group.Id > 0)
                {
                    message.GroupId = group.Id;
                    _Message.Add(message);
                    context.SaveChanges();
                    return _Message
                        .Include(m => m.Group)
                        .ThenInclude(g => g.UsersList)
                        .Include(m => m.User)
                        .First(m => m.Id == message.Id);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                var groupList = _group.Include(g => g.UsersList)
                    .Where( g=>
                        g.UsersList.Any(u => u.UserId == user) &&
                        g.UsersList.Any(u => u.UserId == creator))
                    .FirstOrDefault( g => 
                    g.Category == type && 
                    g.CompanyId == company
                    );
                if (groupList != null)
                {
                    message.GroupId = groupList.Id;
                    _Message.Add(message);
                    context.SaveChanges();
                    return _Message
                        .Include(m => m.Group)
                        .ThenInclude(g => g.UsersList)
                        .Include(m => m.User)
                        .First(m => m.Id == message.Id);
                }
                else
                {
                    return null;
                }
            }
            
        }

        public async Task Create(MessageEntity entity){
            _Message.Add(entity);
            await context.SaveChangesAsync();
        } 
    }
}