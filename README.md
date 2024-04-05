# jygasoft


## Crear una migración:
Para generar una migración, ejecuta el siguiente comando en la terminal:

`dotnet ef migrations add FirstMigration -p DataAccess/DataAccess.csproj -s Api/Api.csproj`

Esto creará una nueva migración con el nombre “FirstMigration”.

## Aplicar la migración a la base de datos:

Una vez que hayas creado la migración, es hora de aplicarla a la base de datos. Para ello abre la terminal y navega al directorio “DataAccess”:

`cd DataAccess`

Ejecuta el siguiente comando para aplicar la migración:
`dotnet ef database update`

Esto actualizará la base de datos según los cambios definidos en la migración.
Ejecutar estos cambios cada vez q se realice una modificación al modelo
