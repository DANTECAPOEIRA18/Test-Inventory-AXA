USE [InventoryDB]
GO

/****** Objeto: UserDefinedFunction [dbo].[fn_NormalizeName] Fecha de script: 3/05/2026 11:20:56 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   FUNCTION [dbo].[fn_NormalizeName] (@name NVARCHAR(200))
RETURNS NVARCHAR(200)
AS
BEGIN
    DECLARE @result NVARCHAR(200)

    SET @result = LOWER(LTRIM(RTRIM(@name)))
    SET @result = REPLACE(@result, '.', '')

    WHILE CHARINDEX('  ', @result) > 0
        SET @result = REPLACE(@result, '  ', ' ')

    RETURN @result
END;
GO


