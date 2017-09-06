using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using CoTech.Bi.Entity;
using CoTech.Bi.Core.Users.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using CoTech.Bi.Core.Permissions.Model;

namespace CoTech.Bi.Migrations
{
    /// <summary>
    /// Esta clase no fue creada automáticamente.
    /// Si se cambia la clase UserEntity, se puede eliminar este archivo y 
    /// volver a poner el codigo en un migration vacío
    /// Mlp -Nan
    /// </summary>
    public partial class RootUsersSeed : Migration
    {
        List<UserEntity> users = new List<UserEntity> {
            new UserEntity {
                Name = "Roberto",
                Lastname = "Montaño",
                Email = "lmontano@cotecnologias.com", // debe estar en minusculas
                Password = "benancio", // será hasheada
                EmailConfirmed = true
            }
        };
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var db = new BiContext()){
                var dbUser = db.Set<UserEntity>();
                var dbPermission = db.Set<PermissionEntity>();
                var hasher = new PasswordHasher<UserEntity>();
                
                users.ForEach(u => {
                    u.Password = hasher.HashPassword(u, u.Password);
                    var entry = dbUser.Add(u);
                });
                db.SaveChanges();
                users.ForEach(u => {
                    var foundUser = dbUser.First(dbu => dbu.Email == u.Email);
                    dbPermission.Add(new PermissionEntity {
                        UserId = foundUser.Id,
                        CompanyId = -1,
                        RoleId = Role.Root
                    });
                });
                db.SaveChanges();
            }

        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
