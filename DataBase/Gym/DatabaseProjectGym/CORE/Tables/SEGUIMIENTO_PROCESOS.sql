CREATE TABLE [CORE].[SEGUIMIENTO_PROCESOS] (
    [SPR_ID]                 INT              IDENTITY (1, 1) NOT NULL,
    [SPR_FECHA_REGISTRO]     DATETIME         NOT NULL,
    [SPR_GUID]               UNIQUEIDENTIFIER NOT NULL,
    [PNA_ID]                 INT              NOT NULL,
    [GYM_ID]                 INT              NOT NULL,
    [SPR_PESO]               DECIMAL (5, 2)   NOT NULL,
    [SPR_ALTURA]             DECIMAL (5, 2)   NOT NULL,
    [SPR_GRASA_PORCENTAJE]   DECIMAL (5, 2)   NULL,
    [SPR_MUSCULO_PORCENTAJE] DECIMAL (5, 2)   NULL,
    [SPR_MEDIDA_PECHO]       DECIMAL (5, 2)   NULL,
    [SPR_MEDIDA_CINTURA]     DECIMAL (5, 2)   NULL,
    [SPR_MEDIDA_CADERA]      DECIMAL (5, 2)   NULL,
    [SPR_MEDIDA_BRAZO_DER]   DECIMAL (5, 2)   NULL,
    [SPR_MEDIDA_MUSLO_DER]   DECIMAL (5, 2)   NULL,
    [SPR_OBSERVACIONES]      VARCHAR (1024)   NULL,
    [USR_ID_REGISTRADOR]     INT              NULL,
    CONSTRAINT [PK_SEGUIMIENTO_PROCESOS] PRIMARY KEY CLUSTERED ([SPR_ID] ASC),
    CONSTRAINT [UK_SEGUIMIENTO_PROCESOS_GUID] UNIQUE NONCLUSTERED ([SPR_GUID] ASC)
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Altura de la persona en centímetros', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_ALTURA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del usuario que registró la información', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'USR_ID_REGISTRADOR';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Circunferencia de la cintura en centímetros', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_MEDIDA_CINTURA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la persona (FK a tabla de personas)', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'PNA_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del gimnasio (FK a tabla de gimnasios)', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'GYM_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Circunferencia de la cadera en centímetros', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_MEDIDA_CADERA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de grasa corporal', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_GRASA_PORCENTAJE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del seguimiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_FECHA_REGISTRO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso corporal actual en kilogramos', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_PESO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Circunferencia del muslo derecho en centímetros', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_MEDIDA_MUSLO_DER';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'GUID único para identificación externa', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_GUID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Circunferencia del brazo derecho en centímetros', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_MEDIDA_BRAZO_DER';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de masa muscular', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_MUSCULO_PORCENTAJE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y comentarios del seguimiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_OBSERVACIONES';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Circunferencia del pecho en centímetros', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_MEDIDA_PECHO';


GO

