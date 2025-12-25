CREATE TABLE [ADMINISTRACION].[PARROQUIA] (
    [PAR_ID]     INT           IDENTITY (1, 1) NOT NULL,
    [PAR_CODIGO] VARCHAR (10)  NOT NULL,
    [PAR_NOMBRE] VARCHAR (100) NOT NULL,
    [CIU_ID]     INT           NOT NULL,
    [PAR_ESTADO] BIT           NOT NULL,
    CONSTRAINT [PK_PARROQUIA] PRIMARY KEY CLUSTERED ([PAR_ID] ASC),
    CONSTRAINT [FK_PARROQUIA_CIUDAD] FOREIGN KEY ([CIU_ID]) REFERENCES [ADMINISTRACION].[CIUDAD] ([CIU_ID])
);


GO

CREATE NONCLUSTERED INDEX [IX_PARROQUIA_CODIGO]
    ON [ADMINISTRACION].[PARROQUIA]([PAR_CODIGO] ASC);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Parroquia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA', @level2type = N'COLUMN', @level2name = N'PAR_ESTADO';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Parroquia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA', @level2type = N'CONSTRAINT', @level2name = N'PK_PARROQUIA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la Ciudad', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA', @level2type = N'CONSTRAINT', @level2name = N'FK_PARROQUIA_CIUDAD';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Parroquias', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la parroquia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA', @level2type = N'COLUMN', @level2name = N'PAR_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la parroquia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA', @level2type = N'COLUMN', @level2name = N'PAR_NOMBRE';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la parroquia', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PARROQUIA', @level2type = N'COLUMN', @level2name = N'PAR_CODIGO';


GO

