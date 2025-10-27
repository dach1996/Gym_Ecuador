CREATE TABLE [GIMNASIO].[MEMBRESIAS] (
    [MEM_ID]              INT              IDENTITY (1, 1) NOT NULL,
    [MEM_GUID]            UNIQUEIDENTIFIER NOT NULL,
    [PNA_ID]              INT              NOT NULL,
    [GYM_ID]              INT              NOT NULL,
    [TMP_ID]              INT              NOT NULL,
    [MEM_FECHA_INICIO]    DATETIME         NOT NULL,
    [MEM_FECHA_FIN]       DATETIME         NOT NULL,
    [MEM_ESTADO]          VARCHAR (50)     NOT NULL,
    [MEM_ROL_GIMNASIO]    VARCHAR (50)     NOT NULL,
    [MEM_FECHA_REGISTRO]  DATETIME         NULL,
    [USR_ID_REGISTRADOR]  INT              NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Membresía', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'COLUMN', @level2name = N'MEM_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guid de Membresía', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'COLUMN', @level2name = N'MEM_GUID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la persona', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'COLUMN', @level2name = N'PNA_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del gimnasio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'COLUMN', @level2name = N'GYM_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de membresía', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'COLUMN', @level2name = N'TMP_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'COLUMN', @level2name = N'MEM_FECHA_INICIO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de fin', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'COLUMN', @level2name = N'MEM_FECHA_FIN';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la membresía (Activa, Vencida, Congelada)', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'COLUMN', @level2name = N'MEM_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rol en el gimnasio (Miembro, Entrenador, Administrador)', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'COLUMN', @level2name = N'MEM_ROL_GIMNASIO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'COLUMN', @level2name = N'MEM_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de usuario registrador', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'COLUMN', @level2name = N'USR_ID_REGISTRADOR';
GO

ALTER TABLE [GIMNASIO].[MEMBRESIAS]
    ADD CONSTRAINT [DEFAULT_MEMBRESIAS_MEM_GUID] DEFAULT (newid()) FOR [MEM_GUID];
GO

ALTER TABLE [GIMNASIO].[MEMBRESIAS]
    ADD CONSTRAINT [PK_MEMBRESIAS] PRIMARY KEY CLUSTERED ([MEM_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Membresías', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS', @level2type = N'CONSTRAINT', @level2name = N'PK_MEMBRESIAS';
GO

ALTER TABLE [GIMNASIO].[MEMBRESIAS]
    ADD CONSTRAINT [FK_MEMBRESIAS_PERSONA] FOREIGN KEY ([PNA_ID]) REFERENCES [AUTENTICACION].[PERSONA] ([PNA_ID]);
GO

ALTER TABLE [GIMNASIO].[MEMBRESIAS]
    ADD CONSTRAINT [FK_MEMBRESIAS_GIMNASIO] FOREIGN KEY ([GYM_ID]) REFERENCES [GIMNASIO].[GIMNASIO] ([GYM_ID]);
GO

ALTER TABLE [GIMNASIO].[MEMBRESIAS]
    ADD CONSTRAINT [FK_MEMBRESIAS_TIPOS_MEMBRESIA] FOREIGN KEY ([TMP_ID]) REFERENCES [GIMNASIO].[TIPOS_MEMBRESIA] ([TMP_ID]);
GO

ALTER TABLE [GIMNASIO].[MEMBRESIAS]
    ADD CONSTRAINT [FK_MEMBRESIAS_USUARIO_REGISTRADOR] FOREIGN KEY ([USR_ID_REGISTRADOR]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Membresías', @level0type = N'SCHEMA', @level0name = N'GIMNASIO', @level1type = N'TABLE', @level1name = N'MEMBRESIAS';
GO
