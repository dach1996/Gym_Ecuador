-- =================================================================
-- Insert: parámetro IMC en CORE.PARAMETROS_FISICOS
-- Ejecutar si el catálogo ya existe pero falta IMC (PAF_ID = 10)
-- =================================================================

USE [Gym];
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (SELECT 1 FROM [CORE].[PARAMETROS_FISICOS] WHERE [PAF_CODIGO] = 'IMC')
BEGIN
    INSERT INTO [CORE].[PARAMETROS_FISICOS] ([PAF_ID], [PAF_CODIGO], [PAF_NOMBRE], [PAF_ESTADO], [PAF_TIPO_DIFERENCIA], [PAF_UNIDAD_MEDIDA])
    VALUES (10, 'IMC', N'Índice de masa corporal', 1, 0, 3);

    PRINT 'Parámetro IMC insertado (PAF_ID = 10).';
END
ELSE
    PRINT 'El parámetro IMC ya existe; no se realizó inserción.';
GO
