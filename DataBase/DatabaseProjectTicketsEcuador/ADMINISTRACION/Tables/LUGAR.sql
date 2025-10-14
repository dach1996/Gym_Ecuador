CREATE TABLE [ADMINISTRACION].[LUGAR] (
    [LOD_ID]                  INT          IDENTITY (1, 1) NOT NULL,
    [LOD_CODIGO_LUGAR]        VARCHAR (50) NOT NULL,
    [LOD_NOMBRE]              VARCHAR (50) NOT NULL,
    [LOD_NOMBRE_CORTO]        VARCHAR (50) NOT NULL,
    [LOD_ESTADO]              BIT          NOT NULL,
    [LOD_UBICACION_LATITUD]   FLOAT (53)   NOT NULL,
    [LOD_UBICACION_LONGITUD]  FLOAT (53)   NOT NULL,
    [LOD_UBICACION_CALCULADA] AS           ([geography]::Point([LOD_UBICACION_LATITUD],[LOD_UBICACION_LONGITUD],(4326))) PERSISTED NOT NULL,
    [PRO_ID]                  INT          NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Registro Origen Destino', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR', @level2type = N'COLUMN', @level2name = N'LOD_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Lugar ', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR', @level2type = N'COLUMN', @level2name = N'LOD_CODIGO_LUGAR';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del Lugar Visualizado por el App', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR', @level2type = N'COLUMN', @level2name = N'LOD_NOMBRE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre corto utilizado en visualización', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR', @level2type = N'COLUMN', @level2name = N'LOD_NOMBRE_CORTO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Lugar', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR', @level2type = N'COLUMN', @level2name = N'LOD_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Latitud', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR', @level2type = N'COLUMN', @level2name = N'LOD_UBICACION_LATITUD';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Longitud', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR', @level2type = N'COLUMN', @level2name = N'LOD_UBICACION_LONGITUD';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de provincia en donde se encuentra el lugar', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR', @level2type = N'COLUMN', @level2name = N'PRO_ID';
GO

CREATE NONCLUSTERED INDEX [IX_LOD_ID]
    ON [ADMINISTRACION].[LUGAR]([LOD_ID] ASC, [LOD_ESTADO] ASC)
    INCLUDE([LOD_NOMBRE], [LOD_CODIGO_LUGAR], [LOD_NOMBRE_CORTO]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de consulta de todos los registros', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR', @level2type = N'INDEX', @level2name = N'IX_LOD_ID';
GO

ALTER TABLE [ADMINISTRACION].[LUGAR]
    ADD CONSTRAINT [PK_LUGAR] PRIMARY KEY CLUSTERED ([LOD_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de lugares de Origen o Destino', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR', @level2type = N'CONSTRAINT', @level2name = N'PK_LUGAR';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla Lugares de Origenes y Destino', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR';
GO

ALTER TABLE [ADMINISTRACION].[LUGAR]
    ADD CONSTRAINT [FK_LUGAR_PROVINCIA] FOREIGN KEY ([PRO_ID]) REFERENCES [ADMINISTRACION].[PROVINCIA] ([PRO_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Provincia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR', @level2type = N'CONSTRAINT', @level2name = N'FK_LUGAR_PROVINCIA';
GO

