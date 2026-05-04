USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_GetAudit] Fecha de script: 3/05/2026 11:16:48 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_GetAudit]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Action,
        EntityName AS TableName,
        OldValue AS OldData,
        NewValue AS NewData,
        PerformedAt AS CreatedAt
    FROM AuditLog
    ORDER BY PerformedAt DESC;
END;
GO


