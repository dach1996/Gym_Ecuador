CREATE TABLE [AUTENTICACION].[DISPOSITIVO] (
    [DIS_ID]                    INT           IDENTITY (1, 1) NOT NULL,
    [DIS_CODIGO_PLATAFORMA]     TINYINT       NOT NULL,
    [DIS_ID_DISPOSITIVO]        VARCHAR (100) NOT NULL,
    [DIS_MODELO]                VARCHAR (100) NOT NULL,
    [DIS_MARCA]                 VARCHAR (250) NOT NULL,
    [DIS_SISTEMA_OPERATIVO]     VARCHAR (50)  NOT NULL,
    [DIS_TIENE_BLOQUEO]         BIT           NOT NULL,
    [DIS_TIENE_SERVICIO_GOOGLE] BIT           NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de Registro', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'DIS_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Plataforma', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'DIS_CODIGO_PLATAFORMA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Dipositivo Creado', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'DIS_ID_DISPOSITIVO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modelo de Dipositivo', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'DIS_MODELO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca del Dispositivo', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'DIS_MARCA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sistema Operativo del Dispositivo', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'DIS_SISTEMA_OPERATIVO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Verifica si el Dispositivo tiene Bloqueo', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'DIS_TIENE_BLOQUEO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Verifica si tiene servicios de Google', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'DISPOSITIVO', @level2type = N'COLUMN', @level2name = N'DIS_TIENE_SERVICIO_GOOGLE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Dipositivos', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'DISPOSITIVO';
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_DIS_ID_DISPOSITIVO]
    ON [AUTENTICACION].[DISPOSITIVO]([DIS_ID_DISPOSITIVO] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice Único para Id de Dipositivo', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'DISPOSITIVO', @level2type = N'INDEX', @level2name = N'IX_DIS_ID_DISPOSITIVO';
GO

ALTER TABLE [AUTENTICACION].[DISPOSITIVO]
    ADD CONSTRAINT [PK_DISPOSITIVO] PRIMARY KEY CLUSTERED ([DIS_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave Primaria de Tabla de Dipositivos', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'DISPOSITIVO', @level2type = N'CONSTRAINT', @level2name = N'PK_DISPOSITIVO';
GO

ALTER TABLE [AUTENTICACION].[DISPOSITIVO]
    ADD CONSTRAINT [UC_DIS_ID_DISPOSITIVO] UNIQUE NONCLUSTERED ([DIS_ID_DISPOSITIVO] ASC);
GO

