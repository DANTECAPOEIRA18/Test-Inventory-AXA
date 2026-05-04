USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_CreateRole] Fecha de script: 3/05/2026 11:13:30 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_CreateRole]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Roles (Id, Name)
        VALUES (@Id, @Name);

        INSERT INTO AuditLog (EntityName, Action, EntityId, NewValue, PerformedBy)
        VALUES ('Role', 'CREATE', @Id,
                CONCAT('Name:', @Name),
                'SYSTEM');

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        THROW;
    END CATCH
END;
GO


