CREATE TABLE [CORE].[GIMNASIO] (
    [GYM_ID]                INT              IDENTITY (1, 1) NOT NULL,
    [GYM_GUID]              UNIQUEIDENTIFIER NOT NULL,
    [GYM_FECHA_REGISTRO]    DATETIME2 (7)    NULL,
    [GYM_NOMBRE]            NVARCHAR (200)   NOT NULL,
    [GYM_DESCRIPCION]       NVARCHAR (1000)  NULL,
    [GYM_DESCRIPCION_CORTA] NVARCHAR (300)   NULL,
    [GYM_DIRECCION]         NVARCHAR (500)   NULL,
    [GYM_TELEFONO]          NVARCHAR (50)    NULL,
    [GYM_EMAIL]             NVARCHAR (200)   NULL,
    [GYM_SITIO_WEB]         NVARCHAR (300)   NULL,
    [GYM_HORARIO_APERTURA]  TIME (7)         NULL,
    [GYM_HORARIO_CIERRE]    TIME (7)         NULL,
    [GYM_LATITUD]           DECIMAL (9, 6)   NULL,
    [GYM_LONGITUD]          DECIMAL (9, 6)   NULL,
    [GYM_ESTADO]            BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([GYM_ID] ASC)
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección física completa del gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_DIRECCION';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del gimnasio (servicios, instalaciones).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_DESCRIPCION';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de cierre diaria del gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_HORARIO_CIERRE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador global único del registro.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_GUID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Longitud para la geolocalización del gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_LONGITUD';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico de contacto del gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_EMAIL';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comercial o razón social del gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_NOMBRE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del gimnasio (clave primaria).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción corta del gimnasio, ideal para resúmenes.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_DESCRIPCION_CORTA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica el estado de actividad del gimnasio (Activo/Inactivo).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_ESTADO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de apertura diaria del gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_HORARIO_APERTURA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Latitud para la geolocalización del gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_LATITUD';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del sitio web del gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_SITIO_WEB';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_FECHA_REGISTRO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono de contacto del gimnasio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'GIMNASIO', @level2type = N'COLUMN', @level2name = N'GYM_TELEFONO';


GO

