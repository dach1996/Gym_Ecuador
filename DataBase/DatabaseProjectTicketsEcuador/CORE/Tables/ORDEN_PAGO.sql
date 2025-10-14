CREATE TABLE [CORE].[ORDEN_PAGO] (
    [ORD_ID]             INT             NOT NULL,
    [PAO_FECHA_REGISTRO] DATETIME        NOT NULL,
    [PAO_PRECIO_BASE]    DECIMAL (10, 2) NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Pago', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_PAGO', @level2type = N'COLUMN', @level2name = N'ORD_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valod de Pago', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_PAGO', @level2type = N'COLUMN', @level2name = N'PAO_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_PAGO', @level2type = N'COLUMN', @level2name = N'PAO_PRECIO_BASE';
GO

ALTER TABLE [CORE].[ORDEN_PAGO]
    ADD CONSTRAINT [FK_ORDEN_PAGO_ORDEN] FOREIGN KEY ([ORD_ID]) REFERENCES [CORE].[ORDEN] ([ORD_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla de órdenes', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_PAGO', @level2type = N'CONSTRAINT', @level2name = N'FK_ORDEN_PAGO_ORDEN';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Pagos de Órdenes', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_PAGO';
GO

ALTER TABLE [CORE].[ORDEN_PAGO]
    ADD CONSTRAINT [PK_PAGO] PRIMARY KEY CLUSTERED ([ORD_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Pago', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_PAGO', @level2type = N'CONSTRAINT', @level2name = N'PK_PAGO';
GO

