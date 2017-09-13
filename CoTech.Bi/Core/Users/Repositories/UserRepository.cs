using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Users.Controllers;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Users.Repositories
{
    public class UserRepository
    {
        private readonly BiContext context;
        private UserManager<UserEntity> userManager;
        private DbSet<UserEntity> db {
          get { return context.Set<UserEntity>(); }
        }

        public UserRepository(BiContext context, 
                              UserManager<UserEntity> userManager)
        {
          this.context = context;
          this.userManager = userManager;
        }

        public Task<List<UserEntity>> GetAll() {
            return db.Where(u => !u.DeletedAt.HasValue).ToListAsync();
        }

        public Task<List<UserEntity>> InCompany(Guid companyId){
          return db.Where(u => u.Permissions.Any(p => p.CompanyId == companyId))
            .ToListAsync();
        }

        public Task<List<UserEntity>> GetRootUsers() {
          return db.Where(u => u.Root != null).ToListAsync();
        }

        /// <summary>
        /// Regresa el usuario, si existe, asociado con el correo dado
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<UserEntity> WithEmail(string email) {
          return userManager.FindByEmailAsync(email);
        }

        /// <summary>
        /// Crea un usuario, hasheando la contraseña y asegurando que el email no esté ocupado
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<IdentityResult> Create(UserEntity entity, string password) {
          return await userManager.CreateAsync(entity, password);
        }
    }
}