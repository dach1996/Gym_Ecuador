-- =================================================================
-- Alteración: PAF_ID INT -> TINYINT en PARAMETROS_FISICOS
--             y PAF_ID en SEGUIMIENTO_PROCESOS_MEDIDAS
-- Ejecutar después de Script.MigrateProcessTrackingMeasurements.sql
-- Los PAF_ID quedan alineados con PhysicalParameterCode (: byte)
-- =================================================================

USE [Gym];
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

BEGIN TRY
    -- Recuperación: ejecución anterior falló tras DROP de PARAMETROS_FISICOS
    IF OBJECT_ID(N'CORE.PARAMETROS_FISICOS', N'U') IS NULL
       AND OBJECT_ID(N'CORE.PARAMETROS_FISICOS_NEW', N'U') IS NOT NULL
    BEGIN
        BEGIN TRANSACTION;

        PRINT 'Recuperando: finalizando conversión desde PARAMETROS_FISICOS_NEW...';

        CREATE TABLE [CORE].[PARAMETROS_FISICOS] (
            [PAF_ID]     TINYINT        NOT NULL,
            [PAF_CODIGO] VARCHAR (64)   NOT NULL,
            [PAF_NOMBRE] NVARCHAR (128) NOT NULL,
            [PAF_ESTADO] BIT            NOT NULL CONSTRAINT [DF_PARAMETROS_FISICOS_ESTADO] DEFAULT (1),
            CONSTRAINT [PK_PARAMETROS_FISICOS] PRIMARY KEY CLUSTERED ([PAF_ID] ASC)
        );

        INSERT INTO [CORE].[PARAMETROS_FISICOS] ([PAF_ID], [PAF_CODIGO], [PAF_NOMBRE], [PAF_ESTADO])
        SELECT [PAF_ID], [PAF_CODIGO], [PAF_NOMBRE], [PAF_ESTADO]
        FROM [CORE].[PARAMETROS_FISICOS_NEW];

        DROP TABLE [CORE].[PARAMETROS_FISICOS_NEW];

        CREATE UNIQUE NONCLUSTERED INDEX [UQ_PARAMETROS_FISICOS_CODIGO]
            ON [CORE].[PARAMETROS_FISICOS]([PAF_CODIGO] ASC);

        IF OBJECT_ID(N'CORE.SEGUIMIENTO_PROCESOS_MEDIDAS', N'U') IS NOT NULL
        BEGIN
            IF NOT EXISTS (
                SELECT 1 FROM sys.indexes
                WHERE name = N'UQ_SEGUIMIENTO_PROCESOS_MEDIDAS_SPR_PAF'
                  AND object_id = OBJECT_ID(N'CORE.SEGUIMIENTO_PROCESOS_MEDIDAS'))
            BEGIN
                CREATE UNIQUE NONCLUSTERED INDEX [UQ_SEGUIMIENTO_PROCESOS_MEDIDAS_SPR_PAF]
                    ON [CORE].[SEGUIMIENTO_PROCESOS_MEDIDAS]([SPR_ID] ASC, [PAF_ID] ASC);
            END

            IF NOT EXISTS (
                SELECT 1 FROM sys.foreign_keys
                WHERE name = N'FK_SEGUIMIENTO_PROCESOS_MEDIDAS_PARAMETROS_FISICOS'
                  AND parent_object_id = OBJECT_ID(N'CORE.SEGUIMIENTO_PROCESOS_MEDIDAS'))
            BEGIN
                ALTER TABLE [CORE].[SEGUIMIENTO_PROCESOS_MEDIDAS]
                    ADD CONSTRAINT [FK_SEGUIMIENTO_PROCESOS_MEDIDAS_PARAMETROS_FISICOS]
                    FOREIGN KEY ([PAF_ID]) REFERENCES [CORE].[PARAMETROS_FISICOS] ([PAF_ID]);
            END
        END

        COMMIT TRANSACTION;
        PRINT 'Recuperación completada correctamente.';
        RETURN;
    END

    IF OBJECT_ID(N'CORE.PARAMETROS_FISICOS', N'U') IS NULL
        RAISERROR('La tabla CORE.PARAMETROS_FISICOS no existe. Ejecute primero Script.MigrateProcessTrackingMeasurements.sql', 16, 1);

    IF EXISTS (SELECT 1 FROM [CORE].[PARAMETROS_FISICOS] WHERE [PAF_ID] > 255)
        RAISERROR('Existen PAF_ID fuera del rango TINYINT (0-255).', 16, 1);

    DECLARE @pafIdType SYSNAME = (
        SELECT ty.name
        FROM sys.columns c
        INNER JOIN sys.types ty ON c.user_type_id = ty.user_type_id
        WHERE c.object_id = OBJECT_ID(N'CORE.PARAMETROS_FISICOS')
          AND c.name = N'PAF_ID');

    IF @pafIdType = 'tinyint'
    BEGIN
        PRINT 'PAF_ID ya es TINYINT; no se requiere conversión.';
        RETURN;
    END

    BEGIN TRANSACTION;

    PRINT 'Iniciando conversión de PAF_ID a TINYINT...';

    -- 1. Eliminar FKs que referencian PARAMETROS_FISICOS (cualquier nombre)
    DECLARE @dropFkSql NVARCHAR(MAX) = N'';
    SELECT @dropFkSql += N'ALTER TABLE '
        + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
        + N' DROP CONSTRAINT ' + QUOTENAME(fk.name) + N';' + CHAR(13)
    FROM sys.foreign_keys fk
    WHERE fk.referenced_object_id = OBJECT_ID(N'CORE.PARAMETROS_FISICOS');

    IF LEN(@dropFkSql) > 0
    BEGIN
        EXEC sp_executesql @dropFkSql;
        PRINT 'FKs hacia PARAMETROS_FISICOS eliminadas.';
    END

    -- 2. Eliminar índices en SEGUIMIENTO_PROCESOS_MEDIDAS que incluyen PAF_ID
    IF OBJECT_ID(N'CORE.SEGUIMIENTO_PROCESOS_MEDIDAS', N'U') IS NOT NULL
    BEGIN
        DECLARE @dropIndexSql NVARCHAR(MAX) = N'';
        SELECT @dropIndexSql += N'DROP INDEX '
            + QUOTENAME(i.name) + N' ON '
            + QUOTENAME(OBJECT_SCHEMA_NAME(i.object_id)) + N'.' + QUOTENAME(OBJECT_NAME(i.object_id)) + N';' + CHAR(13)
        FROM sys.indexes i
        INNER JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
        INNER JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
        WHERE i.object_id = OBJECT_ID(N'CORE.SEGUIMIENTO_PROCESOS_MEDIDAS')
          AND c.name = N'PAF_ID'
          AND i.is_primary_key = 0
          AND i.type > 0;

        IF LEN(@dropIndexSql) > 0
        BEGIN
            EXEC sp_executesql @dropIndexSql;
            PRINT 'Índices sobre PAF_ID eliminados en SEGUIMIENTO_PROCESOS_MEDIDAS.';
        END

        ALTER TABLE [CORE].[SEGUIMIENTO_PROCESOS_MEDIDAS]
            ALTER COLUMN [PAF_ID] TINYINT NOT NULL;
        PRINT 'PAF_ID convertido a TINYINT en SEGUIMIENTO_PROCESOS_MEDIDAS.';
    END

    -- 3. Copiar catálogo a tabla temporal con IDs TINYINT fijos (enum : byte)
    IF OBJECT_ID(N'CORE.PARAMETROS_FISICOS_NEW', N'U') IS NOT NULL
        DROP TABLE [CORE].[PARAMETROS_FISICOS_NEW];

    CREATE TABLE [CORE].[PARAMETROS_FISICOS_NEW] (
        [PAF_ID]     TINYINT        NOT NULL,
        [PAF_CODIGO] VARCHAR (64)   NOT NULL,
        [PAF_NOMBRE] NVARCHAR (128) NOT NULL,
        [PAF_ESTADO] BIT            NOT NULL
    );

    INSERT INTO [CORE].[PARAMETROS_FISICOS_NEW] ([PAF_ID], [PAF_CODIGO], [PAF_NOMBRE], [PAF_ESTADO])
    SELECT
        CASE [PAF_CODIGO]
            WHEN 'PESO'                 THEN CAST(1 AS TINYINT)
            WHEN 'ALTURA'               THEN CAST(2 AS TINYINT)
            WHEN 'GRASA_PORCENTAJE'     THEN CAST(3 AS TINYINT)
            WHEN 'MUSCULO_PORCENTAJE'   THEN CAST(4 AS TINYINT)
            WHEN 'MEDIDA_PECHO'         THEN CAST(5 AS TINYINT)
            WHEN 'MEDIDA_CINTURA'       THEN CAST(6 AS TINYINT)
            WHEN 'MEDIDA_CADERA'        THEN CAST(7 AS TINYINT)
            WHEN 'MEDIDA_BRAZO_DER'     THEN CAST(8 AS TINYINT)
            WHEN 'MEDIDA_MUSLO_DER'     THEN CAST(9 AS TINYINT)
            WHEN 'IMC'                  THEN CAST(10 AS TINYINT)
            ELSE CAST([PAF_ID] AS TINYINT)
        END,
        [PAF_CODIGO],
        [PAF_NOMBRE],
        [PAF_ESTADO]
    FROM [CORE].[PARAMETROS_FISICOS];

    IF NOT EXISTS (SELECT 1 FROM [CORE].[PARAMETROS_FISICOS_NEW] WHERE [PAF_CODIGO] = 'IMC')
    BEGIN
        INSERT INTO [CORE].[PARAMETROS_FISICOS_NEW] ([PAF_ID], [PAF_CODIGO], [PAF_NOMBRE], [PAF_ESTADO])
        VALUES (10, 'IMC', N'Índice de masa corporal', 1);
    END

    -- 4. Actualizar referencias en medidas según código del parámetro
    IF OBJECT_ID(N'CORE.SEGUIMIENTO_PROCESOS_MEDIDAS', N'U') IS NOT NULL
    BEGIN
        UPDATE spm
        SET spm.[PAF_ID] = pafNew.[PAF_ID]
        FROM [CORE].[SEGUIMIENTO_PROCESOS_MEDIDAS] spm
        INNER JOIN [CORE].[PARAMETROS_FISICOS] pafOld ON pafOld.[PAF_ID] = spm.[PAF_ID]
        INNER JOIN [CORE].[PARAMETROS_FISICOS_NEW] pafNew ON pafNew.[PAF_CODIGO] = pafOld.[PAF_CODIGO];

        PRINT 'Referencias PAF_ID actualizadas en SEGUIMIENTO_PROCESOS_MEDIDAS.';
    END

    -- 5. Reemplazar tabla catálogo (sin sp_rename)
    DROP TABLE [CORE].[PARAMETROS_FISICOS];

    CREATE TABLE [CORE].[PARAMETROS_FISICOS] (
        [PAF_ID]     TINYINT        NOT NULL,
        [PAF_CODIGO] VARCHAR (64)   NOT NULL,
        [PAF_NOMBRE] NVARCHAR (128) NOT NULL,
        [PAF_ESTADO] BIT            NOT NULL CONSTRAINT [DF_PARAMETROS_FISICOS_ESTADO] DEFAULT (1),
        CONSTRAINT [PK_PARAMETROS_FISICOS] PRIMARY KEY CLUSTERED ([PAF_ID] ASC)
    );

    INSERT INTO [CORE].[PARAMETROS_FISICOS] ([PAF_ID], [PAF_CODIGO], [PAF_NOMBRE], [PAF_ESTADO])
    SELECT [PAF_ID], [PAF_CODIGO], [PAF_NOMBRE], [PAF_ESTADO]
    FROM [CORE].[PARAMETROS_FISICOS_NEW];

    DROP TABLE [CORE].[PARAMETROS_FISICOS_NEW];

    CREATE UNIQUE NONCLUSTERED INDEX [UQ_PARAMETROS_FISICOS_CODIGO]
        ON [CORE].[PARAMETROS_FISICOS]([PAF_CODIGO] ASC);

    -- 6. Recrear índice único y FK en medidas
    IF OBJECT_ID(N'CORE.SEGUIMIENTO_PROCESOS_MEDIDAS', N'U') IS NOT NULL
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [UQ_SEGUIMIENTO_PROCESOS_MEDIDAS_SPR_PAF]
            ON [CORE].[SEGUIMIENTO_PROCESOS_MEDIDAS]([SPR_ID] ASC, [PAF_ID] ASC);

        ALTER TABLE [CORE].[SEGUIMIENTO_PROCESOS_MEDIDAS]
            ADD CONSTRAINT [FK_SEGUIMIENTO_PROCESOS_MEDIDAS_PARAMETROS_FISICOS]
            FOREIGN KEY ([PAF_ID]) REFERENCES [CORE].[PARAMETROS_FISICOS] ([PAF_ID]);
    END

    COMMIT TRANSACTION;
    PRINT 'Conversión PAF_ID a TINYINT completada correctamente.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
GO
