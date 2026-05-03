# ExamenChub — Clientes con SQL Server

Repositorio con una **API REST** en **.NET 8** (ASP.NET Core) que gestiona **clientes** en **Microsoft SQL Server**, y un **cliente web** en **React + Vite + TypeScript** (`client/`).

Funcionalidades del API: listar, obtener por id, crear, actualizar, eliminar y **buscar** por coincidencia parcial en nombre y/o correo electrónico. El cliente React implementa listado, formulario (alta/edición), eliminación y **búsqueda solo por nombre**.

---

## Requisitos

| Componente | Uso |
|------------|-----|
| [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) | Compilar y ejecutar `webapi` |
| [Node.js](https://nodejs.org/) (LTS) | `npm install` / `npm run dev` en `client` |
| **SQL Server** (LocalDB, Express, instancia completa o Azure SQL) | Persistencia; ajusta la cadena de conexión |

---

## Estructura del repositorio

```
ExamenChub.sln          → solución Visual Studio (solo proyecto WebAPI)
webapi/                 → API ASP.NET Core
client/                 → aplicación React (Vite)
scripts/sql-server/     → script SQL único de creación de base/tabla
```

| Ruta | Descripción |
|------|-------------|
| **webapi** | Controladores, `ClienteService`, entidad `Cliente`, `ClienteRepository` (**Dapper** + **Microsoft.Data.SqlClient**), `DatabaseInitializer`. **Swagger** en `Development`. **CORS** para orígenes `http://localhost:5173` y `https://localhost:5173`. |
| **client** | UI: tabla, formulario, búsqueda por nombre. URL base del API configurable con **`VITE_API_URL`** (ver `client/.env` y `client/.env.example`). |
| **scripts/sql-server/DatabaseSetup.sql** | Crea la base `ExamenChub` y la tabla `dbo.Cliente` si no existen (opcional; la API también crea la tabla al arrancar). |

Capas dentro de `webapi` (orden lógico):

1. `1.Controllers` — `ClientesController`
2. `2.Business` — `ClienteService`
3. `3.Entities` — modelo `Cliente`
4. `4.Data` — `ClienteRepository`, `IClienteRepository`, `DatabaseInitializer`

---

## Base de datos

### Cadena de conexión

Configuración en **`webapi/appsettings.json`** y **`webapi/appsettings.Development.json`**, clave **`ConnectionStrings:DefaultConnection`**.

Ejemplo por defecto (LocalDB, base `ExamenChub`):

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ExamenChub;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0"
}
```

Si la conexión falla por TLS en local, prueba **`TrustServerCertificate=True`**. Para otras instancias, cambia `Data Source` / `Initial Catalog` o usa usuario y contraseña según tu entorno.

También puedes definir la cadena con variables de entorno: **`ConnectionStrings__DefaultConnection`**.

### Tabla `dbo.Cliente`

Creada al iniciar la API si no existe (`DatabaseInitializer`):

| Columna | Tipo |
|---------|------|
| `Id` | `INT IDENTITY(1,1)` PK |
| `Nombre` | `NVARCHAR(200)` |
| `Edad` | `NVARCHAR(50)` |
| `Direccion` | `NVARCHAR(500)` |
| `CorreoElectronico` | `NVARCHAR(320)` |

### Script SQL manual

Ejecuta en SSMS o `sqlcmd`:

`scripts/sql-server/DatabaseSetup.sql`

Incluye bloque comentado opcional para borrar la base en desarrollo.

### Paquetes NuGet (API)

- **Dapper**
- **Microsoft.Data.SqlClient**
- **Swashbuckle.AspNetCore** (Swagger)

---

## Ejecutar la API

Desde la raíz del repo:

```bash
dotnet run --project webapi/WebAPI.csproj
```

El puerto HTTPS depende del **perfil de lanzamiento** (`webapi/Properties/launchSettings.json`):

| Perfil | HTTPS típico | Notas |
|--------|----------------|--------|
| **IIS Express** o **YourAppName** | `https://localhost:44309` | Alineado con el `client/.env` por defecto |
| **https** (Kestrel) | `https://localhost:7274` (y HTTP `5080`) | Si usas este perfil, cambia `VITE_API_URL` y el `target` del proxy en `client/vite.config.ts` |

En **Development**, Swagger suele abrirse en la raíz del sitio (p. ej. `https://localhost:44309/index.html` o la URL equivalente de tu perfil).

### Endpoints REST

Prefijo: **`/api/Clientes`**. Las rutas con `{id}` usan restricción **`int`** para no chocar con `search`.

| Método | Ruta | Descripción |
|--------|------|-------------|
| `GET` | `/api/Clientes` | Lista todos |
| `GET` | `/api/Clientes/search` | Búsqueda; query opcionales `nombre`, `correoElectronico` (coincidencia parcial; sin filtros devuelve todos) |
| `GET` | `/api/Clientes/{id}` | Por id |
| `POST` | `/api/Clientes` | Crea (respuesta incluye `id` generado) |
| `PUT` | `/api/Clientes/{id}` | Actualiza |
| `DELETE` | `/api/Clientes/{id}` | Elimina |

### Conexión SQL por petición

`IDbConnection` está registrada como **`Scoped`**: una `SqlConnection` por petición HTTP, adecuada para **pooling** y uso concurrente.

---

## Ejecutar el cliente React

1. **Arranca la API** en el mismo origen que definas en el cliente (por defecto **`https://localhost:44309`** con IIS Express / perfil equivalente en Visual Studio).

2. **Variables de entorno** (`client/.env`):

   - `VITE_API_URL=https://localhost:44309` — las peticiones van a `https://localhost:44309/api/...` (origen distinto al de Vite → usa CORS del API).
   - Si **no** defines `VITE_API_URL`, las rutas relativas `/api` las reenvía el **proxy** de Vite; su `target` está en `client/vite.config.ts` (mismo host/puerto que el `.env` por defecto).

3. Instalación y desarrollo:

```bash
cd client
npm install
npm run dev
```

Abre la URL indicada en consola (habitualmente **`http://localhost:5173`**).

### Build de producción del cliente

```bash
cd client
npm run build
```

Salida en **`client/dist/`**. En despliegue, configura **`VITE_API_URL`** apuntando al API público (sin barra final).

---

## Solución de problemas

| Problema | Qué revisar |
|----------|-------------|
| Excepción por **`DefaultConnection`** | Cadena en `appsettings*.json` o variable `ConnectionStrings__DefaultConnection`. |
| No conecta a SQL Server | Servicio iniciado, instancia correcta, permisos; prueba `TrustServerCertificate=True` en local si hay errores de certificado. |
| React no obtiene datos | API en marcha; puerto igual al de `VITE_API_URL`; certificado de desarrollo (`dotnet dev-certs https --trust`); CORS solo incluye `localhost:5173` — si sirves el front desde otro origen, amplía CORS en `webapi/Program.cs`. |
| `dotnet run` en otro puerto | Actualiza **`client/.env`** y, si usas proxy sin `VITE_API_URL`, **`client/vite.config.ts`**. |

---

## Licencia y origen

Repositorio académico / de examen; adapta la licencia según tu organización.
