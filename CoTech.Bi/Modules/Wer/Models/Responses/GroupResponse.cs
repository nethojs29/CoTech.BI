using System;
using System.Collections.Generic;
using System.Linq;
using CoTech.Bi.Modules.Wer.Models.Entities;

namespace CoTech.Bi.Modules.Wer.Models.Responses
{
    public class GroupResponse
    {
        public long Id { set; get; }
        public long UserId { set; get; }
        public long CompanyId { set; get; }
        public int Type { set; get; }
        public List<PartyResponse> Party { set; get; }
        public List<MessageResponse> messages { set; get; } = new List<MessageResponse>();

        public GroupResponse(GroupEntity entity)
        {
            this.Id = entity.Id;
            this.UserId = entity.UserId;
            this.Type = entity.Category;
            this.CompanyId = entity.CompanyId;
            this.Party = entity.UsersList.Select(u => new PartyResponse(u)).ToList();
        }
        public GroupResponse(GroupEntity entity,List<MessageResponse> messages)
        {
            this.Id = entity.Id;
            this.UserId = entity.UserId;
            this.Type = entity.Category;
            this.CompanyId = entity.CompanyId;
            this.Party = entity.UsersList.Select(u => new PartyResponse(u)).ToList();
            this.messages = messages ?? this.messages;
        }
    }

    public class PartyResponse
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Lastname { set; get; }
        public string Email { set; get; }
        public DateTime DateIn { set; get; }
        public long Timestamp { set; get; }

        public PartyResponse(PartyEntity entity)
        {
            this.Id = entity.UserId;
            this.Name = entity.User.Name;
            this.Lastname = entity.User.Lastname;
            this.DateIn = entity.DateIn;
            this.Email = entity.User.Email;
            this.Timestamp = (long)entity.DateIn.Subtract(new DateTime(1970, 1,1)).TotalMilliseconds;
        }
    }
}