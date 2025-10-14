CREATE TABLE [ADMINISTRACION].[PUNTO_TRANSPORTE] (
    [PTT_ID]                  INT          IDENTITY (1, 1) NOT NULL,
    [PTT_CODIGO]              VARCHAR (50) NOT NULL,
    [PTT_NOMBRE]              VARCHAR (50) NOT NULL,
    [PTT_ESTADO]              BIT          NOT NULL,
    [PTT_TIPO]                TINYINT      NOT NULL,
    [PRO_ID]                  INT          NOT NULL,
    [PTT_UBICACION_LATITUD]   FLOAT (53)   NOT NULL,
    [PTT_UBICACION_LONGITUD]  FLOAT (53)   NOT NULL,
    [PTT_UBICACION_CALCULADA] AS           ([geography]::Point([PTT_UBICACION_LATITUD],[PTT_UBICACION_LONGITUD],(4326))) PERSISTED NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de estación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PUNTO_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'PTT_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Estación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PUNTO_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'PTT_CODIGO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Estación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PUNTO_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'PTT_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Punto de Transporte', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PUNTO_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'PTT_TIPO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Provincia donde se encuentra Registrada', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PUNTO_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'PRO_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Punto de Ubicación Latitud', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PUNTO_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'PTT_UBICACION_LATITUD';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Punto de Ubicación de Longitud', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PUNTO_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'PTT_UBICACION_LONGITUD';
GO

ALTER TABLE [ADMINISTRACION].[PUNTO_TRANSPORTE]
    ADD CONSTRAINT [FK_PUNTO_TRANSPORTE_PROVINCIA] FOREIGN KEY ([PRO_ID]) REFERENCES [ADMINISTRACION].[PROVINCIA] ([PRO_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la Provincia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PUNTO_TRANSPORTE', @level2type = N'CONSTRAINT', @level2name = N'FK_PUNTO_TRANSPORTE_PROVINCIA';
GO

ALTER TABLE [ADMINISTRACION].[PUNTO_TRANSPORTE]
    ADD CONSTRAINT [PK_PUNTO_TRANSPORTE] PRIMARY KEY CLUSTERED ([PTT_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria Punto de Transporte', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PUNTO_TRANSPORTE', @level2type = N'CONSTRAINT', @level2name = N'PK_PUNTO_TRANSPORTE';
GO

CREATE NONCLUSTERED INDEX [IX_CODIGO_ESTADO]
    ON [ADMINISTRACION].[PUNTO_TRANSPORTE]([PTT_ESTADO] ASC)
    INCLUDE([PTT_CODIGO], [PTT_NOMBRE]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice por código', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PUNTO_TRANSPORTE', @level2type = N'INDEX', @level2name = N'IX_CODIGO_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Puntos de Transporte', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PUNTO_TRANSPORTE';
GO

