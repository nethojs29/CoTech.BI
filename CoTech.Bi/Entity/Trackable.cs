using System;
using EntityFrameworkCore.Triggers;

namespace CoTech.Bi.Entity
{
    public abstract class Trackable {
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        static Trackable() {
          Triggers<Trackable>.Inserting += e => e.Entity.CreatedAt = e.Entity.UpdatedAt = DateTime.UtcNow;
          Triggers<Trackable>.Updating += e => e.Entity.UpdatedAt = DateTime.UtcNow;
          
        }
    }

    public abstract class SoftDeletable {
        public DateTime? DeletedAt { get; protected set; }

        static SoftDeletable() {
          Triggers<SoftDeletable>.Deleting += e => {
            e.Cancel = true;
            e.Entity.DeletedAt = DateTime.UtcNow;
          };
        }
    }
}