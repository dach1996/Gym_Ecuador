CREATE TABLE [CORE].[SUCURSAL_GIMNASIO_SERVICIO] (
    [SGS_ID]                   INT              IDENTITY (1, 1) NOT NULL,
    [SGS_GUID]                 UNIQUEIDENTIFIER NOT NULL,
    [SGY_ID]                   INT              NOT NULL,
    [SGS_FECHA_REGISTRO]       DATETIME2 (7)    NULL,
    [SGS_NOMBRE]               NVARCHAR (200)   NOT NULL,
    [SGS_DESCRIPCION]          NVARCHAR (1000)  NULL,
    [SGS_CODIGO]               NVARCHAR (50)    NULL,
    [ITC_TIPO_SERVICIO]        INT              NULL,
    [SGS_PRECIO]               DECIMAL (18, 2)  NULL,
    [SGS_DURACION_MINUTOS]     INT              NULL,
    [SGS_CAPACIDAD_MAXIMA]     INT              NULL,
    [SGS_REQUIERE_RESERVA]     BIT              CONSTRAINT [DEFAULT_SUCURSAL_GIMNASIO_SERVICIO_SGS_REQUIERE_RESERVA] DEFAULT ((0)) NOT NULL,
    [SGS_INCLUIDO_MEMBRESIA]   BIT              CONSTRAINT [DEFAULT_SUCURSAL_GIMNASIO_SERVICIO_SGS_INCLUIDO_MEMBRESIA] DEFAULT ((0)) NOT NULL,
    [SGS_ESTADO]               BIT              CONSTRAINT [DEFAULT_SUCURSAL_GIMNASIO_SERVICIO_SGS_ESTADO] DEFAULT ((1)) NOT NULL,
    [SGS_FECHA_INICIO]         DATETIME2 (7)    NULL,
    [SGS_FECHA_FIN]            DATETIME2 (7)    NULL,
    [SGS_OBSERVACIONES]        NVARCHAR (500)   NULL,
    CONSTRAINT [PK_SUCURSAL_GIMNASIO_SERVICIO] PRIMARY KEY CLUSTERED ([SGS_ID] ASC),
    CONSTRAINT [FK_SUCURSAL_GIMNASIO_SERVICIO_SUCURSAL] FOREIGN KEY ([SGY_ID]) REFERENCES [CORE].[SUCURSAL_GIMNASIO] ([SGY_ID])
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Servicios de Sucursal de Gimnasio', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del servicio (clave primaria).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador global único del servicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_GUID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la sucursal a la que pertenece el servicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGY_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del servicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_FECHA_REGISTRO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del servicio ofrecido.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_NOMBRE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del servicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_DESCRIPCION';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del servicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_CODIGO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio (Item de Catálogo). Ej: Entrenamiento Personal, Clases Grupales, Spa, Nutrición, etc.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'ITC_TIPO_SERVICIO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio del servicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_PRECIO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración estimada del servicio en minutos.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_DURACION_MINUTOS';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Capacidad máxima de personas que pueden usar el servicio simultáneamente.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_CAPACIDAD_MAXIMA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el servicio requiere reserva previa.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_REQUIERE_RESERVA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el servicio está incluido en la membresía.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_INCLUIDO_MEMBRESIA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de actividad del servicio (Activo/Inactivo).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_ESTADO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de disponibilidad del servicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_FECHA_INICIO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de fin de disponibilidad del servicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_FECHA_FIN';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas adicionales sobre el servicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'COLUMN', @level2name = N'SGS_OBSERVACIONES';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Servicios de Sucursal de Gimnasio', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'CONSTRAINT', @level2name = N'PK_SUCURSAL_GIMNASIO_SERVICIO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla de Sucursales de Gimnasio', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SUCURSAL_GIMNASIO_SERVICIO', @level2type = N'CONSTRAINT', @level2name = N'FK_SUCURSAL_GIMNASIO_SERVICIO_SUCURSAL';


GO


