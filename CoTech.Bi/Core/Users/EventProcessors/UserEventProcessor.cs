using System;
using System.Linq;
using System.Reactive.Linq;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Users.EventProcessors
{
    public class UserEventProcessor
    {
        private System.IObservable<IBeforeEntry<EventEntity, BiContext>> eventObservable;

        public UserEventProcessor() {
          eventObservable = DbObservable<BiContext>
            .FromInserting<EventEntity>()
            .Where(e => e.Entity.Body is UserEvent);
          eventObservable.Where(e => e.Entity.Body is UserCreatedEvt)
            .Subscribe(onCreate);
          eventObservable.Where(e => e.Entity.Body is UserUpdatedEvt)
            .Subscribe(onUpdate);
          eventObservable.Where(e => e.Entity.Body is PasswordChangedEvt)
            .Subscribe(onPasswordChange);
        }

        private void onCreate(IBeforeEntry<EventEntity, BiContext> entry) {
          var db = entry.Context.Set<UserEntity>();
          var body = entry.Entity.Body as UserCreatedEvt;
          var user = new UserEntity{
            CreatorEventId = entry.Entity.Id,
            Name = body.Name,
            Lastname = body.Lastname,
            Email = body.Email,
          };
          if(db.FirstOrDefault(u => u.Email == user.Email) != null) {
            entry.Cancel = true;
            return;
          }
          db.Add(user);
        }

        private void onUpdate(IBeforeEntry<EventEntity, BiContext> entry) {
          var db = entry.Context.Set<UserEntity>();
          var body = entry.Entity.Body as UserUpdatedEvt;
          var user = db.Find(body.Id);
          if (user == null) {
            entry.Cancel = true;
            return;
          }
          user.Name = body.Name;
          user.Lastname = body.Lastname;
          db.Add(user);
        }

        private void onPasswordChange(IBeforeEntry<EventEntity, BiContext> entry) {
          var db = entry.Context.Set<UserEntity>();
          var body = entry.Entity.Body as PasswordChangedEvt;
          var user = db.Find(body.UserId);
          if(user == null){
            entry.Cancel = true;
            return;
          }
          user.Password = body.HashedPassword;
          db.Update(user);
        }
    }
}