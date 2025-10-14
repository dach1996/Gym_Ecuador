CREATE PROCEDURE [ADMINISTRACION].[INSER_OR_UPCATE_CATALOGUE]
    @CAT_CODIGO VARCHAR(64),
    @CAT_NOMBRE VARCHAR(64),
    @CAT_FATHER_CODE VARCHAR(64) = NULL,
    @CAT_ESTADO BIT = 1,
    @CAT_VALOR VARCHAR(128) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CAT_PADRE INT = NULL;

    -- Buscar el ID del catálogo padre si se proporciona CAT_FATHER_CODE
    IF @CAT_FATHER_CODE IS NOT NULL
    BEGIN
        SELECT @CAT_PADRE = CAT_ID
        FROM [ADMINISTRACION].CATALOGO
        WHERE CAT_CODIGO = @CAT_FATHER_CODE;
    END

    -- Verificar si el código ya existe
    IF EXISTS (SELECT 1
    FROM [ADMINISTRACION].CATALOGO
    WHERE CAT_CODIGO = @CAT_CODIGO)
    BEGIN
        -- Si existe, actualizar los datos
        UPDATE [ADMINISTRACION].CATALOGO
        SET CAT_NOMBRE = @CAT_NOMBRE,
            CAT_PADRE = @CAT_PADRE,
            CAT_ESTADO = @CAT_ESTADO,
            CAT_VALOR = @CAT_VALOR
        WHERE CAT_CODIGO = @CAT_CODIGO;
         PRINT 'Catálogo actualizado: ' + @CAT_CODIGO;
    
    END
    ELSE
    BEGIN
        -- Si no existe, insertarlo
        INSERT INTO [ADMINISTRACION].CATALOGO
            (CAT_CODIGO, CAT_NOMBRE, CAT_PADRE, CAT_ESTADO, CAT_VALOR)
        VALUES
            (@CAT_CODIGO, @CAT_NOMBRE, @CAT_PADRE, @CAT_ESTADO, @CAT_VALOR);
            PRINT 'Catálogo insertado: ' + @CAT_CODIGO;
    END
END;
GO

