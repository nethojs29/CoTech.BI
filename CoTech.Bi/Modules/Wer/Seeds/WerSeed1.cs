using System.Collections.Generic;
using System.Linq;
using System;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Modules.Wer.Seeds
{
  public class WerSeed1 : ISeed
  {
    public int Version => 1;

    public void Up(BiContext context)
    {
      var dbCompany = context.Set<CompanyEntity>();
      var dbUser = context.Set<UserEntity>();
      var passwordHasher = new PasswordHasher<UserEntity>();
      dbCompany.AddRange(companies);
      context.SaveChanges();
      var savedCompanies = dbCompany.ToList();
      var companyDic = savedCompanies.ToDictionary(c => c.Url);
      var seededUsers = users(companyDic);
      seededUsers.ForEach(u => u.Password = passwordHasher.HashPassword(u, u.Password));
      dbUser.AddRange(seededUsers);
      context.SaveChanges();
    }

    public void Down(BiContext context)
    {
      var dbCompany = context.Set<CompanyEntity>();
      var dbUser = context.Set<UserEntity>();
      var persistedCompanies = dbCompany.Where(c => companies.Any(sc => sc.Url == c.Url));
      dbCompany.RemoveRange(persistedCompanies);
      var companyDic = companies.ToDictionary(c => c.Url);
      var seededUsers = users(companyDic);
      var persistedUsers = dbUser.Where(u => seededUsers.Any(lu => lu.Email == u.Email)).ToList();
      dbUser.RemoveRange(persistedUsers);
      context.SaveChanges();
    }

    /// <summary>
    /// Es una funcion porque ocupa que las empresas ya esten añadidas (dbCompany.AddRange(companies))
    /// Aunque yo creo que si le hubiera dejado Company en vez de CompanyId no ocupaba. Son las 12:48am
    /// </summary>
    /// <param name="companies"></param>
    /// <returns></returns>
    private List<UserEntity> users(Dictionary<string, CompanyEntity> companies) => new List<UserEntity>{
            new UserEntity(){
              // Id = 4,
              Name = "Francisco",
              Lastname = "Esquer",
              Email = "fesquer@opessa.net",
              Password = "fesquer",
              EmailConfirmed=true,
              Permissions = new List<PermissionEntity> {
                new PermissionEntity()
                {
                    RoleId = 601,
                    CompanyId  = companies["antejo"].Id
                }, new PermissionEntity()
                {
                    RoleId = 601,
                    CompanyId  = companies["entradas-salidas-noticias"].Id
                }, new PermissionEntity()
                {
                    RoleId = 601,
                    CompanyId  = companies["semaforo-de-endeudamiento"].Id
                }, new PermissionEntity()
                {
                    RoleId = 601,
                    CompanyId  = companies["comite-de-credito"].Id
                }, new PermissionEntity()
                {
                    RoleId = 601,
                    CompanyId = companies["estatus-portafolio"].Id
                }
              }
            },new UserEntity(){
                // Id = 5,
                Lastname= "Ortiz",
                EmailConfirmed = true,
                Name="Daniel",
                Email="dortiz@opessa.net",
                Password="dortiz",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["nuevos-proyectos"].Id
                  },
                  new PermissionEntity {
                    RoleId = 601,
                    CompanyId = companies["opessa"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["corporativo"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["inmae"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["arcadia"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["andenes"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["sunmall"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["kino-pitic"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["cimarron"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["concesionaria"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["libr-puentes"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["terminal-manzanillo"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["sofimas"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["mazon-kyriakis"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["piscsa"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["condor"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["venturas"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["hermosillo"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["aguascalientes"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["playa-del-carmen"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["costa-maya"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["family-office"].Id
                  }
                }
            },new UserEntity(){
                // Id = 6,
                Lastname= "Noriega",
                EmailConfirmed = true,
                Name="Jesus",
                Email="jnoriega@opessa.net",
                Password="jnoriega",
                Permissions = new List<PermissionEntity>{
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["requerimiento-de-capital"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["golden-calf"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["jll-gpo-mulatos-opessa"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["jll-gpo-mulatos-gde"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["puma"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["cotec"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["nuevos-proyectos"].Id
                  }
                }
            },new UserEntity(){
                // Id = 7,
                Lastname= "Leyva",
                EmailConfirmed = true,
                Name="Alfredo",
                Email="aleyva@opessa.net",
                Password="aleyva",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["agronegocios"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["la-casita-ganado"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["la-casita-patrimonial"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["genetica"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["chaparral-caceria"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["chaparral-patrimonial"].Id
                  }
                }
            },new UserEntity(){
                // Id = 8,
                Lastname= "Cortina",
                EmailConfirmed = true,
                Name="Ruben",
                Email="ruben.cortina@tfwarren.com",
                Password="rcortina",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["tanquera"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["tarsco"].Id
                  }
                }
            },new UserEntity(){
                // Id = 9,
                Lastname= "Mazon",
                EmailConfirmed = true,
                Name="Gustavo",
                Email="mazon.gustavo@gmail.com",
                Password="gmazon",
                
            },new UserEntity(){
                // Id = 10,
                Lastname= "Cruz",
                EmailConfirmed = true,
                Name="Edgardo",
                Email="edgardo.cruz@groen.com.mx",
                Password="ecruz",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["provida"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["ventura-hillo"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["prohosa-bc"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["prohosa-nog"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["puerta-norte"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["puerta-oeste"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["groen"].Id
                  }
                }
            },new UserEntity(){
                // Id = 11,
                Lastname= "Dessens",
                EmailConfirmed = true,
                Name="Heberto",
                Email="heberto@gila.com.mx",
                Password="hdessens",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["gila"].Id
                  }
                }
            },new UserEntity(){
                // Id = 12,
                Lastname= "Lambarri",
                EmailConfirmed = true,
                Name="Iker",
                Email="ikerlambarri@gmail.com",
                Password="ilambarri",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["gerencia-inmobilaria"].Id
                  }
                }
            },new UserEntity(){
                // Id = 13,
                Lastname= "Peraza",
                EmailConfirmed = true,
                Name="Jesus Oscar",
                Email="jperaza@prodigy.net.mx",
                Password="jperaza",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["promocion"].Id
                  }
                }
            },new UserEntity(){
                // Id = 14,
                Lastname= "Suarez",
                EmailConfirmed = true,
                Name="Daniel",
                Email="dsuarez@dummy.com",
                Password="dsuarez",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["mexport"].Id
                  }
                }
            },new UserEntity(){
                // Id = 15,
                Lastname= "Cota",
                EmailConfirmed = true,
                Name="Rogelio",
                Email="rogeliocota@hotmail.com",
                Password="rcota",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["mazcot"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["siac"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["super-del-sol"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["gastro-capital"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["p&l"].Id
                  }
                }
            },new UserEntity(){
                // Id = 16,
                Lastname= "Villaescusa",
                EmailConfirmed = true,
                Name="Carolina",
                Email="caroovg@hotmail.com",
                Password="cvillaescusa",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["mazcot"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["siac"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["super-del-sol"].Id
                  }
                }
            },new UserEntity(){
                // Id = 17,
                Lastname= "Hernandez",
                EmailConfirmed = true,
                Name="Carla",
                Email="carla.hernandez246@gmail.com",
                Password="chernandez",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["ilt"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["urbanide"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["exem"].Id
                  }
                }
            },new UserEntity(){
                // Id = 18,
                Lastname= "Lopez",
                EmailConfirmed = true,
                Name="Rafael",
                Email="rlopezgiron@gmail.com",
                Password="rlopez",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["ilt"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["exem"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["urbanide"].Id
                  }
                }
            },new UserEntity(){
                // Id = 19,
                Lastname= "Joffoy",
                EmailConfirmed = true,
                Name="Andre",
                Email="andrejoffroy@gmail.com",
                Password="ajoffoy",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["fukushu"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["goodnes"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["obon"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["bird"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["vanderbuild"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["gastro-capital"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["p&l"].Id
                  }
                }
            },new UserEntity(){
                // Id = 20,
                Lastname= "Rodrigez",
                EmailConfirmed = true,
                Name="Arturo",
                Email="arodriguez@gdesarrollos.com",
                Password="arodriguez",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["fukushu"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["goodnes"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["obon"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["bird"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["asteria"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["rex-argentum"].Id
                  }
                }
            },new UserEntity(){
                // Id = 21,
                Lastname= "Millan",
                EmailConfirmed = true,
                Name="Jaime",
                Email="jmillan@integra.legal",
                Password="jmillan",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                {
                    RoleId = 601,
                    CompanyId = companies["integra"].Id
                }, new PermissionEntity()
                {
                    RoleId = 601,
                    CompanyId = companies["m-asesores"].Id
                }
                }
            },new UserEntity(){
                // Id = 22,
                Lastname= "Valenzuela",
                EmailConfirmed = true,
                Name="Paulina",
                Email="pvalenzuela@gdesarrollos.com",
                Password="pvalenzuela",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["petroleos-mty"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["nase"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["san-francisquito"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["nuevos-proyectos"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["comercializadora-minera"].Id
                  },
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["tom-hast"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["asteria"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["rex-argentum"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["gde-familia"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["gme"].Id
                  }
                }
            },new UserEntity(){
                // Id = 23,
                Lastname= "Vazquez",
                EmailConfirmed = true,
                Name="Hector",
                Email="hvazquez@dummy.com",
                Password="hvazquez",
                Permissions = new List<PermissionEntity> {
                  new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["las-palomas"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["turismo"].Id
                  }, new PermissionEntity()
                  {
                      RoleId = 601,
                      CompanyId = companies["desarrollo"].Id
                  }
                }
            }
    };

    private List<CompanyEntity> companies = new List<CompanyEntity> {
            
            new CompanyEntity {
                // Id = 100,
                Name = "Corporativo Operador de Empresas",
                Url = "opessa-corp",
                Activity = "Sin especificar",
                Children = new List<CompanyEntity> {
                  new CompanyEntity()
                  {
                      Url = "antejo",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "BANCA CORPORATIVA",
                      Color="47CC4D",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Url = "entradas-salidas-noticias",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "ENTRADAS, SALIDAS, NOTICIAS",
                            Color="47CC4D",
                            // ParentId=1
                        }, new CompanyEntity()
                            {
                            Url = "estatus-portafolio",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "ESTATUS PORTAFOLIO",
                            Color="47CC4D",
                            // ParentId=1
                        }, new CompanyEntity()
                            {
                            Url = "comite-de-credito",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "COMITÉ DE CREDITO",
                            Color="47CC4D",
                            // ParentId=1
                        }, new CompanyEntity()
                            {
                            Url = "semaforo-de-endeudamiento",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "SEMAFORO DE ENDEUDAMIENTO",
                            Color="47CC4D",
                            // ParentId=1
                        }
                      }
                      // ParentId=100,
                      // Id = 1
                  }, new CompanyEntity()
                      {
                      Url = "opessa",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "OPESSA",
                      Color="A0D4FF",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Url = "corporativo",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "CORPORATIVO",
                            Color="A0D4FF",
                            // ParentId=2
                        }, new CompanyEntity()
                            {
                            Url = "requerimiento-de-capital",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "REQUERIMIENTO DE CAPITAL",
                            Color="A0D4FF",
                            // ParentId=2
                        }
                      }
                      // ParentId = 100,
                      // Id = 2
                  }, new CompanyEntity()
                      {
                      Url = "agronegocios",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "AGRONEGOCIOS",
                      Color="FFAF91",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Url = "la-casita-ganado",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "LA CASITA GANADO",
                            Color="FFAF91",
                            // ParentId=3
                        }, new CompanyEntity()
                            {
                            Url = "la-casita-patrimonial",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "LA CASITA PATRIMONIAL",
                            Color="FFAF91",
                            // ParentId=3
                        }, new CompanyEntity()
                            {
                            Url = "genetica",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "GENETICA",
                            Color="FFAF91",
                            // ParentId=3
                        }, new CompanyEntity()
                            {
                            Url = "chaparral-caceria",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "CHAPARRAL CACERIA",
                            Color="FFAF91",
                            // ParentId=3
                        }, new CompanyEntity()
                            {
                            Url = "chaparral-patrimonial",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "CHAPARRAL PATRIMONIAL",
                            Color="FFAF91",
                            // ParentId=3
                        }
                      }
                      // ParentId = 100,
                      // Id = 3
                  }, new CompanyEntity()
                      {
                      Url = "cotec",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "COTEC",
                      Color="FFAF91",
                      // ParentId = 100,
                      // Id = 4
                  }, new CompanyEntity()
                      {
                      Url = "inmae",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "INMAE",
                      Color="FFAF91",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Url = "arcadia",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "ARCADIA",
                            Color="FFAF91",
                            // ParentId=5
                        }, new CompanyEntity()
                            {
                            Url = "andenes",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "ANDENES",
                            Color="FFAF91",
                            // ParentId=5
                        }, new CompanyEntity()
                            {
                            Url = "sunmall",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "SUNMALL",
                            Color="FFAF91",
                            // ParentId=5
                        }, new CompanyEntity()
                            {
                            Url = "kino-pitic",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "KINO PITIC",
                            Color="FFAF91",
                            // ParentId=5
                        }
                      }
                      // ParentId = 100,
                      // Id = 5
                  }, new CompanyEntity()
                      {
                      Url = "cimarron",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "GRUPO CIMARRON",
                      Color="FFAF91",
                      // ParentId = 100,
                      // Id = 6
                  }, new CompanyEntity()
                      {
                      Url = "golden-calf",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "GOLDEN CALF",
                      Color="FF8713",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Url = "jll-gpo-mulatos-opessa",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "JLL GPO MULATOS",
                            Color="FF8713",
                            // ParentId=7
                        }
                      }
                      // ParentId = 100,
                      // Id = 7
                  }, new CompanyEntity()
                      {
                      Url = "tanquera",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "TANQUERA",
                      Color="FF8713",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Url = "tarsco",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "TARSCO",
                            Color="FF8713",
                            // ParentId=8
                        }, new CompanyEntity()
                            {
                            Url = "petroleos-mty",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "PETROLEOS MTY",
                            Color="FF8713",
                            // ParentId=8
                        }
                      }
                      // ParentId = 100,
                      // Id = 8
                  }, new CompanyEntity()
                      {
                      Url = "concesionaria",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "CONCESIONARIA",
                      Color="FF8713",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Url = "libr-puentes",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "LIBR Y PUENTES",
                            Color="FF8713",
                            // ParentId=9
                        }
                      }
                      // ParentId = 100,
                      // Id = 9
                  }, new CompanyEntity()
                      {
                      Url = "terminal-manzanillo",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "TERMINAL MANZANILLO",
                      Color="FF8713",
                      // ParentId = 100,
                      // Id=10
                  }, new CompanyEntity()
                      {
                      Url = "puma",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "PUMA",
                      Color="FF1005",
                      // ParentId = 100,
                      // Id=11
                  }, new CompanyEntity()
                      {
                      Url = "provida",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "PROVIDA",
                      Color="FF1005",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Url = "puerta-norte",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "PUERTA NORTE",
                            Color="FF1005",
                            // ParentId=12
                        }, new CompanyEntity()
                            {
                            Url = "puerta-oeste",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "PUERTA OESTE",
                            Color="FF1005",
                            // ParentId=12
                        }, new CompanyEntity()
                            {
                            Url = "prohosa-nog",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "PROHOSA-NOG",
                            Color="FF1005",
                            // ParentId=12
                        }, new CompanyEntity()
                            {
                            Url = "prohosa-bc",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "PROHOSA-BC",
                            Color="FF1005",
                            // ParentId=12
                        }, new CompanyEntity()
                            {
                            Url = "ventura-hillo",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "VENTURA HILLO",
                            Color="FF1005",
                            // ParentId=12
                        }
                      }
                      // ParentId = 100,
                      // Id=12
                  }, new CompanyEntity()
                      {
                      Url = "groen",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "GROEN",
                      Color="FF1005",
                      // ParentId = 100,
                      // Id=13
                  }, new CompanyEntity()
                      {
                      Url = "gila",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "GILA",
                      Color="FF1005",
                      // ParentId = 100,
                      // Id=14
                  }, new CompanyEntity()
                      {
                      Url = "mexport",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "MEXPORT",
                      Color="FF1005",
                      // ParentId = 100,
                      // Id=15
                  }, new CompanyEntity()
                      {
                      Url = "sofimas",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "SOFIMAS",
                      Color="FF1005",
                      // ParentId = 100,
                      // Id=16
                  }, new CompanyEntity()
                      {
                      Url = "nase",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "NASE",
                      Color="FF1005",
                      // ParentId = 100,
                      // Id=17
                  }, new CompanyEntity()
                      {
                      Url = "mazon-kyriakis",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "MAZON KYRIAKIS",
                      Color="FF1005",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Url = "piscsa",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "PISCSA",
                            Color="FF1005",
                            // ParentId=18
                        }, new CompanyEntity()
                            {
                            Url = "condor",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "CONDOR",
                            Color="FF1005",
                            // ParentId=18
                        }
                      }
                      // ParentId = 100,
                      // Id=18
                  }, new CompanyEntity()
                      {
                      Url = "las-palomas",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "LAS PALOMAS",
                      Color="FF1005",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Url = "turismo",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "TURISMO",
                            Color="FF1005",
                            // // ParentId=19
                        }, new CompanyEntity()
                            {
                            Url = "desarrollo",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "DESARROLLO",
                            Color="FF1005",
                            // ParentId=19
                        }
                      }
                      // ParentId = 100,
                      // Id=19
                  }, new CompanyEntity()
                      {
                      Url = "venturas",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "VENTURAS",
                      Color="8800FF",
                      // ParentId = 100,
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                            {
                            Url = "hermosillo",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "HERMOSILLO",
                            Color="8800FF",
                            // ParentId=20
                        }, new CompanyEntity()
                            {
                            Url = "aguascalientes",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "AGUASCALIENTES",
                            Color="8800FF",
                            // ParentId=20
                        }, new CompanyEntity()
                            {
                            Url = "playa-del-carmen",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "PLAYA DEL CARMEN",
                            Color="8800FF",
                            // ParentId=20
                        }
                      }
                      // Id=20
                  }, new CompanyEntity()
                      {
                      Url = "costa-maya",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "COSTA MAYAS",
                      Color="8800FF",
                      // ParentId = 100,
                      // Id=21
                  }, new CompanyEntity()
                      {
                      Url = "grupo-mazon",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "GRUPO MAZON",
                      Color="7CCC83",
                      // ParentId = 100,
                      // Id=22
                  }, new CompanyEntity()
                      {
                      Url = "gerencia-inmobilaria",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "GERENCIA INMOBILARIA",
                      Color="FFD527",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                            {
                            Url = "san-francisquito",
                            Activity = "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name= "SAN FRANCISQUITO",
                            Color="FFD527",
                            // ParentId=23
                        }
                      }
                      // ParentId = 100,
                      // Id=23
                  }, new CompanyEntity()
                      {
                      Url = "comercializadora-minera",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "COMERCIALIZADORA MINERA",
                      Color="CCB600",
                      // ParentId = 100,
                      // Id=24
                  }, new CompanyEntity()
                      {
                      Url = "family-office",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "FAMILY OFFICE",
                      Color="FFF6D0",
                      // ParentId = 100,
                      // Id=25
                  }, new CompanyEntity()
                      {
                      Url = "promocion",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "PROMOCION",
                      Color="47CC4D",
                      // ParentId = 100,
                      // Id=26
                  }, new CompanyEntity()
                      {
                      Url = "nuevos-proyectos",
                      Activity = "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name= "NUEVOS PROYECTOS",
                      Color="82A5FF",
                      // ParentId = 100,
                      // Id=27
                  }
                }
            }, new CompanyEntity()
            {
                // Id = 101,
                Name = "GME Desarrollos Empresariales",
                Url = "gde",
                Activity = "Sin especificar",
                Children = new List<CompanyEntity> {
                  new CompanyEntity()
                  {
                      Activity= "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name = "MAZCOT",
                      Color ="1078CC",
                      // ParentId = 101,
                      // Id = 30,
                      Url = "mazcot",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Activity= "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name = "SIAC",
                            Color ="1078CC",
                            // ParentId = 30,
                            Url = "siac"
                        }, new CompanyEntity()
                        {
                            Activity= "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name = "SUPER DEL SOL",
                            Color ="1078CC",
                            // ParentId =30,
                            Url = "super-del-sol"
                        }
                      }
                  }, new CompanyEntity()
                      {
                          Activity= "Sin especificar",
                          Modules = new List<CompanyToModule> { 
                              new CompanyToModule {
                                  ModuleId = 6
                              }
                           },
                          Name = "ILT",
                          Color ="24C0CC",
                          // ParentId = 101,
                          // Id = 31,
                          Url = "ilt",
                          Children = new List<CompanyEntity> {
                            new CompanyEntity()
                            {
                                Activity= "Sin especificar",
                                Modules = new List<CompanyToModule> { 
                                    new CompanyToModule {
                                        ModuleId = 6
                                    }
                                 },
                                Name = "URBANIDE",
                                Color ="24C0CC",
                                // ParentId =31,
                                Url = "urbanide"
                            }
                          }
                      }, new CompanyEntity()
                  {
                      Activity= "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name = "FUKUSHU",
                      Color ="47CC4D",
                      // ParentId = 101,
                      // Id= 32,
                      Url = "fukushu",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Activity= "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name = "GOODNES",
                            Color ="47CC4D",
                            // ParentId =32,
                            Url = "goodnes"
                        }, new CompanyEntity()
                        {
                            Activity= "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name = "OBON",
                            Color ="47CC4D",
                            // ParentId =32,
                            Url = "obon"
                        }, new CompanyEntity()
                        {
                            Activity= "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name = "BIRD",
                            Color ="47CC4D",
                            // ParentId =32,
                            Url = "bird"
                        }
                      }
                  }, new CompanyEntity()
                  {
                      Activity= "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name = "INTEGRA",
                      Color ="FFFE0A",
                      // ParentId = 101,
                      // Id= 33,
                      Url = "integra",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Activity= "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name = "M ASESORES",
                            Color ="FFFE0A",
                            // ParentId =33,
                            Url = "m-asesores"
                        }
                      }
                  }, new CompanyEntity()
                  {
                      Activity= "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name = "VANDERBUILD",
                      Color ="FFEFD0",
                      // ParentId = 101,
                      // Id= 34,
                      Url = "vanderbuild"
                  }, new CompanyEntity()
                  {
                      Activity= "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name = "GASTRO CAPITAL",
                      Color ="FF1005",
                      // ParentId = 101,
                      // Id= 35,
                      Url = "gastro-capital",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Activity= "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name = "P&L",
                            Color ="FF1005",
                            // ParentId =35,
                            Url = "p&l"
                        }
                      }
                  }, new CompanyEntity()
                  {
                      Activity= "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name = "EXEM",
                      Color ="960099",
                      // ParentId = 101,
                      // Id= 37,
                      Url = "exem"
                  }, new CompanyEntity()
                  {
                      Activity= "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name = "TOM HAST",
                      Color ="999799",
                      // ParentId = 101,
                      // Id= 38,
                      Url = "tom-hast",
                      Children = new List<CompanyEntity> {
                        new CompanyEntity()
                        {
                            Activity= "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name = "ASTERIA",
                            Color ="999799",
                            // ParentId =38,
                            Url = "asteria"
                        }, new CompanyEntity()
                        {
                            Activity= "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name = "REX ARGENTUM",
                            Color ="999799",
                            // ParentId =38,
                            Url = "rex-argentum"
                        }, new CompanyEntity()
                        {
                            Activity= "Sin especificar",
                            Modules = new List<CompanyToModule> { 
                                new CompanyToModule {
                                    ModuleId = 6
                                }
                             },
                            Name = "JLL GPO MULATOS",
                            Color ="999799",
                            // ParentId =38,
                            Url = "jll-gpo-mulatos-gde"
                        }
                      }
                  }, new CompanyEntity()
                  {
                      Activity= "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name = "GDE-FAMILIA",
                      Color ="CCB600",
                      // ParentId = 101,
                      // Id= 39,
                      Url = "gde-familia"
                  }, new CompanyEntity()
                  {
                      Activity= "Sin especificar",
                      Modules = new List<CompanyToModule> { 
                          new CompanyToModule {
                              ModuleId = 6
                          }
                       },
                      Name = "GME",
                      Color ="014399",
                      // ParentId = 101,
                      // Id = 40,
                      Url = "gme"
                  }
                }
            }
    };

  }
}