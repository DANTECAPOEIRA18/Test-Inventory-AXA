USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_CreateArea] Fecha de script: 3/05/2026 11:12:55 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_CreateArea]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Evitar duplicados
        IF EXISTS (SELECT 1 FROM Areas WHERE Name = @Name)
        BEGIN
            RAISERROR('Area already exists', 16, 1);
            ROLLBACK;
            RETURN;
        END

        INSERT INTO Areas (Id, Name)
        VALUES (@Id, @Name);

        INSERT INTO AuditLog (EntityName, Action, EntityId, NewValue, PerformedBy)
        VALUES ('Area', 'CREATE', @Id,
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


