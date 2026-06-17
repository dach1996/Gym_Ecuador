-- =================================================================
-- Tabla CORE.PERFILES (Perfiles nutricionales / fitness)
-- Ejecutar después de Script.InsertProfileCatalogs.sql
-- =================================================================

USE [Gym];
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE s.name = 'CORE' AND t.name = 'PERFILES')
BEGIN
    CREATE TABLE [CORE].[PERFILES] (
        [PRF_ID]                   INT              IDENTITY (1, 1) NOT NULL,
        [PRF_GUID]                 UNIQUEIDENTIFIER NOT NULL,
        [PNA_ID]                   INT              NOT NULL,
        [PRF_NOMBRE]               VARCHAR (50)     NOT NULL,
        [PRF_TIPO]                 TINYINT          NOT NULL,
        [CAT_ID_NIVEL_ACTIVIDAD]   INT              NOT NULL,
        [PRF_ALTURA]               DECIMAL (5, 2)   NOT NULL,
        [CAT_ID_RITMO_PROGRESO]    INT              NOT NULL,
        [PRF_SEMANAS_ESTIMADAS]    TINYINT          NOT NULL,
        [PRF_PROTEINA]             SMALLINT         NOT NULL,
        [PRF_CARBOHIDRATOS]        SMALLINT         NOT NULL,
        [PRF_GRASAS]               SMALLINT         NOT NULL,
        [PRF_ESTADO]               BIT              NOT NULL CONSTRAINT [DF_PERFILES_ESTADO] DEFAULT ((1)),
        [PRF_FECHA_REGISTRO]       DATETIME         NULL,
        [USR_ID_REGISTRADOR]       INT              NULL,
        CONSTRAINT [PK_PERFILES] PRIMARY KEY CLUSTERED ([PRF_ID] ASC),
        CONSTRAINT [FK_PERFILES_PERSONA] FOREIGN KEY ([PNA_ID]) REFERENCES [AUTENTICACION].[PERSONA] ([PNA_ID]),
        CONSTRAINT [FK_PERFILES_NIVEL_ACTIVIDAD] FOREIGN KEY ([CAT_ID_NIVEL_ACTIVIDAD]) REFERENCES [ADMINISTRACION].[CATALOGO] ([CAT_ID]),
        CONSTRAINT [FK_PERFILES_RITMO_PROGRESO] FOREIGN KEY ([CAT_ID_RITMO_PROGRESO]) REFERENCES [ADMINISTRACION].[CATALOGO] ([CAT_ID])
    );

    CREATE NONCLUSTERED INDEX [IX_PERFILES_PNA_ESTADO]
        ON [CORE].[PERFILES] ([PNA_ID] ASC, [PRF_ESTADO] ASC);

    PRINT 'Tabla CORE.PERFILES creada.';
END
ELSE
    PRINT 'La tabla CORE.PERFILES ya existe; no se realizó ningún cambio.';
GO
