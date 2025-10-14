CREATE TABLE [ADMINISTRACION].[LUGAR_USUARIO] (
    [USR_ID]               INT NOT NULL,
    [LOD_ID]               INT NOT NULL,
    [LUS_FAVORITO_ORIGEN]  BIT NOT NULL,
    [LUS_FAVORITO_DESTINO] BIT NOT NULL
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Lugar Favorito', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR_USUARIO', @level2type = N'COLUMN', @level2name = N'USR_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Usuario ', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR_USUARIO', @level2type = N'COLUMN', @level2name = N'LOD_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar favorito en Origen', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR_USUARIO', @level2type = N'COLUMN', @level2name = N'LUS_FAVORITO_ORIGEN';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar  favorito en Destino', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR_USUARIO', @level2type = N'COLUMN', @level2name = N'LUS_FAVORITO_DESTINO';
GO

CREATE NONCLUSTERED INDEX [IX_LUS_USER_ID]
    ON [ADMINISTRACION].[LUGAR_USUARIO]([USR_ID] ASC)
    INCLUDE([LOD_ID], [LUS_FAVORITO_DESTINO], [LUS_FAVORITO_ORIGEN]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice por Id de Usuario', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR_USUARIO', @level2type = N'INDEX', @level2name = N'IX_LUS_USER_ID';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Lugares Relacionadas al Usuario', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR_USUARIO';
GO

ALTER TABLE [ADMINISTRACION].[LUGAR_USUARIO]
    ADD CONSTRAINT [FK_LUGAR_USUARIO_USUARIO] FOREIGN KEY ([USR_ID]) REFERENCES [AUTENTICACION].[USUARIO] ([USR_ID]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con Usuario', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR_USUARIO', @level2type = N'CONSTRAINT', @level2name = N'FK_LUGAR_USUARIO_USUARIO';
GO

ALTER TABLE [ADMINISTRACION].[LUGAR_USUARIO]
    ADD CONSTRAINT [PK_LUGAR_USUARIO] PRIMARY KEY CLUSTERED ([LOD_ID] ASC, [USR_ID] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave primaria de Tabla Lugar Usuario', @level0type = N'SCHEMA', @level0name = N'ADMINISTRACION', @level1type = N'TABLE', @level1name = N'LUGAR_USUARIO', @level2type = N'CONSTRAINT', @level2name = N'PK_LUGAR_USUARIO';
GO

