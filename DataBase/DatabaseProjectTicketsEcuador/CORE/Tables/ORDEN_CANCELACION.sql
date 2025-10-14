CREATE TABLE [CORE].[ORDEN_CANCELACION] (
    [ORD_ID]                 INT           NOT NULL,
    [OCN_FECHA_CANCELACION]  DATETIME      NOT NULL,
    [OCN_TIPO_CANCELACION]   TINYINT       NOT NULL,
    [OCN_MOTIVO_CANCELACION] VARCHAR (512) NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la orden relacionada a cancelar', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_CANCELACION', @level2type = N'COLUMN', @level2name = N'ORD_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de Cancelación de una orden', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_CANCELACION', @level2type = N'COLUMN', @level2name = N'OCN_FECHA_CANCELACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de cancelación de la órden', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_CANCELACION', @level2type = N'COLUMN', @level2name = N'OCN_TIPO_CANCELACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Cancelación de Orden', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_CANCELACION', @level2type = N'COLUMN', @level2name = N'OCN_MOTIVO_CANCELACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de cancelaciones de Órdenes', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_CANCELACION';
GO

ALTER TABLE [CORE].[ORDEN_CANCELACION]
    ADD CONSTRAINT [FK_ORDEN_CANCELACION_ORDEN] FOREIGN KEY ([ORD_ID]) REFERENCES [CORE].[ORDEN] ([ORD_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación directa con la orden a Cancelar', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_CANCELACION', @level2type = N'CONSTRAINT', @level2name = N'FK_ORDEN_CANCELACION_ORDEN';
GO

ALTER TABLE [CORE].[ORDEN_CANCELACION]
    ADD CONSTRAINT [PK_ORDEN_CANCELACION] PRIMARY KEY CLUSTERED ([ORD_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Orden', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_CANCELACION', @level2type = N'CONSTRAINT', @level2name = N'PK_ORDEN_CANCELACION';
GO

