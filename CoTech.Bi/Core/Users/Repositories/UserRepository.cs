using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.EventSourcing.Repositories;
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
        private IPasswordHasher<UserEntity> passwordHasher;
        private readonly EventRepository eventRepository;

        private DbSet<UserEntity> db {
          get { return context.Set<UserEntity>(); }
        }

        public UserRepository(BiContext context, 
                              IPasswordHasher<UserEntity> passwordHasher,
                              EventRepository eventRepository)
        {
          this.context = context;
          this.passwordHasher = passwordHasher;
          this.eventRepository = eventRepository;
        }

        public Task<List<UserEntity>> GetAll() {
            return db.Where(u => !u.DeletedAt.HasValue).ToListAsync();
        }

        public Task<List<UserEntity>> InCompany(long companyId){
          return db.Where(u => u.Permissions.Any(p => p.CompanyId == companyId))
            .Include(u => u.Permissions)
            .ToListAsync();
        }

        public Task<List<UserEntity>> GetRootUsers() {
          return db.Where(u => u.Root != null).ToListAsync();
        }

        public Task<UserEntity> WithId(long id) {
          return db.FindAsync(id);
        }

        /// <summary>
        /// Regresa el usuario, si existe, asociado con el correo dado
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<UserEntity> WithEmail(string email) {
          return db.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Crea un usuario, hasheando la contraseña y asegurando que el email no esté ocupado
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public async Task<UserEntity> Create(CreateUserCmd cmd) {
          var evt = UserCreatedEvt.MakeEventEntity(cmd);
          var insertions = await eventRepository.Create(evt);
          if(insertions == 0) return null;
          var user = await db.FirstAsync(u => u.CreatorEventId == evt.Id);
          var hashedPass = passwordHasher.HashPassword(user, cmd.Password);
          evt = PasswordChangedEvt.MakeEventEntity(cmd, user.Id, hashedPass);
          await eventRepository.Create(evt);
          return user;
        }

      public Task<int> Update(UserEntity entity)
      {
        db.Update(entity);
        return context.SaveChangesAsync();
      }
    }
}