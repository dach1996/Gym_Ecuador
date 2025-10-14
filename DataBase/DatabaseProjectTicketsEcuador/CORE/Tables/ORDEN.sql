CREATE TABLE [CORE].[ORDEN] (
    [ORD_ID]                         INT              IDENTITY (1, 1) NOT NULL,
    [ORD_GUID]                       UNIQUEIDENTIFIER NOT NULL,
    [RUT_ID]                         INT              NOT NULL,
    [USR_ID]                         INT              NOT NULL,
    [ORD_FECHA_REGISTRO]             DATETIME         NOT NULL,
    [ORD_ESTADO]                     TINYINT          NOT NULL,
    [ORD_FECHA_EXPIRACION]           DATETIME         NULL,
    [MEC_ID]                         INT              NULL,
    [ORD_ULTIMA_FECHA_ACTUALIZACION] DATETIME         NOT NULL,
    [ORD_CONTROL_VERSION]            DATETIME2 (7)    NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Orden generado', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'COLUMN', @level2name = N'ORD_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guid de la órden', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'COLUMN', @level2name = N'ORD_GUID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la ruta', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'COLUMN', @level2name = N'RUT_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Usuario', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'COLUMN', @level2name = N'ORD_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de estación de destino relacionado a la cooperativa', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'COLUMN', @level2name = N'ORD_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de exipración de la Orden', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'COLUMN', @level2name = N'ORD_FECHA_EXPIRACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de mensaje encolado', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'COLUMN', @level2name = N'MEC_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última actualización de Orden', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'COLUMN', @level2name = N'ORD_ULTIMA_FECHA_ACTUALIZACION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha para control de versiones', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'COLUMN', @level2name = N'ORD_CONTROL_VERSION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ordenes de solicitudes', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN';
GO

ALTER TABLE [CORE].[ORDEN]
    ADD CONSTRAINT [FK_ORDEN_MENSAJES_ENCOLADOS] FOREIGN KEY ([MEC_ID]) REFERENCES [CORE].[MENSAJES_ENCOLADOS] ([MEC_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Mensaje Encolado', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'CONSTRAINT', @level2name = N'FK_ORDEN_MENSAJES_ENCOLADOS';
GO

ALTER TABLE [CORE].[ORDEN]
    ADD CONSTRAINT [FK_ORDEN_USUARIO] FOREIGN KEY ([USR_ID]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Usuario', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'CONSTRAINT', @level2name = N'FK_ORDEN_USUARIO';
GO

ALTER TABLE [CORE].[ORDEN]
    ADD CONSTRAINT [PK_ORDEN] PRIMARY KEY CLUSTERED ([ORD_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Orden', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'CONSTRAINT', @level2name = N'PK_ORDEN';
GO

CREATE NONCLUSTERED INDEX [IX_GUID]
    ON [CORE].[ORDEN]([ORD_GUID] ASC)
    INCLUDE([ORD_ID], [ORD_ESTADO], [ORD_CONTROL_VERSION], [MEC_ID], [USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice por el Guid de la Órden', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ORDEN', @level2type = N'INDEX', @level2name = N'IX_GUID';
GO

ALTER TABLE [CORE].[ORDEN]
    ADD CONSTRAINT [DEFAULT_ORDEN_ORD_GUID] DEFAULT (newid()) FOR [ORD_GUID];
GO

