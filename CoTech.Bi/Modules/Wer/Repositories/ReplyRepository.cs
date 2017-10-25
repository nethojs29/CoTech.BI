using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Notifications.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Wer.Models.Entities;
using EntityFrameworkCore.Rx;
using Microsoft.EntityFrameworkCore;

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
        
        public ReplyRepository(BiContext context){
            this.context = context;
        }

        public IObservable<MessageEntity> UserMessages(long userId) {
            
            var obs = DbObservable<BiContext>.FromInserted<MessageEntity>()
                .Where(m => m.Entity.Group.UsersList.Any(u => u.UserId == m.Entity.UserId))
                .Select(n => n.Entity);
            return obs;
        }

        public Task<List<GroupEntity>> SearchGroups(long user)
        {
            return _group.Include(g => g.UsersList)
                .ThenInclude(u => u.User)
                .Where(g => g.UsersList.Any(u => u.UserId == user))
                .ToListAsync();
        }

        public Task<PartyEntity> UpdateParty(long company, long user, long creator,
            int type, MessageEntity message)
        {
            var group = _group.Include(g => g.UsersList)
                .Where( g=>
                    g.UsersList.Any(u => u.UserId == user) &&
                    g.UsersList.Any(u => u.UserId == creator))
                .FirstOrDefault( g => 
                    g.Category == type && 
                    g.CompanyId == company
                );
            var usr = group.UsersList.FirstOrDefault(u => u.UserId == creator);
            if (user != null)
            {
                var aux = usr;
                aux.DateIn = DateTime.Now;
                context.Entry(usr).CurrentValues.SetValues(aux);
                context.SaveChanges();
                return usr;
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
                    message.Seen = new List<SeenMessagesEntity>
                    {
                        new SeenMessagesEntity(){
                            Message = message,
                            UserId = creator
                    }  
                    };
                    _Message.Add(message);
                    context.SaveChanges();
                    return message;
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
                    message.Seen = new List<SeenMessagesEntity>
                    {
                        new SeenMessagesEntity(){
                            Message = message,
                            UserId = creator
                        }  
                    };
                    _Message.Add(message);
                    context.SaveChanges();
                    return message;
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