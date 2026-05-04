USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_GetRoles] Fecha de script: 3/05/2026 11:19:12 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_GetRoles]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name
    FROM Roles
    ORDER BY Name;
END;
GO


