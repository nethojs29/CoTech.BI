using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Users.Controllers;
using CoTech.Bi.Entity;
using Microsoft.AspNetCore.Identity;
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

        public Task<List<UserEntity>> InCompany(long companyId){
          return db.Where(u => u.Permissions.Any(p => p.CompanyId == companyId))
            .ToListAsync();
        }

        public Task<List<UserEntity>> GetRootUsers() {
          return db.Where(u => u.Root != null).ToListAsync();
        }
        public Task<int> UpdateUsers(UserEntity user)
        {
            db.Update(user);
            return context.SaveChangesAsync();
        }

        public int AddUser(string email, string name, string lastname, string password)
        {
            var user = new UserEntity();
            user.Email = email;
            user.Name = name;
            user.Lastname = lastname;
            user.Password = password;
            db.Add(user);
            return context.SaveChanges();
        }
    }
}