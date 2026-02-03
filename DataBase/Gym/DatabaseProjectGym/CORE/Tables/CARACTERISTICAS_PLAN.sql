CREATE TABLE [CORE].[CARACTERISTICAS_PLAN] (
    [CPL_ID]           INT             IDENTITY (1, 1) NOT NULL,
    [PLS_ID]           INT             NOT NULL,
    [CPL_DESCRIPCION]  NVARCHAR (500)  NOT NULL,
    [CPL_TIPO]         TINYINT         NOT NULL,
    CONSTRAINT [PK_CARACTERISTICAS_PLAN] PRIMARY KEY CLUSTERED ([CPL_ID] ASC),
    CONSTRAINT [FK_CARACTERISTICAS_PLAN_PLAN_SUCURSAL] FOREIGN KEY ([PLS_ID]) REFERENCES [CORE].[PLAN_SUCURSAL] ([PLS_ID])
);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Características incluidas o excluidas de cada plan de sucursal', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'CARACTERISTICAS_PLAN';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'CARACTERISTICAS_PLAN', @level2type = N'COLUMN', @level2name = N'CPL_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del plan de sucursal', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'CARACTERISTICAS_PLAN', @level2type = N'COLUMN', @level2name = N'PLS_ID';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la característica', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'CARACTERISTICAS_PLAN', @level2type = N'COLUMN', @level2name = N'CPL_DESCRIPCION';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo: 1 = Incluido, 2 = Excluido', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'CARACTERISTICAS_PLAN', @level2type = N'COLUMN', @level2name = N'CPL_TIPO';


GO
