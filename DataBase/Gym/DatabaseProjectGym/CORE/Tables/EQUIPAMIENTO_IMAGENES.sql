CREATE TABLE [CORE].[EQUIPAMIENTO_IMAGENES] (
    [EQP_ID] INT NOT NULL,
    [ARG_ID] INT NOT NULL,
    CONSTRAINT [PK_EQUIPAMIENTO_IMAGENES] PRIMARY KEY CLUSTERED ([EQP_ID] ASC, [ARG_ID] ASC),
    CONSTRAINT [FK_EQUIPAMIENTO_IMAGENES_EQUIPAMIENTO] FOREIGN KEY ([EQP_ID]) REFERENCES [CORE].[EQUIPAMIENTO] ([EQP_ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EQUIPAMIENTO_IMAGENES_ARCHIVOS_GUARDADOS] FOREIGN KEY ([ARG_ID]) REFERENCES [ADMINISTRACION].[ARCHIVOS_GUARDADOS] ([ARG_ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del equipamiento (parte de la llave primaria compuesta). Hace referencia a EQUIPAMIENTO.EQP_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EQUIPAMIENTO_IMAGENES', @level2type = N'COLUMN', @level2name = N'EQP_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del archivo guardado (parte de la llave primaria compuesta). Hace referencia a ARCHIVOS_GUARDADOS.ARG_ID. Contiene la imagen del equipamiento.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EQUIPAMIENTO_IMAGENES', @level2type = N'COLUMN', @level2name = N'ARG_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación muchos a muchos entre Equipamientos e Imágenes. Permite que un equipamiento tenga múltiples imágenes y que una imagen pueda estar asociada a múltiples equipamientos.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EQUIPAMIENTO_IMAGENES';


GO

