USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_GetAreas] Fecha de script: 3/05/2026 11:16:14 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_GetAreas]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name
    FROM Areas
    ORDER BY Name;
END;
GO


