CREATE TABLE [ADMINISTRACION].[REGION] (
    [REG_ID]     INT           IDENTITY (1, 1) NOT NULL,
    [REG_CODIGO] VARCHAR (10)  NOT NULL,
    [REG_NOMBRE] VARCHAR (100) NOT NULL,
    [PAI_ID]     INT           NOT NULL,
    [REG_ESTADO] BIT           NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la región', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'REGION', @level2type = N'COLUMN', @level2name = N'REG_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la región', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'REGION', @level2type = N'COLUMN', @level2name = N'REG_CODIGO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la región', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'REGION', @level2type = N'COLUMN', @level2name = N'REG_NOMBRE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Región', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'REGION', @level2type = N'COLUMN', @level2name = N'REG_ESTADO';
GO

CREATE NONCLUSTERED INDEX [IX_REGION_CODIGO]
    ON [ADMINISTRACION].[REGION]([REG_CODIGO] ASC);
GO

ALTER TABLE [ADMINISTRACION].[REGION]
    ADD CONSTRAINT [PK_REGION] PRIMARY KEY CLUSTERED ([REG_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Región', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'REGION', @level2type = N'CONSTRAINT', @level2name = N'PK_REGION';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Regiones', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'REGION';
GO

ALTER TABLE [ADMINISTRACION].[REGION]
    ADD CONSTRAINT [FK_REGION_PAIS] FOREIGN KEY ([PAI_ID]) REFERENCES [ADMINISTRACION].[PAIS] ([PAI_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con el País', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'REGION', @level2type = N'CONSTRAINT', @level2name = N'FK_REGION_PAIS';
GO

