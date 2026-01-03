CREATE TABLE [CORE].[EQUIPAMIENTO] (
    [EQP_ID]                    INT              IDENTITY (1, 1) NOT NULL,
    [EQP_GUID]                 UNIQUEIDENTIFIER NOT NULL,
    [SGY_ID]                   INT              NOT NULL,
    [EQP_NOMBRE]               NVARCHAR (200)   NOT NULL,
    [EQP_DESCRIPCION]          NVARCHAR (1000)  NULL,
    [EQP_TIPO]                 INT              NOT NULL,
    [EQP_ESTADO]                BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([EQP_ID] ASC),
    CONSTRAINT [FK_EQUIPAMIENTO_SUCURSAL_GIMNASIO] FOREIGN KEY ([SGY_ID]) REFERENCES [CORE].[SUCURSAL_GIMNASIO] ([SGY_ID]),
    CONSTRAINT [FK_EQUIPAMIENTO_TIPO_CATALOGO] FOREIGN KEY ([EQP_TIPO]) REFERENCES [ADMINISTRACION].[CATALOGO] ([CAT_ID])
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del equipamiento (clave primaria).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EQUIPAMIENTO', @level2type = N'COLUMN', @level2name = N'EQP_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador global único del registro.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EQUIPAMIENTO', @level2type = N'COLUMN', @level2name = N'EQP_GUID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la sucursal de gimnasio a la que pertenece el equipamiento.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EQUIPAMIENTO', @level2type = N'COLUMN', @level2name = N'SGY_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del equipamiento.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EQUIPAMIENTO', @level2type = N'COLUMN', @level2name = N'EQP_NOMBRE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del equipamiento.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EQUIPAMIENTO', @level2type = N'COLUMN', @level2name = N'EQP_DESCRIPCION';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipamiento en el catálogo.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EQUIPAMIENTO', @level2type = N'COLUMN', @level2name = N'EQP_TIPO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica el estado de actividad del registro (Activo/Inactivo).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EQUIPAMIENTO', @level2type = N'COLUMN', @level2name = N'EQP_ESTADO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de equipamientos de sucursales de gimnasio. Almacena información sobre los equipos disponibles en cada sucursal.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'EQUIPAMIENTO';


GO
