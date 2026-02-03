-- =================================================================
-- Script de creación de tabla CARACTERISTICAS_PLAN
-- Define las características incluidas o excluidas de cada plan
-- (1 = Incluido, 2 = Excluido)
--
-- Depende de: CORE.PLAN_SUCURSAL
-- Schema: CORE
-- =================================================================

USE [Gym]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

BEGIN TRANSACTION
GO

BEGIN TRY
    PRINT '================================================================='
    PRINT 'Creando tabla CORE.CARACTERISTICAS_PLAN'
    PRINT '================================================================='
    PRINT ''

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CORE].[CARACTERISTICAS_PLAN]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [CORE].[CARACTERISTICAS_PLAN] (
            [CPL_ID]           INT             IDENTITY(1,1) NOT NULL,
            [PLS_ID]           INT             NOT NULL,
            [CPL_DESCRIPCION]  NVARCHAR(500)   NOT NULL,
            [CPL_TIPO]         TINYINT         NOT NULL,

            CONSTRAINT [PK_CARACTERISTICAS_PLAN] PRIMARY KEY CLUSTERED ([CPL_ID] ASC),
            CONSTRAINT [FK_CARACTERISTICAS_PLAN_PLAN_SUCURSAL] FOREIGN KEY ([PLS_ID])
                REFERENCES [CORE].[PLAN_SUCURSAL]([PLS_ID])
        );

        PRINT '  ✓ Tabla CORE.CARACTERISTICAS_PLAN creada exitosamente.'
    END
    ELSE
    BEGIN
        PRINT '  - La tabla CORE.CARACTERISTICAS_PLAN ya existe.'
    END

    -- Índice por plan (consultas por plan)
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CARACTERISTICAS_PLAN_PLAN' AND object_id = OBJECT_ID('CORE.CARACTERISTICAS_PLAN'))
    BEGIN
        CREATE NONCLUSTERED INDEX [IX_CARACTERISTICAS_PLAN_PLAN]
        ON [CORE].[CARACTERISTICAS_PLAN] ([PLS_ID]);
        PRINT '  ✓ Índice por plan creado.'
    END

    -- Descripción extendida de la tabla
    IF NOT EXISTS (
        SELECT 1 FROM sys.extended_properties
        WHERE major_id = OBJECT_ID(N'[CORE].[CARACTERISTICAS_PLAN]')
          AND minor_id = 0
          AND name = N'MS_Description'
    )
    BEGIN
        EXECUTE sp_addextendedproperty
            @name = N'MS_Description',
            @value = N'Características incluidas o excluidas de cada plan de sucursal',
            @level0type = N'SCHEMA', @level0name = N'CORE',
            @level1type = N'TABLE',  @level1name = N'CARACTERISTICAS_PLAN';
        PRINT '  ✓ Descripción de tabla agregada.'
    END

    -- Descripción de columnas
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id = OBJECT_ID(N'[CORE].[CARACTERISTICAS_PLAN]') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID(N'[CORE].[CARACTERISTICAS_PLAN]') AND name = N'CPL_ID') AND name = N'MS_Description')
        EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'CARACTERISTICAS_PLAN', @level2type = N'COLUMN', @level2name = N'CPL_ID';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id = OBJECT_ID(N'[CORE].[CARACTERISTICAS_PLAN]') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID(N'[CORE].[CARACTERISTICAS_PLAN]') AND name = N'PLS_ID') AND name = N'MS_Description')
        EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del plan de sucursal', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'CARACTERISTICAS_PLAN', @level2type = N'COLUMN', @level2name = N'PLS_ID';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id = OBJECT_ID(N'[CORE].[CARACTERISTICAS_PLAN]') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID(N'[CORE].[CARACTERISTICAS_PLAN]') AND name = N'CPL_DESCRIPCION') AND name = N'MS_Description')
        EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la característica', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'CARACTERISTICAS_PLAN', @level2type = N'COLUMN', @level2name = N'CPL_DESCRIPCION';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id = OBJECT_ID(N'[CORE].[CARACTERISTICAS_PLAN]') AND minor_id = (SELECT column_id FROM sys.columns WHERE object_id = OBJECT_ID(N'[CORE].[CARACTERISTICAS_PLAN]') AND name = N'CPL_TIPO') AND name = N'MS_Description')
        EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo: 1 = Incluido, 2 = Excluido', @level0type = N'SCHEMA', @level0name = N'CORE', @level1type = N'TABLE', @level1name = N'CARACTERISTICAS_PLAN', @level2type = N'COLUMN', @level2name = N'CPL_TIPO';

    COMMIT TRANSACTION

    PRINT ''
    PRINT '================================================================='
    PRINT 'Script completado correctamente.'
    PRINT '================================================================='

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();

    PRINT ''
    PRINT 'ERROR: ' + @ErrorMessage;
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH
GO
