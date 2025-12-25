-- =================================================================
-- Script de migración: Cambio de tipo de dato INT a TINYINT
-- Este script cambia los tipos de dato de las columnas ID de:
--   1. PAIS.PAI_ID: INT -> TINYINT
--   2. PROVINCIA.PRO_ID: INT -> TINYINT
--   3. CIUDAD.CIU_ID: INT -> TINYINT
-- 
-- También actualiza todas las claves foráneas relacionadas:
--   - REGION.PAI_ID (FK a PAIS)
--   - TIPO_IDENTIFICACION.PAI_ID (FK a PAIS)
--   - CIUDAD.PRO_ID (FK a PROVINCIA)
--   - PARROQUIA.CIU_ID (FK a CIUDAD)
-- 
-- IMPORTANTE: 
--   - TINYINT permite valores de 0 a 255
--   - Verificar que los datos existentes no excedan este rango
--   - Este script debe ejecutarse en una transacción
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
    PRINT 'Iniciando migración de tipos de dato INT a TINYINT'
    PRINT '================================================================='
    PRINT ''

    -- =================================================================
    -- VERIFICACIÓN DE DATOS EXISTENTES
    -- =================================================================
    PRINT 'Verificando que los datos existentes no excedan el rango de TINYINT (0-255)...'
    
    DECLARE @MaxPaisId INT
    DECLARE @MaxProvinciaId INT
    DECLARE @MaxCiudadId INT
    
    SELECT @MaxPaisId = MAX(PAI_ID) FROM [ADMINISTRACION].[PAIS]
    SELECT @MaxProvinciaId = MAX(PRO_ID) FROM [ADMINISTRACION].[PROVINCIA]
    SELECT @MaxCiudadId = MAX(CIU_ID) FROM [ADMINISTRACION].[CIUDAD]
    
    IF @MaxPaisId > 255
    BEGIN
        RAISERROR('ERROR: El valor máximo de PAI_ID (%d) excede el rango de TINYINT (255)', 16, 1, @MaxPaisId)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    IF @MaxProvinciaId > 255
    BEGIN
        RAISERROR('ERROR: El valor máximo de PRO_ID (%d) excede el rango de TINYINT (255)', 16, 1, @MaxProvinciaId)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    IF @MaxCiudadId > 255
    BEGIN
        RAISERROR('ERROR: El valor máximo de CIU_ID (%d) excede el rango de TINYINT (255)', 16, 1, @MaxCiudadId)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    PRINT 'Verificación exitosa. Todos los valores están dentro del rango permitido.'
    PRINT ''

    -- =================================================================
    -- PASO 1: ELIMINAR CLAVES FORÁNEAS QUE REFERENCIAN CIUDAD
    -- =================================================================
    PRINT 'Paso 1: Eliminando claves foráneas que referencian CIUDAD...'
    
    -- Eliminar FK de PARROQUIA -> CIUDAD
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PARROQUIA_CIUDAD')
    BEGIN
        ALTER TABLE [ADMINISTRACION].[PARROQUIA] DROP CONSTRAINT [FK_PARROQUIA_CIUDAD]
        PRINT '  - FK_PARROQUIA_CIUDAD eliminada'
    END

    -- =================================================================
    -- PASO 2: CAMBIAR CIUDAD.CIU_ID Y CIUDAD.PRO_ID A TINYINT
    -- =================================================================
    PRINT 'Paso 2: Cambiando CIUDAD.CIU_ID y CIUDAD.PRO_ID a TINYINT...'
    
    -- Cambiar CIU_ID a TINYINT
    ALTER TABLE [ADMINISTRACION].[CIUDAD] ALTER COLUMN [CIU_ID] TINYINT NOT NULL
    PRINT '  - CIUDAD.CIU_ID cambiado a TINYINT'
    
    -- Cambiar PRO_ID a TINYINT
    ALTER TABLE [ADMINISTRACION].[CIUDAD] ALTER COLUMN [PRO_ID] TINYINT NOT NULL
    PRINT '  - CIUDAD.PRO_ID cambiado a TINYINT'

    -- =================================================================
    -- PASO 3: RECREAR FK DE PARROQUIA -> CIUDAD
    -- =================================================================
    PRINT 'Paso 3: Recreando FK_PARROQUIA_CIUDAD...'
    
    ALTER TABLE [ADMINISTRACION].[PARROQUIA] 
    ADD CONSTRAINT [FK_PARROQUIA_CIUDAD] FOREIGN KEY ([CIU_ID]) 
    REFERENCES [ADMINISTRACION].[CIUDAD] ([CIU_ID])
    PRINT '  - FK_PARROQUIA_CIUDAD recreada'

    -- =================================================================
    -- PASO 4: ELIMINAR CLAVES FORÁNEAS QUE REFERENCIAN PROVINCIA
    -- =================================================================
    PRINT 'Paso 4: Eliminando claves foráneas que referencian PROVINCIA...'
    
    -- Eliminar FK de CIUDAD -> PROVINCIA
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CIUDAD_PROVINCIA')
    BEGIN
        ALTER TABLE [ADMINISTRACION].[CIUDAD] DROP CONSTRAINT [FK_CIUDAD_PROVINCIA]
        PRINT '  - FK_CIUDAD_PROVINCIA eliminada'
    END

    -- =================================================================
    -- PASO 5: CAMBIAR PROVINCIA.PRO_ID A TINYINT
    -- =================================================================
    PRINT 'Paso 5: Cambiando PROVINCIA.PRO_ID a TINYINT...'
    
    ALTER TABLE [ADMINISTRACION].[PROVINCIA] ALTER COLUMN [PRO_ID] TINYINT NOT NULL
    PRINT '  - PROVINCIA.PRO_ID cambiado a TINYINT'

    -- =================================================================
    -- PASO 6: RECREAR FK DE CIUDAD -> PROVINCIA
    -- =================================================================
    PRINT 'Paso 6: Recreando FK_CIUDAD_PROVINCIA...'
    
    ALTER TABLE [ADMINISTRACION].[CIUDAD] 
    ADD CONSTRAINT [FK_CIUDAD_PROVINCIA] FOREIGN KEY ([PRO_ID]) 
    REFERENCES [ADMINISTRACION].[PROVINCIA] ([PRO_ID])
    PRINT '  - FK_CIUDAD_PROVINCIA recreada'

    -- =================================================================
    -- PASO 7: ELIMINAR CLAVES FORÁNEAS QUE REFERENCIAN PAIS
    -- =================================================================
    PRINT 'Paso 7: Eliminando claves foráneas que referencian PAIS...'
    
    -- Eliminar FK de REGION -> PAIS
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_REGION_PAIS')
    BEGIN
        ALTER TABLE [ADMINISTRACION].[REGION] DROP CONSTRAINT [FK_REGION_PAIS]
        PRINT '  - FK_REGION_PAIS eliminada'
    END
    
    -- Eliminar FK de TIPO_IDENTIFICACION -> PAIS
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_TIPO_IDENTIFICACION_PAIS')
    BEGIN
        ALTER TABLE [ADMINISTRACION].[TIPO_IDENTIFICACION] DROP CONSTRAINT [FK_TIPO_IDENTIFICACION_PAIS]
        PRINT '  - FK_TIPO_IDENTIFICACION_PAIS eliminada'
    END

    -- =================================================================
    -- PASO 8: CAMBIAR PAIS.PAI_ID Y COLUMNAS FK RELACIONADAS A TINYINT
    -- =================================================================
    PRINT 'Paso 8: Cambiando PAIS.PAI_ID y columnas FK relacionadas a TINYINT...'
    
    -- Cambiar REGION.PAI_ID a TINYINT
    ALTER TABLE [ADMINISTRACION].[REGION] ALTER COLUMN [PAI_ID] TINYINT NOT NULL
    PRINT '  - REGION.PAI_ID cambiado a TINYINT'
    
    -- Cambiar TIPO_IDENTIFICACION.PAI_ID a TINYINT
    ALTER TABLE [ADMINISTRACION].[TIPO_IDENTIFICACION] ALTER COLUMN [PAI_ID] TINYINT NOT NULL
    PRINT '  - TIPO_IDENTIFICACION.PAI_ID cambiado a TINYINT'
    
    -- Cambiar PAIS.PAI_ID a TINYINT (debe ser el último)
    ALTER TABLE [ADMINISTRACION].[PAIS] ALTER COLUMN [PAI_ID] TINYINT NOT NULL
    PRINT '  - PAIS.PAI_ID cambiado a TINYINT'

    -- =================================================================
    -- PASO 9: RECREAR FK DE REGION Y TIPO_IDENTIFICACION -> PAIS
    -- =================================================================
    PRINT 'Paso 9: Recreando claves foráneas hacia PAIS...'
    
    -- Recrear FK de REGION -> PAIS
    ALTER TABLE [ADMINISTRACION].[REGION] 
    ADD CONSTRAINT [FK_REGION_PAIS] FOREIGN KEY ([PAI_ID]) 
    REFERENCES [ADMINISTRACION].[PAIS] ([PAI_ID])
    PRINT '  - FK_REGION_PAIS recreada'
    
    -- Recrear FK de TIPO_IDENTIFICACION -> PAIS
    ALTER TABLE [ADMINISTRACION].[TIPO_IDENTIFICACION] 
    ADD CONSTRAINT [FK_TIPO_IDENTIFICACION_PAIS] FOREIGN KEY ([PAI_ID]) 
    REFERENCES [ADMINISTRACION].[PAIS] ([PAI_ID])
    PRINT '  - FK_TIPO_IDENTIFICACION_PAIS recreada'

    -- =================================================================
    -- PASO 10: ACTUALIZAR IDENTITY SEED SI ES NECESARIO
    -- =================================================================
    PRINT 'Paso 10: Verificando y actualizando IDENTITY seed...'
    
    -- Verificar y resetear IDENTITY para PAIS si es necesario
    DECLARE @PaisMaxId TINYINT
    SELECT @PaisMaxId = ISNULL(MAX(PAI_ID), 0) FROM [ADMINISTRACION].[PAIS]
    IF @PaisMaxId > 0
    BEGIN
        DBCC CHECKIDENT('[ADMINISTRACION].[PAIS]', RESEED, @PaisMaxId)
        PRINT '  - IDENTITY de PAIS actualizado'
    END
    
    -- Verificar y resetear IDENTITY para PROVINCIA si es necesario
    DECLARE @ProvinciaMaxId TINYINT
    SELECT @ProvinciaMaxId = ISNULL(MAX(PRO_ID), 0) FROM [ADMINISTRACION].[PROVINCIA]
    IF @ProvinciaMaxId > 0
    BEGIN
        DBCC CHECKIDENT('[ADMINISTRACION].[PROVINCIA]', RESEED, @ProvinciaMaxId)
        PRINT '  - IDENTITY de PROVINCIA actualizado'
    END
    
    -- Verificar y resetear IDENTITY para CIUDAD si es necesario
    DECLARE @CiudadMaxId TINYINT
    SELECT @CiudadMaxId = ISNULL(MAX(CIU_ID), 0) FROM [ADMINISTRACION].[CIUDAD]
    IF @CiudadMaxId > 0
    BEGIN
        DBCC CHECKIDENT('[ADMINISTRACION].[CIUDAD]', RESEED, @CiudadMaxId)
        PRINT '  - IDENTITY de CIUDAD actualizado'
    END

    -- =================================================================
    -- VERIFICACIÓN FINAL
    -- =================================================================
    PRINT ''
    PRINT 'Verificando tipos de dato finales...'
    
    DECLARE @PaisDataType VARCHAR(50)
    DECLARE @ProvinciaDataType VARCHAR(50)
    DECLARE @CiudadDataType VARCHAR(50)
    
    SELECT @PaisDataType = TYPE_NAME(system_type_id) 
    FROM sys.columns 
    WHERE object_id = OBJECT_ID('ADMINISTRACION.PAIS') AND name = 'PAI_ID'
    
    SELECT @ProvinciaDataType = TYPE_NAME(system_type_id) 
    FROM sys.columns 
    WHERE object_id = OBJECT_ID('ADMINISTRACION.PROVINCIA') AND name = 'PRO_ID'
    
    SELECT @CiudadDataType = TYPE_NAME(system_type_id) 
    FROM sys.columns 
    WHERE object_id = OBJECT_ID('ADMINISTRACION.CIUDAD') AND name = 'CIU_ID'
    
    PRINT '  - PAIS.PAI_ID: ' + @PaisDataType
    PRINT '  - PROVINCIA.PRO_ID: ' + @ProvinciaDataType
    PRINT '  - CIUDAD.CIU_ID: ' + @CiudadDataType
    
    IF @PaisDataType != 'tinyint' OR @ProvinciaDataType != 'tinyint' OR @CiudadDataType != 'tinyint'
    BEGIN
        RAISERROR('ERROR: Algunos tipos de dato no se cambiaron correctamente', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    PRINT ''
    PRINT '================================================================='
    PRINT 'Migración completada exitosamente!'
    PRINT '================================================================='
    PRINT ''
    PRINT 'Resumen de cambios:'
    PRINT '  ✓ PAIS.PAI_ID: INT -> TINYINT'
    PRINT '  ✓ PROVINCIA.PRO_ID: INT -> TINYINT'
    PRINT '  ✓ CIUDAD.CIU_ID: INT -> TINYINT'
    PRINT '  ✓ Todas las claves foráneas relacionadas actualizadas'
    PRINT ''
    
    COMMIT TRANSACTION
    PRINT 'Transacción confirmada.'
    
END TRY
BEGIN CATCH
    PRINT ''
    PRINT '================================================================='
    PRINT 'ERROR durante la migración:'
    PRINT '================================================================='
    PRINT 'Mensaje: ' + ERROR_MESSAGE()
    PRINT 'Número: ' + CAST(ERROR_NUMBER() AS VARCHAR(10))
    PRINT 'Línea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
    PRINT ''
    PRINT 'Revirtiendo cambios...'
    
    ROLLBACK TRANSACTION
    PRINT 'Transacción revertida. No se realizaron cambios.'
    
    RAISERROR('La migración falló. Revise los errores anteriores.', 16, 1)
END CATCH
GO

