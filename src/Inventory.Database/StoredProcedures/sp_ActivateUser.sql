USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_ActivateUser] Fecha de script: 3/05/2026 11:11:56 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[sp_ActivateUser]
    @UserId UNIQUEIDENTIFIER,
    @UserAction NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Users
        SET IsActive = 1
        WHERE Id = @UserId;

        INSERT INTO AuditLog (EntityName, Action, EntityId, NewValue,PerformedBy)
        VALUES (
            'Users', 
            'ACTIVATE', 
            @UserId,
                        CONCAT(
                '{ 
                  "UserId": "', @UserId,
                '" }'
            ),
            @UserAction);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        THROW;
    END CATCH
END;
GO


