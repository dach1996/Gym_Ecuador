-- =============================================
-- Author:      Danny Cárdenas
-- alter date:  08-05-2024
-- Description: Permite Crear o Actualizar un Catalogo.
--              CORREGIDO: Lanza error si el código padre se proporciona pero no se encuentra.
-- =============================================
CREATE PROCEDURE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_CATALOG]
    (
    @CAT_CODIGO VARCHAR(MAX),
    @CAT_NOMBRE VARCHAR(MAX),
    @CAT_ID_PADRE_CODIGO VARCHAR(MAX) = NULL,
    -- Código del Catálogo Padre
    @CAT_VALOR VARCHAR(MAX) = NULL,
    @CAT_DESCRIPCION VARCHAR(MAX) = NULL
)
AS
BEGIN
    -- Declarar variables para los valores a insertar/actualizar
    DECLARE @CAT_DESCRIPCION_FINAL VARCHAR(MAX);
    DECLARE @CAT_ID_PADRE_FINAL INT;
    DECLARE @ErrorMessage NVARCHAR(4000);

    -- 1. Lógica para asignar la Descripción (Si es NULL, usa el Nombre)
    SET @CAT_DESCRIPCION_FINAL = ISNULL(@CAT_DESCRIPCION, @CAT_NOMBRE);

    -- 2. Lógica para obtener el ID del Catálogo Padre y VERIFICAR su existencia
    IF @CAT_ID_PADRE_CODIGO IS NOT NULL AND LTRIM(RTRIM(@CAT_ID_PADRE_CODIGO)) <> ''
    BEGIN
        SELECT @CAT_ID_PADRE_FINAL = CAT_ID
        FROM [ADMINISTRACION].[CATALOGO]
        WHERE [CAT_CODIGO] = @CAT_ID_PADRE_CODIGO;

        -- *** VALIDACIÓN CLAVE: LANZAR ERROR SI EL CÓDIGO PADRE NO EXISTE ***
        IF @CAT_ID_PADRE_FINAL IS NULL
        BEGIN
            SET @ErrorMessage = 'ERROR: El código de Catálogo Padre "' + @CAT_ID_PADRE_CODIGO + '" no fue encontrado en la tabla [ADMINISTRACION].[CATALOGO].';
            -- Lanza un error con severidad 16 (detiene la ejecución)
            RAISERROR(@ErrorMessage, 16, 1) WITH NOWAIT;
            RETURN;
        -- Detiene el SP después de lanzar el error
        END
    END
    ELSE
    BEGIN
        SET @CAT_ID_PADRE_FINAL = NULL;
    END

    IF EXISTS
    (
        SELECT 1
    FROM [ADMINISTRACION].[CATALOGO]
    WHERE [CAT_CODIGO] = @CAT_CODIGO
    )
    BEGIN
        -- ** OPCIÓN DE ACTUALIZACIÓN (UPDATE) **
        UPDATE  [ADMINISTRACION].[CATALOGO]
        SET [CAT_NOMBRE] = @CAT_NOMBRE,
            [CAT_DESCRIPCION] = @CAT_DESCRIPCION_FINAL,
            [CAT_ID_PADRE] = @CAT_ID_PADRE_FINAL,      
            [CAT_VALOR] = @CAT_VALOR,                  
            [CAT_ESTADO] = 1
        WHERE CAT_CODIGO = @CAT_CODIGO;

        PRINT('Catálogo: ' + @CAT_CODIGO + ' Actualizado');
    END
    ELSE
    BEGIN
        -- ** OPCIÓN DE INSERCIÓN (INSERT) **
        INSERT INTO [ADMINISTRACION].[CATALOGO]
            (
            [CAT_CODIGO],
            [CAT_NOMBRE],
            [CAT_DESCRIPCION],
            [CAT_ID_PADRE],
            [CAT_ESTADO],
            [CAT_VALOR]
            )
        VALUES
            (
                @CAT_CODIGO,
                @CAT_NOMBRE,
                @CAT_DESCRIPCION_FINAL,
                @CAT_ID_PADRE_FINAL,
                1,
                @CAT_VALOR
        );

        PRINT('Catálogo: ' + @CAT_CODIGO + ' Ingresado');
    END
END

GO

