USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_GetTypeDocuments] Fecha de script: 3/05/2026 11:19:41 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_GetTypeDocuments]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name
    FROM DocumentTypes
    ORDER BY Name;
END;
GO


