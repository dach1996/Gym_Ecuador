CREATE TABLE [ADMINISTRACION].[COOPERATIVA] (
    [COP_ID]                         INT            IDENTITY (1, 1) NOT NULL,
    [COP_CODIGO]                     VARCHAR (20)  NOT NULL,
    [COP_FECHA_REGISTRO]             DATETIME       NOT NULL,
    [COP_FECHA_ULTIMA_ACTUALIZACION] DATETIME       NOT NULL,
    [COP_NOMBRE]                     VARCHAR (30)  NOT NULL,
    [COP_DESCRIPCION]                VARCHAR (250) NOT NULL,
    [COP_LOGO_IMAGEN_ID]             INT            NULL,
    [COP_BUS_IMAGEN_ID]              INT            NULL,
    [COP_ESTADO]                     BIT            NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Comperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA', @level2type = N'COLUMN', @level2name = N'COP_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Coperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA', @level2type = N'COLUMN', @level2name = N'COP_CODIGO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro ', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA', @level2type = N'COLUMN', @level2name = N'COP_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última Actualización', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA', @level2type = N'COLUMN', @level2name = N'COP_FECHA_ULTIMA_ACTUALIZACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre Coperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA', @level2type = N'COLUMN', @level2name = N'COP_NOMBRE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Imágen Almacenada del Bus', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA', @level2type = N'COLUMN', @level2name = N'COP_DESCRIPCION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción Cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA', @level2type = N'COLUMN', @level2name = N'COP_LOGO_IMAGEN_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Imágen Almacenada del Logo', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA', @level2type = N'COLUMN', @level2name = N'COP_BUS_IMAGEN_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la Coperativa (Activada, Desactivada)', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA', @level2type = N'COLUMN', @level2name = N'COP_ESTADO';
GO

ALTER TABLE [ADMINISTRACION].[COOPERATIVA]
    ADD CONSTRAINT [FK_COP_LOGO_IMAGEN] FOREIGN KEY ([COP_LOGO_IMAGEN_ID]) REFERENCES [ADMINISTRACION].[ARCHIVOS_GUARDADOS] ([ARG_ID]);
GO

ALTER TABLE [ADMINISTRACION].[COOPERATIVA]
    ADD CONSTRAINT [FK_COP_BUS_IMAGEN] FOREIGN KEY ([COP_BUS_IMAGEN_ID]) REFERENCES [ADMINISTRACION].[ARCHIVOS_GUARDADOS] ([ARG_ID]);
GO

CREATE NONCLUSTERED INDEX [IX_COP_ESTADO]
    ON [ADMINISTRACION].[COOPERATIVA]([COP_ESTADO] ASC, [COP_LOGO_IMAGEN_ID] ASC, [COP_BUS_IMAGEN_ID] ASC)
    INCLUDE([COP_CODIGO], [COP_ID], [COP_NOMBRE]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de estado de cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA', @level2type = N'INDEX', @level2name = N'IX_COP_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Compañia o coperativa de Buses', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA';
GO

ALTER TABLE [ADMINISTRACION].[COOPERATIVA]
    ADD CONSTRAINT [PK_COOPERATIVA] PRIMARY KEY CLUSTERED ([COP_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Cooperativa de Buses', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA', @level2type = N'CONSTRAINT', @level2name = N'PK_COOPERATIVA';
GO

