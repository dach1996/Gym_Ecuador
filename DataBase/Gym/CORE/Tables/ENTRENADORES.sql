CREATE TABLE [CORE].[ENTRENADORES] (
    [ENT_ID]                  INT              IDENTITY (1, 1) NOT NULL,
    [ENT_GUID]                UNIQUEIDENTIFIER NOT NULL,
    [ENT_FECHA_REGISTRO]      DATETIME         NOT NULL,
    [PNA_ID]                  INT              NOT NULL,
    [ENT_BIOGRAFIA]           VARCHAR (2000)   NULL,
    [ENT_ESTADO]              BIT              NOT NULL,
    [USR_ID_REGISTRADOR]      INT              NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del entrenador', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADORES', @level2type = N'COLUMN', @level2name = N'ENT_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guid del entrenador', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADORES', @level2type = N'COLUMN', @level2name = N'ENT_GUID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro del entrenador', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADORES', @level2type = N'COLUMN', @level2name = N'ENT_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la persona', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADORES', @level2type = N'COLUMN', @level2name = N'PNA_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Biografía del entrenador', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADORES', @level2type = N'COLUMN', @level2name = N'ENT_BIOGRAFIA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del entrenador', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADORES', @level2type = N'COLUMN', @level2name = N'ENT_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de usuario registrador', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADORES', @level2type = N'COLUMN', @level2name = N'USR_ID_REGISTRADOR';
GO

ALTER TABLE [CORE].[ENTRENADORES]
    ADD CONSTRAINT [DEFAULT_ENTRENADORES_ENT_GUID] DEFAULT (newid()) FOR [ENT_GUID];
GO

ALTER TABLE [CORE].[ENTRENADORES]
    ADD CONSTRAINT [DEFAULT_ENTRENADORES_ENT_ESTADO] DEFAULT (1) FOR [ENT_ESTADO];
GO

ALTER TABLE [CORE].[ENTRENADORES]
    ADD CONSTRAINT [PK_ENTRENADORES] PRIMARY KEY CLUSTERED ([ENT_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Entrenadores', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADORES', @level2type = N'CONSTRAINT', @level2name = N'PK_ENTRENADORES';
GO

ALTER TABLE [CORE].[ENTRENADORES]
    ADD CONSTRAINT [FK_ENTRENADORES_PERSONA] FOREIGN KEY ([PNA_ID]) REFERENCES [AUTENTICACION].[PERSONA] ([PNA_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla de Persona', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADORES', @level2type = N'CONSTRAINT', @level2name = N'FK_ENTRENADORES_PERSONA';
GO

ALTER TABLE [CORE].[ENTRENADORES]
    ADD CONSTRAINT [FK_ENTRENADORES_USUARIO_REGISTRADOR] FOREIGN KEY ([USR_ID_REGISTRADOR]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con usuario registrador', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADORES', @level2type = N'CONSTRAINT', @level2name = N'FK_ENTRENADORES_USUARIO_REGISTRADOR';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Entrenadores', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADORES';
GO

