CREATE TABLE [ADMINISTRACION].[TIPO_IDENTIFICACION] (
    [TID_ID]     INT           IDENTITY (1, 1) NOT NULL,
    [TID_CODIGO] VARCHAR (10)  NOT NULL,
    [TID_NOMBRE] VARCHAR (100) NOT NULL,
    [PAI_ID]     INT           NOT NULL,
    [TID_ESTADO] BIT           NOT NULL,
    CONSTRAINT [PK_TIPO_IDENTIFICACION] PRIMARY KEY CLUSTERED ([TID_ID] ASC),
    CONSTRAINT [FK_TIPO_IDENTIFICACION_PAIS] FOREIGN KEY ([PAI_ID]) REFERENCES [ADMINISTRACION].[PAIS] ([PAI_ID])
);


GO

CREATE NONCLUSTERED INDEX [IX_TIPO_IDENTIFICACION_CODIGO]
    ON [ADMINISTRACION].[TIPO_IDENTIFICACION]([TID_CODIGO] ASC);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Tipo de Identificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION', @level2type = N'COLUMN', @level2name = N'TID_ESTADO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Tipo de Identificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION', @level2type = N'CONSTRAINT', @level2name = N'PK_TIPO_IDENTIFICACION';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el País', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION', @level2type = N'CONSTRAINT', @level2name = N'FK_TIPO_IDENTIFICACION_PAIS';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Tipos de Identificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del tipo de identificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION', @level2type = N'COLUMN', @level2name = N'TID_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del tipo de identificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION', @level2type = N'COLUMN', @level2name = N'TID_NOMBRE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de identificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'TIPO_IDENTIFICACION', @level2type = N'COLUMN', @level2name = N'TID_CODIGO';


GO

