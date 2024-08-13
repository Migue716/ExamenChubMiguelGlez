using System.Data;
using Dapper;

public class ClienteRepository : IClienteRepository
{
    private readonly IDbConnection _dbConnection;

    public ClienteRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        InitializeDatabase();
    }

    // Inicializar la base de datos en memoria
    private void InitializeDatabase()
    {
        string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Cliente (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombre TEXT NOT NULL,
                Edad TEXT NOT NULL,
                Direccion TEXT NOT NULL,
                CorreoElectronico TEXT NOT NULL
            )";
        _dbConnection.Execute(createTableQuery);
    }

    // CREATE
    public void AddCliente(Cliente cliente)
    {
        string insertQuery = "INSERT INTO Cliente (Nombre, Edad, Direccion, CorreoElectronico) VALUES (@Nombre, @Edad, @Direccion, @CorreoElectronico)";
        _dbConnection.Execute(insertQuery, cliente);
    }

    // READ
    public IEnumerable<Cliente> GetClientes()
    {
        string selectQuery = "SELECT * FROM Cliente";
        return _dbConnection.Query<Cliente>(selectQuery);
    }

    public Cliente GetClienteById(int id)
    {
        string selectQuery = "SELECT * FROM Cliente WHERE Id = @Id";
        return _dbConnection.QuerySingleOrDefault<Cliente>(selectQuery, new { Id = id });
    }

    // UPDATE
    public void UpdateCliente(Cliente cliente)
    {
        string updateQuery = "UPDATE Cliente SET Nombre = @Nombre, Edad = @Edad, Direccion = @Direccion, CorreoElectronico = @CorreoElectronico WHERE Id = @Id";
        _dbConnection.Execute(updateQuery, cliente);
    }

    // DELETE
    public void DeleteCliente(int id)
    {
        string deleteQuery = "DELETE FROM Cliente WHERE Id = @Id";
        _dbConnection.Execute(deleteQuery, new { Id = id });
    }

    // SEARCH (Nuevo método)
    public IEnumerable<Cliente> SearchClientes(string nombre = null, string correoElectronico = null)
    {
        string searchQuery = "SELECT * FROM Cliente WHERE 1=1";
        if (!string.IsNullOrEmpty(nombre))
        {
            searchQuery += " AND Nombre LIKE @Nombre";
        }
        if (!string.IsNullOrEmpty(correoElectronico))
        {
            searchQuery += " AND CorreoElectronico LIKE @CorreoElectronico";
        }

        return _dbConnection.Query<Cliente>(searchQuery, new { Nombre = $"%{nombre}%", CorreoElectronico = $"%{correoElectronico}%" });
    }
}   
