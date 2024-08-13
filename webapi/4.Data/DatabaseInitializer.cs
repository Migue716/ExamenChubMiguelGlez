using System.Data;

public static class DatabaseInitializer
{
    public static void Initialize(IDbConnection dbConnection)
    {
        var command = dbConnection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Cliente (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombre TEXT NOT NULL,
                Edad TEXT NOT NULL,
                Direccion TEXT NOT NULL,
                CorreoElectronico TEXT NOT NULL
            );
        ";
        command.ExecuteNonQuery();
    }
}
