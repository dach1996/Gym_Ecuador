-- =================================================================
-- Script de creación de tablas CLIENTE_SUCURSAL_GIMNASIO y MEMBRECIAS
-- Este script crea las siguientes tablas:
--   1. CLIENTE_SUCURSAL_GIMNASIO - Relaciona usuarios con sucursales de gimnasio
--   2. MEMBRECIAS - Relaciona clientes con planes de suscripción
--   3. Renombra SUCURSAL_PLAN_SUSCRIPCION a PLAN_SUCURSAL (si existe)
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
    PRINT 'Iniciando creación de tablas CLIENTE_SUCURSAL_GIMNASIO y MEMBRECIAS'
    PRINT '================================================================='
    PRINT ''

    -- =================================================================
    -- PASO 1: RENOMBRAR TABLA SUCURSAL_PLAN_SUSCRIPCION A PLAN_SUCURSAL
    -- =================================================================
    PRINT 'Paso 1: Renombrando tabla SUCURSAL_PLAN_SUSCRIPCION a PLAN_SUCURSAL...'
    PRINT ''

    IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[SUCURSAL_PLAN_SUSCRIPCION]') AND type in (N'U'))
    BEGIN
        -- Renombrar la tabla
        EXEC sp_rename '[CORE].[SUCURSAL_PLAN_SUSCRIPCION]', 'PLAN_SUCURSAL'
        PRINT '  ✓ Tabla SUCURSAL_PLAN_SUSCRIPCION renombrada a PLAN_SUCURSAL'
        
        -- Renombrar las columnas si es necesario (SPS_* a PLS_*)
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.PLAN_SUCURSAL') AND name = 'SPS_ID')
        BEGIN
            EXEC sp_rename '[CORE].[PLAN_SUCURSAL].[SPS_ID]', 'PLS_ID', 'COLUMN'
            PRINT '  ✓ Columna SPS_ID renombrada a PLS_ID'
        END
        
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.PLAN_SUCURSAL') AND name = 'SPS_GUID')
        BEGIN
            EXEC sp_rename '[CORE].[PLAN_SUCURSAL].[SPS_GUID]', 'PLS_GUID', 'COLUMN'
            PRINT '  ✓ Columna SPS_GUID renombrada a PLS_GUID'
        END
        
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.PLAN_SUCURSAL') AND name = 'SPS_NOMBRE')
        BEGIN
            EXEC sp_rename '[CORE].[PLAN_SUCURSAL].[SPS_NOMBRE]', 'PLS_NOMBRE', 'COLUMN'
            PRINT '  ✓ Columna SPS_NOMBRE renombrada a PLS_NOMBRE'
        END
        
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.PLAN_SUCURSAL') AND name = 'SPS_CODIGO')
        BEGIN
            EXEC sp_rename '[CORE].[PLAN_SUCURSAL].[SPS_CODIGO]', 'PLS_CODIGO', 'COLUMN'
            PRINT '  ✓ Columna SPS_CODIGO renombrada a PLS_CODIGO'
        END
        
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.PLAN_SUCURSAL') AND name = 'SPS_DESCRIPCION')
        BEGIN
            EXEC sp_rename '[CORE].[PLAN_SUCURSAL].[SPS_DESCRIPCION]', 'PLS_DESCRIPCION', 'COLUMN'
            PRINT '  ✓ Columna SPS_DESCRIPCION renombrada a PLS_DESCRIPCION'
        END
        
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.PLAN_SUCURSAL') AND name = 'SPS_PRECIO')
        BEGIN
            EXEC sp_rename '[CORE].[PLAN_SUCURSAL].[SPS_PRECIO]', 'PLS_PRECIO', 'COLUMN'
            PRINT '  ✓ Columna SPS_PRECIO renombrada a PLS_PRECIO'
        END
        
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.PLAN_SUCURSAL') AND name = 'SPS_DURACION_DIAS')
        BEGIN
            EXEC sp_rename '[CORE].[PLAN_SUCURSAL].[SPS_DURACION_DIAS]', 'PLS_DURACION_DIAS', 'COLUMN'
            PRINT '  ✓ Columna SPS_DURACION_DIAS renombrada a PLS_DURACION_DIAS'
        END
        
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.PLAN_SUCURSAL') AND name = 'SPS_PRECIO_INSCRIPCION')
        BEGIN
            EXEC sp_rename '[CORE].[PLAN_SUCURSAL].[SPS_PRECIO_INSCRIPCION]', 'PLS_PRECIO_INSCRIPCION', 'COLUMN'
            PRINT '  ✓ Columna SPS_PRECIO_INSCRIPCION renombrada a PLS_PRECIO_INSCRIPCION'
        END
        
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.PLAN_SUCURSAL') AND name = 'SPS_ESTADO')
        BEGIN
            EXEC sp_rename '[CORE].[PLAN_SUCURSAL].[SPS_ESTADO]', 'PLS_ESTADO', 'COLUMN'
            PRINT '  ✓ Columna SPS_ESTADO renombrada a PLS_ESTADO'
        END
        
        -- Actualizar la foreign key si existe
        IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SUCURSAL_PLAN_SUSCRIPCION_GIMNASIO')
        BEGIN
            EXEC sp_rename '[CORE].[FK_SUCURSAL_PLAN_SUSCRIPCION_GIMNASIO]', 'FK_PLAN_SUCURSAL_SUCURSAL_GIMNASIO'
            PRINT '  ✓ Foreign key renombrada'
        END
        
        -- Actualizar la primary key si es necesario
        IF EXISTS (SELECT * FROM sys.key_constraints WHERE name = 'PK_SUCURSAL_PLAN_SUSCRIPCION')
        BEGIN
            EXEC sp_rename '[CORE].[PK_SUCURSAL_PLAN_SUSCRIPCION]', 'PK_PLAN_SUCURSAL'
            PRINT '  ✓ Primary key renombrada'
        END
    END
    ELSE IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[PLAN_SUCURSAL]') AND type in (N'U'))
    BEGIN
        -- Crear la tabla si no existe
        PRINT 'Creando tabla CORE.PLAN_SUCURSAL...'
        
        CREATE TABLE [CORE].[PLAN_SUCURSAL] (
            [PLS_ID] INT IDENTITY(1,1) NOT NULL,
            [PLS_GUID] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
            [SGY_ID] INT NOT NULL,
            [PLS_NOMBRE] NVARCHAR(200) NOT NULL,
            [PLS_CODIGO] NVARCHAR(50) NULL,
            [PLS_DESCRIPCION] NVARCHAR(1000) NULL,
            [PLS_PRECIO] DECIMAL(18,2) NOT NULL,
            [PLS_DURACION_DIAS] INT NOT NULL,
            [PLS_PRECIO_INSCRIPCION] DECIMAL(18,2) NULL,
            [PLS_ESTADO] BIT NOT NULL DEFAULT 1,
            
            CONSTRAINT [PK_PLAN_SUCURSAL] PRIMARY KEY CLUSTERED ([PLS_ID] ASC),
            CONSTRAINT [FK_PLAN_SUCURSAL_SUCURSAL_GIMNASIO] FOREIGN KEY ([SGY_ID]) 
                REFERENCES [CORE].[SUCURSAL_GIMNASIO]([SGY_ID])
        );
        
        PRINT '  ✓ Tabla CORE.PLAN_SUCURSAL creada exitosamente.'
    END
    ELSE
    BEGIN
        PRINT '  - La tabla CORE.PLAN_SUCURSAL ya existe.'
    END
    GO

    -- =================================================================
    -- PASO 2: CREAR TABLA CLIENTE_SUCURSAL_GIMNASIO
    -- =================================================================
    PRINT ''
    PRINT 'Paso 2: Creando tabla CLIENTE_SUCURSAL_GIMNASIO...'
    PRINT ''

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[CLIENTE_SUCURSAL_GIMNASIO]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [CORE].[CLIENTE_SUCURSAL_GIMNASIO] (
            [CSG_ID] INT IDENTITY(1,1) NOT NULL,
            [CSG_GUID] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
            [USR_ID] INT NOT NULL,
            [SGY_ID] INT NOT NULL,
            [CSG_FECHA_REGISTRO] DATETIME NOT NULL DEFAULT GETDATE(),
            [CSG_ESTADO] BIT NOT NULL DEFAULT 1,
            
            CONSTRAINT [PK_CLIENTE_SUCURSAL_GIMNASIO] PRIMARY KEY CLUSTERED ([CSG_ID] ASC),
            CONSTRAINT [FK_CLIENTE_SUCURSAL_GIMNASIO_USUARIO] FOREIGN KEY ([USR_ID]) 
                REFERENCES [AUTENTICACION].[USUARIO]([USR_ID]),
            CONSTRAINT [FK_CLIENTE_SUCURSAL_GIMNASIO_SUCURSAL_GIMNASIO] FOREIGN KEY ([SGY_ID]) 
                REFERENCES [CORE].[SUCURSAL_GIMNASIO]([SGY_ID])
        );
        
        PRINT '  ✓ Tabla CORE.CLIENTE_SUCURSAL_GIMNASIO creada exitosamente.'
    END
    ELSE
    BEGIN
        PRINT '  - La tabla CORE.CLIENTE_SUCURSAL_GIMNASIO ya existe.'
    END
    GO

    -- Crear índice único para evitar duplicados de usuario-sucursal
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CLIENTE_SUCURSAL_GIMNASIO_USUARIO_SUCURSAL' AND object_id = OBJECT_ID('CORE.CLIENTE_SUCURSAL_GIMNASIO'))
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [IX_CLIENTE_SUCURSAL_GIMNASIO_USUARIO_SUCURSAL]
        ON [CORE].[CLIENTE_SUCURSAL_GIMNASIO] ([USR_ID], [SGY_ID])
        WHERE [CSG_ESTADO] = 1;
        PRINT '  ✓ Índice único creado para usuario-sucursal activos.'
    END
    GO

    -- Crear índice para GUID
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CLIENTE_SUCURSAL_GIMNASIO_GUID' AND object_id = OBJECT_ID('CORE.CLIENTE_SUCURSAL_GIMNASIO'))
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [IX_CLIENTE_SUCURSAL_GIMNASIO_GUID]
        ON [CORE].[CLIENTE_SUCURSAL_GIMNASIO] ([CSG_GUID]);
        PRINT '  ✓ Índice único para GUID creado.'
    END
    GO

    -- =================================================================
    -- PASO 3: CREAR TABLA MEMBRECIAS
    -- =================================================================
    PRINT ''
    PRINT 'Paso 3: Creando tabla MEMBRECIAS...'
    PRINT ''

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[MEMBRECIAS]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [CORE].[MEMBRECIAS] (
            [MEM_ID] INT IDENTITY(1,1) NOT NULL,
            [MEM_GUID] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
            [CSG_ID] INT NOT NULL,
            [PLS_ID] INT NOT NULL,
            [MEM_FECHA_INICIO] DATETIME NOT NULL DEFAULT GETDATE(),
            [MEM_FECHA_FIN] DATETIME NULL,
            [MEM_ESTADO] BIT NOT NULL DEFAULT 1,
            [MEM_FECHA_REGISTRO] DATETIME NOT NULL DEFAULT GETDATE(),
            
            CONSTRAINT [PK_MEMBRECIAS] PRIMARY KEY CLUSTERED ([MEM_ID] ASC),
            CONSTRAINT [FK_MEMBRECIAS_CLIENTE_SUCURSAL_GIMNASIO] FOREIGN KEY ([CSG_ID]) 
                REFERENCES [CORE].[CLIENTE_SUCURSAL_GIMNASIO]([CSG_ID]),
            CONSTRAINT [FK_MEMBRECIAS_PLAN_SUCURSAL] FOREIGN KEY ([PLS_ID]) 
                REFERENCES [CORE].[PLAN_SUCURSAL]([PLS_ID])
        );
        
        PRINT '  ✓ Tabla CORE.MEMBRECIAS creada exitosamente.'
    END
    ELSE
    BEGIN
        PRINT '  - La tabla CORE.MEMBRECIAS ya existe.'
    END
    GO

    -- Crear índice para GUID
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_MEMBRECIAS_GUID' AND object_id = OBJECT_ID('CORE.MEMBRECIAS'))
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [IX_MEMBRECIAS_GUID]
        ON [CORE].[MEMBRECIAS] ([MEM_GUID]);
        PRINT '  ✓ Índice único para GUID creado.'
    END
    GO

    -- Crear índice para cliente y estado
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_MEMBRECIAS_CLIENTE_ESTADO' AND object_id = OBJECT_ID('CORE.MEMBRECIAS'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_MEMBRECIAS_CLIENTE_ESTADO]
        ON [CORE].[MEMBRECIAS] ([CSG_ID], [MEM_ESTADO], [MEM_FECHA_INICIO]);
        PRINT '  ✓ Índice para cliente y estado creado.'
    END
    GO

    -- Crear índice para plan y estado
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_MEMBRECIAS_PLAN_ESTADO' AND object_id = OBJECT_ID('CORE.MEMBRECIAS'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_MEMBRECIAS_PLAN_ESTADO]
        ON [CORE].[MEMBRECIAS] ([PLS_ID], [MEM_ESTADO]);
        PRINT '  ✓ Índice para plan y estado creado.'
    END
    GO

    -- =================================================================
    -- ACTUALIZAR TABLA SUCURSAL_PLAN_SERVICIO SI EXISTE
    -- =================================================================
    PRINT ''
    PRINT 'Paso 4: Actualizando tabla SUCURSAL_PLAN_SERVICIO...'
    PRINT ''

    IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[SUCURSAL_PLAN_SERVICIO]') AND type in (N'U'))
    BEGIN
        -- Verificar si la foreign key existe y apunta a la tabla antigua
        IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SUCURSAL_PLAN_SERVICIO_SUCURSAL_PLAN_SUSCRIPCION')
        BEGIN
            -- Eliminar la foreign key antigua
            ALTER TABLE [CORE].[SUCURSAL_PLAN_SERVICIO] 
            DROP CONSTRAINT [FK_SUCURSAL_PLAN_SERVICIO_SUCURSAL_PLAN_SUSCRIPCION];
            PRINT '  ✓ Foreign key antigua eliminada.'
            
            -- Crear la nueva foreign key
            ALTER TABLE [CORE].[SUCURSAL_PLAN_SERVICIO]
            ADD CONSTRAINT [FK_SUCURSAL_PLAN_SERVICIO_PLAN_SUCURSAL] 
            FOREIGN KEY ([PLS_ID]) REFERENCES [CORE].[PLAN_SUCURSAL]([PLS_ID]);
            PRINT '  ✓ Nueva foreign key creada.'
        END
        ELSE IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SUCURSAL_PLAN_SERVICIO_PLAN_SUCURSAL')
        BEGIN
            -- Crear la foreign key si no existe
            ALTER TABLE [CORE].[SUCURSAL_PLAN_SERVICIO]
            ADD CONSTRAINT [FK_SUCURSAL_PLAN_SERVICIO_PLAN_SUCURSAL] 
            FOREIGN KEY ([PLS_ID]) REFERENCES [CORE].[PLAN_SUCURSAL]([PLS_ID]);
            PRINT '  ✓ Foreign key creada.'
        END
        ELSE
        BEGIN
            PRINT '  - La foreign key ya existe correctamente.'
        END
    END
    ELSE
    BEGIN
        PRINT '  - La tabla SUCURSAL_PLAN_SERVICIO no existe, se creará cuando sea necesario.'
    END
    GO

    COMMIT TRANSACTION
    GO

    PRINT ''
    PRINT '================================================================='
    PRINT 'Script completado exitosamente.'
    PRINT 'Tablas creadas/actualizadas:'
    PRINT '  1. CORE.PLAN_SUCURSAL (renombrada o creada)'
    PRINT '  2. CORE.CLIENTE_SUCURSAL_GIMNASIO'
    PRINT '  3. CORE.MEMBRECIAS'
    PRINT '================================================================='

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();
    
    PRINT 'ERROR: ' + @ErrorMessage;
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH
GO

