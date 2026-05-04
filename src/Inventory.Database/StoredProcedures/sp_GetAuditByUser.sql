USE [InventoryDB]
GO

/****** Objeto: StoredProcedure [dbo].[sp_GetAuditByUser] Fecha de script: 3/05/2026 11:17:27 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_GetAuditByUser]
    @UserId UNIQUEIDENTIFIER
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
    WHERE EntityId = @UserId
    ORDER BY PerformedAt DESC;
END;
GO


