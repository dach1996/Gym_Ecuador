CREATE TABLE [CORE].[REGISTRO_SERIES] (
    [SER_ID]                    INT              IDENTITY (1, 1) NOT NULL,
    [SER_GUID]                 UNIQUEIDENTIFIER NOT NULL,
    [EJE_ID]                   INT              NOT NULL,
    [SER_FECHA_REGISTRO]       DATETIME         NOT NULL,
    [USR_ID]                   INT              NOT NULL,
    [SER_PESO]                 DECIMAL (5, 2)   NULL,
    [SER_REPETICIONES]         INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([SER_ID] ASC),
    CONSTRAINT [FK_REGISTRO_SERIES_EJERCICIO] FOREIGN KEY ([EJE_ID]) REFERENCES [CORE].[EJERCICIOS] ([EJE_ID]),
    CONSTRAINT [FK_REGISTRO_SERIES_USUARIO] FOREIGN KEY ([USR_ID]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID])
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de serie (clave primaria).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'SER_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador global único del registro.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'SER_GUID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ejercicio realizado. Hace referencia a CORE.EJERCICIOS.EJE_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'EJE_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó el registro de la serie.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'SER_FECHA_REGISTRO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que realizó la serie. Hace referencia a AUTENTICACION.USUARIO.USR_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'USR_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso utilizado en kilogramos (opcional, puede ser NULL para ejercicios sin peso).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'SER_PESO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de repeticiones realizadas en la serie.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES', @level2type = N'COLUMN', @level2name = N'SER_REPETICIONES';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de registro de series realizadas. Almacena el historial de entrenamientos de los usuarios con información sobre peso, repeticiones y fecha.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'REGISTRO_SERIES';


GO
