CREATE TABLE [AUTENTICACION].[FORMA_REGISTRO_USUARIO] (
    [USR_ID]                   INT           NOT NULL,
    [FRU_CODIGO_TIPO_REGISTRO] TINYINT       NOT NULL,
    [FRU_FECHA_REGISTRO]       DATETIME      NOT NULL,
    [FRU_PASSWORD]             VARCHAR (100) NOT NULL,
    [FRU_PASSWORD_TEMPORAL]    VARCHAR (100) NULL,
    [FRU_FECHA_ULTIMO_ACCESO]  DATETIME      NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'FORMA_REGISTRO_USUARIO', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'FORMA_REGISTRO_USUARIO', @level2type = N'COLUMN', @level2name = N'FRU_CODIGO_TIPO_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de registro', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'FORMA_REGISTRO_USUARIO', @level2type = N'COLUMN', @level2name = N'FRU_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña de tipo de registro', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'FORMA_REGISTRO_USUARIO', @level2type = N'COLUMN', @level2name = N'FRU_PASSWORD';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña temporal de tipo de registro', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'FORMA_REGISTRO_USUARIO', @level2type = N'COLUMN', @level2name = N'FRU_PASSWORD_TEMPORAL';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de último acceso', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'FORMA_REGISTRO_USUARIO', @level2type = N'COLUMN', @level2name = N'FRU_FECHA_ULTIMO_ACCESO';
GO

ALTER TABLE [AUTENTICACION].[FORMA_REGISTRO_USUARIO]
    ADD CONSTRAINT [PK_FORMA_REGISTRO_USUARIO] PRIMARY KEY CLUSTERED ([USR_ID] ASC, [FRU_CODIGO_TIPO_REGISTRO] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Forma de Registro de Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'FORMA_REGISTRO_USUARIO', @level2type = N'CONSTRAINT', @level2name = N'PK_FORMA_REGISTRO_USUARIO';
GO

ALTER TABLE [AUTENTICACION].[FORMA_REGISTRO_USUARIO]
    ADD CONSTRAINT [FK_FORMA_REGISTRO_USUARIO] FOREIGN KEY ([USR_ID]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'FORMA_REGISTRO_USUARIO', @level2type = N'CONSTRAINT', @level2name = N'FK_FORMA_REGISTRO_USUARIO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Forma Registro de Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'FORMA_REGISTRO_USUARIO';
GO

