# ExamenChub — API de clientes con SQL Server

Solución .NET 8 que expone una **API REST** para gestionar **clientes** (altas, consultas, actualización, borrado y búsqueda) persistiendo datos en **Microsoft SQL Server**. Incluye una **aplicación Windows Forms** de ejemplo que consume la API.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server** accesible desde tu máquina, por ejemplo:
  - [SQL Server Express LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb) (incluido con Visual Studio y muchas cargas de trabajo de datos), o
  - SQL Server / Azure SQL con cadena de conexión válida

## Estructura de la solución

| Proyecto | Descripción |
|----------|-------------|
| **webapi** | ASP.NET Core Web API: controladores, servicio de negocio, repositorio con **Dapper** y **Microsoft.Data.SqlClient**. Swagger en entorno **Development**. |
| **CapaPresentacion** | Cliente WinForms (`Demo`) que llama a los endpoints HTTP de la API. |

Capas en la API (orden conceptual):

1. `1.Controllers` — `ClientesController`
2. `2.Business` — `ClienteService`
3. `3.Entities` — modelo `Cliente`
4. `4.Data` — `ClienteRepository`, `IClienteRepository`, `DatabaseInitializer`

## Base de datos (SQL Server)

### Cadena de conexión

La API lee **`ConnectionStrings:DefaultConnection`** desde `webapi/appsettings.json` o `webapi/appsettings.Development.json`.

Por defecto se usa **LocalDB** y una base llamada `ExamenChub`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ExamenChub;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Ajusta `Server`, `Database`, usuario y contraseña según tu instancia. Ejemplos:

- **Instancia con nombre:** `Server=localhost\\SQLEXPRESS;Database=ExamenChub;Trusted_Connection=True;TrustServerCertificate=True`
- **SQL con login:** `Server=tu-servidor;Database=ExamenChub;User Id=...;Password=...;TrustServerCertificate=True`

### Esquema

Al arrancar la API se ejecuta `DatabaseInitializer`: si no existe la tabla **`dbo.Cliente`**, se crea con:

| Columna | Tipo |
|---------|------|
| `Id` | `INT IDENTITY(1,1)` clave primaria |
| `Nombre` | `NVARCHAR(200)` |
| `Edad` | `NVARCHAR(50)` |
| `Direccion` | `NVARCHAR(500)` |
| `CorreoElectronico` | `NVARCHAR(320)` |

No hace falta crear la base manualmente si el usuario de la cadena de conexión tiene permiso para crearla al conectar (comportamiento habitual con LocalDB).

### Dependencias NuGet (API)

- **Dapper** — acceso a datos con SQL parametrizado
- **Microsoft.Data.SqlClient** — proveedor SQL Server para .NET
- **Swashbuckle** — Swagger / OpenAPI en desarrollo

## Cómo ejecutar la API

Desde la raíz del repositorio:

```bash
dotnet run --project webapi/WebAPI.csproj
```

Asegúrate de que la cadena de conexión apunte a un SQL Server en ejecución. En **Development**, la UI de Swagger suele abrirse en la raíz del sitio (por ejemplo `https://localhost:7274` según `launchSettings.json`).

### Endpoints principales

| Método | Ruta | Descripción |
|--------|------|-------------|
| `GET` | `/api/Clientes` | Lista todos los clientes |
| `GET` | `/api/Clientes/{id}` | Cliente por id |
| `POST` | `/api/Clientes` | Crea cliente (el `Id` generado se devuelve en la respuesta) |
| `PUT` | `/api/Clientes/{id}` | Actualiza |
| `DELETE` | `/api/Clientes/{id}` | Elimina |
| `GET` | `/api/Clientes/search?nombre=&correoElectronico=` | Búsqueda por coincidencia parcial |

## Cómo ejecutar el cliente Windows Forms

1. Arranca la API (puerto HTTPS según `webapi/Properties/launchSettings.json`).
2. En `CapaPresentacion/DataAccess/API/Call.cs`, las URLs apuntan a `https://localhost:44309/...`. **Unifica el puerto** con el que use realmente tu perfil de ejecución de la API (por ejemplo `https://localhost:7274`) o ejecuta la API con el mismo puerto que espera el cliente.
3. Ejecuta el proyecto de presentación:

```bash
dotnet run --project CapaPresentacion/CapaPresentacion.csproj
```

## Conexión por solicitud

La conexión a SQL Server está registrada como **`AddScoped<IDbConnection>`**: cada petición HTTP obtiene su propia `SqlConnection`, que el contenedor dispone al terminar el ámbito. Esto es adecuado para **pooling** del proveedor y evita compartir una conexión global entre hilos.

## Solución de problemas

- **`InvalidOperationException` sobre `DefaultConnection`:** define la cadena en `appsettings*.json` o con variables de entorno (`ConnectionStrings__DefaultConnection`).
- **No se puede conectar al servidor:** comprueba que el servicio SQL esté iniciado, el nombre de instancia sea correcto y `TrustServerCertificate` / certificados encajen con tu política (sobre todo en entornos corporativos).
- **Cliente WinForms no recibe datos:** revisa firewall, certificado HTTPS de desarrollo (`dotnet dev-certs https --trust`) y que la URL base coincida con la de la API.

## Licencia y origen

Repositorio académico / de examen; adapta licencia según tu organización.
