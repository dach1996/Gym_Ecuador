CREATE TABLE [ADMINISTRACION].[ARCHIVOS_GUARDADOS] (
    [ARG_ID]             INT           IDENTITY (1, 1) NOT NULL,
    [ARG_FECHA_REGISTRO] DATETIME      NOT NULL,
    [ARG_NOMBRE]         VARCHAR (50)  NOT NULL,
    [ARG_RUTA]           VARCHAR (300) NOT NULL,
    [ARG_URL]            VARCHAR (350) NULL,
    [ARG_ESTADO]         BIT           NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Archivo Almacenado', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'ARCHIVOS_GUARDADOS', @level2type = N'COLUMN', @level2name = N'ARG_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'ARCHIVOS_GUARDADOS', @level2type = N'COLUMN', @level2name = N'ARG_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de Archivo', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'ARCHIVOS_GUARDADOS', @level2type = N'COLUMN', @level2name = N'ARG_NOMBRE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta, Carpeta o Blob', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'ARCHIVOS_GUARDADOS', @level2type = N'COLUMN', @level2name = N'ARG_RUTA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Url de Acceso', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'ARCHIVOS_GUARDADOS', @level2type = N'COLUMN', @level2name = N'ARG_URL';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Archivo', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'ARCHIVOS_GUARDADOS', @level2type = N'COLUMN', @level2name = N'ARG_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Archivos Almacenados', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'ARCHIVOS_GUARDADOS';
GO

CREATE NONCLUSTERED INDEX [IX_ARG_URL]
    ON [ADMINISTRACION].[ARCHIVOS_GUARDADOS]([ARG_ID] ASC)
    INCLUDE([ARG_URL]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice por Id', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'ARCHIVOS_GUARDADOS', @level2type = N'INDEX', @level2name = N'IX_ARG_URL';
GO

ALTER TABLE [ADMINISTRACION].[ARCHIVOS_GUARDADOS]
    ADD CONSTRAINT [PK_ARCHIVOS_ALMACENADOS] PRIMARY KEY CLUSTERED ([ARG_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria para archivos Almacenados', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'ARCHIVOS_GUARDADOS', @level2type = N'CONSTRAINT', @level2name = N'PK_ARCHIVOS_ALMACENADOS';
GO

