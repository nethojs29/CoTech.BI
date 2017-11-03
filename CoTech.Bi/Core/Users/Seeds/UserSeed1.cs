using System.Collections.Generic;
using System.Linq;
using System;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Users.Seeds
{
  public class UserSeed1 : ISeed
  {
    public int Version => 1;

    public void Up(BiContext context)
    {
      var dbUser = context.Set<UserEntity>();
      var passwordHasher = new PasswordHasher<UserEntity>();
      listUsers.ForEach(user => user.Password = passwordHasher.HashPassword(user, user.Password));
      dbUser.AddRange(listUsers);
      context.SaveChanges(); // Not async
    }

    public void Down(BiContext context)
    {
      var dbUser = context.Set<UserEntity>();
      var users = dbUser.Where(u => listUsers.Any(lu => lu.Email == u.Email)).ToList();
      dbUser.RemoveRange(users);
      context.SaveChanges();
    }

    private List<UserEntity> listUsers = new List<UserEntity> {
          new UserEntity {
              Name = "Luis",
              Lastname = "Moya",
              Email = "lmoya@cotecnologias.com",
              EmailConfirmed = true,
              Password = "prueba123",
              Root = new RootEntity()
          },
          new UserEntity {
              Name = "Roberto",
              Lastname = "Montaño",
              Email = "lmontano@cotecnologias.com",
              EmailConfirmed = true,
              Password = "benancio",
              Root = new RootEntity()
          }
    };

  }
}