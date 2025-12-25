-- =================================================================
-- Script de creación de tablas nuevas de Administración
-- Este script crea las siguientes tablas:
--   1. CIUDAD - Tabla de ciudades asociadas a provincias
--   2. PARROQUIA - Tabla de parroquias asociadas a ciudades
--   3. TIPO_IDENTIFICACION - Tabla de tipos de identificación asociados a países
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

-- =================================================================
-- 1. TABLA CIUDAD
-- Descripción: Almacena las ciudades asociadas a provincias
-- Relaciones: 
--   - FK_CIUDAD_PROVINCIA: Relación con tabla PROVINCIA
-- =================================================================

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ADMINISTRACION].[CIUDAD]') AND type in (N'U'))
BEGIN
    PRINT 'Creando tabla ADMINISTRACION.CIUDAD...'
    
    CREATE TABLE [ADMINISTRACION].[CIUDAD] (
        [CIU_ID]     INT           IDENTITY (1, 1) NOT NULL,
        [CIU_CODIGO] VARCHAR (10)  NOT NULL,
        [CIU_NOMBRE] VARCHAR (100) NOT NULL,
        [PRO_ID]     INT           NOT NULL,
        [CIU_ESTADO] BIT           NOT NULL,
        CONSTRAINT [PK_CIUDAD] PRIMARY KEY CLUSTERED ([CIU_ID] ASC),
        CONSTRAINT [FK_CIUDAD_PROVINCIA] FOREIGN KEY ([PRO_ID]) REFERENCES [ADMINISTRACION].[PROVINCIA] ([PRO_ID])
    );
    
    PRINT 'Tabla ADMINISTRACION.CIUDAD creada exitosamente.'
END
ELSE
BEGIN
    PRINT 'La tabla ADMINISTRACION.CIUDAD ya existe.'
END
GO

-- Crear índice para código de ciudad
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CIUDAD_CODIGO' AND object_id = OBJECT_ID('ADMINISTRACION.CIUDAD'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_CIUDAD_CODIGO]
        ON [ADMINISTRACION].[CIUDAD]([CIU_CODIGO] ASC);
    PRINT 'Índice IX_CIUDAD_CODIGO creado exitosamente.'
END
GO

-- Agregar propiedades extendidas para documentación
IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.CIUDAD') AND minor_id = 0 AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Ciudades', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.CIUDAD') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.CIUDAD') AND name = 'CIU_ID') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la ciudad', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD', @level2type = N'COLUMN', @level2name = N'CIU_ID';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.CIUDAD') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.CIUDAD') AND name = 'CIU_CODIGO') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la ciudad', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD', @level2type = N'COLUMN', @level2name = N'CIU_CODIGO';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.CIUDAD') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.CIUDAD') AND name = 'CIU_NOMBRE') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la ciudad', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD', @level2type = N'COLUMN', @level2name = N'CIU_NOMBRE';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.CIUDAD') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.CIUDAD') AND name = 'CIU_ESTADO') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Ciudad (1=Activo, 0=Inactivo)', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD', @level2type = N'COLUMN', @level2name = N'CIU_ESTADO';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.CIUDAD') AND minor_id = 0 AND name = 'FK_CIUDAD_PROVINCIA')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la Provincia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD', @level2type = N'CONSTRAINT', @level2name = N'FK_CIUDAD_PROVINCIA';
END
GO

-- =================================================================
-- 2. TABLA PARROQUIA
-- Descripción: Almacena las parroquias asociadas a ciudades
-- Relaciones: 
--   - FK_PARROQUIA_CIUDAD: Relación con tabla CIUDAD
-- =================================================================

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ADMINISTRACION].[PARROQUIA]') AND type in (N'U'))
BEGIN
    PRINT 'Creando tabla ADMINISTRACION.PARROQUIA...'
    
    CREATE TABLE [ADMINISTRACION].[PARROQUIA] (
        [PAR_ID]     INT           IDENTITY (1, 1) NOT NULL,
        [PAR_CODIGO] VARCHAR (10)  NOT NULL,
        [PAR_NOMBRE] VARCHAR (100) NOT NULL,
        [CIU_ID]     INT           NOT NULL,
        [PAR_ESTADO] BIT           NOT NULL,
        CONSTRAINT [PK_PARROQUIA] PRIMARY KEY CLUSTERED ([PAR_ID] ASC),
        CONSTRAINT [FK_PARROQUIA_CIUDAD] FOREIGN KEY ([CIU_ID]) REFERENCES [ADMINISTRACION].[CIUDAD] ([CIU_ID])
    );
    
    PRINT 'Tabla ADMINISTRACION.PARROQUIA creada exitosamente.'
END
ELSE
BEGIN
    PRINT 'La tabla ADMINISTRACION.PARROQUIA ya existe.'
END
GO

-- Crear índice para código de parroquia
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PARROQUIA_CODIGO' AND object_id = OBJECT_ID('ADMINISTRACION.PARROQUIA'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_PARROQUIA_CODIGO]
        ON [ADMINISTRACION].[PARROQUIA]([PAR_CODIGO] ASC);
    PRINT 'Índice IX_PARROQUIA_CODIGO creado exitosamente.'
END
GO

-- Agregar propiedades extendidas para documentación
IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.PARROQUIA') AND minor_id = 0 AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Parroquias', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.PARROQUIA') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.PARROQUIA') AND name = 'PAR_ID') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la parroquia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA', @level2type = N'COLUMN', @level2name = N'PAR_ID';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.PARROQUIA') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.PARROQUIA') AND name = 'PAR_CODIGO') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la parroquia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA', @level2type = N'COLUMN', @level2name = N'PAR_CODIGO';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.PARROQUIA') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.PARROQUIA') AND name = 'PAR_NOMBRE') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la parroquia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA', @level2type = N'COLUMN', @level2name = N'PAR_NOMBRE';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.PARROQUIA') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.PARROQUIA') AND name = 'PAR_ESTADO') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Parroquia (1=Activo, 0=Inactivo)', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA', @level2type = N'COLUMN', @level2name = N'PAR_ESTADO';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.PARROQUIA') AND minor_id = 0 AND name = 'FK_PARROQUIA_CIUDAD')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la Ciudad', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA', @level2type = N'CONSTRAINT', @level2name = N'FK_PARROQUIA_CIUDAD';
END
GO

-- =================================================================
-- 3. TABLA TIPO_IDENTIFICACION
-- Descripción: Almacena los tipos de identificación asociados a países
-- Relaciones: 
--   - FK_TIPO_IDENTIFICACION_PAIS: Relación con tabla PAIS
-- Nota: Cada país puede tener diferentes tipos de documentos de identificación
--       (ej: Cédula para Ecuador, DNI para Argentina, etc.)
-- =================================================================

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ADMINISTRACION].[TIPO_IDENTIFICACION]') AND type in (N'U'))
BEGIN
    PRINT 'Creando tabla ADMINISTRACION.TIPO_IDENTIFICACION...'
    
    CREATE TABLE [ADMINISTRACION].[TIPO_IDENTIFICACION] (
        [TID_ID]     INT           IDENTITY (1, 1) NOT NULL,
        [TID_CODIGO] VARCHAR (10)  NOT NULL,
        [TID_NOMBRE] VARCHAR (100) NOT NULL,
        [PAI_ID]     INT           NOT NULL,
        [TID_ESTADO] BIT           NOT NULL,
        CONSTRAINT [PK_TIPO_IDENTIFICACION] PRIMARY KEY CLUSTERED ([TID_ID] ASC),
        CONSTRAINT [FK_TIPO_IDENTIFICACION_PAIS] FOREIGN KEY ([PAI_ID]) REFERENCES [ADMINISTRACION].[PAIS] ([PAI_ID])
    );
    
    PRINT 'Tabla ADMINISTRACION.TIPO_IDENTIFICACION creada exitosamente.'
END
ELSE
BEGIN
    PRINT 'La tabla ADMINISTRACION.TIPO_IDENTIFICACION ya existe.'
END
GO

-- Crear índice para código de tipo de identificación
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_TIPO_IDENTIFICACION_CODIGO' AND object_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_TIPO_IDENTIFICACION_CODIGO]
        ON [ADMINISTRACION].[TIPO_IDENTIFICACION]([TID_CODIGO] ASC);
    PRINT 'Índice IX_TIPO_IDENTIFICACION_CODIGO creado exitosamente.'
END
GO

-- Crear índice para país (útil para consultas por país)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_TIPO_IDENTIFICACION_PAIS' AND object_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_TIPO_IDENTIFICACION_PAIS]
        ON [ADMINISTRACION].[TIPO_IDENTIFICACION]([PAI_ID] ASC);
    PRINT 'Índice IX_TIPO_IDENTIFICACION_PAIS creado exitosamente.'
END
GO

-- Agregar propiedades extendidas para documentación
IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION') AND minor_id = 0 AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Tipos de Identificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION') AND name = 'TID_ID') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del tipo de identificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION', @level2type = N'COLUMN', @level2name = N'TID_ID';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION') AND name = 'TID_CODIGO') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de identificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION', @level2type = N'COLUMN', @level2name = N'TID_CODIGO';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION') AND name = 'TID_NOMBRE') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del tipo de identificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION', @level2type = N'COLUMN', @level2name = N'TID_NOMBRE';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION') AND name = 'TID_ESTADO') AND name = 'MS_Description')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Tipo de Identificación (1=Activo, 0=Inactivo)', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION', @level2type = N'COLUMN', @level2name = N'TID_ESTADO';
END
GO

IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('ADMINISTRACION.TIPO_IDENTIFICACION') AND minor_id = 0 AND name = 'FK_TIPO_IDENTIFICACION_PAIS')
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el País', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION', @level2type = N'CONSTRAINT', @level2name = N'FK_TIPO_IDENTIFICACION_PAIS';
END
GO

-- =================================================================
-- RESUMEN DE RELACIONES CREADAS
-- =================================================================
-- 
-- Estructura jerárquica geográfica:
-- PAIS
--   └── REGION
--       └── PROVINCIA
--           └── CIUDAD (NUEVA)
--               └── PARROQUIA (NUEVA)
--
-- Estructura de tipos de identificación:
-- PAIS
--   └── TIPO_IDENTIFICACION (NUEVA)
--
-- =================================================================

PRINT '================================================================='
PRINT 'Script de creación de tablas completado exitosamente.'
PRINT 'Tablas creadas:'
PRINT '  1. ADMINISTRACION.CIUDAD'
PRINT '  2. ADMINISTRACION.PARROQUIA'
PRINT '  3. ADMINISTRACION.TIPO_IDENTIFICACION'
PRINT '================================================================='
GO

