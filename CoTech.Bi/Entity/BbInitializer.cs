using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Core.Permissions.Model;

namespace CoTech.Bi.Entity
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
    public class DbInitializer : IDbInitializer
    {
        private readonly BiContext _context;
        private readonly UserManager<UserEntity> _userManager;

        public DbInitializer(
            BiContext context,
            UserManager<UserEntity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //This example just creates an Administrator role and one Admin users
        public async Task Initialize()
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
            string user = "lmoya@cotecnologias.com";
            string password = "prueba123";

            var newUser = new UserEntity { 
                Name = "Luis",
                Lastname = "Moya", 
                Email = user, 
                EmailConfirmed = true,
                Root = new RootEntity()
            };
            var result = await _userManager.CreateAsync(newUser, password);
            var bdRoot = _context.Set<RootEntity>();
            bdRoot.Add(new RootEntity{
                UserId = newUser.Id
            });
            await _context.SaveChangesAsync();
            //await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), "Administrator");
        }
    }
}