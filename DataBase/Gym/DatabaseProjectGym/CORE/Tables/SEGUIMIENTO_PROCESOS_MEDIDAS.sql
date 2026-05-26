CREATE TABLE [CORE].[SEGUIMIENTO_PROCESOS_MEDIDAS] (
    [SPM_ID]    INT            IDENTITY (1, 1) NOT NULL,
    [SPR_ID]    INT            NOT NULL,
    [PAF_ID]    TINYINT        NOT NULL,
    [SPM_VALOR] DECIMAL (5, 2) NOT NULL,
    CONSTRAINT [PK_SEGUIMIENTO_PROCESOS_MEDIDAS] PRIMARY KEY CLUSTERED ([SPM_ID] ASC),
    CONSTRAINT [FK_SEGUIMIENTO_PROCESOS_MEDIDAS_PARAMETROS_FISICOS] FOREIGN KEY ([PAF_ID]) REFERENCES [CORE].[PARAMETROS_FISICOS] ([PAF_ID]),
    CONSTRAINT [FK_SEGUIMIENTO_PROCESOS_MEDIDAS_SEGUIMIENTO_PROCESOS] FOREIGN KEY ([SPR_ID]) REFERENCES [CORE].[SEGUIMIENTO_PROCESOS] ([SPR_ID]) ON DELETE CASCADE
);


GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_SEGUIMIENTO_PROCESOS_MEDIDAS_SPR_PAF]
    ON [CORE].[SEGUIMIENTO_PROCESOS_MEDIDAS]([SPR_ID] ASC, [PAF_ID] ASC);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores de parámetros físicos por registro de seguimiento de proceso', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS_MEDIDAS';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del valor de medida', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS_MEDIDAS', @level2type = N'COLUMN', @level2name = N'SPM_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del seguimiento de proceso', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS_MEDIDAS', @level2type = N'COLUMN', @level2name = N'SPR_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del parámetro físico', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS_MEDIDAS', @level2type = N'COLUMN', @level2name = N'PAF_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor registrado del parámetro', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'SEGUIMIENTO_PROCESOS_MEDIDAS', @level2type = N'COLUMN', @level2name = N'SPM_VALOR';


GO
