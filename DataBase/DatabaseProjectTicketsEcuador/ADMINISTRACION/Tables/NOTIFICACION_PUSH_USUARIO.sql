CREATE TABLE [ADMINISTRACION].[NOTIFICACION_PUSH_USUARIO] (
    [NPU_ID]          INT IDENTITY (1, 1) NOT NULL,
    [USR_ID]          INT NOT NULL,
    [NCP_ID]          INT NOT NULL,
    [NPU_TIENE_VISTA] BIT NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de NPU', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO', @level2type = N'COLUMN', @level2name = N'NPU_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de usuario al cuál se envió la notificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Notificación Push', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO', @level2type = N'COLUMN', @level2name = N'NCP_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Verifica si el usuario de la app móvil ha visto la notificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO', @level2type = N'COLUMN', @level2name = N'NPU_TIENE_VISTA';
GO

ALTER TABLE [ADMINISTRACION].[NOTIFICACION_PUSH_USUARIO]
    ADD CONSTRAINT [FK_NOTIFICACION_PUSH_USUARIO_USUARIO] FOREIGN KEY ([USR_ID]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el Usuario', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO', @level2type = N'CONSTRAINT', @level2name = N'FK_NOTIFICACION_PUSH_USUARIO_USUARIO';
GO

ALTER TABLE [ADMINISTRACION].[NOTIFICACION_PUSH_USUARIO]
    ADD CONSTRAINT [FK_NOTIFICACION_PUSH_USUARIO_NOTIFICACION_PUSH] FOREIGN KEY ([NCP_ID]) REFERENCES [ADMINISTRACION].[NOTIFICACION_PUSH] ([NCP_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la Notificación ', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO', @level2type = N'CONSTRAINT', @level2name = N'FK_NOTIFICACION_PUSH_USUARIO_NOTIFICACION_PUSH';
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_NOTIFICACION_USUARIO]
    ON [ADMINISTRACION].[NOTIFICACION_PUSH_USUARIO]([NCP_ID] ASC, [USR_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice único de Notificación y usuario', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO', @level2type = N'INDEX', @level2name = N'UQ_NOTIFICACION_USUARIO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Notificaciones enviadas al Usuario', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO';
GO

ALTER TABLE [ADMINISTRACION].[NOTIFICACION_PUSH_USUARIO]
    ADD CONSTRAINT [PK_NOTIFICACION_PUSH_USUARIO] PRIMARY KEY CLUSTERED ([NPU_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Notificaciones push de Usuario', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH_USUARIO', @level2type = N'CONSTRAINT', @level2name = N'PK_NOTIFICACION_PUSH_USUARIO';
GO

