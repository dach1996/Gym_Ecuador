CREATE TABLE [CORE].[EJERCICIOS_TAGS] (
    [EJE_ID] INT NOT NULL,
    [CAT_ID] INT NOT NULL,
    CONSTRAINT [PK_EJERCICIOS_TAGS] PRIMARY KEY CLUSTERED ([EJE_ID] ASC, [CAT_ID] ASC),
    CONSTRAINT [FK_EJERCICIOS_TAGS_EJERCICIO] FOREIGN KEY ([EJE_ID]) REFERENCES [CORE].[EJERCICIOS] ([EJE_ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EJERCICIOS_TAGS_CATALOGO] FOREIGN KEY ([CAT_ID]) REFERENCES [ADMINISTRACION].[CATALOGO] ([CAT_ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ejercicio (parte de la llave primaria compuesta). Hace referencia a CORE.EJERCICIOS.EJE_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS_TAGS', @level2type = N'COLUMN', @level2name = N'EJE_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del catálogo/tag/categoría (parte de la llave primaria compuesta). Hace referencia a ADMINISTRACION.CATALOGO.CAT_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS_TAGS', @level2type = N'COLUMN', @level2name = N'CAT_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación muchos a muchos entre Ejercicios y Tags/Categorías del catálogo. Permite que un ejercicio tenga múltiples categorías y que una categoría pueda estar asociada a múltiples ejercicios.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS_TAGS';


GO
