CREATE TABLE [CORE].[RESERVA_ASIENTO_HISTORIAL] (
    [RAH_ID]             INT      IDENTITY (1, 1) NOT NULL,
    [RSA_ID]             INT      NOT NULL,
    [USR_ID]             INT      NOT NULL,
    [RAH_FECHA_REGISTRO] DATETIME NOT NULL,
    [RAH_ESTADO]         TINYINT  NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Reserva Asiento Historial', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO_HISTORIAL', @level2type = N'COLUMN', @level2name = N'RAH_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Reserva de Asiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO_HISTORIAL', @level2type = N'COLUMN', @level2name = N'RSA_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Usuario', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO_HISTORIAL', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha re registro de historial', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO_HISTORIAL', @level2type = N'COLUMN', @level2name = N'RAH_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Asiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO_HISTORIAL', @level2type = N'COLUMN', @level2name = N'RAH_ESTADO';
GO

ALTER TABLE [CORE].[RESERVA_ASIENTO_HISTORIAL]
    ADD CONSTRAINT [PK_RESERVA_ASIENTO_HISTORIAL] PRIMARY KEY CLUSTERED ([RAH_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Reserva Asiento Historial', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO_HISTORIAL';
GO

ALTER TABLE [CORE].[RESERVA_ASIENTO_HISTORIAL]
    ADD CONSTRAINT [FK_RESERVA_ASIENTO_HISTORIAL_RESERVA_ASIENTO] FOREIGN KEY ([RSA_ID]) REFERENCES [CORE].[RESERVA_ASIENTO] ([RSA_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación de Historial con Reserva', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO_HISTORIAL', @level2type = N'CONSTRAINT', @level2name = N'FK_RESERVA_ASIENTO_HISTORIAL_RESERVA_ASIENTO';
GO

ALTER TABLE [CORE].[RESERVA_ASIENTO_HISTORIAL]
    ADD CONSTRAINT [FK_RESERVA_ASIENTO_HISTORIAL_USUARIO] FOREIGN KEY ([USR_ID]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación de Historial con Asiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO_HISTORIAL', @level2type = N'CONSTRAINT', @level2name = N'FK_RESERVA_ASIENTO_HISTORIAL_USUARIO';
GO

