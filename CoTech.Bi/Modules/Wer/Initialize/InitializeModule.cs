using System;
using System.Collections.Generic;
using System.Linq;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Wer.Initialize
{
    public class InitializeModule
    {
        private BiContext _context;
        private UserManager<UserEntity> _manager;

        private DbSet<PermissionEntity> _dbPermission
        {
            get { return this._context.Set<PermissionEntity>(); }
        }

        private DbSet<CompanyEntity> _dbCompany
        {
            get { return this._context.Set<CompanyEntity>(); }
        }

        public InitializeModule(BiContext context, UserManager<UserEntity> manager)
        {
            this._context = context;
            this._manager = manager;
            this.Init();
        }
        
        private async void Init()
        {
            //  **EMPRESAS**
            
            var listCompanies = new List<CompanyEntity>();
            
            listCompanies.Add(new CompanyEntity()
            {
                 Id = 100,
                Name = "Corporativo Operador de Empresas",
                Url = "opessa-corp",
                Activity = "Sin especificar"
                
            });
            listCompanies.Add(new CompanyEntity()
            {
                Id = 101,
                Name = "GME Desarrollos Empresariales",
                Url = "gde",
                Activity = "Sin especificar"
            });
            

    
            listCompanies.Add(new CompanyEntity()
                {
                Url = "antejo",
                Activity = "Sin especificar",
                Name= "BANCA CORPORATIVA",
                Color="47CC4D",
                ParentId=100,
                Id = 1
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "opessa",
                Activity = "Sin especificar",
                Name= "OPESSA",
                Color="A0D4FF",
                ParentId = 100,
                Id = 2
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "agronegocios",
                Activity = "Sin especificar",
                Name= "AGRONEGOCIOS",
                Color="FFAF91",
                ParentId = 100,
                Id = 3
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "cotec",
                Activity = "Sin especificar",
                Name= "COTEC",
                Color="FFAF91",
                ParentId = 100,
                Id = 4
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "inmae",
                Activity = "Sin especificar",
                Name= "INMAE",
                Color="FFAF91",
                ParentId = 100,
                Id = 5
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "cimarron",
                Activity = "Sin especificar",
                Name= "GRUPO CIMARRON",
                Color="FFAF91",
                ParentId = 100,
                Id = 6
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "golden-calf",
                Activity = "Sin especificar",
                Name= "GOLDEN CALF",
                Color="FF8713",
                ParentId = 100,
                Id = 7
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "tanquera",
                Activity = "Sin especificar",
                Name= "TANQUERA",
                Color="FF8713",
                ParentId = 100,
                Id = 8
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "concesionaria",
                Activity = "Sin especificar",
                Name= "CONCESIONARIA",
                Color="FF8713",
                ParentId = 100,
                Id = 9
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "terminal-manzanillo",
                Activity = "Sin especificar",
                Name= "TERMINAL MANZANILLO",
                Color="FF8713",
                ParentId = 100,
                Id=10
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "puma",
                Activity = "Sin especificar",
                Name= "PUMA",
                Color="FF1005",
                ParentId = 100,
                Id=11
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "provida",
                Activity = "Sin especificar",
                Name= "PROVIDA",
                Color="FF1005",
                ParentId = 100,
                Id=12
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "groen",
                Activity = "Sin especificar",
                Name= "GROEN",
                Color="FF1005",
                ParentId = 100,
                Id=13
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "gila",
                Activity = "Sin especificar",
                Name= "GILA",
                Color="FF1005",
                ParentId = 100,
                Id=14
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "mexport",
                Activity = "Sin especificar",
                Name= "MEXPORT",
                Color="FF1005",
                ParentId = 100,
                Id=15
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "sofimas",
                Activity = "Sin especificar",
                Name= "SOFIMAS",
                Color="FF1005",
                ParentId = 100,
                Id=16
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "nase",
                Activity = "Sin especificar",
                Name= "NASE",
                Color="FF1005",
                ParentId = 100,
                Id=17
            });
           listCompanies.Add(new CompanyEntity()
                {
                Url = "mazon-kyriakis",
                Activity = "Sin especificar",
                Name= "MAZON KYRIAKIS",
                Color="FF1005",
                ParentId = 100,
                Id=18
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "las-palomas",
                Activity = "Sin especificar",
                Name= "LAS PALOMAS",
                Color="FF1005",
                ParentId = 100,
                Id=19
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "venturas",
                Activity = "Sin especificar",
                Name= "VENTURAS",
                Color="8800FF",
                ParentId = 100,
                Id=20
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "costa-maya",
                Activity = "Sin especificar",
                Name= "COSTA MAYAS",
                Color="8800FF",
                ParentId = 100,
                Id=21
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "grupo-mazon",
                Activity = "Sin especificar",
                Name= "GRUPO MAZON",
                Color="7CCC83",
                ParentId = 100,
                Id=22
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "gerencia-inmobilaria",
                Activity = "Sin especificar",
                Name= "GERENCIA INMOBILARIA",
                Color="FFD527",
                ParentId = 100,
                Id=23
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "comercializadora-minera",
                Activity = "Sin especificar",
                Name= "COMERCIALIZADORA MINERA",
                Color="CCB600",
                ParentId = 100,
                Id=24
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "family-office",
                Activity = "Sin especificar",
                Name= "FAMILY OFFICE",
                Color="FFF6D0",
                ParentId = 100,
                Id=25
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "promocion",
                Activity = "Sin especificar",
                Name= "PROMOCION",
                Color="47CC4D",
                ParentId = 100,
                Id=26
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "nuevos-proyectos",
                Activity = "Sin especificar",
                Name= "NUEVOS PROYECTOS",
                Color="82A5FF",
                ParentId = 100,
                Id=27
            });
    
            listCompanies.Add(new CompanyEntity()
                {
                Url = "entradas-salidas-noticias",
                Activity = "Sin especificar",
                Name= "ENTRADAS, SALIDAS, NOTICIAS",
                Color="47CC4D",
                ParentId=1
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "estatus-portafolio",
                Activity = "Sin especificar",
                Name= "ESTATUS PORTAFOLIO",
                Color="47CC4D",
                ParentId=1
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "comite-de-credito",
                Activity = "Sin especificar",
                Name= "COMITÉ DE CREDITO",
                Color="47CC4D",
                ParentId=1
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "semaforo-de-endeudamiento",
                Activity = "Sin especificar",
                Name= "SEMAFORO DE ENDEUDAMIENTO",
                Color="47CC4D",
                ParentId=1
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "corporativo",
                Activity = "Sin especificar",
                Name= "CORPORATIVO",
                Color="A0D4FF",
                ParentId=2
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "requerimiento-de-capital",
                Activity = "Sin especificar",
                Name= "REQUERIMIENTO DE CAPITAL",
                Color="A0D4FF",
                ParentId=2
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "la-casita-ganado",
                Activity = "Sin especificar",
                Name= "LA CASITA GANADO",
                Color="FFAF91",
                ParentId=3
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "la-casita-patrimonial",
                Activity = "Sin especificar",
                Name= "LA CASITA PATRIMONIAL",
                Color="FFAF91",
                ParentId=3
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "genetica",
                Activity = "Sin especificar",
                Name= "GENETICA",
                Color="FFAF91",
                ParentId=3
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "chaparral-caceria",
                Activity = "Sin especificar",
                Name= "CHAPARRAL CACERIA",
                Color="FFAF91",
                ParentId=3
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "chaparral-patrimonial",
                Activity = "Sin especificar",
                Name= "CHAPARRAL PATRIMONIAL",
                Color="FFAF91",
                ParentId=3
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "arcadia",
                Activity = "Sin especificar",
                Name= "ARCADIA",
                Color="FFAF91",
                ParentId=5
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "andenes",
                Activity = "Sin especificar",
                Name= "ANDENES",
                Color="FFAF91",
                ParentId=5
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "sunmall",
                Activity = "Sin especificar",
                Name= "SUNMALL",
                Color="FFAF91",
                ParentId=5
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "kino-pitic",
                Activity = "Sin especificar",
                Name= "KINO PITIC",
                Color="FFAF91",
                ParentId=5
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "jll-gpo-mulatos-opessa",
                Activity = "Sin especificar",
                Name= "JLL GPO MULATOS",
                Color="FF8713",
                ParentId=7
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "tarsco",
                Activity = "Sin especificar",
                Name= "TARSCO",
                Color="FF8713",
                ParentId=8
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "petroleos-mty",
                Activity = "Sin especificar",
                Name= "PETROLEOS MTY",
                Color="FF8713",
                ParentId=8
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "libr-puentes",
                Activity = "Sin especificar",
                Name= "LIBR Y PUENTES",
                Color="FF8713",
                ParentId=9
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "puerta-norte",
                Activity = "Sin especificar",
                Name= "PUERTA NORTE",
                Color="FF1005",
                ParentId=12
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "puerta-oeste",
                Activity = "Sin especificar",
                Name= "PUERTA OESTE",
                Color="FF1005",
                ParentId=12
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "prohosa-nog",
                Activity = "Sin especificar",
                Name= "PROHOSA-NOG",
                Color="FF1005",
                ParentId=12
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "prohosa-bc",
                Activity = "Sin especificar",
                Name= "PROHOSA-BC",
                Color="FF1005",
                ParentId=12
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "ventura-hillo",
                Activity = "Sin especificar",
                Name= "VENTURA HILLO",
                Color="FF1005",
                ParentId=12
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "piscsa",
                Activity = "Sin especificar",
                Name= "PISCSA",
                Color="FF1005",
                ParentId=18
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "condor",
                Activity = "Sin especificar",
                Name= "CONDOR",
                Color="FF1005",
                ParentId=18
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "turismo",
                Activity = "Sin especificar",
                Name= "TURISMO",
                Color="FF1005",
                ParentId=19
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "desarrollo",
                Activity = "Sin especificar",
                Name= "DESARROLLO",
                Color="FF1005",
                ParentId=19
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "hermosillo",
                Activity = "Sin especificar",
                Name= "HERMOSILLO",
                Color="8800FF",
                ParentId=20
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "aguascalientes",
                Activity = "Sin especificar",
                Name= "AGUASCALIENTES",
                Color="8800FF",
                ParentId=20
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "playa-del-carmen",
                Activity = "Sin especificar",
                Name= "PLAYA DEL CARMEN",
                Color="8800FF",
                ParentId=20
            });
            listCompanies.Add(new CompanyEntity()
                {
                Url = "san-francisquito",
                Activity = "Sin especificar",
                Name= "SAN FRANCISQUITO",
                Color="FFD527",
                ParentId=23
            });
            
            
            
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "MAZCOT",
                Color ="1078CC",
                ParentId = 101,
                Id = 30,
                Url = "mazcot"
            });
            listCompanies.Add(new CompanyEntity()
                {
                    Activity= "Sin especificar",
                    Name = "ILT",
                    Color ="24C0CC",
                    ParentId = 101,
                    Id = 31,
                    Url = "ilt"
                });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "FUKUSHU",
                Color ="47CC4D",
                ParentId = 101,
                Id= 32,
                Url = "fukushu"
            });
            listCompanies.Add(new CompanyEntity()
             {
                Activity= "Sin especificar",
                Name = "INTEGRA",
                Color ="FFFE0A",
                ParentId = 101,
                Id= 33,
                Url = "integra"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "VANDERBUILD",
                Color ="FFEFD0",
                ParentId = 101,
                Id= 34,
                Url = "vanderbuild"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "GASTRO CAPITAL",
                Color ="FF1005",
                ParentId = 101,
                Id= 35,
                Url = "gastro-capital"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "EXEM",
                Color ="960099",
                ParentId = 101,
                Id= 37,
                Url = "exem"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "TOM HAST",
                Color ="999799",
                ParentId = 101,
                Id= 38,
                Url = "tom-hast"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "GDE-FAMILIA",
                Color ="CCB600",
                ParentId = 101,
                Id= 39,
                Url = "gde-familia"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "GME",
                Color ="014399",
                ParentId = 101,
                Id = 40,
                Url = "gme"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "URBANIDE",
                Color ="24C0CC",
                ParentId =31,
                Url = "urbanide"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "SIAC",
                Color ="1078CC",
                ParentId = 30,
                Url = "siac"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "SUPER DEL SOL",
                Color ="1078CC",
                ParentId =30,
                Url = "super-del-sol"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "GOODNES",
                Color ="47CC4D",
                ParentId =32,
                Url = "goodnes"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "OBON",
                Color ="47CC4D",
                ParentId =32,
                Url = "obon"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "BIRD",
                Color ="47CC4D",
                ParentId =32,
                Url = "bird"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "M ASESORES",
                Color ="FFFE0A",
                ParentId =33,
                Url = "m-asesores"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "P&L",
                Color ="FF1005",
                ParentId =35,
                Url = "p&l"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "ASTERIA",
                Color ="999799",
                ParentId =38,
                Url = "asteria"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "REX ARGENTUM",
                Color ="999799",
                ParentId =38,
                Url = "rex-argentum"
            });
            listCompanies.Add(new CompanyEntity()
            {
                Activity= "Sin especificar",
                Name = "JLL GPO MULATOS",
                Color ="999799",
                ParentId =38,
                Url = "jll-gpo-mulatos-gde"
            });
            

            var users = new List<UserEntity>();
            users.Add(new UserEntity(){
                Id = 4,
                Name = "Fransisco",
                Lastname = "Esquer",
                Email = "fesquer@opessa.net",
                Password = "fesquer",
                EmailConfirmed=true
            });
            users.Add(new UserEntity(){
                Id = 5,
                Lastname= "Ortiz",
                EmailConfirmed = true,
                Name="Daniel",
                Email="dortiz@opessa.net",
                Password="dortiz",
            });
            users.Add(new UserEntity(){
                Id = 6,
                Lastname= "Noriega",
                EmailConfirmed = true,
                Name="Jesus",
                Email="jnoriega@opessa.net",
                Password="jnoriega",
            });
            users.Add(new UserEntity(){
                Id = 7,
                Lastname= "Leyva",
                EmailConfirmed = true,
                Name="Alfredo",
                Email="aleyva@opessa.net",
                Password="aleyva",
            });
            users.Add(new UserEntity(){
                Id = 8,
                Lastname= "Cortina",
                EmailConfirmed = true,
                Name="Ruben",
                Email="ruben.cortina@tfwarren.com",
                Password="rcortina",
            });
            users.Add(new UserEntity(){
                Id = 9,
                Lastname= "Mazon",
                EmailConfirmed = true,
                Name="Gustavo",
                Email="mazon.gustavo@gmail.com",
                Password="gmazon",
                
            });
            users.Add(new UserEntity(){
                Id = 10,
                Lastname= "Cruz",
                EmailConfirmed = true,
                Name="Edgardo",
                Email="edgardo.cruz@groen.com.mx",
                Password="ecruz",
            });
            users.Add(new UserEntity(){
                Id = 11,
                Lastname= "Dessens",
                EmailConfirmed = true,
                Name="Heberto",
                Email="Heberto@gila.com.mx",
                Password="hdessens",
            });
            users.Add(new UserEntity(){
                Id = 12,
                Lastname= "Lambarri",
                EmailConfirmed = true,
                Name="Iker",
                Email="ikerlambarri@gmail.com",
                Password="ilambarri",
            });
            users.Add(new UserEntity(){
                Id = 13,
                Lastname= "Peraza",
                EmailConfirmed = true,
                Name="Jesus Oscar",
                Email="joperaza@prodigy.net.mx",
                Password="jperaza",
            });
            users.Add(new UserEntity(){
                Id = 14,
                Lastname= "Suarez",
                EmailConfirmed = true,
                Name="Daniel",
                Email="dsuarez@dummy.com",
                Password="dsuarez",
            });
            users.Add(new UserEntity(){
                Id = 15,
                Lastname= "Cota",
                EmailConfirmed = true,
                Name="Rogelio",
                Email="rogelioota@hotmail.com",
                Password="rcota",
            });
            users.Add(new UserEntity(){
                Id = 16,
                Lastname= "Villaescusa",
                EmailConfirmed = true,
                Name="Carolina",
                Email="caroovg@hotmail.com",
                Password="cvillaescusa",
            });
            users.Add(new UserEntity(){
                Id = 17,
                Lastname= "Hernandez",
                EmailConfirmed = true,
                Name="Carla",
                Email="carla.hernandez246@gmail.com",
                Password="chernandez",
            });
            users.Add(new UserEntity(){
                Id = 18,
                Lastname= "Lopez",
                EmailConfirmed = true,
                Name="Rafael",
                Email="rlopezgiron@gmail.com",
                Password="rlopez",
            });
            users.Add(new UserEntity(){
                Id = 19,
                Lastname= "Joffoy",
                EmailConfirmed = true,
                Name="Andre",
                Email="andrejoffroy@gmail.com",
                Password="ajoffoy",
            });
            users.Add(new UserEntity(){
                Id = 20,
                Lastname= "Rodrigez",
                EmailConfirmed = true,
                Name="Arturo",
                Email="arodriguez@gdesarrollos.com",
                Password="arodriguez",
            });
            users.Add(new UserEntity(){
                Id = 21,
                Lastname= "Millan",
                EmailConfirmed = true,
                Name="Jaime",
                Email="jmillan@integra.legal",
                Password="jmillan",
            });
            users.Add(new UserEntity(){
                Id = 22,
                Lastname= "Valenzuela",
                EmailConfirmed = true,
                Name="Paulina",
                Email="pvalenzuela@gdesarrollos.com",
                Password="pvalenzuela",
            });
            users.Add(new UserEntity(){
                Id = 23,
                Lastname= "Vazquez",
                EmailConfirmed = true,
                Name="Hector",
                Email="hvazquez@dummy.com",
                Password="pvalenzuela",
            });

            
            try
            {
                foreach (UserEntity item in users)
                {
                    _manager.CreateAsync(item, item.Password);
                } 
                foreach (CompanyEntity company in listCompanies)
                {
                    _dbCompany.Add(company);
                }
                _context.SaveChanges();
                var dictionariyCompanies = listCompanies.ToDictionary(c => c.Url);
                var dictionariyUsers = users.ToDictionary(c => c.Email);
                var listPermission = new List<PermissionEntity>();
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    UserId = dictionariyUsers["fesquer@opessa.net"].Id,
                    Company = dictionariyCompanies["antejo"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    UserId = dictionariyUsers["fesquer@opessa.net"].Id,
                    Company = dictionariyCompanies["entradas-salidas-noticias"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    UserId = dictionariyUsers["fesquer@opessa.net"].Id,
                    Company = dictionariyCompanies["semaforo-de-endeudamiento"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    UserId = dictionariyUsers["fesquer@opessa.net"].Id,
                    Company = dictionariyCompanies["comite-de-credito"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    UserId = dictionariyUsers["fesquer@opessa.net"].Id,
                    Company= dictionariyCompanies["estatus-portafolio"]
                });
                
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["opessa"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["corporativo"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["inmae"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["arcadia"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["andenes"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["sunmall"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["kino-pitic"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["cimarron"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["concesionaria"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["libr-puentes"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["terminal-manzanillo"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["sofimas"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["mazon-kyriakis"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["piscsa"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["condor"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["venturas"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["hermosillo"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["aguascalientes"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["playa-del-carmen"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["costa-maya"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["family-office"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["jnoriega@opessa.net"],
                    Company = dictionariyCompanies["requerimiento-de-capital"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["jnoriega@opessa.net"],
                    Company = dictionariyCompanies["golden-calf"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["jnoriega@opessa.net"],
                    Company = dictionariyCompanies["jll-gpo-mulatos-opessa"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["jnoriega@opessa.net"],
                    Company = dictionariyCompanies["jll-gpo-mulatos-gde"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["jnoriega@opessa.net"],
                    Company = dictionariyCompanies["puma"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["jnoriega@opessa.net"],
                    Company = dictionariyCompanies["cotec"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["jnoriega@opessa.net"],
                    Company = dictionariyCompanies["nuevos-proyectos"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dortiz@opessa.net"],
                    Company = dictionariyCompanies["nuevos-proyectos"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["aleyva@opessa.net"],
                    Company = dictionariyCompanies["agronegocios"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["aleyva@opessa.net"],
                    Company = dictionariyCompanies["la-casita-ganado"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["aleyva@opessa.net"],
                    Company = dictionariyCompanies["la-casita-patrimonial"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["aleyva@opessa.net"],
                    Company = dictionariyCompanies["genetica"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["aleyva@opessa.net"],
                    Company = dictionariyCompanies["chaparral-caceria"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["aleyva@opessa.net"],
                    Company = dictionariyCompanies["chaparral-patrimonial"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["edgardo.cruz@groen.com.mx"],
                    Company = dictionariyCompanies["provida"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["edgardo.cruz@groen.com.mx"],
                    Company = dictionariyCompanies["ventura-hillo"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["edgardo.cruz@groen.com.mx"],
                    Company = dictionariyCompanies["prohosa-bc"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["edgardo.cruz@groen.com.mx"],
                    Company = dictionariyCompanies["prohosa-nog"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["edgardo.cruz@groen.com.mx"],
                    Company = dictionariyCompanies["puerta-norte"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["edgardo.cruz@groen.com.mx"],
                    Company = dictionariyCompanies["puerta-oeste"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["edgardo.cruz@groen.com.mx"],
                    Company = dictionariyCompanies["groen"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["heberto@gila.com.mx"],
                    Company = dictionariyCompanies["gila"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["dsuarez@dummy.com"],
                    Company = dictionariyCompanies["mexport"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["hvazquez@dummy.com"],
                    Company = dictionariyCompanies["las-palomas"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["hvazquez@dummy.com"],
                    Company = dictionariyCompanies["turismo"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["hvazquez@dummy.com"],
                    Company = dictionariyCompanies["desarrollo"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["ruben.cortina@tfwarren.com"],
                    Company = dictionariyCompanies["tanquera"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["ruben.cortina@tfwarren.com"],
                    Company = dictionariyCompanies["tarsco"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["pvalenzuela@gdesarrollos.com"],
                    Company = dictionariyCompanies["petroleos-mty"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["pvalenzuela@gdesarrollos.com"],
                    Company = dictionariyCompanies["nase"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["pvalenzuela@gdesarrollos.com"],
                    Company = dictionariyCompanies["san-francisquito"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["pvalenzuela@gdesarrollos.com"],
                    Company = dictionariyCompanies["nuevos-proyectos"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["pvalenzuela@gdesarrollos.com"],
                    Company = dictionariyCompanies["comercializadora-minera"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["ikerlambarri@gmail.com"],
                    Company = dictionariyCompanies["gerencia-inmobilaria"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["joperaza@prodigy.net.mx"],
                    Company = dictionariyCompanies["promocion"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["rogelioota@hotmail.com"],
                    Company = dictionariyCompanies["mazcot"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["rogelioota@hotmail.com"],
                    Company = dictionariyCompanies["siac"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["rogelioota@hotmail.com"],
                    Company = dictionariyCompanies["super-del-sol"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["caroovg@hotmail.com"],
                    Company = dictionariyCompanies["mazcot"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["caroovg@hotmail.com"],
                    Company = dictionariyCompanies["siac"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["caroovg@hotmail.com"],
                    Company = dictionariyCompanies["super-del-sol"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["carla.hernandez246@gmail.com"],
                    Company = dictionariyCompanies["ilt"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["carla.hernandez246@gmail.com"],
                    Company = dictionariyCompanies["urbanide"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["rlopezgiron@gmail.com"],
                    Company = dictionariyCompanies["ilt"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["carla.hernandez246@gmail.com"],
                    Company = dictionariyCompanies["exem"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["rlopezgiron@gmail.com"],
                    Company = dictionariyCompanies["exem"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["rlopezgiron@gmail.com"],
                    Company = dictionariyCompanies["urbanide"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["andrejoffroy@gmail.com"],
                    Company = dictionariyCompanies["fukushu"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["andrejoffroy@gmail.com"],
                    Company = dictionariyCompanies["goodnes"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["andrejoffroy@gmail.com"],
                    Company = dictionariyCompanies["obon"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["andrejoffroy@gmail.com"],
                    Company = dictionariyCompanies["bird"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["andrejoffroy@gmail.com"],
                    Company = dictionariyCompanies["vanderbuild"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["andrejoffroy@gmail.com"],
                    Company = dictionariyCompanies["gastro-capital"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["andrejoffroy@gmail.com"],
                    Company = dictionariyCompanies["p&l"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["rogelioota@hotmail.com"],
                    Company = dictionariyCompanies["gastro-capital"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["rogelioota@hotmail.com"],
                    Company = dictionariyCompanies["p&l"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["arodriguez@gdesarrollos.com"],
                    Company = dictionariyCompanies["fukushu"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["arodriguez@gdesarrollos.com"],
                    Company = dictionariyCompanies["goodnes"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["arodriguez@gdesarrollos.com"],
                    Company = dictionariyCompanies["obon"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["arodriguez@gdesarrollos.com"],
                    Company = dictionariyCompanies["bird"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["jmillan@integra.legal"],
                    Company = dictionariyCompanies["integra"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["jmillan@integra.legal"],
                    Company = dictionariyCompanies["m-asesores"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["pvalenzuela@gdesarrollos.com"],
                    Company = dictionariyCompanies["tom-hast"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["pvalenzuela@gdesarrollos.com"],
                    Company = dictionariyCompanies["asteria"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["pvalenzuela@gdesarrollos.com"],
                    Company = dictionariyCompanies["rex-argentum"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["pvalenzuela@gdesarrollos.com"],
                    Company = dictionariyCompanies["gde-familia"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["pvalenzuela@gdesarrollos.com"],
                    Company = dictionariyCompanies["gme"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["arodriguez@gdesarrollos.com"],
                    Company = dictionariyCompanies["asteria"]
                });
                listPermission.Add(new PermissionEntity()
                {
                    RoleId = 601,
                    User = dictionariyUsers["arodriguez@gdesarrollos.com"],
                    Company = dictionariyCompanies["rex-argentum"]
                });
                
                
                
                foreach (PermissionEntity permissionEntity in listPermission)
                {
                    _dbPermission.Add(permissionEntity);
                }
                _context.SaveChanges();
                
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}