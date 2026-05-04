USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_CreateTypeDocument] Fecha de script: 3/05/2026 11:14:17 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_CreateTypeDocument]
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
            RAISERROR('TypeDocument already exists', 16, 1);
            ROLLBACK;
            RETURN;
        END

        INSERT INTO DocumentTypes (Id, Name)
        VALUES (@Id, @Name);

        INSERT INTO AuditLog (EntityName, Action, EntityId, NewValue, PerformedBy)
        VALUES ('TypeDocument', 'CREATE', @Id,
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


