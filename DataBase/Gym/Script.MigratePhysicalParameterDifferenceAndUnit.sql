-- =================================================================
-- Migración: PAF_TIPO_DIFERENCIA y PAF_UNIDAD_MEDIDA en PARAMETROS_FISICOS
-- Valores alineados con PhysicalParameterDifferenceValueType y PhysicalParameterUnit
-- =================================================================
-- DifferenceValueType: 0=Positive, 1=Negative, 2=Zero
-- MeasurementUnit:     1=Centimeter, 2=Kilogram, 3=Point
-- =================================================================

USE [Gym];
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE s.name = 'CORE' AND t.name = 'PARAMETROS_FISICOS')
BEGIN
    RAISERROR('La tabla CORE.PARAMETROS_FISICOS no existe.', 16, 1);
END
GO

-- Lote 1: agregar columnas (debe ejecutarse antes de los UPDATE)
IF COL_LENGTH('CORE.PARAMETROS_FISICOS', 'PAF_TIPO_DIFERENCIA') IS NULL
BEGIN
    ALTER TABLE [CORE].[PARAMETROS_FISICOS]
        ADD [PAF_TIPO_DIFERENCIA] TINYINT NOT NULL
            CONSTRAINT [DF_PARAMETROS_FISICOS_TIPO_DIFERENCIA] DEFAULT (0);
    PRINT 'Columna PAF_TIPO_DIFERENCIA agregada (default Positive).';
END
GO

IF COL_LENGTH('CORE.PARAMETROS_FISICOS', 'PAF_UNIDAD_MEDIDA') IS NULL
BEGIN
    ALTER TABLE [CORE].[PARAMETROS_FISICOS]
        ADD [PAF_UNIDAD_MEDIDA] TINYINT NOT NULL
            CONSTRAINT [DF_PARAMETROS_FISICOS_UNIDAD_MEDIDA] DEFAULT (1);
    PRINT 'Columna PAF_UNIDAD_MEDIDA agregada (default Centimeter).';
END
GO

-- Lote 2: datos (las columnas ya existen en este batch)
BEGIN TRY
    BEGIN TRANSACTION;

    UPDATE [CORE].[PARAMETROS_FISICOS]
    SET [PAF_TIPO_DIFERENCIA] = 0,
        [PAF_UNIDAD_MEDIDA] = 2
    WHERE [PAF_CODIGO] = 'PESO';

    UPDATE [CORE].[PARAMETROS_FISICOS]
    SET [PAF_TIPO_DIFERENCIA] = 0,
        [PAF_UNIDAD_MEDIDA] = 1
    WHERE [PAF_CODIGO] IN (
        'ALTURA',
        'MEDIDA_PECHO',
        'MEDIDA_CINTURA',
        'MEDIDA_CADERA',
        'MEDIDA_BRAZO_DER',
        'MEDIDA_MUSLO_DER'
    );

    UPDATE [CORE].[PARAMETROS_FISICOS]
    SET [PAF_TIPO_DIFERENCIA] = 0,
        [PAF_UNIDAD_MEDIDA] = 3
    WHERE [PAF_CODIGO] IN (
        'GRASA_PORCENTAJE',
        'MUSCULO_PORCENTAJE',
        'IMC'
    );

    COMMIT TRANSACTION;
    PRINT 'Migración PAF_TIPO_DIFERENCIA / PAF_UNIDAD_MEDIDA completada.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
GO
