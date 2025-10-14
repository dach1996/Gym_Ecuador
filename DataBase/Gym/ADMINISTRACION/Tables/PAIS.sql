CREATE TABLE [ADMINISTRACION].[PAIS] (
    [PAI_ID]     INT           IDENTITY (1, 1) NOT NULL,
    [PAI_CODIGO] VARCHAR (10)  NOT NULL,
    [PAI_NOMBRE] VARCHAR (100) NOT NULL,
    [PAI_ESTADO] BIT           NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del país', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PAIS', @level2type = N'COLUMN', @level2name = N'PAI_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del país', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PAIS', @level2type = N'COLUMN', @level2name = N'PAI_CODIGO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del país', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PAIS', @level2type = N'COLUMN', @level2name = N'PAI_NOMBRE';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Páis', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PAIS', @level2type = N'COLUMN', @level2name = N'PAI_ESTADO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Países', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PAIS';
GO

ALTER TABLE [ADMINISTRACION].[PAIS]
    ADD CONSTRAINT [PK_PAIS] PRIMARY KEY CLUSTERED ([PAI_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de País', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PAIS', @level2type = N'CONSTRAINT', @level2name = N'PK_PAIS';
GO

CREATE NONCLUSTERED INDEX [IX_PAIS_CODIGO]
    ON [ADMINISTRACION].[PAIS]([PAI_CODIGO] ASC);
GO

