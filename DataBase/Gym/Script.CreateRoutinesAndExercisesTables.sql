-- =================================================================
-- Script de creación de tablas de Rutinas y Ejercicios
-- Este script crea las siguientes tablas:
--   1. RUTINAS - Tabla principal de rutinas de ejercicio
--   2. EJERCICIOS - Tabla de ejercicios disponibles
--   3. EJERCICIOS_TAGS - Tabla de relación muchos a muchos entre ejercicios y categorías del catálogo
--   4. RUTINA_EJERCICIO - Tabla de relación entre rutinas y ejercicios con parámetros de entrenamiento
--   5. REGISTRO_SERIES - Tabla para registrar el historial de series realizadas
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

BEGIN TRY
    PRINT '================================================================='
    PRINT 'Iniciando creación de tablas de Rutinas y Ejercicios'
    PRINT '================================================================='
    PRINT ''

    -- =================================================================
    -- TABLA 1: RUTINAS
    -- =================================================================
    PRINT 'Creando tabla CORE.RUTINAS...'

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[RUTINAS]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [CORE].[RUTINAS] (
            [RUT_ID] INT IDENTITY(1,1) NOT NULL,
            [RUT_GUID] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
            [RUT_NOMBRE] NVARCHAR(200) NOT NULL,
            [RUT_FECHA_CREACION] DATETIME NOT NULL,
            [USR_ID] INT NOT NULL,
            [USR_ID_CREADOR] INT NOT NULL,
            
            CONSTRAINT [PK_RUTINAS] PRIMARY KEY CLUSTERED ([RUT_ID] ASC),
            CONSTRAINT [FK_RUTINAS_USUARIO] FOREIGN KEY ([USR_ID]) 
                REFERENCES [AUTENTICACION].[USUARIO]([USR_ID]),
            CONSTRAINT [FK_RUTINAS_USUARIO_CREADOR] FOREIGN KEY ([USR_ID_CREADOR]) 
                REFERENCES [AUTENTICACION].[USUARIO]([USR_ID])
        );
        
        PRINT '  ✓ Tabla CORE.RUTINAS creada exitosamente.'
    END
    ELSE
    BEGIN
        PRINT '  - La tabla CORE.RUTINAS ya existe.'
    END

    -- Crear índice único para GUID
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RUTINAS_GUID' AND object_id = OBJECT_ID('CORE.RUTINAS'))
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [IX_RUTINAS_GUID] 
        ON [CORE].[RUTINAS] ([RUT_GUID]);
        PRINT '  ✓ Índice único para GUID creado.'
    END

    -- Crear índice para usuario propietario
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RUTINAS_USUARIO' AND object_id = OBJECT_ID('CORE.RUTINAS'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_RUTINAS_USUARIO] 
        ON [CORE].[RUTINAS] ([USR_ID]);
        PRINT '  ✓ Índice para usuario propietario creado.'
    END

    -- Crear índice para usuario creador
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RUTINAS_USUARIO_CREADOR' AND object_id = OBJECT_ID('CORE.RUTINAS'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_RUTINAS_USUARIO_CREADOR] 
        ON [CORE].[RUTINAS] ([USR_ID_CREADOR]);
        PRINT '  ✓ Índice para usuario creador creado.'
    END

    -- =================================================================
    -- TABLA 2: EJERCICIOS
    -- =================================================================
    PRINT ''
    PRINT 'Creando tabla CORE.EJERCICIOS...'

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[EJERCICIOS]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [CORE].[EJERCICIOS] (
            [EJE_ID] INT IDENTITY(1,1) NOT NULL,
            [EJE_GUID] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
            [EJE_NOMBRE] NVARCHAR(200) NOT NULL,
            [EJE_DESCRIPCION] NVARCHAR(1000) NULL,
            [EJE_INSTRUCCIONES] NVARCHAR(2000) NULL,
            [ARG_ID] INT NULL,
            
            CONSTRAINT [PK_EJERCICIOS] PRIMARY KEY CLUSTERED ([EJE_ID] ASC),
            CONSTRAINT [FK_EJERCICIOS_ARCHIVOS_GUARDADOS] FOREIGN KEY ([ARG_ID]) 
                REFERENCES [ADMINISTRACION].[ARCHIVOS_GUARDADOS]([ARG_ID])
        );
        
        PRINT '  ✓ Tabla CORE.EJERCICIOS creada exitosamente.'
    END
    ELSE
    BEGIN
        PRINT '  - La tabla CORE.EJERCICIOS ya existe.'
    END

    -- Crear índice único para GUID
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_EJERCICIOS_GUID' AND object_id = OBJECT_ID('CORE.EJERCICIOS'))
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [IX_EJERCICIOS_GUID] 
        ON [CORE].[EJERCICIOS] ([EJE_GUID]);
        PRINT '  ✓ Índice único para GUID creado.'
    END

    -- Crear índice para nombre (búsqueda)
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_EJERCICIOS_NOMBRE' AND object_id = OBJECT_ID('CORE.EJERCICIOS'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_EJERCICIOS_NOMBRE] 
        ON [CORE].[EJERCICIOS] ([EJE_NOMBRE]);
        PRINT '  ✓ Índice para nombre creado.'
    END

    -- =================================================================
    -- TABLA 3: EJERCICIOS_TAGS (Clave compuesta)
    -- =================================================================
    PRINT ''
    PRINT 'Creando tabla CORE.EJERCICIOS_TAGS...'

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[EJERCICIOS_TAGS]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [CORE].[EJERCICIOS_TAGS] (
            [EJE_ID] INT NOT NULL,
            [CAT_ID] INT NOT NULL,
            
            CONSTRAINT [PK_EJERCICIOS_TAGS] PRIMARY KEY CLUSTERED ([EJE_ID] ASC, [CAT_ID] ASC),
            CONSTRAINT [FK_EJERCICIOS_TAGS_EJERCICIO] FOREIGN KEY ([EJE_ID]) 
                REFERENCES [CORE].[EJERCICIOS]([EJE_ID]) ON DELETE CASCADE ON UPDATE CASCADE,
            CONSTRAINT [FK_EJERCICIOS_TAGS_CATALOGO] FOREIGN KEY ([CAT_ID]) 
                REFERENCES [ADMINISTRACION].[CATALOGO]([CAT_ID]) ON DELETE CASCADE ON UPDATE CASCADE
        );
        
        PRINT '  ✓ Tabla CORE.EJERCICIOS_TAGS creada exitosamente.'
    END
    ELSE
    BEGIN
        PRINT '  - La tabla CORE.EJERCICIOS_TAGS ya existe.'
    END

    -- =================================================================
    -- TABLA 4: RUTINA_EJERCICIO
    -- =================================================================
    PRINT ''
    PRINT 'Creando tabla CORE.RUTINA_EJERCICIO...'

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[RUTINA_EJERCICIO]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [CORE].[RUTINA_EJERCICIO] (
            [RUE_ID] INT IDENTITY(1,1) NOT NULL,
            [RUT_ID] INT NOT NULL,
            [EJE_ID] INT NOT NULL,
            [RUE_SERIES] INT NOT NULL,
            [RUE_REPETICIONES_DESDE] INT NOT NULL,
            [RUE_REPETICIONES_HASTA] INT NOT NULL,
            [RUE_SEGUNDOS_DESCANSO] INT NOT NULL,
            
            CONSTRAINT [PK_RUTINA_EJERCICIO] PRIMARY KEY CLUSTERED ([RUE_ID] ASC),
            CONSTRAINT [FK_RUTINA_EJERCICIO_RUTINA] FOREIGN KEY ([RUT_ID]) 
                REFERENCES [CORE].[RUTINAS]([RUT_ID]) ON DELETE CASCADE ON UPDATE CASCADE,
            CONSTRAINT [FK_RUTINA_EJERCICIO_EJERCICIO] FOREIGN KEY ([EJE_ID]) 
                REFERENCES [CORE].[EJERCICIOS]([EJE_ID]) ON DELETE CASCADE ON UPDATE CASCADE
        );
        
        PRINT '  ✓ Tabla CORE.RUTINA_EJERCICIO creada exitosamente.'
    END
    ELSE
    BEGIN
        PRINT '  - La tabla CORE.RUTINA_EJERCICIO ya existe.'
    END

    -- Crear índice para rutina
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RUTINA_EJERCICIO_RUTINA' AND object_id = OBJECT_ID('CORE.RUTINA_EJERCICIO'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_RUTINA_EJERCICIO_RUTINA] 
        ON [CORE].[RUTINA_EJERCICIO] ([RUT_ID]);
        PRINT '  ✓ Índice para rutina creado.'
    END

    -- Crear índice para ejercicio
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RUTINA_EJERCICIO_EJERCICIO' AND object_id = OBJECT_ID('CORE.RUTINA_EJERCICIO'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_RUTINA_EJERCICIO_EJERCICIO] 
        ON [CORE].[RUTINA_EJERCICIO] ([EJE_ID]);
        PRINT '  ✓ Índice para ejercicio creado.'
    END

    -- Crear índice único compuesto para evitar duplicados
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RUTINA_EJERCICIO_UNIQUE' AND object_id = OBJECT_ID('CORE.RUTINA_EJERCICIO'))
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [IX_RUTINA_EJERCICIO_UNIQUE] 
        ON [CORE].[RUTINA_EJERCICIO] ([RUT_ID], [EJE_ID]);
        PRINT '  ✓ Índice único compuesto creado para evitar duplicados.'
    END

    -- =================================================================
    -- TABLA 5: REGISTRO_SERIES
    -- =================================================================
    PRINT ''
    PRINT 'Creando tabla CORE.REGISTRO_SERIES...'

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[REGISTRO_SERIES]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [CORE].[REGISTRO_SERIES] (
            [SER_ID] INT IDENTITY(1,1) NOT NULL,
            [SER_GUID] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
            [EJE_ID] INT NOT NULL,
            [SER_FECHA_REGISTRO] DATETIME NOT NULL,
            [USR_ID] INT NOT NULL,
            [SER_PESO] DECIMAL(5,2) NULL,
            [SER_REPETICIONES] INT NOT NULL,
            
            CONSTRAINT [PK_REGISTRO_SERIES] PRIMARY KEY CLUSTERED ([SER_ID] ASC),
            CONSTRAINT [FK_REGISTRO_SERIES_EJERCICIO] FOREIGN KEY ([EJE_ID]) 
                REFERENCES [CORE].[EJERCICIOS]([EJE_ID]),
            CONSTRAINT [FK_REGISTRO_SERIES_USUARIO] FOREIGN KEY ([USR_ID]) 
                REFERENCES [AUTENTICACION].[USUARIO]([USR_ID])
        );
        
        PRINT '  ✓ Tabla CORE.REGISTRO_SERIES creada exitosamente.'
    END
    ELSE
    BEGIN
        PRINT '  - La tabla CORE.REGISTRO_SERIES ya existe.'
    END

    -- Crear índice único para GUID
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_REGISTRO_SERIES_GUID' AND object_id = OBJECT_ID('CORE.REGISTRO_SERIES'))
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [IX_REGISTRO_SERIES_GUID] 
        ON [CORE].[REGISTRO_SERIES] ([SER_GUID]);
        PRINT '  ✓ Índice único para GUID creado.'
    END

    -- Crear índice para usuario y fecha (consultas de historial)
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_REGISTRO_SERIES_USUARIO_FECHA' AND object_id = OBJECT_ID('CORE.REGISTRO_SERIES'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_REGISTRO_SERIES_USUARIO_FECHA] 
        ON [CORE].[REGISTRO_SERIES] ([USR_ID], [SER_FECHA_REGISTRO] DESC);
        PRINT '  ✓ Índice para usuario y fecha creado.'
    END

    -- Crear índice para ejercicio y usuario (filtros comunes)
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_REGISTRO_SERIES_EJERCICIO_USUARIO' AND object_id = OBJECT_ID('CORE.REGISTRO_SERIES'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_REGISTRO_SERIES_EJERCICIO_USUARIO] 
        ON [CORE].[REGISTRO_SERIES] ([EJE_ID], [USR_ID]);
        PRINT '  ✓ Índice para ejercicio y usuario creado.'
    END

    -- =================================================================
    -- AGREGAR COMENTARIOS A LAS TABLAS Y COLUMNAS
    -- =================================================================
    PRINT ''
    PRINT 'Agregando comentarios a las tablas y columnas...'

    -- Comentarios para tabla RUTINAS
    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINAS') AND minor_id = 0 AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Tabla de rutinas de ejercicio. Almacena las rutinas creadas por usuarios o administradores y a qué usuario están asignadas.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINAS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINAS'), 'RUT_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador único de la rutina (clave primaria).', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'RUT_ID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINAS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINAS'), 'RUT_GUID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador global único del registro.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'RUT_GUID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINAS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINAS'), 'RUT_NOMBRE', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Nombre de la rutina de ejercicio.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'RUT_NOMBRE';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINAS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINAS'), 'RUT_FECHA_CREACION', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Fecha de creación de la rutina.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'RUT_FECHA_CREACION';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINAS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINAS'), 'USR_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador del usuario propietario de la rutina. Hace referencia a AUTENTICACION.USUARIO.USR_ID', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'USR_ID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINAS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINAS'), 'USR_ID_CREADOR', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador del usuario que creó la rutina. Hace referencia a AUTENTICACION.USUARIO.USR_ID', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'USR_ID_CREADOR';
    END

    -- Comentarios para tabla EJERCICIOS
    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.EJERCICIOS') AND minor_id = 0 AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Tabla de ejercicios disponibles. Almacena información sobre los ejercicios que pueden ser incluidos en las rutinas.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.EJERCICIOS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.EJERCICIOS'), 'EJE_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador único del ejercicio (clave primaria).', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'EJE_ID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.EJERCICIOS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.EJERCICIOS'), 'EJE_GUID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador global único del registro.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'EJE_GUID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.EJERCICIOS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.EJERCICIOS'), 'EJE_NOMBRE', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Nombre del ejercicio.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'EJE_NOMBRE';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.EJERCICIOS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.EJERCICIOS'), 'EJE_DESCRIPCION', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Descripción del ejercicio.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'EJE_DESCRIPCION';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.EJERCICIOS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.EJERCICIOS'), 'EJE_INSTRUCCIONES', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Instrucciones detalladas sobre cómo realizar el ejercicio.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'EJE_INSTRUCCIONES';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.EJERCICIOS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.EJERCICIOS'), 'ARG_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador de la imagen del ejercicio. Hace referencia a ADMINISTRACION.ARCHIVOS_GUARDADOS.ARG_ID', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'ARG_ID';
    END

    -- Comentarios para tabla EJERCICIOS_TAGS
    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.EJERCICIOS_TAGS') AND minor_id = 0 AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Tabla de relación muchos a muchos entre Ejercicios y Tags/Categorías del catálogo. Permite que un ejercicio tenga múltiples categorías y que una categoría pueda estar asociada a múltiples ejercicios.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS_TAGS';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.EJERCICIOS_TAGS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.EJERCICIOS_TAGS'), 'EJE_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador del ejercicio (parte de la llave primaria compuesta). Hace referencia a CORE.EJERCICIOS.EJE_ID', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS_TAGS', @level2type = N'COLUMN', @level2name = N'EJE_ID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.EJERCICIOS_TAGS') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.EJERCICIOS_TAGS'), 'CAT_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador del catálogo/tag/categoría (parte de la llave primaria compuesta). Hace referencia a ADMINISTRACION.CATALOGO.CAT_ID', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS_TAGS', @level2type = N'COLUMN', @level2name = N'CAT_ID';
    END

    -- Comentarios para tabla RUTINA_EJERCICIO
    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINA_EJERCICIO') AND minor_id = 0 AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Tabla de relación entre Rutinas y Ejercicios. Almacena los parámetros de entrenamiento (series, repeticiones, descanso) para cada ejercicio dentro de una rutina.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINA_EJERCICIO') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINA_EJERCICIO'), 'RUE_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador único de la relación rutina-ejercicio (clave primaria).', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUE_ID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINA_EJERCICIO') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINA_EJERCICIO'), 'RUT_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador de la rutina. Hace referencia a CORE.RUTINAS.RUT_ID', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUT_ID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINA_EJERCICIO') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINA_EJERCICIO'), 'EJE_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador del ejercicio. Hace referencia a CORE.EJERCICIOS.EJE_ID', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'EJE_ID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINA_EJERCICIO') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINA_EJERCICIO'), 'RUE_SERIES', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Número de series a realizar para este ejercicio en la rutina.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUE_SERIES';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINA_EJERCICIO') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINA_EJERCICIO'), 'RUE_REPETICIONES_DESDE', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Rango mínimo de repeticiones a realizar para este ejercicio en la rutina.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUE_REPETICIONES_DESDE';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINA_EJERCICIO') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINA_EJERCICIO'), 'RUE_REPETICIONES_HASTA', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Rango máximo de repeticiones a realizar para este ejercicio en la rutina.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUE_REPETICIONES_HASTA';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.RUTINA_EJERCICIO') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.RUTINA_EJERCICIO'), 'RUE_SEGUNDOS_DESCANSO', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Segundos de descanso entre series para este ejercicio en la rutina.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUE_SEGUNDOS_DESCANSO';
    END

    -- Comentarios para tabla REGISTRO_SERIES
    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.REGISTRO_SERIES') AND minor_id = 0 AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Tabla de registro de series realizadas. Almacena el historial de entrenamientos de los usuarios con información sobre peso, repeticiones y fecha.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.REGISTRO_SERIES') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.REGISTRO_SERIES'), 'SER_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador único del registro de serie (clave primaria).', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'SER_ID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.REGISTRO_SERIES') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.REGISTRO_SERIES'), 'SER_GUID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador global único del registro.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'SER_GUID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.REGISTRO_SERIES') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.REGISTRO_SERIES'), 'EJE_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador del ejercicio realizado. Hace referencia a CORE.EJERCICIOS.EJE_ID', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'EJE_ID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.REGISTRO_SERIES') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.REGISTRO_SERIES'), 'SER_FECHA_REGISTRO', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Fecha y hora en que se realizó el registro de la serie.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'SER_FECHA_REGISTRO';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.REGISTRO_SERIES') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.REGISTRO_SERIES'), 'USR_ID', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Identificador del usuario que realizó la serie. Hace referencia a AUTENTICACION.USUARIO.USR_ID', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'USR_ID';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.REGISTRO_SERIES') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.REGISTRO_SERIES'), 'SER_PESO', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Peso utilizado en kilogramos (opcional, puede ser NULL para ejercicios sin peso).', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'SER_PESO';
    END

    IF NOT EXISTS (SELECT * FROM sys.extended_properties WHERE major_id = OBJECT_ID('CORE.REGISTRO_SERIES') AND minor_id = COLUMNPROPERTY(OBJECT_ID('CORE.REGISTRO_SERIES'), 'SER_REPETICIONES', 'ColumnId') AND name = 'MS_Description')
    BEGIN
        EXECUTE sp_addextendedproperty @name = N'MS_Description', 
            @value = N'Número de repeticiones realizadas en la serie.', 
            @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'SER_REPETICIONES';
    END

    PRINT '  ✓ Comentarios agregados exitosamente.'

    -- =================================================================
    -- FINALIZACIÓN
    -- =================================================================
    PRINT ''
    PRINT '================================================================='
    PRINT 'Creación de tablas de Rutinas y Ejercicios completada exitosamente'
    PRINT '================================================================='
    PRINT ''

    COMMIT TRANSACTION

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION
    
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
    DECLARE @ErrorState INT = ERROR_STATE()
    
    PRINT '================================================================='
    PRINT 'ERROR: Ocurrió un error durante la creación de las tablas'
    PRINT '================================================================='
    PRINT 'Mensaje de error: ' + @ErrorMessage
    PRINT ''
    
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
END CATCH
GO
