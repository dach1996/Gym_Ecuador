CREATE TABLE [CORE].[RUTINAS] (
    [RUT_ID]                    INT              IDENTITY (1, 1) NOT NULL,
    [RUT_GUID]                 UNIQUEIDENTIFIER NOT NULL,
    [RUT_NOMBRE]               NVARCHAR (200)   NOT NULL,
    [RUT_FECHA_CREACION]        DATETIME         NOT NULL,
    [USR_ID]                   INT              NOT NULL,
    [USR_ID_CREADOR]           INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([RUT_ID] ASC),
    CONSTRAINT [FK_RUTINAS_USUARIO] FOREIGN KEY ([USR_ID]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]),
    CONSTRAINT [FK_RUTINAS_USUARIO_CREADOR] FOREIGN KEY ([USR_ID_CREADOR]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID])
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la rutina (clave primaria).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'RUT_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador global único del registro.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'RUT_GUID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la rutina de ejercicio.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'RUT_NOMBRE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación de la rutina.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'RUT_FECHA_CREACION';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario propietario de la rutina. Hace referencia a AUTENTICACION.USUARIO.USR_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'USR_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que creó la rutina. Hace referencia a AUTENTICACION.USUARIO.USR_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS', @level2type = N'COLUMN', @level2name = N'USR_ID_CREADOR';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de rutinas de ejercicio. Almacena las rutinas creadas por usuarios o administradores y a qué usuario están asignadas.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINAS';


GO
