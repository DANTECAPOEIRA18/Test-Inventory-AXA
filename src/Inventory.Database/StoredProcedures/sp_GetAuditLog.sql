USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_GetAuditLog] Fecha de script: 3/05/2026 11:18:08 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetAuditLog]
AS
BEGIN
    SELECT *
    FROM AuditLog
    ORDER BY PerformedAt DESC;
END;
GO


