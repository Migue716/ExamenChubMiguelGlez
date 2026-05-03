/*
  =============================================================================
  ExamenChub — SQL Server: un solo script (creación y opcional borrado)
  =============================================================================
  Ejecutar en SSMS o sqlcmd conectado a la instancia deseada (ej. LocalDB).

  Por defecto solo CREA la base y la tabla si no existen.

  Para RECREAR desde cero: descomenta el bloque "ELIMINAR BASE" más abajo,
  ejecuta una vez, vuelve a comentarlo y ejecuta el resto (o ejecuta todo
  si dejas el DROP activo seguido del CREATE).
  =============================================================================
*/

SET NOCOUNT ON;

/* ========== ELIMINAR BASE (opcional — descomentar solo en desarrollo) ==========
IF DB_ID(N'ExamenChub') IS NOT NULL
BEGIN
    ALTER DATABASE [ExamenChub] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [ExamenChub];
    PRINT N'Base de datos [ExamenChub] eliminada.';
END
ELSE
    PRINT N'La base [ExamenChub] no existía.';
GO
============================================================================= */

IF DB_ID(N'ExamenChub') IS NULL
BEGIN
    CREATE DATABASE [ExamenChub];
    PRINT N'Base de datos [ExamenChub] creada.';
END
ELSE
    PRINT N'La base de datos [ExamenChub] ya existe.';
GO

USE [ExamenChub];
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = N'Cliente' AND s.name = N'dbo')
BEGIN
    CREATE TABLE dbo.Cliente (
        Id INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_Cliente PRIMARY KEY,
        Nombre NVARCHAR(200) NOT NULL,
        Edad NVARCHAR(50) NOT NULL,
        Direccion NVARCHAR(500) NOT NULL,
        CorreoElectronico NVARCHAR(320) NOT NULL
    );
    PRINT N'Tabla [dbo].[Cliente] creada.';
END
ELSE
    PRINT N'La tabla [dbo].[Cliente] ya existe.';
GO
