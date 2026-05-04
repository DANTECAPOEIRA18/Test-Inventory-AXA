USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_GetLastUsers] Fecha de script: 3/05/2026 11:18:42 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetLastUsers]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 10
        u.Id,
        u.Name,
        u.Contact,
        u.Email,
        u.DocumentNumber,
        u.AreaId,
        u.RoleId,
        u.DocumentTypeId,
        u.IsActive,
        u.CreatedAt,
        a.Name AS AreaName,
        r.Name AS RoleName,
        d.Name AS DocumentName
    FROM Users u
    LEFT JOIN Areas a ON u.AreaId = a.Id
    LEFT JOIN Roles r ON u.RoleId = r.Id
    LEFT JOIN DocumentTypes d ON u.DocumentTypeId = d.Id

    ORDER BY u.CreatedAt DESC;
END;
GO


