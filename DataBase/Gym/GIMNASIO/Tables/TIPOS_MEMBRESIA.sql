CREATE TABLE [GIMNASIO].[TIPOS_MEMBRESIA] (
    [TMP_ID]                      INT              IDENTITY (1, 1) NOT NULL,
    [TMP_GUID]                    UNIQUEIDENTIFIER NOT NULL,
    [GYM_ID]                      INT              NOT NULL,
    [TMP_NOMBRE_PLAN]             VARCHAR (150)    NOT NULL,
    [TMP_PRECIO]                  DECIMAL (18, 2)  NOT NULL,
    [TMP_DURACION_DIAS]           INT              NOT NULL,
    [TMP_DESCRIPCION_BENEFICIOS]  VARCHAR (1000)   NULL,
    [TMP_ESTADO]                  BIT              NOT NULL,
    [TMP_FECHA_REGISTRO]          DATETIME         NULL,
    [USR_ID_REGISTRADOR]          INT              NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Tipo de Membresía', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'COLUMN', @level2name = N'TMP_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guid de Tipo de Membresía', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'COLUMN', @level2name = N'TMP_GUID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'COLUMN', @level2name = N'GYM_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del plan', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'COLUMN', @level2name = N'TMP_NOMBRE_PLAN';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio del plan', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'COLUMN', @level2name = N'TMP_PRECIO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en días', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'COLUMN', @level2name = N'TMP_DURACION_DIAS';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de beneficios', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'COLUMN', @level2name = N'TMP_DESCRIPCION_BENEFICIOS';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tipo de membresía', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'COLUMN', @level2name = N'TMP_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'COLUMN', @level2name = N'TMP_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de usuario registrador', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'COLUMN', @level2name = N'USR_ID_REGISTRADOR';
GO

ALTER TABLE [GIMNASIO].[TIPOS_MEMBRESIA]
    ADD CONSTRAINT [DEFAULT_TIPOS_MEMBRESIA_TMP_GUID] DEFAULT (newid()) FOR [TMP_GUID];
GO

ALTER TABLE [GIMNASIO].[TIPOS_MEMBRESIA]
    ADD CONSTRAINT [DEFAULT_TIPOS_MEMBRESIA_TMP_ESTADO] DEFAULT (1) FOR [TMP_ESTADO];
GO

ALTER TABLE [GIMNASIO].[TIPOS_MEMBRESIA]
    ADD CONSTRAINT [PK_TIPOS_MEMBRESIA] PRIMARY KEY CLUSTERED ([TMP_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Tipos de Membresía', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'CONSTRAINT', @level2name = N'PK_TIPOS_MEMBRESIA';
GO

ALTER TABLE [GIMNASIO].[TIPOS_MEMBRESIA]
    ADD CONSTRAINT [FK_TIPOS_MEMBRESIA_GIMNASIO] FOREIGN KEY ([GYM_ID]) REFERENCES [GIMNASIO].[GIMNASIO] ([GYM_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'CONSTRAINT', @level2name = N'FK_TIPOS_MEMBRESIA_GIMNASIO';
GO

ALTER TABLE [GIMNASIO].[TIPOS_MEMBRESIA]
    ADD CONSTRAINT [FK_TIPOS_MEMBRESIA_USUARIO_REGISTRADOR] FOREIGN KEY ([USR_ID_REGISTRADOR]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con usuario registrador', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA', @level2type = N'CONSTRAINT', @level2name = N'FK_TIPOS_MEMBRESIA_USUARIO_REGISTRADOR';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Tipos de Membresía', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'TIPOS_MEMBRESIA';
GO
