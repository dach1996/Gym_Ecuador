-- =================================================================
-- Script de creación de nuevas tablas
-- Este script crea las siguientes tablas:
--   1. PLAN_SUCURSAL - Planes de suscripción por sucursal
--   2. CLIENTE_SUCURSAL_GIMNASIO - Relaciona usuarios con sucursales de gimnasio
--   3. MEMBRECIAS - Relaciona clientes con planes de suscripción
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
    PRINT 'Iniciando creación de nuevas tablas'
    PRINT '================================================================='
    PRINT ''

    -- =================================================================
    -- TABLA 1: PLAN_SUCURSAL
    -- =================================================================
    PRINT 'Creando tabla CORE.PLAN_SUCURSAL...'

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[PLAN_SUCURSAL]') AND type in (N'U'))
    BEGIN
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

    -- Crear índice único para el código del plan por sucursal
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PLAN_SUCURSAL_CODIGO_SUCURSAL' AND object_id = OBJECT_ID('CORE.PLAN_SUCURSAL'))
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [IX_PLAN_SUCURSAL_CODIGO_SUCURSAL] 
        ON [CORE].[PLAN_SUCURSAL] ([SGY_ID], [PLS_CODIGO]) 
        WHERE [PLS_CODIGO] IS NOT NULL;
        PRINT '  ✓ Índice único creado para código de plan por sucursal.'
    END
    GO

    -- Crear índice para GUID
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PLAN_SUCURSAL_GUID' AND object_id = OBJECT_ID('CORE.PLAN_SUCURSAL'))
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [IX_PLAN_SUCURSAL_GUID] 
        ON [CORE].[PLAN_SUCURSAL] ([PLS_GUID]);
        PRINT '  ✓ Índice único para GUID creado.'
    END
    GO

    -- Crear índice para sucursal y estado
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PLAN_SUCURSAL_SUCURSAL_ESTADO' AND object_id = OBJECT_ID('CORE.PLAN_SUCURSAL'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_PLAN_SUCURSAL_SUCURSAL_ESTADO] 
        ON [CORE].[PLAN_SUCURSAL] ([SGY_ID], [PLS_ESTADO]);
        PRINT '  ✓ Índice para sucursal y estado creado.'
    END
    GO

    -- =================================================================
    -- TABLA 2: CLIENTE_SUCURSAL_GIMNASIO
    -- =================================================================
    PRINT ''
    PRINT 'Creando tabla CORE.CLIENTE_SUCURSAL_GIMNASIO...'

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

    -- Crear índice único para evitar duplicados de usuario-sucursal activos
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

    -- Crear índice para usuario y estado
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CLIENTE_SUCURSAL_GIMNASIO_USUARIO_ESTADO' AND object_id = OBJECT_ID('CORE.CLIENTE_SUCURSAL_GIMNASIO'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_CLIENTE_SUCURSAL_GIMNASIO_USUARIO_ESTADO]
        ON [CORE].[CLIENTE_SUCURSAL_GIMNASIO] ([USR_ID], [CSG_ESTADO]);
        PRINT '  ✓ Índice para usuario y estado creado.'
    END
    GO

    -- Crear índice para sucursal y estado
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CLIENTE_SUCURSAL_GIMNASIO_SUCURSAL_ESTADO' AND object_id = OBJECT_ID('CORE.CLIENTE_SUCURSAL_GIMNASIO'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_CLIENTE_SUCURSAL_GIMNASIO_SUCURSAL_ESTADO]
        ON [CORE].[CLIENTE_SUCURSAL_GIMNASIO] ([SGY_ID], [CSG_ESTADO]);
        PRINT '  ✓ Índice para sucursal y estado creado.'
    END
    GO

    -- =================================================================
    -- TABLA 3: MEMBRECIAS
    -- =================================================================
    PRINT ''
    PRINT 'Creando tabla CORE.MEMBRECIAS...'

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

    -- Crear índice para fechas de membresía activa
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_MEMBRECIAS_FECHAS_ACTIVA' AND object_id = OBJECT_ID('CORE.MEMBRECIAS'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_MEMBRECIAS_FECHAS_ACTIVA]
        ON [CORE].[MEMBRECIAS] ([MEM_FECHA_INICIO], [MEM_FECHA_FIN], [MEM_ESTADO])
        WHERE [MEM_ESTADO] = 1;
        PRINT '  ✓ Índice para fechas de membresía activa creado.'
    END
    GO

    COMMIT TRANSACTION
    GO

    PRINT ''
    PRINT '================================================================='
    PRINT 'Script completado exitosamente.'
    PRINT 'Tablas creadas:'
    PRINT '  1. CORE.PLAN_SUCURSAL'
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

