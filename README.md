# ExamenChub — API de clientes con SQL Server

Solución .NET 8 que expone una **API REST** para gestionar **clientes** (altas, consultas, actualización, borrado y búsqueda) persistiendo datos en **Microsoft SQL Server**. Incluye un **cliente web React (Vite + TypeScript)** en la carpeta `client` que consume la API.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (LTS recomendado) para el cliente React
- **SQL Server** accesible desde tu máquina, por ejemplo:
  - [SQL Server Express LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb) (incluido con Visual Studio y muchas cargas de trabajo de datos), o
  - SQL Server / Azure SQL con cadena de conexión válida

## Estructura de la solución

| Proyecto | Descripción |
|----------|-------------|
| **webapi** | ASP.NET Core Web API: controladores, servicio de negocio, repositorio con **Dapper** y **Microsoft.Data.SqlClient**. Swagger en entorno **Development**. CORS permitido para `http(s)://localhost:5173` (Vite). |
| **client** | React + Vite + TypeScript: llama al API en **`https://localhost:44309`** (`VITE_API_URL` en `client/.env`). *Proxy* de respaldo en `vite.config.ts` al mismo host. |

Capas en la API (orden conceptual):

1. `1.Controllers` — `ClientesController`
2. `2.Business` — `ClienteService`
3. `3.Entities` — modelo `Cliente`
4. `4.Data` — `ClienteRepository`, `IClienteRepository`, `DatabaseInitializer`

## Base de datos (SQL Server)

### Cadena de conexión

La API lee **`ConnectionStrings:DefaultConnection`** desde `webapi/appsettings.json` o `webapi/appsettings.Development.json`.

Por defecto se usa **LocalDB** y una base llamada `ExamenChub` (cadena alineada con un perfil tipo SSMS: cifrado activo y `TrustServerCertificate=False`; si falla la conexión local, prueba `TrustServerCertificate=True`):

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ExamenChub;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0"
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

## Cómo ejecutar el cliente React

1. Arranca la API en **`https://localhost:44309`** (por ejemplo perfil **IIS Express** o **YourAppName** en Visual Studio, ver `webapi/Properties/launchSettings.json`). El cliente React usa `VITE_API_URL=https://localhost:44309` en `client/.env`. El *proxy* de Vite reenvía `/api` al mismo puerto si trabajas sin variable de entorno.
2. Instala dependencias y levanta Vite:

```bash
cd client
npm install
npm run dev
```

3. Abre la URL que muestre la consola (normalmente `http://localhost:5173`).

**Producción / otro host de API:** crea `client/.env` con `VITE_API_URL=https://tu-servidor` (sin barra final). Las peticiones irán a esa base en lugar del proxy.

**Build estático del cliente:**

```bash
cd client
npm run build
```

Salida en `client/dist/`.

## Conexión por solicitud

La conexión a SQL Server está registrada como **`AddScoped<IDbConnection>`**: cada petición HTTP obtiene su propia `SqlConnection`, que el contenedor dispone al terminar el ámbito. Esto es adecuado para **pooling** del proveedor y evita compartir una conexión global entre hilos.

## Solución de problemas

- **`InvalidOperationException` sobre `DefaultConnection`:** define la cadena en `appsettings*.json` o con variables de entorno (`ConnectionStrings__DefaultConnection`).
- **No se puede conectar al servidor:** comprueba que el servicio SQL esté iniciado, el nombre de instancia sea correcto y `TrustServerCertificate` / certificados encajen con tu política (sobre todo en entornos corporativos).
- **Cliente React no recibe datos:** ejecuta la API primero en el mismo puerto que `client/.env` (`44309` por defecto); confía el certificado HTTPS (`dotnet dev-certs https --trust`). Si usas otro puerto (p. ej. Kestrel `7274`), cambia `VITE_API_URL` y el `target` del *proxy* en `client/vite.config.ts`.

## Licencia y origen

Repositorio académico / de examen; adapta licencia según tu organización.
