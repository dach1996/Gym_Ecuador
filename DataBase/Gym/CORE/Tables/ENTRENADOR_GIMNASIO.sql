CREATE TABLE [CORE].[ENTRENADOR_GIMNASIO] (
    [EGY_ID]                  INT              IDENTITY (1, 1) NOT NULL,
    [ENT_ID]                  INT              NOT NULL,
    [GYM_ID]                  INT              NOT NULL,
    [EGY_FECHA_REGISTRO]      DATETIME         NOT NULL,
    [EGY_ESTADO]              BIT              NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la relación Entrenador-Gimnasio', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADOR_GIMNASIO', @level2type = N'COLUMN', @level2name = N'EGY_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del entrenador', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADOR_GIMNASIO', @level2type = N'COLUMN', @level2name = N'ENT_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del gimnasio', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADOR_GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro de la relación', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADOR_GIMNASIO', @level2type = N'COLUMN', @level2name = N'EGY_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la relación Entrenador-Gimnasio', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADOR_GIMNASIO', @level2type = N'COLUMN', @level2name = N'EGY_ESTADO';
GO

ALTER TABLE [CORE].[ENTRENADOR_GIMNASIO]
    ADD CONSTRAINT [DEFAULT_ENTRENADOR_GIMNASIO_EGY_ESTADO] DEFAULT (1) FOR [EGY_ESTADO];
GO

ALTER TABLE [CORE].[ENTRENADOR_GIMNASIO]
    ADD CONSTRAINT [PK_ENTRENADOR_GIMNASIO] PRIMARY KEY CLUSTERED ([EGY_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Entrenador-Gimnasio', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADOR_GIMNASIO', @level2type = N'CONSTRAINT', @level2name = N'PK_ENTRENADOR_GIMNASIO';
GO

ALTER TABLE [CORE].[ENTRENADOR_GIMNASIO]
    ADD CONSTRAINT [FK_ENTRENADOR_GIMNASIO_ENTRENADOR] FOREIGN KEY ([ENT_ID]) REFERENCES [CORE].[ENTRENADORES] ([ENT_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla de Entrenadores', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADOR_GIMNASIO', @level2type = N'CONSTRAINT', @level2name = N'FK_ENTRENADOR_GIMNASIO_ENTRENADOR';
GO

ALTER TABLE [CORE].[ENTRENADOR_GIMNASIO]
    ADD CONSTRAINT [FK_ENTRENADOR_GIMNASIO_GIMNASIO] FOREIGN KEY ([GYM_ID]) REFERENCES [GIMNASIO].[GIMNASIO] ([GYM_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla de Gimnasio', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADOR_GIMNASIO', @level2type = N'CONSTRAINT', @level2name = N'FK_ENTRENADOR_GIMNASIO_GIMNASIO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación entre Entrenadores y Gimnasios', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ENTRENADOR_GIMNASIO';
GO

