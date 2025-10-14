CREATE TABLE [CORE].[RESERVA_ASIENTO] (
    [RSA_ID]                    INT              IDENTITY (1, 1) NOT NULL,
    [RSA_GUID]                  UNIQUEIDENTIFIER NOT NULL,
    [RUT_ID]                    INT              NOT NULL,
    [USR_ID]                    INT              NULL,
    [RSA_FECHA_REGISTRO]        DATETIME         NOT NULL,
    [RSA_ESTADO]                TINYINT          NOT NULL,
    [RSA_IDENTIFICADOR_ASIENTO] VARCHAR (64)     NOT NULL,
    [PDB_ID]                    INT              NOT NULL,
    [RSA_FECHA_EXPIRACION]      DATETIME         NULL,
    [MEC_ID]                    INT              NULL,
    [RSA_CONTROL_VERSION]       DATETIME2 (7)    NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de reserva de asiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'COLUMN', @level2name = N'RSA_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la ruta ', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'COLUMN', @level2name = N'RUT_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Último id de usuario que registró el asiento, puede ser null ya que la compra se realiza en la estación.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última fecha de registro de asiento.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'COLUMN', @level2name = N'RSA_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Reserva Asiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'COLUMN', @level2name = N'RSA_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Asiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'COLUMN', @level2name = N'RSA_IDENTIFICADOR_ASIENTO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id relacionado al Piso del bus donde se ubica el asiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'COLUMN', @level2name = N'PDB_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Expiración de Reserva', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'COLUMN', @level2name = N'RSA_FECHA_EXPIRACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Mensaje Encolado para Funcionalidades', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'COLUMN', @level2name = N'MEC_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de versión de Fila', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'COLUMN', @level2name = N'RSA_CONTROL_VERSION';
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_RSA_IDENTIFICACION]
    ON [CORE].[RESERVA_ASIENTO]([RSA_IDENTIFICADOR_ASIENTO] ASC, [PDB_ID] ASC, [RUT_ID] ASC)
    INCLUDE([RSA_ID], [RSA_ESTADO], [RSA_FECHA_REGISTRO], [USR_ID], [RSA_FECHA_EXPIRACION], [MEC_ID], [RSA_CONTROL_VERSION]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice Único por identificación', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'INDEX', @level2name = N'UQ_RSA_IDENTIFICACION';
GO

CREATE NONCLUSTERED INDEX [IX_MENSAJE_ENCOLADO]
    ON [CORE].[RESERVA_ASIENTO]([MEC_ID] ASC, [RSA_ID] ASC, [RSA_ESTADO] ASC)
    INCLUDE([RSA_CONTROL_VERSION], [RSA_GUID], [USR_ID], [RSA_IDENTIFICADOR_ASIENTO]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice para consulta por identificaicòn de Mensaje encolado', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'INDEX', @level2name = N'IX_MENSAJE_ENCOLADO';
GO

ALTER TABLE [CORE].[RESERVA_ASIENTO]
    ADD CONSTRAINT [PK_RESERVA_ASIENTO] PRIMARY KEY CLUSTERED ([RSA_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de reserva asiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'CONSTRAINT', @level2name = N'PK_RESERVA_ASIENTO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Reserva con Asiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO';
GO

ALTER TABLE [CORE].[RESERVA_ASIENTO]
    ADD CONSTRAINT [FK_RESERVA_ASIENTO_MENSAJES_ENCOLADOS] FOREIGN KEY ([MEC_ID]) REFERENCES [CORE].[MENSAJES_ENCOLADOS] ([MEC_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el mensaje encolado', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'CONSTRAINT', @level2name = N'FK_RESERVA_ASIENTO_MENSAJES_ENCOLADOS';
GO

ALTER TABLE [CORE].[RESERVA_ASIENTO]
    ADD CONSTRAINT [FK_RESERVA_ASIENTO_PISO_DIAGRAMA_COOPERATIVA_BUS] FOREIGN KEY ([PDB_ID]) REFERENCES [ADMINISTRACION].[PISO_DIAGRAMA_COOPERATIVA_BUS] ([PDB_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el diagrama de pisos del bus', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RESERVA_ASIENTO', @level2type = N'CONSTRAINT', @level2name = N'FK_RESERVA_ASIENTO_PISO_DIAGRAMA_COOPERATIVA_BUS';
GO

