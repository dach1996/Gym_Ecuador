CREATE TABLE [ADMINISTRACION].[NOTIFICACION_PUSH] (
    [NCP_ID]                       INT           IDENTITY (1, 1) NOT NULL,
    [NCP_FECHA_REGISTRO]           DATETIME      NOT NULL,
    [NCP_TITULO]                   VARCHAR (100) NOT NULL,
    [NCP_DESCRIPCION]              VARCHAR (250) NOT NULL,
    [NCP_CODIGO_TIPO_NOTIFICACION] TINYINT       NOT NULL,
    [NCP_VALOR_TIPO_NOTIFICACION]  VARCHAR (50)  NOT NULL,
    [NCP_PERMITIR_VER_USUARIO]     BIT           NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de mensaje push', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH', @level2type = N'COLUMN', @level2name = N'NCP_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Plataforma', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH', @level2type = N'COLUMN', @level2name = N'NCP_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH', @level2type = N'COLUMN', @level2name = N'NCP_TITULO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Título de Notificación Push', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH', @level2type = N'COLUMN', @level2name = N'NCP_DESCRIPCION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de notificación push (Custom = 1, Topic = 2)', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH', @level2type = N'COLUMN', @level2name = N'NCP_CODIGO_TIPO_NOTIFICACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de tipo de Notificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH', @level2type = N'COLUMN', @level2name = N'NCP_VALOR_TIPO_NOTIFICACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Verifica si le permite o no ver al usuario esta notificación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH', @level2type = N'COLUMN', @level2name = N'NCP_PERMITIR_VER_USUARIO';
GO

ALTER TABLE [ADMINISTRACION].[NOTIFICACION_PUSH]
    ADD CONSTRAINT [PK_NOTIFICACION_PUSH] PRIMARY KEY CLUSTERED ([NCP_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave Primaria de Notificaciones Push', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH', @level2type = N'CONSTRAINT', @level2name = N'PK_NOTIFICACION_PUSH';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Notificaciones Push', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'NOTIFICACION_PUSH';
GO

