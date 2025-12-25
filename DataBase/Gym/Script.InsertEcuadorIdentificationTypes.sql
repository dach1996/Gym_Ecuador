-- =================================================================
-- Script de inserción de Tipos de Identificación para Ecuador
-- Este script inserta los tipos de identificación más comunes
-- utilizados en Ecuador.
-- 
-- Asume que el ID de país Ecuador (PAI_ID) es 1
-- 
-- Tipos de identificación incluidos:
--   1. Cédula de Identidad (Cédula)
--   2. Pasaporte
--   3. RUC (Registro Único de Contribuyente)
-- 
-- Fecha de creación: 2024
-- Schema: ADMINISTRACION
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
    PRINT 'Iniciando inserción de Tipos de Identificación para Ecuador'
    PRINT '================================================================='
    PRINT ''

    -- Verificar que Ecuador existe con PAI_ID = 1
    IF NOT EXISTS (SELECT * FROM [ADMINISTRACION].[PAIS] WHERE [PAI_ID] = 1)
    BEGIN
        RAISERROR('ERROR: No se encontró el país con PAI_ID = 1. Verifique que Ecuador esté registrado.', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END

    DECLARE @PaisNombre VARCHAR(100)
    SELECT @PaisNombre = [PAI_NOMBRE] FROM [ADMINISTRACION].[PAIS] WHERE [PAI_ID] = 1
    PRINT 'País encontrado: ' + @PaisNombre
    PRINT 'PAI_ID: 1'
    PRINT ''

    -- =================================================================
    -- TIPO 1: CÉDULA DE IDENTIDAD
    -- =================================================================
    PRINT 'Insertando Tipo de Identificación: Cédula de Identidad...'
    
    IF NOT EXISTS (SELECT * FROM [ADMINISTRACION].[TIPO_IDENTIFICACION] 
                   WHERE [TID_CODIGO] = 'CEDULA' AND [PAI_ID] = 1)
    BEGIN
        INSERT INTO [ADMINISTRACION].[TIPO_IDENTIFICACION] 
        ([TID_CODIGO], [TID_NOMBRE], [PAI_ID], [TID_ESTADO])
        VALUES 
        ('CEDULA', 'Cédula de Identidad', 1, 1)
        
        PRINT '  ✓ Cédula de Identidad insertada correctamente'
        PRINT '    Código: CEDULA'
        PRINT '    Nombre: Cédula de Identidad'
    END
    ELSE
    BEGIN
        PRINT '  - Cédula de Identidad ya existe (código: CEDULA)'
        
        -- Actualizar si existe pero está inactiva
        UPDATE [ADMINISTRACION].[TIPO_IDENTIFICACION]
        SET [TID_ESTADO] = 1,
            [TID_NOMBRE] = 'Cédula de Identidad'
        WHERE [TID_CODIGO] = 'CEDULA' AND [PAI_ID] = 1
        
        PRINT '    Estado actualizado a activo'
    END
    PRINT ''

    -- =================================================================
    -- TIPO 2: PASAPORTE
    -- =================================================================
    PRINT 'Insertando Tipo de Identificación: Pasaporte...'
    
    IF NOT EXISTS (SELECT * FROM [ADMINISTRACION].[TIPO_IDENTIFICACION] 
                   WHERE [TID_CODIGO] = 'PASAPORTE' AND [PAI_ID] = 1)
    BEGIN
        INSERT INTO [ADMINISTRACION].[TIPO_IDENTIFICACION] 
        ([TID_CODIGO], [TID_NOMBRE], [PAI_ID], [TID_ESTADO])
        VALUES 
        ('PASAPORTE', 'Pasaporte', 1, 1)
        
        PRINT '  ✓ Pasaporte insertado correctamente'
        PRINT '    Código: PASAPORTE'
        PRINT '    Nombre: Pasaporte'
    END
    ELSE
    BEGIN
        PRINT '  - Pasaporte ya existe (código: PASAPORTE)'
        
        -- Actualizar si existe pero está inactiva
        UPDATE [ADMINISTRACION].[TIPO_IDENTIFICACION]
        SET [TID_ESTADO] = 1,
            [TID_NOMBRE] = 'Pasaporte'
        WHERE [TID_CODIGO] = 'PASAPORTE' AND [PAI_ID] = 1
        
        PRINT '    Estado actualizado a activo'
    END
    PRINT ''

    -- =================================================================
    -- TIPO 3: RUC (REGISTRO ÚNICO DE CONTRIBUYENTE)
    -- =================================================================
    PRINT 'Insertando Tipo de Identificación: RUC...'
    
    IF NOT EXISTS (SELECT * FROM [ADMINISTRACION].[TIPO_IDENTIFICACION] 
                   WHERE [TID_CODIGO] = 'RUC' AND [PAI_ID] = 1)
    BEGIN
        INSERT INTO [ADMINISTRACION].[TIPO_IDENTIFICACION] 
        ([TID_CODIGO], [TID_NOMBRE], [PAI_ID], [TID_ESTADO])
        VALUES 
        ('RUC', 'Registro Único de Contribuyente', 1, 1)
        
        PRINT '  ✓ RUC insertado correctamente'
        PRINT '    Código: RUC'
        PRINT '    Nombre: Registro Único de Contribuyente'
    END
    ELSE
    BEGIN
        PRINT '  - RUC ya existe (código: RUC)'
        
        -- Actualizar si existe pero está inactiva
        UPDATE [ADMINISTRACION].[TIPO_IDENTIFICACION]
        SET [TID_ESTADO] = 1,
            [TID_NOMBRE] = 'Registro Único de Contribuyente'
        WHERE [TID_CODIGO] = 'RUC' AND [PAI_ID] = 1
        
        PRINT '    Estado actualizado a activo'
    END
    PRINT ''

    -- =================================================================
    -- VERIFICACIÓN FINAL
    -- =================================================================
    PRINT 'Verificando tipos de identificación insertados para Ecuador...'
    PRINT ''

    DECLARE @TotalTipos INT
    SELECT @TotalTipos = COUNT(*) 
    FROM [ADMINISTRACION].[TIPO_IDENTIFICACION] 
    WHERE [PAI_ID] = 1 AND [TID_ESTADO] = 1

    PRINT 'Total de tipos de identificación activos para Ecuador: ' + CAST(@TotalTipos AS VARCHAR(2))
    PRINT ''

    -- Mostrar resumen de tipos insertados
    PRINT '================================================================='
    PRINT 'Resumen de Tipos de Identificación para Ecuador:'
    PRINT '================================================================='
    PRINT ''

    SELECT 
        [TID_ID] AS 'ID',
        [TID_CODIGO] AS 'Código',
        [TID_NOMBRE] AS 'Nombre',
        CASE [TID_ESTADO] 
            WHEN 1 THEN 'Activo' 
            ELSE 'Inactivo' 
        END AS 'Estado'
    FROM [ADMINISTRACION].[TIPO_IDENTIFICACION]
    WHERE [PAI_ID] = 1
    ORDER BY [TID_CODIGO]

    PRINT ''
    PRINT '================================================================='
    PRINT 'Script completado exitosamente!'
    PRINT '================================================================='
    PRINT ''
    PRINT 'Tipos de identificación disponibles para Ecuador:'
    PRINT '  1. Cédula de Identidad (CEDULA)'
    PRINT '  2. Pasaporte (PASAPORTE)'
    PRINT '  3. Registro Único de Contribuyente (RUC)'
    PRINT ''
    
    COMMIT TRANSACTION
    PRINT 'Transacción confirmada.'

END TRY
BEGIN CATCH
    PRINT ''
    PRINT '================================================================='
    PRINT 'ERROR durante la inserción de tipos de identificación:'
    PRINT '================================================================='
    PRINT 'Mensaje: ' + ERROR_MESSAGE()
    PRINT 'Número: ' + CAST(ERROR_NUMBER() AS VARCHAR(10))
    PRINT 'Línea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
    PRINT ''
    PRINT 'Revirtiendo cambios...'
    
    ROLLBACK TRANSACTION
    PRINT 'Transacción revertida. No se realizaron cambios.'
    
    RAISERROR('La inserción de tipos de identificación falló. Revise los errores anteriores.', 16, 1)
END CATCH
GO

