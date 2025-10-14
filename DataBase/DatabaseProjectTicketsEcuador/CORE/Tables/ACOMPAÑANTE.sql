CREATE TABLE [CORE].[ACOMPAÑANTE] (
    [USR_ID]             INT   NOT NULL,
    [PNA_ID]             INT   NOT NULL,
    [ACM_FECHA_REGISTRO] DATETIME NOT NULL,
    [ACM_FAVORITO]       BIT      NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Persona', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ACOMPAÑANTE', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ACOMPAÑANTE', @level2type = N'COLUMN', @level2name = N'PNA_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Usuario', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ACOMPAÑANTE', @level2type = N'COLUMN', @level2name = N'ACM_FECHA_REGISTRO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Acompañante es Favorito', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ACOMPAÑANTE', @level2type = N'COLUMN', @level2name = N'ACM_FAVORITO';
GO

ALTER TABLE [CORE].[ACOMPAÑANTE]
    ADD CONSTRAINT [PK_ACOMPAÑANTE] PRIMARY KEY CLUSTERED ([USR_ID] ASC, [PNA_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Compañeros', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ACOMPAÑANTE', @level2type = N'CONSTRAINT', @level2name = N'PK_ACOMPAÑANTE';
GO

CREATE NONCLUSTERED INDEX [IX_PER_ID]
    ON [CORE].[ACOMPAÑANTE]([PNA_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice por Id de Persona', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ACOMPAÑANTE', @level2type = N'INDEX', @level2name = N'IX_PER_ID';
GO

ALTER TABLE [CORE].[ACOMPAÑANTE]
    ADD CONSTRAINT [FK_ACOMPAÑANTE_PERSONA] FOREIGN KEY ([PNA_ID]) REFERENCES [AUTENTICACION].[PERSONA] ([PNA_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con tabla Persona', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ACOMPAÑANTE', @level2type = N'CONSTRAINT', @level2name = N'FK_ACOMPAÑANTE_PERSONA';
GO

ALTER TABLE [CORE].[ACOMPAÑANTE]
    ADD CONSTRAINT [FK_ACOMPAÑANTE_USUARIO] FOREIGN KEY ([USR_ID]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación de tabla Usuario', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ACOMPAÑANTE', @level2type = N'CONSTRAINT', @level2name = N'FK_ACOMPAÑANTE_USUARIO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de ACompañantes', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'ACOMPAÑANTE';
GO

