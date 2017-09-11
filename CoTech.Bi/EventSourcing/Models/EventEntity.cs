using System;
using System.ComponentModel.DataAnnotations.Schema;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Util;
using Newtonsoft.Json;

namespace CoTech.Bi.Core.EventSourcing.Models
{
    public class EventEntity
    {
        public Guid Id { get; set; }
        public long UserId {get; set; }
        public UserEntity User { get; set; }
        [NotMapped]
        public object Body { get; set; }
        [Column("Body")]
        private string BodyJson { 
          get { return JsonConvert.SerializeObject(Body, JsonConverterOptions.JsonTypedSettings); }
          set {
            Body = string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject(value, JsonConverterOptions.JsonTypedSettings);
          }
        }
    }
}