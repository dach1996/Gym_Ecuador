-- Agregar columna URL_FOTO_PERFIL a la tabla PERSONA
ALTER TABLE [AUTENTICACION].[PERSONA]
    ADD [PNA_URL_FOTO_PERFIL] VARCHAR (500) NULL;
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL de foto de perfil del usuario', @level0type = N'SCHEMA', @level0name = N'AUTENTICACION', @level1type = N'TABLE', @level1name = N'PERSONA', @level2type = N'COLUMN', @level2name = N'PNA_URL_FOTO_PERFIL';
GO
