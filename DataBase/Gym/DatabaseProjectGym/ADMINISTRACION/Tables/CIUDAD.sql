CREATE TABLE [ADMINISTRACION].[CIUDAD] (
    [CIU_ID]     INT           IDENTITY (1, 1) NOT NULL,
    [CIU_CODIGO] VARCHAR (10)  NOT NULL,
    [CIU_NOMBRE] VARCHAR (100) NOT NULL,
    [PRO_ID]     INT           NOT NULL,
    [CIU_ESTADO] BIT           NOT NULL,
    CONSTRAINT [PK_CIUDAD] PRIMARY KEY CLUSTERED ([CIU_ID] ASC),
    CONSTRAINT [FK_CIUDAD_PROVINCIA] FOREIGN KEY ([PRO_ID]) REFERENCES [ADMINISTRACION].[PROVINCIA] ([PRO_ID])
);


GO

CREATE NONCLUSTERED INDEX [IX_CIUDAD_CODIGO]
    ON [ADMINISTRACION].[CIUDAD]([CIU_CODIGO] ASC);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Ciudad', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD', @level2type = N'COLUMN', @level2name = N'CIU_ESTADO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Ciudad', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD', @level2type = N'CONSTRAINT', @level2name = N'PK_CIUDAD';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la Provincia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD', @level2type = N'CONSTRAINT', @level2name = N'FK_CIUDAD_PROVINCIA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Ciudades', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la ciudad', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD', @level2type = N'COLUMN', @level2name = N'CIU_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la ciudad', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD', @level2type = N'COLUMN', @level2name = N'CIU_NOMBRE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la ciudad', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'CIUDAD', @level2type = N'COLUMN', @level2name = N'CIU_CODIGO';


GO

