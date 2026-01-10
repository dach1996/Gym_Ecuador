CREATE TABLE [CORE].[EJERCICIOS] (
    [EJE_ID]                    INT              IDENTITY (1, 1) NOT NULL,
    [EJE_GUID]                 UNIQUEIDENTIFIER NOT NULL,
    [EJE_NOMBRE]               NVARCHAR (200)   NOT NULL,
    [EJE_DESCRIPCION]          NVARCHAR (1000)  NULL,
    [EJE_INSTRUCCIONES]        NVARCHAR (2000)  NULL,
    [ARG_ID]                   INT              NULL,
    PRIMARY KEY CLUSTERED ([EJE_ID] ASC),
    CONSTRAINT [FK_EJERCICIOS_ARCHIVOS_GUARDADOS] FOREIGN KEY ([ARG_ID]) REFERENCES [ADMINISTRACION].[ARCHIVOS_GUARDADOS] ([ARG_ID])
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del ejercicio (clave primaria).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'EJE_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador global único del registro.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'EJE_GUID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del ejercicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'EJE_NOMBRE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del ejercicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'EJE_DESCRIPCION';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones detalladas sobre cómo realizar el ejercicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'EJE_INSTRUCCIONES';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la imagen del ejercicio. Hace referencia a ADMINISTRACION.ARCHIVOS_GUARDADOS.ARG_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS', @level2type = N'COLUMN', @level2name = N'ARG_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de ejercicios disponibles. Almacena información sobre los ejercicios que pueden ser incluidos en las rutinas.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EJERCICIOS';


GO
