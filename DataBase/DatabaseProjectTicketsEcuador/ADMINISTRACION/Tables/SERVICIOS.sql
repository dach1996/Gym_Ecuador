CREATE TABLE [ADMINISTRACION].[SERVICIOS] (
    [SRV_ID]                         INT           IDENTITY (1, 1) NOT NULL,
    [SRV_FECHA_REGISTRO]             DATETIME      NOT NULL,
    [SRV_FECHA_ULTIMA_ACTUALIZACION] DATETIME      NOT NULL,
    [SRV_CODIGO]                     VARCHAR (50) NOT NULL,
    [SRV_NOMBRE]                     VARCHAR (50) NOT NULL,
    [IMAGEN_ID]                      INT           NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de registro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'SERVICIOS', @level2type = N'COLUMN', @level2name = N'SRV_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'SERVICIOS', @level2type = N'COLUMN', @level2name = N'SRV_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última actualización', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'SERVICIOS', @level2type = N'COLUMN', @level2name = N'SRV_FECHA_ULTIMA_ACTUALIZACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Servicio', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'SERVICIOS', @level2type = N'COLUMN', @level2name = N'SRV_CODIGO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de Servicio', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'SERVICIOS', @level2type = N'COLUMN', @level2name = N'SRV_NOMBRE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id relacionado con Imagen', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'SERVICIOS', @level2type = N'COLUMN', @level2name = N'IMAGEN_ID';
GO

ALTER TABLE [ADMINISTRACION].[SERVICIOS]
    ADD CONSTRAINT [PK_SERVICIOS] PRIMARY KEY CLUSTERED ([SRV_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Servicios', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'SERVICIOS', @level2type = N'CONSTRAINT', @level2name = N'PK_SERVICIOS';
GO

ALTER TABLE [ADMINISTRACION].[SERVICIOS]
    ADD CONSTRAINT [FK_SERVICIOS_ARCHIVOS_GUARDADOS] FOREIGN KEY ([IMAGEN_ID]) REFERENCES [ADMINISTRACION].[ARCHIVOS_GUARDADOS] ([ARG_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la Imagen relacionada', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'SERVICIOS', @level2type = N'CONSTRAINT', @level2name = N'FK_SERVICIOS_ARCHIVOS_GUARDADOS';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de servicios de Bus', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'SERVICIOS';
GO

