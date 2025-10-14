CREATE TABLE [ADMINISTRACION].[PISO_DIAGRAMA_COOPERATIVA_BUS] (
    [PDB_ID]                 INT              IDENTITY (1, 1) NOT NULL,
    [PDB_GUID]               UNIQUEIDENTIFIER NOT NULL,
    [CPB_ID]                 INT              NOT NULL,
    [PDB_NUMERO_PISO]        INT              NOT NULL,
    [PDB_IDENTIFICADOR_PISO] VARCHAR (50)     NOT NULL,
    [PDB_DIAGRAMA]           VARCHAR (MAX)    NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del bus y piso relacionada a la cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PISO_DIAGRAMA_COOPERATIVA_BUS', @level2type = N'COLUMN', @level2name = N'PDB_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador Guid', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PISO_DIAGRAMA_COOPERATIVA_BUS', @level2type = N'COLUMN', @level2name = N'PDB_GUID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Bus', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PISO_DIAGRAMA_COOPERATIVA_BUS', @level2type = N'COLUMN', @level2name = N'CPB_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagrama de bus', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PISO_DIAGRAMA_COOPERATIVA_BUS', @level2type = N'COLUMN', @level2name = N'PDB_NUMERO_PISO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Piso de Bus con còdigo de Cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PISO_DIAGRAMA_COOPERATIVA_BUS', @level2type = N'COLUMN', @level2name = N'PDB_IDENTIFICADOR_PISO';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Numero de Piso', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PISO_DIAGRAMA_COOPERATIVA_BUS', @level2type = N'COLUMN', @level2name = N'PDB_DIAGRAMA';
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_UQ_BUS_NUMERO_PISO]
    ON [ADMINISTRACION].[PISO_DIAGRAMA_COOPERATIVA_BUS]([CPB_ID] ASC, [PDB_NUMERO_PISO] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice único para el bus y el número de Piso', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PISO_DIAGRAMA_COOPERATIVA_BUS', @level2type = N'INDEX', @level2name = N'IX_UQ_BUS_NUMERO_PISO';
GO

ALTER TABLE [ADMINISTRACION].[PISO_DIAGRAMA_COOPERATIVA_BUS]
    ADD CONSTRAINT [PK_PISO_DIAGRAMA_COOPERATIVA_BUS] PRIMARY KEY CLUSTERED ([PDB_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria del piso con cooperativa', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PISO_DIAGRAMA_COOPERATIVA_BUS', @level2type = N'CONSTRAINT', @level2name = N'PK_PISO_DIAGRAMA_COOPERATIVA_BUS';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pisos Diagrama en relación a los buses de cooperativas', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'PISO_DIAGRAMA_COOPERATIVA_BUS';
GO

ALTER TABLE [ADMINISTRACION].[PISO_DIAGRAMA_COOPERATIVA_BUS]
    ADD CONSTRAINT [DEFAULT_PISO_DIAGRAMA_COOPERATIVA_BUS_PDB_GUID] DEFAULT (newid()) FOR [PDB_GUID];
GO

