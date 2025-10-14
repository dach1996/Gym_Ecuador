CREATE TABLE [AUTENTICACION].[USUARIO_DISPOSITIVO] (
    [USR_ID]                   INT           NOT NULL,
    [DIS_ID]                   INT           NOT NULL,
    [UDR_FECHA_REGISTRO]       DATETIME      NOT NULL,
    [UDR_FECHA_ULTIMO_INGRESO] DATETIME      NOT NULL,
    [UDR_BIOMETRICO]           VARCHAR (150) NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Dispositivo', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'DIS_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fécha de Registro', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'UDR_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fécha de último ingreso en el dispositivo relacionado al usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'UDR_FECHA_ULTIMO_INGRESO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Biométrico registrado', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'UDR_BIOMETRICO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Relación Usuario con Dispositivos', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO';
GO

ALTER TABLE [AUTENTICACION].[USUARIO_DISPOSITIVO]
    ADD CONSTRAINT [FK_USUARIO_DISPOSITIVO_DISPOSITIVO] FOREIGN KEY ([DIS_ID]) REFERENCES [AUTENTICACION].[DISPOSITIVO] ([DIS_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Tabla de Dipositivos (Id de Dispositivo)', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO', @level2type = N'CONSTRAINT', @level2name = N'FK_USUARIO_DISPOSITIVO_DISPOSITIVO';
GO

ALTER TABLE [AUTENTICACION].[USUARIO_DISPOSITIVO]
    ADD CONSTRAINT [FK_USUARIO_DISPOSITIVO_USUARIO] FOREIGN KEY ([USR_ID]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Tabla de Usuarios (Id de Usuario)', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO', @level2type = N'CONSTRAINT', @level2name = N'FK_USUARIO_DISPOSITIVO_USUARIO';
GO

ALTER TABLE [AUTENTICACION].[USUARIO_DISPOSITIVO]
    ADD CONSTRAINT [PK_USUARIO_DISPOSITIVO] PRIMARY KEY CLUSTERED ([USR_ID] ASC, [DIS_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave Primaria de Usuario con Dipositivo', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO_DISPOSITIVO', @level2type = N'CONSTRAINT', @level2name = N'PK_USUARIO_DISPOSITIVO';
GO

