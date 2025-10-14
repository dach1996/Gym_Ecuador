CREATE TABLE [CORE].[RUTA_COOPERATIVA] (
    [RUT_ID]                  INT              IDENTITY (1, 1) NOT NULL,
    [RUT_GUID]                UNIQUEIDENTIFIER NOT NULL,
    [RUT_FECHA_REGISTRO]      DATETIME         NOT NULL,
    [RUT_FECHA_RUTA]          DATE             NOT NULL,
    [RUT_IDENTIFICADOR_VIAJE] VARCHAR (256)    NOT NULL,
    [CPB_ID]                  INT              NOT NULL,
    [COP_ID]                  INT              NOT NULL,
    [CPT_ID_ORIGEN]           INT              NOT NULL,
    [CPT_ID_DESTINO]          INT              NOT NULL,
    [RUT_HORA_SALIDA]         DATETIME         NOT NULL,
    [RUT_HORA_LLEGADA]        DATETIME         NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la ruta', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'COLUMN', @level2name = N'RUT_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la ruta', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'COLUMN', @level2name = N'RUT_GUID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última fecha de la ruta', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'COLUMN', @level2name = N'RUT_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de salida de Ruta', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'COLUMN', @level2name = N'RUT_FECHA_RUTA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de identificador de Viaje de servicio', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'COLUMN', @level2name = N'RUT_IDENTIFICADOR_VIAJE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de bus relacionado a la cooperativa', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'COLUMN', @level2name = N'CPB_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cooperativa', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'COLUMN', @level2name = N'COP_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de salida de Ruta', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'COLUMN', @level2name = N'RUT_HORA_SALIDA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora aproximada de llegada de ruta', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'COLUMN', @level2name = N'RUT_HORA_LLEGADA';
GO

ALTER TABLE [CORE].[RUTA_COOPERATIVA]
    ADD CONSTRAINT [FK_RUTA_COOPERATIVA] FOREIGN KEY ([COP_ID]) REFERENCES [ADMINISTRACION].[COOPERATIVA] ([COP_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciòn con la Cooperativa', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'CONSTRAINT', @level2name = N'FK_RUTA_COOPERATIVA';
GO

ALTER TABLE [CORE].[RUTA_COOPERATIVA]
    ADD CONSTRAINT [FK_RUTA_COOPERATIVA_COOPERATIVA_PUNTO_TRANSPORTE_DESTINO] FOREIGN KEY ([CPT_ID_DESTINO]) REFERENCES [ADMINISTRACION].[COOPERATIVA_PUNTO_TRANSPORTE] ([CPT_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el punto de Destino', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'CONSTRAINT', @level2name = N'FK_RUTA_COOPERATIVA_COOPERATIVA_PUNTO_TRANSPORTE_DESTINO';
GO

ALTER TABLE [CORE].[RUTA_COOPERATIVA]
    ADD CONSTRAINT [FK_RUTA_COOPERATIVA_COOPERATIVA_PUNTO_TRANSPORTE_ORIGEN] FOREIGN KEY ([CPT_ID_ORIGEN]) REFERENCES [ADMINISTRACION].[COOPERATIVA_PUNTO_TRANSPORTE] ([CPT_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el punto de Origen', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'CONSTRAINT', @level2name = N'FK_RUTA_COOPERATIVA_COOPERATIVA_PUNTO_TRANSPORTE_ORIGEN';
GO

ALTER TABLE [CORE].[RUTA_COOPERATIVA]
    ADD CONSTRAINT [FK_RUTA_COOPERATIVA_BUS] FOREIGN KEY ([CPB_ID]) REFERENCES [ADMINISTRACION].[COOPERATIVA_BUS] ([CPB_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación ocn el Bus de la Cooperativa', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'CONSTRAINT', @level2name = N'FK_RUTA_COOPERATIVA_BUS';
GO

ALTER TABLE [CORE].[RUTA_COOPERATIVA]
    ADD CONSTRAINT [PK_RUTA] PRIMARY KEY CLUSTERED ([RUT_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de la ruta', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'CONSTRAINT', @level2name = N'PK_RUTA';
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_RUTA]
    ON [CORE].[RUTA_COOPERATIVA]([RUT_FECHA_RUTA] ASC, [CPB_ID] ASC, [RUT_IDENTIFICADOR_VIAJE] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice único de la Ruta', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'INDEX', @level2name = N'UQ_RUTA';
GO

CREATE NONCLUSTERED INDEX [IX_FECHA_RUTA]
    ON [CORE].[RUTA_COOPERATIVA]([RUT_FECHA_RUTA] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la Ruta', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA', @level2type = N'INDEX', @level2name = N'IX_FECHA_RUTA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Rutas', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTA_COOPERATIVA';
GO

