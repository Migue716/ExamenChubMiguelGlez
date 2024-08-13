using System.Data;
using System.Data.Entity;
using System.Data.SQLite;

// Crear y configurar el builder de la aplicación
var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers();

// Agregar servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar la conexión a la base de datos como servicio
builder.Services.AddSingleton<IDbConnection>(sp =>
{
    var connection = new SQLiteConnection("Data Source=:memory:;Version=3;New=True;");
    connection.Open();
    DatabaseInitializer.Initialize(connection);
    return connection;
});

// Registrar las dependencias de la capa de acceso a datos
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

// Registrar los servicios de la capa de negocio
builder.Services.AddScoped<ClienteService>();

// Construir la aplicación
var app = builder.Build();

// Configurar el pipeline HTTP
app.UseHttpsRedirection();
app.UseAuthorization();

// Configurar Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty; // Esto hará que Swagger UI esté disponible en la raíz del sitio
    });
}

// Mapear los controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();
