CREATE TABLE [AUTENTICACION].[USUARIO] (
    [USR_ID]                                 INT              IDENTITY (1, 1) NOT NULL,
    [USR_FECHA_REGISTRO]                     DATETIME         NOT NULL,
    [USR_USERNAME]                           VARCHAR (100)    NOT NULL,
    [USR_TELEFONO]                           VARCHAR (20)     NULL,
    [USR_CORREO]                             VARCHAR (100)    NOT NULL,
    [USR_FECHA_PRIMER_LOGIN]                 DATETIME         NULL,
    [ARG_ID_IMAGEN_USUARIO]                  INT              NULL,
    [USR_CODIGO_IDIOMA]                      VARCHAR (50)     NOT NULL,
    [USR_INTENTO_LOGIN_FALLIDO]              INT              NULL,
    [USR_FECHA_ULTIMO_INTENTO_LOGIN_FALLIDO] DATETIME         NULL,
    [USR_BLOQUEADO]                          BIT              NOT NULL,
    [PNA_ID]                                 INT              NULL,
    [USR_TIENE_REGISTRO_COMPLETO]            BIT              NOT NULL,
    [USR_SALT]                               VARCHAR (128)    NOT NULL,
    [USR_GUID]                               UNIQUEIDENTIFIER NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro de Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_USERNAME';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_TELEFONO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_CORREO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Primer Login', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_FECHA_PRIMER_LOGIN';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Tabla de Imagenes para Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'ARG_ID_IMAGEN_USUARIO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Idioma Seleccionado', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_CODIGO_IDIOMA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intentos de Logín Fallidos', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_INTENTO_LOGIN_FALLIDO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de último intento de Logín Fallido', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_FECHA_ULTIMO_INTENTO_LOGIN_FALLIDO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Verifica si un usuario está bloqueado', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_BLOQUEADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Persona', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'PNA_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Verifica si tiene registro completo ', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_TIENE_REGISTRO_COMPLETO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salt usado para encripciones por el usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_SALT';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guid de Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'COLUMN', @level2name = N'USR_GUID';
GO

ALTER TABLE [AUTENTICACION].[USUARIO]
    ADD CONSTRAINT [PK_USUARIO] PRIMARY KEY CLUSTERED ([USR_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria del usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'CONSTRAINT', @level2name = N'PK_USUARIO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Usuarios Registrados', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO';
GO

ALTER TABLE [AUTENTICACION].[USUARIO]
    ADD CONSTRAINT [DEFAULT_USUARIO_USR_GUID] DEFAULT (newid()) FOR [USR_GUID];
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_CORREO]
    ON [AUTENTICACION].[USUARIO]([USR_CORREO] ASC)
    INCLUDE([USR_USERNAME]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice por Correo', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'INDEX', @level2name = N'IX_CORREO';
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_USERNAME]
    ON [AUTENTICACION].[USUARIO]([USR_USERNAME] ASC)
    INCLUDE([USR_CORREO]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice por Usuario y Correo', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'INDEX', @level2name = N'IX_USERNAME';
GO

ALTER TABLE [AUTENTICACION].[USUARIO]
    ADD CONSTRAINT [UQ_USERNAME] UNIQUE NONCLUSTERED ([USR_USERNAME] ASC);
GO

ALTER TABLE [AUTENTICACION].[USUARIO]
    ADD CONSTRAINT [UQ_CORREO] UNIQUE NONCLUSTERED ([USR_CORREO] ASC);
GO

ALTER TABLE [AUTENTICACION].[USUARIO]
    ADD CONSTRAINT [FK_USUARIO_ARCHIVOS_GUARDADOS_IMAGEN_USUARIO] FOREIGN KEY ([ARG_ID_IMAGEN_USUARIO]) REFERENCES [ADMINISTRACION].[ARCHIVOS_GUARDADOS] ([ARG_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Tabla de Archivos para verificar Imagen de Usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'USUARIO', @level2type = N'CONSTRAINT', @level2name = N'FK_USUARIO_ARCHIVOS_GUARDADOS_IMAGEN_USUARIO';
GO

