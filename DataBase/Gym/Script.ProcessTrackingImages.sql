-- Script para crear la tabla SEGUIMIENTO_PROCESOS_IMAGENES en SQL Server
-- Tabla de relación entre SEGUIMIENTO_PROCESOS y ARCHIVOS_GUARDADOS
-- Basado en el modelo ProcessTrackingImage.cs

USE [GYM_DATABASE] -- Cambiar por el nombre de tu base de datos
GO

-- Crear el esquema GIMNASIO si no existe
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'GIMNASIO')
BEGIN
    EXEC('CREATE SCHEMA [GIMNASIO]')
END
GO

-- Crear la tabla SEGUIMIENTO_PROCESOS_IMAGENES
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GIMNASIO].[SEGUIMIENTO_PROCESOS_IMAGENES]') AND type in (N'U'))
BEGIN
    CREATE TABLE [GIMNASIO].[SEGUIMIENTO_PROCESOS_IMAGENES](
        [SPI_ID] [int] IDENTITY(1,1) NOT NULL,
        [SPI_GUID] [uniqueidentifier] NOT NULL,
        [SPR_ID] [int] NOT NULL,
        [ARG_ID] [int] NOT NULL,
        [SPI_TIPO_IMAGEN] [nvarchar](50) NULL,
        [SPI_DESCRIPCION] [nvarchar](500) NULL,
        [SPI_ORDEN] [int] NULL,
        [SPI_ESTADO] [bit] NOT NULL DEFAULT 1,
        [SPI_FECHA_REGISTRO] [datetime2](7) NULL,
        [USR_ID_REGISTRADOR] [int] NULL,
        
        CONSTRAINT [PK_SEGUIMIENTO_PROCESOS_IMAGENES] PRIMARY KEY CLUSTERED ([SPI_ID] ASC),
        CONSTRAINT [UK_SEGUIMIENTO_PROCESOS_IMAGENES_GUID] UNIQUE ([SPI_GUID]),
        CONSTRAINT [FK_SEGUIMIENTO_PROCESOS_IMAGENES_SEGUIMIENTO] FOREIGN KEY ([SPR_ID]) 
            REFERENCES [GIMNASIO].[SEGUIMIENTO_PROCESOS]([SPR_ID]),
        CONSTRAINT [FK_SEGUIMIENTO_PROCESOS_IMAGENES_ARCHIVO] FOREIGN KEY ([ARG_ID]) 
            REFERENCES [ADMINISTRACION].[ARCHIVOS_GUARDADOS]([ARG_ID])
    )
END
GO

-- Agregar índices para mejorar el rendimiento
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[GIMNASIO].[SEGUIMIENTO_PROCESOS_IMAGENES]') AND name = N'IX_SEGUIMIENTO_PROCESOS_IMAGENES_SPR_ID')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SEGUIMIENTO_PROCESOS_IMAGENES_SPR_ID] ON [GIMNASIO].[SEGUIMIENTO_PROCESOS_IMAGENES]([SPR_ID])
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[GIMNASIO].[SEGUIMIENTO_PROCESOS_IMAGENES]') AND name = N'IX_SEGUIMIENTO_PROCESOS_IMAGENES_ARG_ID')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SEGUIMIENTO_PROCESOS_IMAGENES_ARG_ID] ON [GIMNASIO].[SEGUIMIENTO_PROCESOS_IMAGENES]([ARG_ID])
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[GIMNASIO].[SEGUIMIENTO_PROCESOS_IMAGENES]') AND name = N'IX_SEGUIMIENTO_PROCESOS_IMAGENES_FECHA_REGISTRO')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SEGUIMIENTO_PROCESOS_IMAGENES_FECHA_REGISTRO] ON [GIMNASIO].[SEGUIMIENTO_PROCESOS_IMAGENES]([SPI_FECHA_REGISTRO])
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[GIMNASIO].[SEGUIMIENTO_PROCESOS_IMAGENES]') AND name = N'IX_SEGUIMIENTO_PROCESOS_IMAGENES_ESTADO')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SEGUIMIENTO_PROCESOS_IMAGENES_ESTADO] ON [GIMNASIO].[SEGUIMIENTO_PROCESOS_IMAGENES]([SPI_ESTADO])
END
GO

-- Agregar comentarios descriptivos a la tabla y columnas
EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Tabla de relación para almacenar las imágenes asociadas a cada seguimiento de proceso', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS_IMAGENES'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Identificador único autoincremental', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS_IMAGENES', 
    @level2type=N'COLUMN', @level2name=N'SPI_ID'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'GUID único para identificación externa', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS_IMAGENES', 
    @level2type=N'COLUMN', @level2name=N'SPI_GUID'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'ID del seguimiento de proceso (FK a SEGUIMIENTO_PROCESOS)', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS_IMAGENES', 
    @level2type=N'COLUMN', @level2name=N'SPR_ID'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'ID del archivo guardado (FK a ARCHIVOS_GUARDADOS)', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS_IMAGENES', 
    @level2type=N'COLUMN', @level2name=N'ARG_ID'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Tipo de imagen (Ej: Frontal, Lateral, Trasera, Detalle)', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS_IMAGENES', 
    @level2type=N'COLUMN', @level2name=N'SPI_TIPO_IMAGEN'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Descripción u observaciones de la imagen', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS_IMAGENES', 
    @level2type=N'COLUMN', @level2name=N'SPI_DESCRIPCION'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Orden de visualización de la imagen', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS_IMAGENES', 
    @level2type=N'COLUMN', @level2name=N'SPI_ORDEN'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Estado activo/inactivo de la imagen', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS_IMAGENES', 
    @level2type=N'COLUMN', @level2name=N'SPI_ESTADO'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'Fecha y hora de registro de la relación', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS_IMAGENES', 
    @level2type=N'COLUMN', @level2name=N'SPI_FECHA_REGISTRO'
GO

EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'ID del usuario que registró la información', 
    @level0type=N'SCHEMA', @level0name=N'GIMNASIO', 
    @level1type=N'TABLE', @level1name=N'SEGUIMIENTO_PROCESOS_IMAGENES', 
    @level2type=N'COLUMN', @level2name=N'USR_ID_REGISTRADOR'
GO

PRINT 'Tabla SEGUIMIENTO_PROCESOS_IMAGENES creada exitosamente'
GO

