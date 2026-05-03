using System.Data;

public static class DatabaseInitializer
{
    public static void Initialize(IDbConnection dbConnection)
    {
        using var command = dbConnection.CreateCommand();
        command.CommandText = @"
IF NOT EXISTS (
    SELECT 1 FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = N'Cliente' AND s.name = N'dbo')
BEGIN
    CREATE TABLE dbo.Cliente (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Cliente PRIMARY KEY,
        Nombre NVARCHAR(200) NOT NULL,
        Edad NVARCHAR(50) NOT NULL,
        Direccion NVARCHAR(500) NOT NULL,
        CorreoElectronico NVARCHAR(320) NOT NULL
    );
END";
        command.ExecuteNonQuery();
    }
}
