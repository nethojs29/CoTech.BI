using System;
using System.Collections.Generic;
using System.Linq;
using CoTech.Bi.Modules.Wer.Models.Entities;

namespace CoTech.Bi.Modules.Wer.Models.Responses
{
    public class MessageResponse
    {
        public long Id { set; get; }
        public long GroupId { set; get; }
        public string Message { set; get; }
        public string Tags { set; get; }
        public DateTime CreatedAt { set; get; }
        public long UserId { set; get; }
        public long WeekId { set; get; }
        public long Timestamp { set; get; }

        public MessageResponse(MessageEntity entity)
        {
            this.Id = entity.Id;
            this.UserId = entity.UserId;
            this.WeekId = entity.WeekId;
            this.CreatedAt = entity.CreatedAt;
            this.GroupId = entity.GroupId;
            this.Message = entity.Message;
            this.Tags = entity.Tags;
            this.Timestamp = (long)entity.CreatedAt.Subtract(new DateTime(1970, 1,1)).TotalMilliseconds;
        }
    }
}