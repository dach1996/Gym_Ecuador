CREATE TABLE [ADMINISTRACION].[PROVINCIA] (
    [PRO_ID]     INT           IDENTITY (1, 1) NOT NULL,
    [PRO_CODIGO] VARCHAR (10)  NOT NULL,
    [PRO_NOMBRE] VARCHAR (100) NOT NULL,
    [REG_ID]     INT           NOT NULL,
    [PRO_ESTADO] BIT           NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la provincia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PROVINCIA', @level2type = N'COLUMN', @level2name = N'PRO_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la provincia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PROVINCIA', @level2type = N'COLUMN', @level2name = N'PRO_CODIGO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la provincia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PROVINCIA', @level2type = N'COLUMN', @level2name = N'PRO_NOMBRE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Provincia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PROVINCIA', @level2type = N'COLUMN', @level2name = N'PRO_ESTADO';
GO

CREATE NONCLUSTERED INDEX [IX_PROVINCIA_CODIGO]
    ON [ADMINISTRACION].[PROVINCIA]([PRO_CODIGO] ASC);
GO

ALTER TABLE [ADMINISTRACION].[PROVINCIA]
    ADD CONSTRAINT [FK_PROVINCIA_REGION] FOREIGN KEY ([REG_ID]) REFERENCES [ADMINISTRACION].[REGION] ([REG_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Provincia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PROVINCIA', @level2type = N'CONSTRAINT', @level2name = N'FK_PROVINCIA_REGION';
GO

ALTER TABLE [ADMINISTRACION].[PROVINCIA]
    ADD CONSTRAINT [PK_PROVINCIA] PRIMARY KEY CLUSTERED ([PRO_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Provincia ', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PROVINCIA', @level2type = N'CONSTRAINT', @level2name = N'PK_PROVINCIA';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Provincias', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PROVINCIA';
GO

