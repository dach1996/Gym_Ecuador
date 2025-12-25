-- =================================================================
-- Script de creación de relaciones (Foreign Keys) entre tablas
-- Este script crea todas las relaciones entre las siguientes tablas:
--   - TIPO_IDENTIFICACION -> PAIS
--   - REGION -> PAIS
--   - PROVINCIA -> REGION
--   - CIUDAD -> PROVINCIA
--   - PARROQUIA -> CIUDAD
-- 
-- Estructura jerárquica:
--   PAIS (tabla raíz)
--     ├── TIPO_IDENTIFICACION
--     └── REGION
--         └── PROVINCIA
--             └── CIUDAD
--                 └── PARROQUIA
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
    PRINT 'Iniciando creación de relaciones entre tablas'
    PRINT '================================================================='
    PRINT ''

    -- =================================================================
    -- PASO 1: ELIMINAR RELACIONES EXISTENTES (SI EXISTEN)
    -- =================================================================
    PRINT 'Paso 1: Eliminando relaciones existentes (si existen)...'
    PRINT ''

    -- Eliminar FK de PARROQUIA -> CIUDAD
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PARROQUIA_CIUDAD')
    BEGIN
        ALTER TABLE [ADMINISTRACION].[PARROQUIA] DROP CONSTRAINT [FK_PARROQUIA_CIUDAD]
        PRINT '  ✓ FK_PARROQUIA_CIUDAD eliminada'
    END
    ELSE
    BEGIN
        PRINT '  - FK_PARROQUIA_CIUDAD no existe'
    END

    -- Eliminar FK de CIUDAD -> PROVINCIA
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CIUDAD_PROVINCIA')
    BEGIN
        ALTER TABLE [ADMINISTRACION].[CIUDAD] DROP CONSTRAINT [FK_CIUDAD_PROVINCIA]
        PRINT '  ✓ FK_CIUDAD_PROVINCIA eliminada'
    END
    ELSE
    BEGIN
        PRINT '  - FK_CIUDAD_PROVINCIA no existe'
    END

    -- Eliminar FK de PROVINCIA -> REGION
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PROVINCIA_REGION')
    BEGIN
        ALTER TABLE [ADMINISTRACION].[PROVINCIA] DROP CONSTRAINT [FK_PROVINCIA_REGION]
        PRINT '  ✓ FK_PROVINCIA_REGION eliminada'
    END
    ELSE
    BEGIN
        PRINT '  - FK_PROVINCIA_REGION no existe'
    END

    -- Eliminar FK de REGION -> PAIS
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_REGION_PAIS')
    BEGIN
        ALTER TABLE [ADMINISTRACION].[REGION] DROP CONSTRAINT [FK_REGION_PAIS]
        PRINT '  ✓ FK_REGION_PAIS eliminada'
    END
    ELSE
    BEGIN
        PRINT '  - FK_REGION_PAIS no existe'
    END

    -- Eliminar FK de TIPO_IDENTIFICACION -> PAIS
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_TIPO_IDENTIFICACION_PAIS')
    BEGIN
        ALTER TABLE [ADMINISTRACION].[TIPO_IDENTIFICACION] DROP CONSTRAINT [FK_TIPO_IDENTIFICACION_PAIS]
        PRINT '  ✓ FK_TIPO_IDENTIFICACION_PAIS eliminada'
    END
    ELSE
    BEGIN
        PRINT '  - FK_TIPO_IDENTIFICACION_PAIS no existe'
    END

    PRINT ''
    PRINT 'Paso 1 completado.'
    PRINT ''

    -- =================================================================
    -- PASO 2: VERIFICAR QUE LAS TABLAS EXISTAN
    -- =================================================================
    PRINT 'Paso 2: Verificando que las tablas existan...'
    PRINT ''

    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PAIS' AND schema_id = SCHEMA_ID('ADMINISTRACION'))
    BEGIN
        RAISERROR('ERROR: La tabla ADMINISTRACION.PAIS no existe', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    PRINT '  ✓ Tabla PAIS existe'

    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'REGION' AND schema_id = SCHEMA_ID('ADMINISTRACION'))
    BEGIN
        RAISERROR('ERROR: La tabla ADMINISTRACION.REGION no existe', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    PRINT '  ✓ Tabla REGION existe'

    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PROVINCIA' AND schema_id = SCHEMA_ID('ADMINISTRACION'))
    BEGIN
        RAISERROR('ERROR: La tabla ADMINISTRACION.PROVINCIA no existe', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    PRINT '  ✓ Tabla PROVINCIA existe'

    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CIUDAD' AND schema_id = SCHEMA_ID('ADMINISTRACION'))
    BEGIN
        RAISERROR('ERROR: La tabla ADMINISTRACION.CIUDAD no existe', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    PRINT '  ✓ Tabla CIUDAD existe'

    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PARROQUIA' AND schema_id = SCHEMA_ID('ADMINISTRACION'))
    BEGIN
        RAISERROR('ERROR: La tabla ADMINISTRACION.PARROQUIA no existe', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    PRINT '  ✓ Tabla PARROQUIA existe'

    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TIPO_IDENTIFICACION' AND schema_id = SCHEMA_ID('ADMINISTRACION'))
    BEGIN
        RAISERROR('ERROR: La tabla ADMINISTRACION.TIPO_IDENTIFICACION no existe', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    PRINT '  ✓ Tabla TIPO_IDENTIFICACION existe'

    PRINT ''
    PRINT 'Paso 2 completado.'
    PRINT ''

    -- =================================================================
    -- PASO 3: CREAR RELACIONES EN ORDEN JERÁRQUICO (DE ARRIBA HACIA ABAJO)
    -- =================================================================
    PRINT 'Paso 3: Creando relaciones en orden jerárquico...'
    PRINT ''

    -- =================================================================
    -- 3.1. RELACIÓN: TIPO_IDENTIFICACION -> PAIS
    -- =================================================================
    PRINT '  3.1. Creando FK_TIPO_IDENTIFICACION_PAIS...'
    ALTER TABLE [ADMINISTRACION].[TIPO_IDENTIFICACION]
    ADD CONSTRAINT [FK_TIPO_IDENTIFICACION_PAIS] 
    FOREIGN KEY ([PAI_ID]) 
    REFERENCES [ADMINISTRACION].[PAIS] ([PAI_ID])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    PRINT '     ✓ FK_TIPO_IDENTIFICACION_PAIS creada'
    PRINT '       Descripción: Relaciona cada tipo de identificación con su país correspondiente'
    PRINT ''

    -- =================================================================
    -- 3.2. RELACIÓN: REGION -> PAIS
    -- =================================================================
    PRINT '  3.2. Creando FK_REGION_PAIS...'
    ALTER TABLE [ADMINISTRACION].[REGION]
    ADD CONSTRAINT [FK_REGION_PAIS] 
    FOREIGN KEY ([PAI_ID]) 
    REFERENCES [ADMINISTRACION].[PAIS] ([PAI_ID])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    PRINT '     ✓ FK_REGION_PAIS creada'
    PRINT '       Descripción: Relaciona cada región con su país correspondiente'
    PRINT ''

    -- =================================================================
    -- 3.3. RELACIÓN: PROVINCIA -> REGION
    -- =================================================================
    PRINT '  3.3. Creando FK_PROVINCIA_REGION...'
    ALTER TABLE [ADMINISTRACION].[PROVINCIA]
    ADD CONSTRAINT [FK_PROVINCIA_REGION] 
    FOREIGN KEY ([REG_ID]) 
    REFERENCES [ADMINISTRACION].[REGION] ([REG_ID])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    PRINT '     ✓ FK_PROVINCIA_REGION creada'
    PRINT '       Descripción: Relaciona cada provincia con su región correspondiente'
    PRINT ''

    -- =================================================================
    -- 3.4. RELACIÓN: CIUDAD -> PROVINCIA
    -- =================================================================
    PRINT '  3.4. Creando FK_CIUDAD_PROVINCIA...'
    ALTER TABLE [ADMINISTRACION].[CIUDAD]
    ADD CONSTRAINT [FK_CIUDAD_PROVINCIA] 
    FOREIGN KEY ([PRO_ID]) 
    REFERENCES [ADMINISTRACION].[PROVINCIA] ([PRO_ID])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    PRINT '     ✓ FK_CIUDAD_PROVINCIA creada'
    PRINT '       Descripción: Relaciona cada ciudad con su provincia correspondiente'
    PRINT ''

    -- =================================================================
    -- 3.5. RELACIÓN: PARROQUIA -> CIUDAD
    -- =================================================================
    PRINT '  3.5. Creando FK_PARROQUIA_CIUDAD...'
    ALTER TABLE [ADMINISTRACION].[PARROQUIA]
    ADD CONSTRAINT [FK_PARROQUIA_CIUDAD] 
    FOREIGN KEY ([CIU_ID]) 
    REFERENCES [ADMINISTRACION].[CIUDAD] ([CIU_ID])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    PRINT '     ✓ FK_PARROQUIA_CIUDAD creada'
    PRINT '       Descripción: Relaciona cada parroquia con su ciudad correspondiente'
    PRINT ''

    PRINT 'Paso 3 completado.'
    PRINT ''

    -- =================================================================
    -- PASO 4: VERIFICACIÓN FINAL DE RELACIONES
    -- =================================================================
    PRINT 'Paso 4: Verificando que todas las relaciones se crearon correctamente...'
    PRINT ''

    DECLARE @RelacionesCreadas INT = 0
    DECLARE @RelacionesEsperadas INT = 5

    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_TIPO_IDENTIFICACION_PAIS')
        SET @RelacionesCreadas = @RelacionesCreadas + 1
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_REGION_PAIS')
        SET @RelacionesCreadas = @RelacionesCreadas + 1
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PROVINCIA_REGION')
        SET @RelacionesCreadas = @RelacionesCreadas + 1
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CIUDAD_PROVINCIA')
        SET @RelacionesCreadas = @RelacionesCreadas + 1
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PARROQUIA_CIUDAD')
        SET @RelacionesCreadas = @RelacionesCreadas + 1

    PRINT '  Relaciones creadas: ' + CAST(@RelacionesCreadas AS VARCHAR(2)) + ' de ' + CAST(@RelacionesEsperadas AS VARCHAR(2))

    IF @RelacionesCreadas = @RelacionesEsperadas
    BEGIN
        PRINT '  ✓ Todas las relaciones se crearon correctamente'
    END
    ELSE
    BEGIN
        RAISERROR('ERROR: No todas las relaciones se crearon correctamente', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END

    PRINT ''
    PRINT '================================================================='
    PRINT 'Resumen de relaciones creadas:'
    PRINT '================================================================='
    PRINT ''
    PRINT '  ✓ FK_TIPO_IDENTIFICACION_PAIS'
    PRINT '    TIPO_IDENTIFICACION.PAI_ID -> PAIS.PAI_ID'
    PRINT ''
    PRINT '  ✓ FK_REGION_PAIS'
    PRINT '    REGION.PAI_ID -> PAIS.PAI_ID'
    PRINT ''
    PRINT '  ✓ FK_PROVINCIA_REGION'
    PRINT '    PROVINCIA.REG_ID -> REGION.REG_ID'
    PRINT ''
    PRINT '  ✓ FK_CIUDAD_PROVINCIA'
    PRINT '    CIUDAD.PRO_ID -> PROVINCIA.PRO_ID'
    PRINT ''
    PRINT '  ✓ FK_PARROQUIA_CIUDAD'
    PRINT '    PARROQUIA.CIU_ID -> CIUDAD.CIU_ID'
    PRINT ''
    PRINT '================================================================='
    PRINT 'Estructura jerárquica completa:'
    PRINT '================================================================='
    PRINT ''
    PRINT '  PAIS'
    PRINT '    ├── TIPO_IDENTIFICACION'
    PRINT '    └── REGION'
    PRINT '        └── PROVINCIA'
    PRINT '            └── CIUDAD'
    PRINT '                └── PARROQUIA'
    PRINT ''
    PRINT '================================================================='
    PRINT 'Script completado exitosamente!'
    PRINT '================================================================='
    PRINT ''
    
    COMMIT TRANSACTION
    PRINT 'Transacción confirmada.'

END TRY
BEGIN CATCH
    PRINT ''
    PRINT '================================================================='
    PRINT 'ERROR durante la creación de relaciones:'
    PRINT '================================================================='
    PRINT 'Mensaje: ' + ERROR_MESSAGE()
    PRINT 'Número: ' + CAST(ERROR_NUMBER() AS VARCHAR(10))
    PRINT 'Línea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
    PRINT ''
    PRINT 'Revirtiendo cambios...'
    
    ROLLBACK TRANSACTION
    PRINT 'Transacción revertida. No se realizaron cambios.'
    
    RAISERROR('La creación de relaciones falló. Revise los errores anteriores.', 16, 1)
END CATCH
GO

