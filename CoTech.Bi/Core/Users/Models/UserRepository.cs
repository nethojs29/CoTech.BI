using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Users.Controllers;
using CoTech.Bi.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Users.Models
{
    public class UserRepository
    {
        private readonly BiContext context;
        private DbSet<UserEntity> db {
          get { return context.Set<UserEntity>(); }
        }

        public UserRepository(BiContext context)
        {
          this.context = context;
        }

<<<<<<< HEAD
        public List<UserResponse.UserNoPassRes> GetAll() {
            var user = db.ToList();
            return user.Select(usr =>
            {
                return new UserResponse.UserNoPassRes(
                    usr.Id,
                    usr.Name,
                    usr.Lastname,
                    usr.Email);
            }).ToList();
=======
        public Task<List<UserEntity>> GetAll() {
          return db.Where(u => !u.DeletedAt.HasValue).ToListAsync();
>>>>>>> ead6e53eef003a0a020f5408186c97db43011bbc
        }
    }
}