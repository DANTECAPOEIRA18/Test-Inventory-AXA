USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_DeleteUser] Fecha de script: 3/05/2026 11:15:32 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_DeleteUser]
    @UserId UNIQUEIDENTIFIER,
    @UserAction NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Users
        SET IsActive = 0
        WHERE Id = @UserId;

        INSERT INTO AuditLog (EntityName, Action, EntityId, NewValue,PerformedBy)
        VALUES (
            'Users', 
            'DELETE', 
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


