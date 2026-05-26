CREATE TABLE [CORE].[SEGUIMIENTO_PROCESOS] (
    [SPR_ID]             INT              IDENTITY (1, 1) NOT NULL,
    [SPR_FECHA_REGISTRO] DATETIME         NOT NULL,
    [SPR_GUID]           UNIQUEIDENTIFIER NOT NULL,
    [SPR_OBSERVACIONES]  VARCHAR (1024)   NULL,
    [EGY_ID]             INT              NULL,
    [USR_ID]             INT              NOT NULL,
    CONSTRAINT [PK_SEGUIMIENTO_PROCESOS] PRIMARY KEY CLUSTERED ([SPR_ID] ASC),
    CONSTRAINT [UK_SEGUIMIENTO_PROCESOS_GUID] UNIQUE NONCLUSTERED ([SPR_GUID] ASC)
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del usuario que registró la información', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'USR_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del seguimiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_FECHA_REGISTRO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'GUID único para identificación externa', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_GUID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y comentarios del seguimiento', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_OBSERVACIONES';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS', @level2type = N'COLUMN', @level2name = N'SPR_ID';


GO
