CREATE TABLE [ADMINISTRACION].[LOG_AUDITORIA] (
    [LOG_ID]                        INT         IDENTITY (1, 1) NOT NULL,
    [LOG_FECHA]                     DATETIME       NOT NULL,
    [LOG_IP_ORIGEN]                 VARCHAR (50)  NOT NULL,
    [USR_ID]                        INT         NULL,
    [LOG_NOMBRE_USUARIO]            VARCHAR (256) NOT NULL,
    [LOG_OPERACION]                 VARCHAR (50)  NOT NULL,
    [LOG_RESULTADO]                 BIT            NOT NULL,
    [LOG_INFORMACION_REQUERIMIENTO] VARCHAR (MAX) NOT NULL,
    [LOG_INFORMACION_RESPUESTA]     VARCHAR (MAX) NOT NULL,
    [DIS_ID]                        INT            NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Log', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA', @level2type = N'COLUMN', @level2name = N'LOG_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Log', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA', @level2type = N'COLUMN', @level2name = N'LOG_FECHA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ip de Origen de Log', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA', @level2type = N'COLUMN', @level2name = N'LOG_IP_ORIGEN';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del usuario de Contexto', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del Usuario que registra Log', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA', @level2type = N'COLUMN', @level2name = N'LOG_NOMBRE_USUARIO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Operación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA', @level2type = N'COLUMN', @level2name = N'LOG_OPERACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de Operación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA', @level2type = N'COLUMN', @level2name = N'LOG_RESULTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información de Entrada (Request) de Operación', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA', @level2type = N'COLUMN', @level2name = N'LOG_INFORMACION_REQUERIMIENTO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Operación de Salida (Response) de Log', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA', @level2type = N'COLUMN', @level2name = N'LOG_INFORMACION_RESPUESTA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de dispositivo que registró la auditoría', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA', @level2type = N'COLUMN', @level2name = N'DIS_ID';
GO

ALTER TABLE [ADMINISTRACION].[LOG_AUDITORIA]
    ADD CONSTRAINT [PK_LOG_AUDITORIA] PRIMARY KEY CLUSTERED ([LOG_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave Primaria de Tabla de Logs de Auditoría', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA', @level2type = N'CONSTRAINT', @level2name = N'PK_LOG_AUDITORIA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Logs de Auditoría', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LOG_AUDITORIA';
GO

