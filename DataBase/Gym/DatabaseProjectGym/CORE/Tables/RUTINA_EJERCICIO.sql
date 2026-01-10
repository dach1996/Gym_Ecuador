CREATE TABLE [CORE].[RUTINA_EJERCICIO] (
    [RUE_ID]                    INT IDENTITY (1, 1) NOT NULL,
    [RUT_ID]                   INT NOT NULL,
    [EJE_ID]                   INT NOT NULL,
    [RUE_SERIES]               INT NOT NULL,
    [RUE_REPETICIONES_DESDE]   INT NOT NULL,
    [RUE_REPETICIONES_HASTA]   INT NOT NULL,
    [RUE_SEGUNDOS_DESCANSO]    INT NOT NULL,
    CONSTRAINT [PK_RUTINA_EJERCICIO] PRIMARY KEY CLUSTERED ([RUE_ID] ASC),
    CONSTRAINT [FK_RUTINA_EJERCICIO_RUTINA] FOREIGN KEY ([RUT_ID]) REFERENCES [CORE].[RUTINAS] ([RUT_ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_RUTINA_EJERCICIO_EJERCICIO] FOREIGN KEY ([EJE_ID]) REFERENCES [CORE].[EJERCICIOS] ([EJE_ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la relación rutina-ejercicio (clave primaria).', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUE_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la rutina. Hace referencia a CORE.RUTINAS.RUT_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUT_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ejercicio. Hace referencia a CORE.EJERCICIOS.EJE_ID', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'EJE_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de series a realizar para este ejercicio en la rutina.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUE_SERIES';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rango mínimo de repeticiones a realizar para este ejercicio en la rutina.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUE_REPETICIONES_DESDE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rango máximo de repeticiones a realizar para este ejercicio en la rutina.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUE_REPETICIONES_HASTA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundos de descanso entre series para este ejercicio en la rutina.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO', @level2type = N'COLUMN', @level2name = N'RUE_SEGUNDOS_DESCANSO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación entre Rutinas y Ejercicios. Almacena los parámetros de entrenamiento (series, repeticiones, descanso) para cada ejercicio dentro de una rutina.', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'RUTINA_EJERCICIO';


GO
