CREATE TABLE [GIMNASIO].[FOTOS_GIMNASIO] (
    [FGY_ID]              INT              IDENTITY (1, 1) NOT NULL,
    [FGY_GUID]            UNIQUEIDENTIFIER NOT NULL,
    [GYM_ID]              INT              NOT NULL,
    [FGY_URL_IMAGEN]      VARCHAR (500)    NOT NULL,
    [FGY_DESCRIPCION]     VARCHAR (300)    NULL,
    [FGY_ES_PRINCIPAL]    BIT              NOT NULL,
    [FGY_ESTADO]          BIT              NOT NULL,
    [FGY_FECHA_REGISTRO]  DATETIME         NULL,
    [USR_ID_REGISTRADOR]  INT              NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Foto de Gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'FOTOS_GIMNASIO', @level2type = N'COLUMN', @level2name = N'FGY_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guid de Foto de Gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'FOTOS_GIMNASIO', @level2type = N'COLUMN', @level2name = N'FGY_GUID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'FOTOS_GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL de la imagen', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'FOTOS_GIMNASIO', @level2type = N'COLUMN', @level2name = N'FGY_URL_IMAGEN';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la imagen', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'FOTOS_GIMNASIO', @level2type = N'COLUMN', @level2name = N'FGY_DESCRIPCION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es imagen principal', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'FOTOS_GIMNASIO', @level2type = N'COLUMN', @level2name = N'FGY_ES_PRINCIPAL';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la foto', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'FOTOS_GIMNASIO', @level2type = N'COLUMN', @level2name = N'FGY_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'FOTOS_GIMNASIO', @level2type = N'COLUMN', @level2name = N'FGY_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de usuario registrador', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'FOTOS_GIMNASIO', @level2type = N'COLUMN', @level2name = N'USR_ID_REGISTRADOR';
GO

ALTER TABLE [GIMNASIO].[FOTOS_GIMNASIO]
    ADD CONSTRAINT [DEFAULT_FOTOS_GIMNASIO_FGY_GUID] DEFAULT (newid()) FOR [FGY_GUID];
GO

ALTER TABLE [GIMNASIO].[FOTOS_GIMNASIO]
    ADD CONSTRAINT [DEFAULT_FOTOS_GIMNASIO_FGY_ES_PRINCIPAL] DEFAULT (0) FOR [FGY_ES_PRINCIPAL];
GO

ALTER TABLE [GIMNASIO].[FOTOS_GIMNASIO]
    ADD CONSTRAINT [DEFAULT_FOTOS_GIMNASIO_FGY_ESTADO] DEFAULT (1) FOR [FGY_ESTADO];
GO

ALTER TABLE [GIMNASIO].[FOTOS_GIMNASIO]
    ADD CONSTRAINT [PK_FOTOS_GIMNASIO] PRIMARY KEY CLUSTERED ([FGY_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Fotos de Gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'FOTOS_GIMNASIO', @level2type = N'CONSTRAINT', @level2name = N'PK_FOTOS_GIMNASIO';
GO

ALTER TABLE [GIMNASIO].[FOTOS_GIMNASIO]
    ADD CONSTRAINT [FK_FOTOS_GIMNASIO_GIMNASIO] FOREIGN KEY ([GYM_ID]) REFERENCES [GIMNASIO].[GIMNASIO] ([GYM_ID]);
GO

ALTER TABLE [GIMNASIO].[FOTOS_GIMNASIO]
    ADD CONSTRAINT [FK_FOTOS_GIMNASIO_USUARIO_REGISTRADOR] FOREIGN KEY ([USR_ID_REGISTRADOR]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Fotos de Gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'FOTOS_GIMNASIO';
GO
