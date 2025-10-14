CREATE TABLE [CORE].[MENSAJES_ENCOLADOS] (
    [MEC_ID]                    INT              IDENTITY (1, 1) NOT NULL,
    [MEC_FECHA_REGISTRO]        DATETIME         NOT NULL,
    [MEC_IDENTIFICADOR_INTERNO] UNIQUEIDENTIFIER NOT NULL,
    [MEC_INFORMACION_ADICIONAL] VARCHAR (150)    NOT NULL,
    [MEC_TIPO]                  TINYINT          NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador Interno', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'MENSAJES_ENCOLADOS', @level2type = N'COLUMN', @level2name = N'MEC_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'MENSAJES_ENCOLADOS', @level2type = N'COLUMN', @level2name = N'MEC_IDENTIFICADOR_INTERNO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información Adicional', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'MENSAJES_ENCOLADOS', @level2type = N'COLUMN', @level2name = N'MEC_INFORMACION_ADICIONAL';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Mensaje (Funcionalidad)', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'MENSAJES_ENCOLADOS', @level2type = N'COLUMN', @level2name = N'MEC_TIPO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla para almacenamiento de mensajes encolados de funcionalidades', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'MENSAJES_ENCOLADOS';
GO

ALTER TABLE [CORE].[MENSAJES_ENCOLADOS]
    ADD CONSTRAINT [PK_MENSAJES_ENCOLADOS] PRIMARY KEY CLUSTERED ([MEC_ID] ASC);
GO

