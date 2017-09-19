using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using CoTech.Bi.Entity;
using CoTech.Bi.Core.Users.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using CoTech.Bi.Core.Permissions.Models;

namespace CoTech.Bi.Util
{

    public static class RootUsersSeedMigrationHelper
    {
        private static List<UserEntity> users = new List<UserEntity> {
            new UserEntity {
                Name = "Roberto",
                Lastname = "Montaño",
                Email = "lmontano@cotecnologias.com", // debe estar en minusculas
                Password = "benancio", // será hasheada
                EmailConfirmed = true
            },
            new UserEntity {
                Name = "Ernesto",
                Lastname = "Jaramillo",
                Email = "jjaramillo@cotecnologias.com",
                Password = "naranja",
                EmailConfirmed = true
            }
        };
        public static void UpRootUsers(this MigrationBuilder migrationBuilder)
        {
            using (var db = new BiContext()){
                var dbUser = db.Set<UserEntity>();
                var dbRoot = db.Set<RootEntity>();
                var hasher = new PasswordHasher<UserEntity>();
                
                users.ForEach(u => {
                    u.Password = hasher.HashPassword(u, u.Password);
                    var entry = dbUser.Add(u);
                });
                db.SaveChanges();
                users.ForEach(u => {
                    var foundUser = dbUser.First(dbu => dbu.Email == u.Email);
                    dbRoot.Add(new RootEntity {
                        UserId = foundUser.Id
                    });
                });
                db.SaveChanges();
            }

        }

        public static void DownRootUsers(this MigrationBuilder migrationBuilder)
        {
            using (var db = new BiContext()){
                var dbUser = db.Set<UserEntity>();
                users.ForEach(u => { // no tengo ids
                    var foundUser = dbUser.First(dbu => dbu.Email == u.Email);
                    dbUser.Remove(foundUser);
                });
                db.SaveChanges();
            }
        }
    }
}