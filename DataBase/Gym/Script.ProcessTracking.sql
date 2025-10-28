-- Script para crear la tabla SEGUIMIENTO_PROCESOS en SQL Server
-- Basado en el modelo ProcessTracking.cs

USE [GYM_DATABASE] -- Cambiar por el nombre de tu base de datos
GO

-- Crear el esquema GIMNASIO si no existe
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'GIMNASIO')
BEGIN
    EXEC('CREATE SCHEMA [GIMNASIO]')
END
GO

-- Crear la tabla SEGUIMIENTO_PROCESOS
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GIMNASIO].[SEGUIMIENTO_PROCESOS]') AND type in (N'U'))
BEGIN
    CREATE TABLE [GIMNASIO].[SEGUIMIENTO_PROCESOS](
        [SPR_ID] [int] IDENTITY(1,1) NOT NULL,
        [SPR_FECHA_REGISTRO] [datetime2](7) NULL,
        [SPR_GUID] [uniqueidentifier] NOT NULL,
        [PNA_ID] [int] NOT NULL,
        [GYM_ID] [int] NOT NULL,
        [SPR_PESO] [decimal](18, 2) NOT NULL,
        [SPR_ALTURA] [decimal](18, 2) NOT NULL,
        [SPR_GRASA_PORCENTAJE] [decimal](5, 2) NULL,
        [SPR_MUSCULO_PORCENTAJE] [decimal](5, 2) NULL,
        [SPR_MEDIDA_PECHO] [decimal](18, 2) NULL,
        [SPR_MEDIDA_CINTURA] [decimal](18, 2) NULL,
        [SPR_MEDIDA_CADERA] [decimal](18, 2) NULL,
        [SPR_MEDIDA_BRAZO_DER] [decimal](18, 2) NULL,
        [SPR_MEDIDA_MUSLO_DER] [decimal](18, 2) NULL,
        [SPR_OBSERVACIONES] [nvarchar](max) NULL,
        [USR_ID_REGISTRADOR] [int] NULL,
        
        CONSTRAINT [PK_SEGUIMIENTO_PROCESOS] PRIMARY KEY CLUSTERED ([SPR_ID] ASC),
        CONSTRAINT [UK_SEGUIMIENTO_PROCESOS_GUID] UNIQUE ([SPR_GUID])
    )
END
GO

-- Agregar índices para mejorar el rendimiento
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[GIMNASIO].[SEGUIMIENTO_PROCESOS]') AND name = N'IX_SEGUIMIENTO_PROCESOS_PNA_ID')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SEGUIMIENTO_PROCESOS_PNA_ID] ON [GIMNASIO].[SEGUIMIENTO_PROCESOS]([PNA_ID])
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[GIMNASIO].[SEGUIMIENTO_PROCESOS]') AND name = N'IX_SEGUIMIENTO_PROCESOS_GYM_ID')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SEGUIMIENTO_PROCESOS_GYM_ID] ON [GIMNASIO].[SEGUIMIENTO_PROCESOS]([GYM_ID])
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[GIMNASIO].[SEGUIMIENTO_PROCESOS]') AND name = N'IX_SEGUIMIENTO_PROCESOS_FECHA_REGISTRO')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SEGUIMIENTO_PROCESOS_FECHA_REGISTRO] ON [GIMNASIO].[SEGUIMIENTO_PROCESOS]([SPR_FECHA_REGISTRO])
END
GO

-- Agregar comentarios descriptivos a la tabla y columnas
EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Tabla para el seguimiento de procesos y mediciones corporales de los usuarios del gimnasio', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Identificador único autoincremental', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_ID'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Fecha y hora de registro del seguimiento', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_FECHA_REGISTRO'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'GUID único para identificación externa', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_GUID'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'ID de la persona (FK a tabla de personas)', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'PNA_ID'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'ID del gimnasio (FK a tabla de gimnasios)', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'GYM_ID'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Peso corporal actual en kilogramos', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_PESO'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Altura de la persona en centímetros', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_ALTURA'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Porcentaje de grasa corporal', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_GRASA_PORCENTAJE'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Porcentaje de masa muscular', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_MUSCULO_PORCENTAJE'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Circunferencia del pecho en centímetros', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_MEDIDA_PECHO'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Circunferencia de la cintura en centímetros', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_MEDIDA_CINTURA'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Circunferencia de la cadera en centímetros', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_MEDIDA_CADERA'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Circunferencia del brazo derecho en centímetros', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_MEDIDA_BRAZO_DER'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Circunferencia del muslo derecho en centímetros', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_MEDIDA_MUSLO_DER'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Observaciones y comentarios del seguimiento', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'SPR_OBSERVACIONES'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'ID del usuario que registró la información', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS', 
    @level2type=N'COLUMN', @level2name=N'USR_ID_REGISTRADOR'
GO

PRINT 'Tabla SEGUIMIENTO_PROCESOS creada exitosamente'
GO

