CREATE TABLE [CORE].[ORDEN_ASIENTO_PERSONA] (
    [ORD_ID]     INT          NOT NULL,
    [RSA_ID]     INT          NOT NULL,
    [PNA_ID]     INT          NOT NULL,
    [OAP_PRECIO] DECIMAL (10, 2) NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de Orden', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_ASIENTO_PERSONA', @level2type = N'COLUMN', @level2name = N'ORD_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de asiento reservado', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_ASIENTO_PERSONA', @level2type = N'COLUMN', @level2name = N'RSA_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Persona ', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_ASIENTO_PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de reserva de asiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_ASIENTO_PERSONA', @level2type = N'COLUMN', @level2name = N'OAP_PRECIO';
GO

ALTER TABLE [CORE].[ORDEN_ASIENTO_PERSONA]
    ADD CONSTRAINT [FK_ORDEN_ASIENTO_PERSONA_ORDEN] FOREIGN KEY ([ORD_ID]) REFERENCES [CORE].[ORDEN] ([ORD_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Orden', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_ASIENTO_PERSONA', @level2type = N'CONSTRAINT', @level2name = N'FK_ORDEN_ASIENTO_PERSONA_ORDEN';
GO

ALTER TABLE [CORE].[ORDEN_ASIENTO_PERSONA]
    ADD CONSTRAINT [FK_ORDEN_ASIENTO_PERSONA_PERSONA] FOREIGN KEY ([PNA_ID]) REFERENCES [AUTENTICACION].[PERSONA] ([PNA_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Persona', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_ASIENTO_PERSONA', @level2type = N'CONSTRAINT', @level2name = N'FK_ORDEN_ASIENTO_PERSONA_PERSONA';
GO

ALTER TABLE [CORE].[ORDEN_ASIENTO_PERSONA]
    ADD CONSTRAINT [FK_ORDEN_ASIENTO_PERSONA_RESERVA_ASIENTO] FOREIGN KEY ([RSA_ID]) REFERENCES [CORE].[RESERVA_ASIENTO] ([RSA_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Asiento Persona', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_ASIENTO_PERSONA', @level2type = N'CONSTRAINT', @level2name = N'FK_ORDEN_ASIENTO_PERSONA_RESERVA_ASIENTO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de Ordenes de Asientos con Personas', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_ASIENTO_PERSONA';
GO

ALTER TABLE [CORE].[ORDEN_ASIENTO_PERSONA]
    ADD CONSTRAINT [PK_ORDEN_ASIENTO_PERSONA] PRIMARY KEY CLUSTERED ([ORD_ID] ASC, [RSA_ID] ASC, [PNA_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de ORdenes de Asiento con persona', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN_ASIENTO_PERSONA', @level2type = N'CONSTRAINT', @level2name = N'PK_ORDEN_ASIENTO_PERSONA';
GO

