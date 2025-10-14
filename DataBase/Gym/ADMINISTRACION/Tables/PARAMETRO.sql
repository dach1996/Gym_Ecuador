CREATE TABLE [ADMINISTRACION].[PARAMETRO] (
    [PAR_ID]               INT           IDENTITY (1, 1) NOT NULL,
    [PAR_CODIGO]           VARCHAR (100) NOT NULL,
    [PAR_NOMBRE]           VARCHAR (200) NOT NULL,
    [PAR_VALOR]            VARCHAR (500) NOT NULL,
    [PAR_DESCRIPCION]      VARCHAR (200) NOT NULL,
    [PAR_ESTADO]           BIT           NOT NULL,
    [PAR_VERSION]          INT           NOT NULL,
    [PAR_FECHA_REGISTRO]   DATETIME      NOT NULL,
    [PAR_FECHA_MOFICACION] DATETIME      NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Parámetro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO', @level2type = N'COLUMN', @level2name = N'PAR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Parámetro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO', @level2type = N'COLUMN', @level2name = N'PAR_CODIGO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de Parámetro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO', @level2type = N'COLUMN', @level2name = N'PAR_NOMBRE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de Parámetro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO', @level2type = N'COLUMN', @level2name = N'PAR_VALOR';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrión de Parámetro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO', @level2type = N'COLUMN', @level2name = N'PAR_DESCRIPCION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Parámetro (1 Activo, 0 Inactivo)', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO', @level2type = N'COLUMN', @level2name = N'PAR_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de Parámetro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO', @level2type = N'COLUMN', @level2name = N'PAR_VERSION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro de Parámetro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO', @level2type = N'COLUMN', @level2name = N'PAR_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última fecha de modificación de Parámetro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO', @level2type = N'COLUMN', @level2name = N'PAR_FECHA_MOFICACION';
GO

ALTER TABLE [ADMINISTRACION].[PARAMETRO]
    ADD CONSTRAINT [PK_PARAMETRO] PRIMARY KEY CLUSTERED ([PAR_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave Primaria de Tabla Parámetro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO', @level2type = N'CONSTRAINT', @level2name = N'PK_PARAMETRO';
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_PAR_CODIGO]
    ON [ADMINISTRACION].[PARAMETRO]([PAR_CODIGO] ASC)
    INCLUDE([PAR_ESTADO], [PAR_VALOR]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice para Códigos de Parámetro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO', @level2type = N'INDEX', @level2name = N'IX_PAR_CODIGO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Parámetros usados por la Aplicación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARAMETRO';
GO

