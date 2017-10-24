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
        
        public ReplyRepository(BiContext context){
            this.context = context;
        }

        public IObservable<MessageEntity> UserMessages(long userId) {
            
            var obs = DbObservable<BiContext>.FromInserted<MessageEntity>()
                .Where(m => m.Entity.Group.UsersList.Any(u => u.UserId == m.Entity.UserId))
                .Select(n => n.Entity);
            return obs;
        }

        public async Task<MessageEntity> SearchOrCreateGroup(long company, long user, long creator,
            int type, MessageEntity message){
            if (_group.Count(g =>
                    g.Category == type && g.CompanyId == company && g.UsersList.Exists(u => u.UserId == user) &&
                    g.UsersList.Exists(u => u.UserId == creator)) == 0)
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
                var groupList = _group.FirstOrDefault(g =>
                    g.Category == type && g.CompanyId == company && g.UsersList.Exists(u => u.UserId == user) &&
                    g.UsersList.Exists(u => u.UserId == creator));
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
                    return _Message.Find(message.Id);
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