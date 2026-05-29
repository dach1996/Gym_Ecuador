CREATE TABLE [CORE].[PARAMETROS_FISICOS] (
    [PAF_ID]     TINYINT        NOT NULL,
    [PAF_CODIGO] VARCHAR (64)   NOT NULL,
    [PAF_NOMBRE] NVARCHAR (128) NOT NULL,
    [PAF_ESTADO]           BIT     NOT NULL CONSTRAINT [DF_PARAMETROS_FISICOS_ESTADO] DEFAULT (1),
    [PAF_TIPO_DIFERENCIA]  TINYINT NOT NULL CONSTRAINT [DF_PARAMETROS_FISICOS_TIPO_DIFERENCIA] DEFAULT (0),
    [PAF_UNIDAD_MEDIDA]    TINYINT        NOT NULL CONSTRAINT [DF_PARAMETROS_FISICOS_UNIDAD_MEDIDA] DEFAULT (1),
    [PAF_ICONO]            VARCHAR (64)   NOT NULL CONSTRAINT [DF_PARAMETROS_FISICOS_ICONO] DEFAULT (''),
    CONSTRAINT [PK_PARAMETROS_FISICOS] PRIMARY KEY CLUSTERED ([PAF_ID] ASC)
);


GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_PARAMETROS_FISICOS_CODIGO]
    ON [CORE].[PARAMETROS_FISICOS]([PAF_CODIGO] ASC);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de parámetros físicos (peso, medidas, porcentajes)', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'PARAMETROS_FISICOS';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del parámetro físico', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'PARAMETROS_FISICOS', @level2type = N'COLUMN', @level2name = N'PAF_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del parámetro (ej. PESO, ALTURA)', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'PARAMETROS_FISICOS', @level2type = N'COLUMN', @level2name = N'PAF_CODIGO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del parámetro', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'PARAMETROS_FISICOS', @level2type = N'COLUMN', @level2name = N'PAF_NOMBRE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del parámetro (activo/inactivo)', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'PARAMETROS_FISICOS', @level2type = N'COLUMN', @level2name = N'PAF_ESTADO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de diferencia de valor (0=Positive, 1=Negative, 2=Zero)', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'PARAMETROS_FISICOS', @level2type = N'COLUMN', @level2name = N'PAF_TIPO_DIFERENCIA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida (1=Centimeter, 2=Kilogram, 3=Point)', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'PARAMETROS_FISICOS', @level2type = N'COLUMN', @level2name = N'PAF_UNIDAD_MEDIDA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del icono para UI', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'PARAMETROS_FISICOS', @level2type = N'COLUMN', @level2name = N'PAF_ICONO';


GO
