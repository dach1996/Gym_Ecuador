-- =================================================================
-- Script de creación de tabla SUCURSAL_GIMNASIO_IMAGENES
-- Tabla de relación muchos a muchos entre Sucursales de Gimnasio e Imágenes
-- Permite que una sucursal tenga múltiples imágenes y que una imagen 
-- pueda estar asociada a múltiples sucursales
-- =================================================================

USE [Gym]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Crear tabla de relación Sucursal de Gimnasio con Imágenes
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[SUCURSAL_GIMNASIO_IMAGENES]') AND type in (N'U'))
BEGIN
    PRINT 'Creando tabla CORE.SUCURSAL_GIMNASIO_IMAGENES...'
    
    CREATE TABLE [CORE].[SUCURSAL_GIMNASIO_IMAGENES] (
        [SGY_ID] INT NOT NULL,
        [ARG_ID] INT NOT NULL,
        CONSTRAINT [PK_SUCURSAL_GIMNASIO_IMAGENES] PRIMARY KEY CLUSTERED ([SGY_ID] ASC, [ARG_ID] ASC),
        CONSTRAINT [FK_SUCURSAL_GIMNASIO_IMAGENES_SUCURSAL_GIMNASIO] FOREIGN KEY ([SGY_ID]) REFERENCES [CORE].[SUCURSAL_GIMNASIO] ([SGY_ID]) ON DELETE CASCADE ON UPDATE CASCADE,
        CONSTRAINT [FK_SUCURSAL_GIMNASIO_IMAGENES_ARCHIVOS_GUARDADOS] FOREIGN KEY ([ARG_ID]) REFERENCES [ADMINISTRACION].[ARCHIVOS_GUARDADOS] ([ARG_ID]) ON DELETE CASCADE ON UPDATE CASCADE
    );
    
    PRINT 'Tabla CORE.SUCURSAL_GIMNASIO_IMAGENES creada exitosamente.'
END
ELSE
BEGIN
    PRINT 'La tabla CORE.SUCURSAL_GIMNASIO_IMAGENES ya existe.'
END
GO

-- Agregar propiedades extendidas para documentación
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[SUCURSAL_GIMNASIO_IMAGENES]') AND type in (N'U'))
BEGIN
    -- Descripción de la columna SGY_ID
    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.SUCURSAL_GIMNASIO_IMAGENES') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('CORE.SUCURSAL_GIMNASIO_IMAGENES') AND name = 'SGY_ID') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la sucursal de gimnasio (parte de la llave primaria compuesta). Hace referencia a SUCURSAL_GIMNASIO.SGY_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_IMAGENES', @level2type = N'COLUMN', @level2name = N'SGY_ID';
        PRINT 'Propiedad extendida agregada para columna SGY_ID.'
    END

    -- Descripción de la columna ARG_ID
    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.SUCURSAL_GIMNASIO_IMAGENES') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('CORE.SUCURSAL_GIMNASIO_IMAGENES') AND name = 'ARG_ID') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del archivo guardado (parte de la llave primaria compuesta). Hace referencia a ARCHIVOS_GUARDADOS.ARG_ID. Contiene la imagen de la sucursal de gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_IMAGENES', @level2type = N'COLUMN', @level2name = N'ARG_ID';
        PRINT 'Propiedad extendida agregada para columna ARG_ID.'
    END

    -- Descripción de la tabla
    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.SUCURSAL_GIMNASIO_IMAGENES') AND minor_id = 0 AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación muchos a muchos entre Sucursales de Gimnasio e Imágenes. Permite que una sucursal tenga múltiples imágenes y que una imagen pueda estar asociada a múltiples sucursales.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_IMAGENES';
        PRINT 'Propiedad extendida agregada para tabla SUCURSAL_GIMNASIO_IMAGENES.'
    END
END
GO

PRINT 'Script completado exitosamente.'
GO

