-- =================================================================
-- Script para agregar campo Día a la tabla RUTINA_EJERCICIO
-- Este script agrega el campo RUE_DIA (TINYINT) para indicar 
-- el día de la semana en que se debe realizar el ejercicio
-- 
-- Fecha de creación: 2024
-- Schema: CORE
-- =================================================================

USE [Gym]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

BEGIN TRANSACTION

BEGIN TRY
    PRINT '================================================================='
    PRINT 'Agregando campo Día a la tabla RUTINA_EJERCICIO'
    PRINT '================================================================='
    PRINT ''

    -- Verificar si la columna ya existe
    IF NOT EXISTS (
        SELECT * FROM sys.columns 
        WHERE object_id = OBJECT_ID(N'[CORE].[RUTINA_EJERCICIO]') 
        AND name = 'RUE_DIA'
    )
    BEGIN
        -- Agregar la columna RUE_DIA
        ALTER TABLE [CORE].[RUTINA_EJERCICIO]
        ADD [RUE_DIA] TINYINT NOT NULL DEFAULT 1;
        
        PRINT '  ✓ Campo RUE_DIA agregado exitosamente a la tabla CORE.RUTINA_EJERCICIO.'
        
        -- Agregar comentario a la columna
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Día de la semana en que se debe realizar el ejercicio (1-7, donde 1=Lunes, 7=Domingo).', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUE_DIA';
        
        PRINT '  ✓ Comentario agregado al campo RUE_DIA.'
        
        -- Actualizar el índice único para incluir el campo Día
        -- Primero eliminar el índice único existente si existe
        IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RUTINA_EJERCICIO_UNIQUE' AND object_id = OBJECT_ID('CORE.RUTINA_EJERCICIO'))
        BEGIN
            DROP INDEX [IX_RUTINA_EJERCICIO_UNIQUE] ON [CORE].[RUTINA_EJERCICIO];
            PRINT '  ✓ Índice único anterior eliminado.'
        END
        
        -- Crear el nuevo índice único que incluye el campo Día
        CREATE UNIQUE NONCLUSTERED INDEX [IX_RUTINA_EJERCICIO_UNIQUE] 
        ON [CORE].[RUTINA_EJERCICIO] ([RUT_ID], [EJE_ID], [RUE_DIA]);
        PRINT '  ✓ Nuevo índice único creado incluyendo el campo RUE_DIA.'
    END
    ELSE
    BEGIN
        PRINT '  - El campo RUE_DIA ya existe en la tabla CORE.RUTINA_EJERCICIO.'
    END

    PRINT ''
    PRINT '================================================================='
    PRINT 'Agregación de campo Día completada exitosamente'
    PRINT '================================================================='
    PRINT ''

    COMMIT TRANSACTION

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION
    
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
    DECLARE @ErrorState INT = ERROR_STATE()
    
    PRINT '================================================================='
    PRINT 'ERROR: Ocurrió un error durante la agregación del campo'
    PRINT '================================================================='
    PRINT 'Mensaje de error: ' + @ErrorMessage
    PRINT ''
    
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
END CATCH
GO
