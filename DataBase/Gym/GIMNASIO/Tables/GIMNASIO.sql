CREATE TABLE [GIMNASIO].[GIMNASIO] (
    [GYM_ID]                INT              IDENTITY (1, 1) NOT NULL,
    [GYM_GUID]              UNIQUEIDENTIFIER NOT NULL,
    [GYM_NOMBRE]            VARCHAR (200)    NOT NULL,
    [GYM_DESCRIPCION]       VARCHAR (1000)   NULL,
    [GYM_DESCRIPCION_CORTA] VARCHAR (300)    NULL,
    [GYM_DIRECCION]         VARCHAR (500)    NULL,
    [GYM_TELEFONO]          VARCHAR (50)     NULL,
    [GYM_EMAIL]             VARCHAR (200)    NULL,
    [GYM_SITIO_WEB]         VARCHAR (300)    NULL,
    [GYM_HORARIO_APERTURA]  TIME             NULL,
    [GYM_HORARIO_CIERRE]    TIME             NULL,
    [GYM_LATITUD]           DECIMAL (18, 6)  NULL,
    [GYM_LONGITUD]          DECIMAL (18, 6)  NULL,
    [GYM_ESTADO]            BIT              NOT NULL,
    [GYM_FECHA_REGISTRO]    DATETIME         NULL,
    [USR_ID_REGISTRADOR]    INT              NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guid de Gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_GUID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_NOMBRE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_DESCRIPCION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción corta del gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_DESCRIPCION_CORTA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección del gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_DIRECCION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono del gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_TELEFONO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Email del gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_EMAIL';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio web del gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_SITIO_WEB';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horario de apertura', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_HORARIO_APERTURA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horario de cierre', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_HORARIO_CIERRE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Latitud para localización', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_LATITUD';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Longitud para localización', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_LONGITUD';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de usuario registrador', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'USR_ID_REGISTRADOR';
GO

ALTER TABLE [GIMNASIO].[GIMNASIO]
    ADD CONSTRAINT [DEFAULT_GIMNASIO_GYM_GUID] DEFAULT (newid()) FOR [GYM_GUID];
GO

ALTER TABLE [GIMNASIO].[GIMNASIO]
    ADD CONSTRAINT [DEFAULT_GIMNASIO_GYM_ESTADO] DEFAULT (1) FOR [GYM_ESTADO];
GO

ALTER TABLE [GIMNASIO].[GIMNASIO]
    ADD CONSTRAINT [PK_GIMNASIO] PRIMARY KEY CLUSTERED ([GYM_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'CONSTRAINT', @level2name = N'PK_GIMNASIO';
GO

ALTER TABLE [GIMNASIO].[GIMNASIO]
    ADD CONSTRAINT [FK_GIMNASIO_USUARIO_REGISTRADOR] FOREIGN KEY ([USR_ID_REGISTRADOR]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con usuario registrador', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'CONSTRAINT', @level2name = N'FK_GIMNASIO_USUARIO_REGISTRADOR';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Gimnasios', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'GIMNASIO';
GO
