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

        public List<UserResponse.UserNoPassRes> GetAll() {
            var user = db.Where(u => !u.DeletedAt.HasValue).ToList();
            return user.Select(usr =>
            {
                return new UserResponse.UserNoPassRes(
                    usr.Id,
                    usr.Name,
                    usr.Lastname,
                    usr.Email);
            }).ToList();
        }
    }
}