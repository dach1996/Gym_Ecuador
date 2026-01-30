-- =================================================================
-- Script: Agregar columna USR_TIENE_DATOS_VERIFICADOS a USUARIO
-- Descripción: Indica si el usuario tiene datos verificados por IA
-- Tabla: AUTENTICACION.USUARIO
-- Columna: USR_TIENE_DATOS_VERIFICADOS (BIT NOT NULL, default 0)
-- =================================================================

USE [Gym]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

BEGIN TRANSACTION
GO

BEGIN TRY
    PRINT '================================================================='
    PRINT 'Agregando columna USR_TIENE_DATOS_VERIFICADOS a AUTENTICACION.USUARIO'
    PRINT '================================================================='
    PRINT ''

    IF NOT EXISTS (
        SELECT 1
        FROM sys.columns
        WHERE object_id = OBJECT_ID(N'[AUTENTICACION].[USUARIO]')
          AND name = 'USR_TIENE_DATOS_VERIFICADOS'
    )
    BEGIN
        ALTER TABLE [AUTENTICACION].[USUARIO]
        ADD [USR_TIENE_DATOS_VERIFICADOS] BIT NOT NULL CONSTRAINT [DF_USUARIO_TIENE_DATOS_VERIFICADOS] DEFAULT (0);

        PRINT '  ✓ Columna USR_TIENE_DATOS_VERIFICADOS agregada correctamente.'
        PRINT ''

        -- Descripción extendida de la columna
        EXECUTE sp_addextendedproperty
            @name = N'MS_Description',
            @value = N'Tiene datos verificados por IA',
            @level0type = N'SCHEMA', @level0name = N'AUTENTICACION',
            @level1type = N'TABLE',  @level1name = N'USUARIO',
            @level2type = N'COLUMN', @level2name = N'USR_TIENE_DATOS_VERIFICADOS';

        PRINT '  ✓ Descripción extendida agregada.'
    END
    ELSE
    BEGIN
        PRINT '  - La columna USR_TIENE_DATOS_VERIFICADOS ya existe en AUTENTICACION.USUARIO.'
    END

    COMMIT TRANSACTION

    PRINT ''
    PRINT '================================================================='
    PRINT 'Script completado correctamente.'
    PRINT '================================================================='

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();

    PRINT ''
    PRINT 'ERROR: ' + @ErrorMessage;
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH
GO
