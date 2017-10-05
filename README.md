# BI .NET Core

## Requerimientos
- [.NET Core 2.0](https://www.microsoft.com/net/download/core)
- MySql

## Preparar ambiente de desarrollo
Primero hay que crear un usuario `bi` en MySql con permisos
en la tabla `bi-core` desde la terminal de comandos `mysql`:
``` mysql
  CREATE USER 'bi'@'localhost' IDENTIFIED BY 'bi-core';
  GRANT ALL ON `bi-core`.* TO 'bi'@'localhost';
```
Ahora en la terminal bash
``` bash
  cd CoTech.Bi
  dotnet restore                  # instala paquetes
  dotnet ef migrations add Init   # crea definiciones para BD
  dotnet ef database update       # aplica las definiciones
```
Esto solo crea las tablas, agregar los usuarios root defaults se explica mas abajo

## Ejecutar con Visual Studio 2017
Solo hay que abrir el archivo CoTech.Bi.sln, dejar que haga lo suyo y 
presionar el botón Ejecutar.

## Ejecutar desde la terminal
``` bash
  dotnet run
```
Si se encuentra en la raíz del proyecto:
``` bash
  dotnet run --project CoTech.Bi
```

## Usuarios root defaults
La preparación de ambiente de desarrollo no crea usuarios root
por si solo, se necesitan estos pasos extra:
``` bash
  dotnet ef migrations add RootUsersSeed
```
En la carpeta migrations verá un archivo nombrado algo como `YYYYMMDDHHmmSS_RootUsersSeed.cs` 
que debería tener dos funciones con cuerpo vacío,
si no es así, ejecute el comando anterior otra vez.

En este archivo lo único que hay que hacer es llamar `migrationBuilder.UpRootUsers()` 
en la funcion `void Up(MigrationBuilder)` y llamar `migrationBuilder.DownRootUsers()` 
en la función `void Down(MigrationBuilder)` (necesitará importar `CoTech.Bi.Util`). 
Debería queadar algo como esto:
``` csharp
public partial class RootUsersSeed : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpRootUsers();
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DownRootUsers();
    }
}
```
Solo falta aplicar la migración y listo:
``` bash
  dotnet ef database update
```

## Empezar a hackear
Para añadir un módulo, primero hay que crear algunos directorios y archivos en la 
carpeta `Modules`. Para un módulo llamado `Example`, quedaría así
``` bash
CoTech.Bi/
└ Modules/
    └ Example/
        ├ Controllers/                    
        │   └ ExampleController.cs        # Acciones REST
        ├ Repositories/
        │   └ ExampleRepository.cs        # Simplificaciones de queries de BD
        ├ Notifiers/
        │   └ ExampleNotifier.cs          # Crea y guarda notificaciones
        ├ Models/
        │   ├ ExampleEntity.cs            # Modelo de base de datos
        │   ├ ExampleNotificationBody.cs  # Acción(es) que se deben notificar
        │   ├ ExampleRequest.cs           # Cuerpo(s) de una petición
        │   └ ExampleResponse.cs          # Respuesta(s) de una petición
        └ ExampleModule.cs                # Preparación de entidades y servicios
```
TODO: explicación detallada de esto. Por mientras pueden ver como funcionan otros
módulos ya hechos (En la carpeta `Core` también hay módulos)
