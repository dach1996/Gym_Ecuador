CREATE TABLE [ADMINISTRACION].[NOTIFICACION_PUSH_USUARIO_DISPOSITIVO] (
    [NPU_ID]                    INT            NOT NULL,
    [DIS_ID]                    INT            NOT NULL,
    [PUD_ESTADO]                TINYINT        NOT NULL,
    [PUD_INFORMACION_ADICIONAL] VARCHAR (2048) NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de notificación push usuario', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO_DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'NPU_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Dispositivo', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO_DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'DIS_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Notificación Push', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO_DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'PUD_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información adicional de envío de notificación push', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO_DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'PUD_INFORMACION_ADICIONAL';
GO

ALTER TABLE [ADMINISTRACION].[NOTIFICACION_PUSH_USUARIO_DISPOSITIVO]
    ADD CONSTRAINT [PK_NOTIFICACION_PUSH_USUARIO_DISPOSITIVO] PRIMARY KEY CLUSTERED ([NPU_ID] ASC, [DIS_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave prmiaria de Notificaciòn push con dispositivo', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO_DISPOSITIVO', @level2type = N'CONSTRAINT', @level2name = N'PK_NOTIFICACION_PUSH_USUARIO_DISPOSITIVO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación notificación push usuarios dispositivos', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO_DISPOSITIVO';
GO

ALTER TABLE [ADMINISTRACION].[NOTIFICACION_PUSH_USUARIO_DISPOSITIVO]
    ADD CONSTRAINT [FK_NOTIFICACION_PUSH_USUARIO_DISPOSITIVO_DISPOSITIVO] FOREIGN KEY ([DIS_ID]) REFERENCES [AUTENTICACION].[DISPOSITIVO] ([DIS_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el Dispositivo', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO_DISPOSITIVO', @level2type = N'CONSTRAINT', @level2name = N'FK_NOTIFICACION_PUSH_USUARIO_DISPOSITIVO_DISPOSITIVO';
GO

ALTER TABLE [ADMINISTRACION].[NOTIFICACION_PUSH_USUARIO_DISPOSITIVO]
    ADD CONSTRAINT [FK_NOTIFICACION_PUSH_USUARIO_DISPOSITIVO_NOTIFICACION_PUSH_USUARIO] FOREIGN KEY ([NPU_ID]) REFERENCES [ADMINISTRACION].[NOTIFICACION_PUSH_USUARIO] ([NPU_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la notificación Push', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO_DISPOSITIVO', @level2type = N'CONSTRAINT', @level2name = N'FK_NOTIFICACION_PUSH_USUARIO_DISPOSITIVO_NOTIFICACION_PUSH_USUARIO';
GO

