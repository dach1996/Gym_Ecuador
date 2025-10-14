CREATE TABLE [AUTENTICACION].[USUARIO_DISPOSITIVO_PUSH_TOKEN] (
    [DIS_ID]                    INT           NOT NULL,
    [USR_ID]                    INT           NULL,
    [UDP_FECHA_ACTUALIZACION]   DATETIME      NOT NULL,
    [UDP_PUSH_TOKEN]            VARCHAR (512) NOT NULL,
    [UDP_CODIGO_IMPLEMENTACION] TINYINT       NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de dispositivo, puede ser null', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO_PUSH_TOKEN', @level2type = N'COLUMN', @level2name = N'DIS_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO_PUSH_TOKEN', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de actualización del registro', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO_PUSH_TOKEN', @level2type = N'COLUMN', @level2name = N'UDP_FECHA_ACTUALIZACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Push Token Registrado', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO_PUSH_TOKEN', @level2type = N'COLUMN', @level2name = N'UDP_PUSH_TOKEN';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Implementaciòn de envìo de notificaciones push', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO_PUSH_TOKEN', @level2type = N'COLUMN', @level2name = N'UDP_CODIGO_IMPLEMENTACION';
GO

ALTER TABLE [AUTENTICACION].[USUARIO_DISPOSITIVO_PUSH_TOKEN]
    ADD CONSTRAINT [FK_USUARIO_DISPOSITIVO_PUSH_TOKEN_DISPOSITIVO_1] FOREIGN KEY ([DIS_ID]) REFERENCES [AUTENTICACION].[DISPOSITIVO] ([DIS_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el Dispositivo', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO_PUSH_TOKEN', @level2type = N'CONSTRAINT', @level2name = N'FK_USUARIO_DISPOSITIVO_PUSH_TOKEN_DISPOSITIVO_1';
GO

ALTER TABLE [AUTENTICACION].[USUARIO_DISPOSITIVO_PUSH_TOKEN]
    ADD CONSTRAINT [FK_USUARIO_DISPOSITIVO_PUSH_TOKEN_USUARIO] FOREIGN KEY ([USR_ID]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO_PUSH_TOKEN', @level2type = N'CONSTRAINT', @level2name = N'FK_USUARIO_DISPOSITIVO_PUSH_TOKEN_USUARIO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Dispositivos registrados para enviar notificaciones por usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO_PUSH_TOKEN';
GO

ALTER TABLE [AUTENTICACION].[USUARIO_DISPOSITIVO_PUSH_TOKEN]
    ADD CONSTRAINT [PK_USUARIO_DISPOSITIVO_PUSH_TOKEN] PRIMARY KEY CLUSTERED ([DIS_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Relaciòn dispositivos con token push', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO_PUSH_TOKEN', @level2type = N'CONSTRAINT', @level2name = N'PK_USUARIO_DISPOSITIVO_PUSH_TOKEN';
GO

CREATE NONCLUSTERED INDEX [IX_ENVIO_NOTIFICACION_USUARIO]
    ON [AUTENTICACION].[USUARIO_DISPOSITIVO_PUSH_TOKEN]([USR_ID] ASC, [UDP_PUSH_TOKEN] ASC)
    INCLUDE([DIS_ID], [UDP_CODIGO_IMPLEMENTACION]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice para envío de notificaciones por usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO_PUSH_TOKEN', @level2type = N'INDEX', @level2name = N'IX_ENVIO_NOTIFICACION_USUARIO';
GO

