CREATE TABLE [AUTENTICACION].[TARJETA] (
    [TTA_ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [TTA_CODIGO_MARCA]        INT            NOT NULL,
    [TTA_BIN]                 VARCHAR (32)  NOT NULL,
    [TTA_PAN]                 VARCHAR (32)  NOT NULL,
    [USR_ID]                  INT         NOT NULL,
    [TTA_ELIMINADO]           BIT            NOT NULL,
    [TTA_CODIGO_TIPO]         INT            NOT NULL,
    [TTA_PROPIETARIO_TARJETA] VARCHAR (128) NOT NULL,
    [TTA_LONGITUD_TARJETA]    INT            NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Tarjeta', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA', @level2type = N'COLUMN', @level2name = N'TTA_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Tarjeta', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA', @level2type = N'COLUMN', @level2name = N'TTA_CODIGO_MARCA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bin de Tarjeta', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA', @level2type = N'COLUMN', @level2name = N'TTA_BIN';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pan de Tarjeta', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA', @level2type = N'COLUMN', @level2name = N'TTA_PAN';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Verifica si la tarjeta está eliminada', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA', @level2type = N'COLUMN', @level2name = N'TTA_ELIMINADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código tipo de Tarjeta', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA', @level2type = N'COLUMN', @level2name = N'TTA_CODIGO_TIPO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Longitud de Tarjeta', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA', @level2type = N'COLUMN', @level2name = N'TTA_LONGITUD_TARJETA';
GO

ALTER TABLE [AUTENTICACION].[TARJETA]
    ADD CONSTRAINT [FK_TARJETA_USUARIO] FOREIGN KEY ([USR_ID]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con tabla Usuarios', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA', @level2type = N'CONSTRAINT', @level2name = N'FK_TARJETA_USUARIO';
GO

ALTER TABLE [AUTENTICACION].[TARJETA]
    ADD CONSTRAINT [PK_TARJETA] PRIMARY KEY CLUSTERED ([TTA_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de tablas Tarjetas', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA', @level2type = N'CONSTRAINT', @level2name = N'PK_TARJETA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Tarjetas', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA';
GO

CREATE NONCLUSTERED INDEX [IX_GET_TARJETAS]
    ON [AUTENTICACION].[TARJETA]([USR_ID] ASC, [TTA_ELIMINADO] ASC)
    INCLUDE([TTA_CODIGO_MARCA], [TTA_CODIGO_TIPO], [TTA_LONGITUD_TARJETA], [TTA_PROPIETARIO_TARJETA], [TTA_PAN], [TTA_BIN]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice para Obtener Tarjetas', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'TARJETA', @level2type = N'INDEX', @level2name = N'IX_GET_TARJETAS';
GO

