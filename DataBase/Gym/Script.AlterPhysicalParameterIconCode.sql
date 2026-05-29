-- =================================================================
-- Migración: PAF_ICONO en PARAMETROS_FISICOS
-- Default: cadena vacía
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

IF COL_LENGTH('CORE.PARAMETROS_FISICOS', 'PAF_ICONO') IS NULL
BEGIN
    ALTER TABLE [CORE].[PARAMETROS_FISICOS]
        ADD [PAF_ICONO] VARCHAR(64) NOT NULL
            CONSTRAINT [DF_PARAMETROS_FISICOS_ICONO] DEFAULT ('');

    PRINT 'Columna PAF_ICONO agregada (default cadena vacía).';
END
ELSE
    PRINT 'La columna PAF_ICONO ya existe; no se realizó ningún cambio.';
GO
