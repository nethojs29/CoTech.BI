using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Loader;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Entity
{
    public interface IDbInitializer
    {
        void Initialize();
    }
    public class DbInitializer : IDbInitializer
    {
        private readonly BiContext _context;
        private readonly UserManager<UserEntity> _userManager;
        private DbSet<RootEntity> _dbRoot;

        public DbInitializer(
            BiContext context,
            UserManager<UserEntity> userManager)
        {
            _context = context;
            _userManager = userManager;
            _dbRoot = _context.Set<RootEntity>();
        }

        //This example just creates an Administrator role and one Admin users
        public void Initialize()
        {
            /*
            //create database schema if none exists
            _context.Database.EnsureCreated();

            //If there is already an Administrator role, abort
            if (_context.Roles.Any(r => r.Name == "Administrator")) return;

            //Create the Administartor Role
            await _roleManager.CreateAsync(new IdentityRole("Administrator"));
            
            */

            //Create the default Admin account and apply the Administrator role

            if (!_context.Database.EnsureCreated())
            {
                var listUsers = new List<UserEntity>();
                listUsers.Add(new UserEntity()
                {
                    Name = "Luis",
                    Lastname = "Moya",
                    Email = "lmoya@cotecnologias.com",
                    EmailConfirmed = true,
                    Password = "prueba123"
                });
                listUsers.Add(new UserEntity {
                    Name = "Roberto",
                    Lastname = "Montaño",
                    Email = "lmontano@cotecnologias.com",
                    EmailConfirmed = true,
                    Password = "benancio"
                });

                foreach (UserEntity item in listUsers)
                {
                    var userIdentityResult = _userManager.CreateAsync(item, item.Password).Result;
                    if(userIdentityResult.Succeeded)
                    {
                        var counter = _dbRoot.Where(r => r.UserId == item.Id).ToList().Count;
                        if(counter == 0)
                        {
                            _dbRoot.Add(new RootEntity() {User = item, UserId = item.Id});
                            _context.SaveChanges();
                        }
                    }
                } 
                _context.BiInitialize(_userManager);
            }
        }
    }
}