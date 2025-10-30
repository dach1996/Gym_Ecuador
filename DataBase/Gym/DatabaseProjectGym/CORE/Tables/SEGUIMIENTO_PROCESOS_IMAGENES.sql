CREATE TABLE [CORE].[SEGUIMIENTO_PROCESOS_IMAGENES] (
    [SPR_ID]             INT      NOT NULL,
    [ARG_ID]             INT      NOT NULL,
    [SPI_FECHA_REGISTRO] DATETIME NOT NULL,
    [USR_ID_REGISTRADOR] INT      NOT NULL,
    CONSTRAINT [PK_SEGUIMIENTO_PROCESOS_IMAGENES] PRIMARY KEY CLUSTERED ([SPR_ID] ASC, [ARG_ID] ASC),
    CONSTRAINT [FK_SEGUIMIENTO_PROCESOS_IMAGENES_ARCHIVOS_GUARDADOS] FOREIGN KEY ([ARG_ID]) REFERENCES [ADMINISTRACION].[ARCHIVOS_GUARDADOS] ([ARG_ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_SEGUIMIENTO_PROCESOS_IMAGENES_SEGUIMIENTO_PROCESOS] FOREIGN KEY ([SPR_ID]) REFERENCES [CORE].[SEGUIMIENTO_PROCESOS] ([SPR_ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del seguimiento de proceso (parte de la llave primaria compuesta). Hace referencia a SEGUIMIENTO_PROCESOS.SPR_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS_IMAGENES', @level2type = N'COLUMN', @level2name = N'SPR_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del archivo guardado (parte de la llave primaria compuesta). Hace referencia a ARCHIVOS_GUARDADOS.ARG_ID. Contiene la imagen del seguimiento de progreso.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS_IMAGENES', @level2type = N'COLUMN', @level2name = N'ARG_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que asoció la imagen al seguimiento. Utilizado para propósitos de auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS_IMAGENES', @level2type = N'COLUMN', @level2name = N'USR_ID_REGISTRADOR';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se asoció la imagen al seguimiento de proceso. Permite llevar un registro temporal de cuándo se agregó cada imagen.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS_IMAGENES', @level2type = N'COLUMN', @level2name = N'SPI_FECHA_REGISTRO';


GO

