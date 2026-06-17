-- =================================================================
-- Migración: PAF_EXPRESION_VALIDACION en PARAMETROS_FISICOS
-- Default: regex para valores > 0 y < 150
-- =================================================================

USE [Gym];
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE s.name = 'CORE' AND t.name = 'PARAMETROS_FISICOS')
BEGIN
    RAISERROR('La tabla CORE.PARAMETROS_FISICOS no existe.', 16, 1);
    RETURN;
END
GO

IF COL_LENGTH('CORE.PARAMETROS_FISICOS', 'PAF_EXPRESION_VALIDACION') IS NULL
BEGIN
    ALTER TABLE [CORE].[PARAMETROS_FISICOS]
        ADD [PAF_EXPRESION_VALIDACION] NVARCHAR(128) NOT NULL
            CONSTRAINT [DF_PARAMETROS_FISICOS_PAF_EXPRESION_VALIDACION]
            DEFAULT (N'^(0\.\d*[1-9]\d*|[1-9]\d{0,2}(\.\d+)?|1[0-4]\d(\.\d+)?|149(\.\d+)?)$');

    PRINT 'Columna PAF_EXPRESION_VALIDACION agregada (default regex > 0 y < 150).';
END
ELSE
    PRINT 'La columna PAF_EXPRESION_VALIDACION ya existe; no se realizó ningún cambio.';
GO
