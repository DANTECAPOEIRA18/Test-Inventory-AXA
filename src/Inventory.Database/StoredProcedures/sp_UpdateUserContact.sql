USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_UpdateUserContact] Fecha de script: 3/05/2026 11:20:18 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_UpdateUserContact]
    @UserId UNIQUEIDENTIFIER,
    @Contact NVARCHAR(150),
    @Email NVARCHAR(150),
    @AreaId UNIQUEIDENTIFIER,
    @RoleId UNIQUEIDENTIFIER,
    @UserAction NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @OldContact NVARCHAR(150);
    DECLARE @OldEmail NVARCHAR(150);
    DECLARE @OldArea NVARCHAR(150);
    DECLARE @OldRole NVARCHAR(150);

    DECLARE @NewArea NVARCHAR(150);
    DECLARE @NewRole NVARCHAR(150);

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT @OldContact = Contact
        FROM Users
        WHERE Id = @UserId;

        SELECT @OldEmail = Email
        FROM Users
        WHERE Id = @UserId;

    SELECT 
        @OldArea = a.Name,
        @OldRole = r.Name
    FROM Users u
    LEFT JOIN Areas a ON u.AreaId = a.Id
    LEFT JOIN Roles r ON u.RoleId = r.Id
    WHERE u.Id = @UserId;

        UPDATE Users
        SET Contact = @Contact, AreaId = @AreaId, RoleId = @RoleId, Email = @Email
        WHERE Id = @UserId;

    SELECT 
        @NewArea = a.Name,
        @NewRole = r.Name
    FROM Users u
    LEFT JOIN Areas a ON u.AreaId = a.Id
    LEFT JOIN Roles r ON u.RoleId = r.Id
    WHERE u.Id = @UserId;

        INSERT INTO AuditLog (EntityName, Action, EntityId, OldValue, NewValue, PerformedBy)
        VALUES ('Users', 'UPDATE_CONTACT', @UserId,             
            CONCAT(
                '{ 
                  "Contact": "', @OldContact,
                '", "Area": "', @OldArea,
                '", "Role": "', @OldRole,
                '", "Email": "', @OldEmail,
                '" }'
            ),             
            CONCAT(
                '{ "Contact": "', @Contact,
                '", "Area": "', @NewArea,
                '", "Role": "', @NewRole,
                '", "Email": "', @Email,
                '" }'
            ), @UserAction);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        THROW;
    END CATCH
END;
GO


