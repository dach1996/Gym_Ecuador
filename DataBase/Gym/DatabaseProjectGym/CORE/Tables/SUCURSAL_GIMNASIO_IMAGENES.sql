CREATE TABLE [CORE].[SUCURSAL_GIMNASIO_IMAGENES] (
    [SGY_ID] INT NOT NULL,
    [ARG_ID] INT NOT NULL,
    CONSTRAINT [PK_SUCURSAL_GIMNASIO_IMAGENES] PRIMARY KEY CLUSTERED ([SGY_ID] ASC, [ARG_ID] ASC),
    CONSTRAINT [FK_SUCURSAL_GIMNASIO_IMAGENES_SUCURSAL_GIMNASIO] FOREIGN KEY ([SGY_ID]) REFERENCES [CORE].[SUCURSAL_GIMNASIO] ([SGY_ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_SUCURSAL_GIMNASIO_IMAGENES_ARCHIVOS_GUARDADOS] FOREIGN KEY ([ARG_ID]) REFERENCES [ADMINISTRACION].[ARCHIVOS_GUARDADOS] ([ARG_ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la sucursal de gimnasio (parte de la llave primaria compuesta). Hace referencia a SUCURSAL_GIMNASIO.SGY_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_IMAGENES', @level2type = N'COLUMN', @level2name = N'SGY_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del archivo guardado (parte de la llave primaria compuesta). Hace referencia a ARCHIVOS_GUARDADOS.ARG_ID. Contiene la imagen de la sucursal de gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_IMAGENES', @level2type = N'COLUMN', @level2name = N'ARG_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación muchos a muchos entre Sucursales de Gimnasio e Imágenes. Permite que una sucursal tenga múltiples imágenes y que una imagen pueda estar asociada a múltiples sucursales.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_IMAGENES';


GO

