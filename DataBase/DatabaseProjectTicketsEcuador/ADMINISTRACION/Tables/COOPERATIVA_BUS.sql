CREATE TABLE [ADMINISTRACION].[COOPERATIVA_BUS] (
    [CPB_ID]     INT          IDENTITY (1, 1) NOT NULL,
    [COP_ID]     INT          NOT NULL,
    [CPB_CODIGO] VARCHAR (50) NOT NULL,
    [CPB_ESTADO] BIT          NOT NULL,
    [CPB_TIPO]   INT          NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Tabla Cooperativa Bus', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS', @level2type = N'COLUMN', @level2name = N'CPB_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS', @level2type = N'COLUMN', @level2name = N'COP_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código relacional de Bus asociado a la cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS', @level2type = N'COLUMN', @level2name = N'CPB_CODIGO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del Bus en la cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS', @level2type = N'COLUMN', @level2name = N'CPB_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Bus', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS', @level2type = N'COLUMN', @level2name = N'CPB_TIPO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tablar relacional de Bus con Cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS';
GO

CREATE NONCLUSTERED INDEX [IX_COOPERATIVA_ESTADO]
    ON [ADMINISTRACION].[COOPERATIVA_BUS]([COP_ID] ASC, [CPB_ESTADO] ASC, [CPB_ID] ASC)
    INCLUDE([CPB_CODIGO]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ìndice por Cooperativa y Estado', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS', @level2type = N'INDEX', @level2name = N'IX_COOPERATIVA_ESTADO';
GO

ALTER TABLE [ADMINISTRACION].[COOPERATIVA_BUS]
    ADD CONSTRAINT [FK_COOPERATIVA_BUS_COOPERATIVA] FOREIGN KEY ([COP_ID]) REFERENCES [ADMINISTRACION].[COOPERATIVA] ([COP_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS', @level2type = N'CONSTRAINT', @level2name = N'FK_COOPERATIVA_BUS_COOPERATIVA';
GO

ALTER TABLE [ADMINISTRACION].[COOPERATIVA_BUS]
    ADD CONSTRAINT [PK_COOPERATIVA_BUS] PRIMARY KEY CLUSTERED ([CPB_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de cooperativa Bus', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'COOPERATIVA_BUS', @level2type = N'CONSTRAINT', @level2name = N'PK_COOPERATIVA_BUS';
GO

