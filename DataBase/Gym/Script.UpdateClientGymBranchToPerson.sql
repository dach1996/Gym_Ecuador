-- =================================================================
-- Script de actualización de tabla CLIENTE_SUCURSAL_GIMNASIO
-- Este script actualiza la tabla para usar PNA_ID (Persona) en lugar de USR_ID (Usuario)
-- 
-- Cambios realizados:
--   - Cambia la foreign key de USR_ID a PNA_ID
--   - Actualiza la columna de USR_ID a PNA_ID
--   - Actualiza los índices relacionados
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
GO

BEGIN TRY
    PRINT '================================================================='
    PRINT 'Iniciando actualización de tabla CLIENTE_SUCURSAL_GIMNASIO'
    PRINT 'Cambio de USR_ID a PNA_ID'
    PRINT '================================================================='
    PRINT ''

    -- Verificar si la tabla existe
    IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[CLIENTE_SUCURSAL_GIMNASIO]') AND type in (N'U'))
    BEGIN
        PRINT 'La tabla CORE.CLIENTE_SUCURSAL_GIMNASIO existe.'
        PRINT ''

        -- Verificar si tiene la columna USR_ID (estructura antigua)
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.CLIENTE_SUCURSAL_GIMNASIO') AND name = 'USR_ID')
        BEGIN
            PRINT 'Paso 1: Migrando de USR_ID a PNA_ID...'
            PRINT ''

            -- Verificar si ya existe PNA_ID
            IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.CLIENTE_SUCURSAL_GIMNASIO') AND name = 'PNA_ID')
            BEGIN
                -- Agregar la nueva columna PNA_ID
                ALTER TABLE [CORE].[CLIENTE_SUCURSAL_GIMNASIO]
                ADD [PNA_ID] INT NULL;
                PRINT '  ✓ Columna PNA_ID agregada temporalmente.'
            END
            GO

            -- Migrar datos de USR_ID a PNA_ID si es posible
            -- Nota: Esto requiere que exista una relación entre USUARIO y PERSONA
            -- Si no existe esta relación, se debe hacer manualmente o establecer valores NULL
            PRINT 'Paso 2: Migrando datos de USR_ID a PNA_ID...'
            PRINT '  Nota: Si existe relación USUARIO->PERSONA, se migrarán los datos.'
            PRINT '  De lo contrario, se debe establecer PNA_ID manualmente.'
            
            -- Intentar migrar datos si existe la relación
            IF EXISTS (SELECT * FROM sys.tables WHERE name = 'USUARIO' AND schema_id = SCHEMA_ID('AUTENTICACION'))
                AND EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('AUTENTICACION.USUARIO') AND name = 'PNA_ID')
            BEGIN
                UPDATE CSG
                SET CSG.[PNA_ID] = U.[PNA_ID]
                FROM [CORE].[CLIENTE_SUCURSAL_GIMNASIO] CSG
                INNER JOIN [AUTENTICACION].[USUARIO] U ON CSG.[USR_ID] = U.[USR_ID]
                WHERE CSG.[PNA_ID] IS NULL;
                
                PRINT '  ✓ Datos migrados de USR_ID a PNA_ID.'
            END
            ELSE
            BEGIN
                PRINT '  ⚠ No se encontró relación USUARIO->PERSONA. PNA_ID debe establecerse manualmente.'
            END
            GO

            -- Eliminar índices que dependen de USR_ID
            PRINT ''
            PRINT 'Paso 3: Eliminando índices antiguos...'
            
            IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CLIENTE_SUCURSAL_GIMNASIO_USUARIO_SUCURSAL' AND object_id = OBJECT_ID('CORE.CLIENTE_SUCURSAL_GIMNASIO'))
            BEGIN
                DROP INDEX [IX_CLIENTE_SUCURSAL_GIMNASIO_USUARIO_SUCURSAL] ON [CORE].[CLIENTE_SUCURSAL_GIMNASIO];
                PRINT '  ✓ Índice IX_CLIENTE_SUCURSAL_GIMNASIO_USUARIO_SUCURSAL eliminado.'
            END
            GO

            -- Eliminar foreign key antigua
            PRINT ''
            PRINT 'Paso 4: Eliminando foreign key antigua...'
            
            IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CLIENTE_SUCURSAL_GIMNASIO_USUARIO')
            BEGIN
                ALTER TABLE [CORE].[CLIENTE_SUCURSAL_GIMNASIO]
                DROP CONSTRAINT [FK_CLIENTE_SUCURSAL_GIMNASIO_USUARIO];
                PRINT '  ✓ Foreign key FK_CLIENTE_SUCURSAL_GIMNASIO_USUARIO eliminada.'
            END
            GO

            -- Hacer PNA_ID NOT NULL si todos los registros tienen valor
            PRINT ''
            PRINT 'Paso 5: Estableciendo PNA_ID como NOT NULL...'
            
            IF EXISTS (SELECT * FROM [CORE].[CLIENTE_SUCURSAL_GIMNASIO] WHERE [PNA_ID] IS NULL)
            BEGIN
                PRINT '  ⚠ Advertencia: Existen registros con PNA_ID NULL. No se puede establecer NOT NULL.'
                PRINT '  Por favor, actualice los registros manualmente antes de continuar.'
            END
            ELSE
            BEGIN
                ALTER TABLE [CORE].[CLIENTE_SUCURSAL_GIMNASIO]
                ALTER COLUMN [PNA_ID] INT NOT NULL;
                PRINT '  ✓ Columna PNA_ID establecida como NOT NULL.'
            END
            GO

            -- Eliminar columna USR_ID
            PRINT ''
            PRINT 'Paso 6: Eliminando columna USR_ID...'
            
            ALTER TABLE [CORE].[CLIENTE_SUCURSAL_GIMNASIO]
            DROP COLUMN [USR_ID];
            PRINT '  ✓ Columna USR_ID eliminada.'
            GO

            -- Crear foreign key nueva a PERSONA
            PRINT ''
            PRINT 'Paso 7: Creando foreign key a PERSONA...'
            
            IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CLIENTE_SUCURSAL_GIMNASIO_PERSONA')
            BEGIN
                ALTER TABLE [CORE].[CLIENTE_SUCURSAL_GIMNASIO]
                ADD CONSTRAINT [FK_CLIENTE_SUCURSAL_GIMNASIO_PERSONA] 
                FOREIGN KEY ([PNA_ID]) 
                REFERENCES [AUTENTICACION].[PERSONA]([PNA_ID]);
                PRINT '  ✓ Foreign key FK_CLIENTE_SUCURSAL_GIMNASIO_PERSONA creada.'
            END
            ELSE
            BEGIN
                PRINT '  - Foreign key FK_CLIENTE_SUCURSAL_GIMNASIO_PERSONA ya existe.'
            END
            GO

            -- Crear índice único nuevo con PNA_ID
            PRINT ''
            PRINT 'Paso 8: Creando índice único con PNA_ID...'
            
            IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CLIENTE_SUCURSAL_GIMNASIO_PERSONA_SUCURSAL' AND object_id = OBJECT_ID('CORE.CLIENTE_SUCURSAL_GIMNASIO'))
            BEGIN
                CREATE UNIQUE NONCLUSTERED INDEX [IX_CLIENTE_SUCURSAL_GIMNASIO_PERSONA_SUCURSAL]
                ON [CORE].[CLIENTE_SUCURSAL_GIMNASIO] ([PNA_ID], [SGY_ID])
                WHERE [CSG_ESTADO] = 1;
                PRINT '  ✓ Índice único IX_CLIENTE_SUCURSAL_GIMNASIO_PERSONA_SUCURSAL creado.'
            END
            ELSE
            BEGIN
                PRINT '  - Índice IX_CLIENTE_SUCURSAL_GIMNASIO_PERSONA_SUCURSAL ya existe.'
            END
            GO

            PRINT ''
            PRINT '  ✓ Migración completada exitosamente.'
        END
        ELSE IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.CLIENTE_SUCURSAL_GIMNASIO') AND name = 'PNA_ID')
        BEGIN
            PRINT 'La tabla ya tiene la columna PNA_ID. Verificando estructura...'
            PRINT ''

            -- Verificar que PNA_ID sea NOT NULL
            IF EXISTS (SELECT * FROM sys.columns 
                      WHERE object_id = OBJECT_ID('CORE.CLIENTE_SUCURSAL_GIMNASIO') 
                      AND name = 'PNA_ID' 
                      AND is_nullable = 1)
            BEGIN
                -- Verificar si hay valores NULL
                IF NOT EXISTS (SELECT * FROM [CORE].[CLIENTE_SUCURSAL_GIMNASIO] WHERE [PNA_ID] IS NULL)
                BEGIN
                    ALTER TABLE [CORE].[CLIENTE_SUCURSAL_GIMNASIO]
                    ALTER COLUMN [PNA_ID] INT NOT NULL;
                    PRINT '  ✓ Columna PNA_ID establecida como NOT NULL.'
                END
            END

            -- Verificar foreign key
            IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CLIENTE_SUCURSAL_GIMNASIO_PERSONA')
            BEGIN
                ALTER TABLE [CORE].[CLIENTE_SUCURSAL_GIMNASIO]
                ADD CONSTRAINT [FK_CLIENTE_SUCURSAL_GIMNASIO_PERSONA] 
                FOREIGN KEY ([PNA_ID]) 
                REFERENCES [AUTENTICACION].[PERSONA]([PNA_ID]);
                PRINT '  ✓ Foreign key FK_CLIENTE_SUCURSAL_GIMNASIO_PERSONA creada.'
            END

            -- Verificar índice único
            IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CLIENTE_SUCURSAL_GIMNASIO_PERSONA_SUCURSAL' AND object_id = OBJECT_ID('CORE.CLIENTE_SUCURSAL_GIMNASIO'))
            BEGIN
                CREATE UNIQUE NONCLUSTERED INDEX [IX_CLIENTE_SUCURSAL_GIMNASIO_PERSONA_SUCURSAL]
                ON [CORE].[CLIENTE_SUCURSAL_GIMNASIO] ([PNA_ID], [SGY_ID])
                WHERE [CSG_ESTADO] = 1;
                PRINT '  ✓ Índice único creado.'
            END

            PRINT ''
            PRINT '  ✓ Estructura verificada y actualizada correctamente.'
        END
        ELSE
        BEGIN
            PRINT 'ERROR: La tabla existe pero no tiene ni USR_ID ni PNA_ID.'
            PRINT 'Por favor, verifique la estructura de la tabla manualmente.'
        END
    END
    ELSE
    BEGIN
        PRINT 'La tabla CORE.CLIENTE_SUCURSAL_GIMNASIO no existe.'
        PRINT 'Creando tabla con estructura correcta...'
        PRINT ''

        CREATE TABLE [CORE].[CLIENTE_SUCURSAL_GIMNASIO] (
            [CSG_ID] INT IDENTITY(1,1) NOT NULL,
            [CSG_GUID] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
            [PNA_ID] INT NOT NULL,
            [SGY_ID] INT NOT NULL,
            [CSG_FECHA_REGISTRO] DATETIME NOT NULL DEFAULT GETDATE(),
            [CSG_ESTADO] BIT NOT NULL DEFAULT 1,
            
            CONSTRAINT [PK_CLIENTE_SUCURSAL_GIMNASIO] PRIMARY KEY CLUSTERED ([CSG_ID] ASC),
            CONSTRAINT [FK_CLIENTE_SUCURSAL_GIMNASIO_PERSONA] FOREIGN KEY ([PNA_ID]) 
                REFERENCES [AUTENTICACION].[PERSONA]([PNA_ID]),
            CONSTRAINT [FK_CLIENTE_SUCURSAL_GIMNASIO_SUCURSAL_GIMNASIO] FOREIGN KEY ([SGY_ID]) 
                REFERENCES [CORE].[SUCURSAL_GIMNASIO]([SGY_ID])
        );
        
        PRINT '  ✓ Tabla CORE.CLIENTE_SUCURSAL_GIMNASIO creada exitosamente.'
        GO

        -- Crear índice único para evitar duplicados de persona-sucursal
        CREATE UNIQUE NONCLUSTERED INDEX [IX_CLIENTE_SUCURSAL_GIMNASIO_PERSONA_SUCURSAL]
        ON [CORE].[CLIENTE_SUCURSAL_GIMNASIO] ([PNA_ID], [SGY_ID])
        WHERE [CSG_ESTADO] = 1;
        PRINT '  ✓ Índice único creado para persona-sucursal activos.'
        GO

        -- Crear índice para GUID
        CREATE UNIQUE NONCLUSTERED INDEX [IX_CLIENTE_SUCURSAL_GIMNASIO_GUID]
        ON [CORE].[CLIENTE_SUCURSAL_GIMNASIO] ([CSG_GUID]);
        PRINT '  ✓ Índice único para GUID creado.'
        GO
    END

    COMMIT TRANSACTION
    GO

    PRINT ''
    PRINT '================================================================='
    PRINT 'Script completado exitosamente.'
    PRINT 'Tabla CORE.CLIENTE_SUCURSAL_GIMNASIO actualizada/creada:'
    PRINT '  - Columna: PNA_ID (INT NOT NULL)'
    PRINT '  - Foreign Key: FK_CLIENTE_SUCURSAL_GIMNASIO_PERSONA'
    PRINT '  - Índice único: IX_CLIENTE_SUCURSAL_GIMNASIO_PERSONA_SUCURSAL'
    PRINT '================================================================='

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();
    
    PRINT ''
    PRINT '================================================================='
    PRINT 'ERROR: ' + @ErrorMessage
    PRINT '================================================================='
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH
GO
