using System.Data;
using Dapper;

public class ClienteRepository : IClienteRepository
{
    private readonly IDbConnection _dbConnection;

    public ClienteRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public void AddCliente(Cliente cliente)
    {
        const string insertQuery = @"
INSERT INTO Cliente (Nombre, Edad, Direccion, CorreoElectronico)
OUTPUT INSERTED.Id
VALUES (@Nombre, @Edad, @Direccion, @CorreoElectronico)";
        var newId = _dbConnection.QuerySingle<int>(insertQuery, cliente);
        cliente.Id = newId;
    }

    public IEnumerable<Cliente> GetClientes()
    {
        const string selectQuery = "SELECT Id, Nombre, Edad, Direccion, CorreoElectronico FROM Cliente";
        return _dbConnection.Query<Cliente>(selectQuery);
    }

    public Cliente? GetClienteById(int id)
    {
        const string selectQuery = "SELECT Id, Nombre, Edad, Direccion, CorreoElectronico FROM Cliente WHERE Id = @Id";
        return _dbConnection.QuerySingleOrDefault<Cliente>(selectQuery, new { Id = id });
    }

    public void UpdateCliente(Cliente cliente)
    {
        const string updateQuery = @"
UPDATE Cliente
SET Nombre = @Nombre, Edad = @Edad, Direccion = @Direccion, CorreoElectronico = @CorreoElectronico
WHERE Id = @Id";
        _dbConnection.Execute(updateQuery, cliente);
    }

    public void DeleteCliente(int id)
    {
        const string deleteQuery = "DELETE FROM Cliente WHERE Id = @Id";
        _dbConnection.Execute(deleteQuery, new { Id = id });
    }

    public IEnumerable<Cliente> SearchClientes(string? nombre = null, string? correoElectronico = null)
    {
        var searchQuery = "SELECT Id, Nombre, Edad, Direccion, CorreoElectronico FROM Cliente WHERE 1 = 1";
        if (!string.IsNullOrEmpty(nombre))
        {
            searchQuery += " AND Nombre LIKE @Nombre";
        }

        if (!string.IsNullOrEmpty(correoElectronico))
        {
            searchQuery += " AND CorreoElectronico LIKE @CorreoElectronico";
        }

        return _dbConnection.Query<Cliente>(searchQuery, new
        {
            Nombre = string.IsNullOrEmpty(nombre) ? null : $"%{nombre}%",
            CorreoElectronico = string.IsNullOrEmpty(correoElectronico) ? null : $"%{correoElectronico}%"
        });
    }
}
