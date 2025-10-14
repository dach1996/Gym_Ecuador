CREATE TABLE [ADMINISTRACION].[COOPERATIVA_PUNTO_TRANSPORTE] (
    [CPT_ID]     INT           IDENTITY (1, 1) NOT NULL,
    [PTT_ID]     INT           NOT NULL,
    [COP_ID]     INT           NOT NULL,
    [CPT_CODIGO] VARCHAR (100) NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Tabla', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_PUNTO_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'CPT_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Lugar', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_PUNTO_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'PTT_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_PUNTO_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'COP_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de relación del Lugar con la Cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_PUNTO_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'CPT_CODIGO';
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_COPERATIVA_LUGAR]
    ON [ADMINISTRACION].[COOPERATIVA_PUNTO_TRANSPORTE]([COP_ID] ASC, [PTT_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice único entre cooperativa y Lugar ', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_PUNTO_TRANSPORTE', @level2type = N'INDEX', @level2name = N'UQ_COPERATIVA_LUGAR';
GO

ALTER TABLE [ADMINISTRACION].[COOPERATIVA_PUNTO_TRANSPORTE]
    ADD CONSTRAINT [FK_COOPERATIVA_PUNTO_TRANSPORTE_COOPERATIVA] FOREIGN KEY ([COP_ID]) REFERENCES [ADMINISTRACION].[COOPERATIVA] ([COP_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_PUNTO_TRANSPORTE', @level2type = N'CONSTRAINT', @level2name = N'FK_COOPERATIVA_PUNTO_TRANSPORTE_COOPERATIVA';
GO

ALTER TABLE [ADMINISTRACION].[COOPERATIVA_PUNTO_TRANSPORTE]
    ADD CONSTRAINT [FK_COOPERATIVA_PUNTO_TRANSPORTE_PUNTO_TRANSPORTE] FOREIGN KEY ([PTT_ID]) REFERENCES [ADMINISTRACION].[PUNTO_TRANSPORTE] ([PTT_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Punto de Transporte', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_PUNTO_TRANSPORTE', @level2type = N'CONSTRAINT', @level2name = N'FK_COOPERATIVA_PUNTO_TRANSPORTE_PUNTO_TRANSPORTE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Relación entre las cooperativas y los puntos de Recogida o Salida', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_PUNTO_TRANSPORTE';
GO

ALTER TABLE [ADMINISTRACION].[COOPERATIVA_PUNTO_TRANSPORTE]
    ADD CONSTRAINT [PK_COOPERATIVA_LUGAR] PRIMARY KEY CLUSTERED ([CPT_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Cooperativa Lugar', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_PUNTO_TRANSPORTE', @level2type = N'CONSTRAINT', @level2name = N'PK_COOPERATIVA_LUGAR';
GO

