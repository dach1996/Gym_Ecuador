-- =================================================================
-- Script de creación de tabla SERVICIO y modificación de 
-- SUCURSAL_GIMNASIO_SERVICIO para segregar servicios
-- =================================================================

-- Crear tabla de servicios
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[SERVICIO]') AND type in (N'U'))
BEGIN
    CREATE TABLE [CORE].[SERVICIO] (
        [SER_ID] INT IDENTITY(1,1) NOT NULL,
        [SER_FECHA_REGISTRO] DATETIME NOT NULL DEFAULT GETDATE(),
        [SER_NOMBRE] NVARCHAR(200) NOT NULL,
        [SER_DESCRIPCION] NVARCHAR(1000) NULL,
        [SER_CODIGO] NVARCHAR(50) NULL,
        [ITC_TIPO_SERVICIO] INT NULL,
        [SER_DURACION_MINUTOS] INT NULL,
        [SER_REQUIERE_RESERVA] BIT NOT NULL DEFAULT 0,
        [SER_ESTADO] BIT NOT NULL DEFAULT 1,
        [SER_OBSERVACIONES] NVARCHAR(500) NULL,
        CONSTRAINT [PK_SERVICIO] PRIMARY KEY CLUSTERED ([SER_ID] ASC)
    );
    
    PRINT 'Tabla CORE.SERVICIO creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla CORE.SERVICIO ya existe.';
END
GO

-- Crear índices para la tabla SERVICIO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SERVICIO_CODIGO' AND object_id = OBJECT_ID('CORE.SERVICIO'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [IX_SERVICIO_CODIGO] ON [CORE].[SERVICIO] ([SER_CODIGO]) 
    WHERE [SER_CODIGO] IS NOT NULL;
    PRINT 'Índice IX_SERVICIO_CODIGO creado.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SERVICIO_TIPO' AND object_id = OBJECT_ID('CORE.SERVICIO'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SERVICIO_TIPO] ON [CORE].[SERVICIO] ([ITC_TIPO_SERVICIO]);
    PRINT 'Índice IX_SERVICIO_TIPO creado.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SERVICIO_ESTADO' AND object_id = OBJECT_ID('CORE.SERVICIO'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SERVICIO_ESTADO] ON [CORE].[SERVICIO] ([SER_ESTADO]);
    PRINT 'Índice IX_SERVICIO_ESTADO creado.';
END
GO

-- =================================================================
-- MIGRACIÓN DE DATOS
-- =================================================================

-- Verificar si la tabla SUCURSAL_GIMNASIO_SERVICIO tiene datos
IF EXISTS (SELECT 1 FROM [CORE].[SUCURSAL_GIMNASIO_SERVICIO])
BEGIN
    PRINT 'Iniciando migración de datos...';
    
    -- Verificar si ya se agregó la columna SER_ID
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.SUCURSAL_GIMNASIO_SERVICIO') AND name = 'SER_ID')
    BEGIN
        -- Paso 1: Insertar servicios únicos en la tabla SERVICIO
        INSERT INTO [CORE].[SERVICIO] (
            [SER_FECHA_REGISTRO],
            [SER_NOMBRE],
            [SER_DESCRIPCION],
            [SER_CODIGO],
            [ITC_TIPO_SERVICIO],
            [SER_DURACION_MINUTOS],
            [SER_REQUIERE_RESERVA],
            [SER_ESTADO],
            [SER_OBSERVACIONES]
        )
        SELECT DISTINCT
            MIN([SGS_FECHA_REGISTRO]),
            [SGS_NOMBRE],
            [SGS_DESCRIPCION],
            [SGS_CODIGO],
            [ITC_TIPO_SERVICIO],
            [SGS_DURACION_MINUTOS],
            [SGS_REQUIERE_RESERVA],
            1, -- Estado activo por defecto
            NULL -- Observaciones generales
        FROM [CORE].[SUCURSAL_GIMNASIO_SERVICIO]
        WHERE [SGS_NOMBRE] IS NOT NULL
        GROUP BY 
            [SGS_NOMBRE],
            [SGS_DESCRIPCION],
            [SGS_CODIGO],
            [ITC_TIPO_SERVICIO],
            [SGS_DURACION_MINUTOS],
            [SGS_REQUIERE_RESERVA];
        
        PRINT 'Servicios únicos insertados en CORE.SERVICIO.';
        
        -- Paso 2: Agregar columna SER_ID a SUCURSAL_GIMNASIO_SERVICIO
        ALTER TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO]
        ADD [SER_ID] INT NULL;
        
        PRINT 'Columna SER_ID agregada a SUCURSAL_GIMNASIO_SERVICIO.';
        
        -- Paso 3: Actualizar la relación basándose en los campos coincidentes
        UPDATE sgs
        SET sgs.[SER_ID] = s.[SER_ID]
        FROM [CORE].[SUCURSAL_GIMNASIO_SERVICIO] sgs
        INNER JOIN [CORE].[SERVICIO] s ON 
            sgs.[SGS_NOMBRE] = s.[SER_NOMBRE]
            AND ISNULL(sgs.[SGS_CODIGO], '') = ISNULL(s.[SER_CODIGO], '')
            AND ISNULL(sgs.[ITC_TIPO_SERVICIO], 0) = ISNULL(s.[ITC_TIPO_SERVICIO], 0);
        
        PRINT 'Relaciones actualizadas con SER_ID.';
        
        -- Paso 4: Hacer la columna SER_ID NOT NULL
        ALTER TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO]
        ALTER COLUMN [SER_ID] INT NOT NULL;
        
        PRINT 'Columna SER_ID establecida como NOT NULL.';
        
        -- Paso 5: Crear Foreign Key
        IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SUCURSAL_GIMNASIO_SERVICIO_SERVICIO')
        BEGIN
            ALTER TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO]
            ADD CONSTRAINT [FK_SUCURSAL_GIMNASIO_SERVICIO_SERVICIO] 
            FOREIGN KEY ([SER_ID]) REFERENCES [CORE].[SERVICIO]([SER_ID]);
            
            PRINT 'Foreign Key FK_SUCURSAL_GIMNASIO_SERVICIO_SERVICIO creada.';
        END
        
        -- Paso 6: Eliminar columnas redundantes de SUCURSAL_GIMNASIO_SERVICIO
        -- IMPORTANTE: Comentado por seguridad. Ejecutar manualmente después de verificar la migración
        /*
        ALTER TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO] DROP COLUMN [SGS_NOMBRE];
        ALTER TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO] DROP COLUMN [SGS_DESCRIPCION];
        ALTER TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO] DROP COLUMN [SGS_CODIGO];
        ALTER TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO] DROP COLUMN [ITC_TIPO_SERVICIO];
        ALTER TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO] DROP COLUMN [SGS_DURACION_MINUTOS];
        ALTER TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO] DROP COLUMN [SGS_REQUIERE_RESERVA];
        PRINT 'Columnas redundantes eliminadas de SUCURSAL_GIMNASIO_SERVICIO.';
        */
        
        PRINT 'NOTA: Las columnas redundantes NO fueron eliminadas. Elimínelas manualmente después de verificar la migración.';
        
    END
    ELSE
    BEGIN
        PRINT 'La columna SER_ID ya existe. Migración omitida.';
    END
END
ELSE
BEGIN
    PRINT 'No hay datos para migrar en SUCURSAL_GIMNASIO_SERVICIO.';
    
    -- Si no hay datos, agregar la columna SER_ID directamente como NOT NULL
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CORE.SUCURSAL_GIMNASIO_SERVICIO') AND name = 'SER_ID')
    BEGIN
        ALTER TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO]
        ADD [SER_ID] INT NOT NULL;
        
        -- Crear Foreign Key
        ALTER TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO]
        ADD CONSTRAINT [FK_SUCURSAL_GIMNASIO_SERVICIO_SERVICIO] 
        FOREIGN KEY ([SER_ID]) REFERENCES [CORE].[SERVICIO]([SER_ID]);
        
        PRINT 'Columna SER_ID y Foreign Key creadas.';
    END
END
GO

-- Crear índice para mejorar el rendimiento de las consultas
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SUCURSAL_GIMNASIO_SERVICIO_SERVICIO' AND object_id = OBJECT_ID('CORE.SUCURSAL_GIMNASIO_SERVICIO'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SUCURSAL_GIMNASIO_SERVICIO_SERVICIO] 
    ON [CORE].[SUCURSAL_GIMNASIO_SERVICIO] ([SER_ID]);
    PRINT 'Índice IX_SUCURSAL_GIMNASIO_SERVICIO_SERVICIO creado.';
END
GO

-- Crear índice compuesto para consultas frecuentes
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SUCURSAL_GIMNASIO_SERVICIO_SUCURSAL_SERVICIO' AND object_id = OBJECT_ID('CORE.SUCURSAL_GIMNASIO_SERVICIO'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SUCURSAL_GIMNASIO_SERVICIO_SUCURSAL_SERVICIO] 
    ON [CORE].[SUCURSAL_GIMNASIO_SERVICIO] ([SGY_ID], [SER_ID], [SGS_ESTADO]);
    PRINT 'Índice IX_SUCURSAL_GIMNASIO_SERVICIO_SUCURSAL_SERVICIO creado.';
END
GO

PRINT '=================================================================';
PRINT 'Script completado exitosamente.';
PRINT 'NOTA: Revise los datos migrados antes de eliminar las columnas redundantes.';
PRINT '=================================================================';
GO

