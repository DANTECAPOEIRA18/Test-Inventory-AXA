USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_CreateUser] Fecha de script: 3/05/2026 11:14:47 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CreateUser]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @Contact NVARCHAR(20),
    @Email NVARCHAR(250),
    @Document INT,
    @DocumentId UNIQUEIDENTIFIER,
    @AreaId UNIQUEIDENTIFIER,
    @RoleId UNIQUEIDENTIFIER,
    @UserAction NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @NormalizedInput NVARCHAR(200)
        DECLARE @Area NVARCHAR(150);
        DECLARE @Role NVARCHAR(150);
        DECLARE @DocumentTypeName NVARCHAR(150);

        SET @NormalizedInput = dbo.fn_NormalizeName(@Name)

        -- =========================================
        -- VALIDACIÓN NOMBRE (FUZZY)
        -- =========================================
        IF EXISTS (
            SELECT 1
            FROM Users
            WHERE 
                dbo.fn_NormalizeName(Name) = @NormalizedInput
                OR dbo.fn_NormalizeName(Name) LIKE '%' + @NormalizedInput + '%'
                OR @NormalizedInput LIKE '%' + dbo.fn_NormalizeName(Name) + '%'
                OR DIFFERENCE(Name, @Name) >= 3
        )
        BEGIN
            RAISERROR('Similar user name already exists', 16, 1);
            ROLLBACK;
            RETURN;
        END

        -- =========================================
        -- VALIDAR CONTACTO
        -- =========================================
        IF @Contact LIKE '%[^0-9]%'
        BEGIN
            RAISERROR('Contact must contain only numbers', 16, 1);
            ROLLBACK;
            RETURN;
        END

        IF LEN(@Contact) < 7 OR LEN(@Contact) > 15
        BEGIN
            RAISERROR('Invalid contact length', 16, 1);
            ROLLBACK;
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Users WHERE Contact = @Contact)
        BEGIN
            RAISERROR('Contact already exists', 16, 1);
            ROLLBACK;
            RETURN;
        END

        -- =========================================
        -- VALIDAR EMAIL (BÁSICO)
        -- =========================================
        IF @Email IS NOT NULL AND @Email NOT LIKE '%_@__%.__%'
        BEGIN
            RAISERROR('Invalid email format', 16, 1);
            ROLLBACK;
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
        BEGIN
            RAISERROR('Email already exists', 16, 1);
            ROLLBACK;
            RETURN;
        END

        -- =========================================
        -- VALIDAR DOCUMENTO DUPLICADO
        -- =========================================
        IF EXISTS (
            SELECT 1 
            FROM Users 
            WHERE DocumentNumber = @Document 
              AND DocumentTypeId = @DocumentId
        )
        BEGIN
            RAISERROR('Document already exists', 16, 1);
            ROLLBACK;
            RETURN;
        END

        -- =========================================
        -- INSERT USER (YA CON NUEVOS CAMPOS)
        -- =========================================
        INSERT INTO Users (
            Id,
            Name,
            Contact,
            Email,
            DocumentNumber,
            DocumentTypeId,
            AreaId,
            RoleId,
            IsActive,
            CreatedAt
        )
        VALUES (
            @Id,
            @Name,
            @Contact,
            @Email,
            @Document,
            @DocumentId,
            @AreaId,
            @RoleId,
            1,
            GETDATE()
        );

        -- =========================================
        -- TRAER NOMBRES PARA AUDITORÍA
        -- =========================================
        SELECT 
            @Area = a.Name,
            @Role = r.Name
        FROM Areas a
        CROSS JOIN Roles r
        WHERE a.Id = @AreaId AND r.Id = @RoleId;

        SELECT @DocumentTypeName = Name 
        FROM DocumentTypes 
        WHERE Id = @DocumentId;

        -- =========================================
        -- AUDITORÍA
        -- =========================================
        INSERT INTO AuditLog (
            EntityName,
            Action,
            EntityId,
            OldValue,
            NewValue,
            PerformedBy,
            PerformedAt
        )
        VALUES (
            'Users',
            'CREATE',
            @Id,
            NULL,
            CONCAT(
                '{ "Name":"', @Name,
                '", "Contact":"', @Contact,
                '", "Email":"', @Email,
                '", "Document":"', @Document,
                '", "DocumentType":"', @DocumentTypeName,
                '", "Area":"', @Area,
                '", "Role":"', @Role,
                '" }'
            ),
            @UserAction,
            GETDATE()
        );

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;

        THROW;
    END CATCH
END;
GO


