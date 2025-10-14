CREATE TABLE [ADMINISTRACION].[COOPERATIVA_BUS_SERVICIO] (
    [CPB_ID]     INT           NOT NULL,
    [SRV_ID]     INT           NOT NULL,
    [CBS_CODIGO] VARCHAR (50) NOT NULL,
    [CPB_ESTADO] BIT           NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID be Bus', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS_SERVICIO', @level2type = N'COLUMN', @level2name = N'CPB_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Servicio', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS_SERVICIO', @level2type = N'COLUMN', @level2name = N'SRV_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de bus Asociado a los servicios con la Cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS_SERVICIO', @level2type = N'COLUMN', @level2name = N'CBS_CODIGO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del servicio asociado al bus y cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS_SERVICIO', @level2type = N'COLUMN', @level2name = N'CPB_ESTADO';
GO

CREATE NONCLUSTERED INDEX [IX_ESTADO_BUS_ID]
    ON [ADMINISTRACION].[COOPERATIVA_BUS_SERVICIO]([CPB_ID] ASC, [CPB_ESTADO] ASC)
    INCLUDE([CBS_CODIGO]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice por estado y Id', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS_SERVICIO', @level2type = N'INDEX', @level2name = N'IX_ESTADO_BUS_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Relación Bus Servicio Cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS_SERVICIO';
GO

ALTER TABLE [ADMINISTRACION].[COOPERATIVA_BUS_SERVICIO]
    ADD CONSTRAINT [FK_COOPERATIVA_BUS_SERVICIO_COOPERATIVA_BUS] FOREIGN KEY ([CPB_ID]) REFERENCES [ADMINISTRACION].[COOPERATIVA_BUS] ([CPB_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Cooperativa Bus', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS_SERVICIO', @level2type = N'CONSTRAINT', @level2name = N'FK_COOPERATIVA_BUS_SERVICIO_COOPERATIVA_BUS';
GO

ALTER TABLE [ADMINISTRACION].[COOPERATIVA_BUS_SERVICIO]
    ADD CONSTRAINT [FK_COOPERATIVA_BUS_SERVICIO_SERVICIOS] FOREIGN KEY ([SRV_ID]) REFERENCES [ADMINISTRACION].[SERVICIOS] ([SRV_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación  con Servicio', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS_SERVICIO', @level2type = N'CONSTRAINT', @level2name = N'FK_COOPERATIVA_BUS_SERVICIO_SERVICIOS';
GO

ALTER TABLE [ADMINISTRACION].[COOPERATIVA_BUS_SERVICIO]
    ADD CONSTRAINT [PK_BUS_SERVICIO] PRIMARY KEY CLUSTERED ([SRV_ID] ASC, [CPB_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Tabla de Bus Servicio', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS_SERVICIO', @level2type = N'CONSTRAINT', @level2name = N'PK_BUS_SERVICIO';
GO

