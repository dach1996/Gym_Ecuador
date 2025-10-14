-- =============================================
-- Author:		Danny Cárdenas
-- alter date:  08-05-2024
-- Description:	Permite ingresar o actualizar un parámetro
-- =============================================
CREATE PROCEDURE [ADMINISTRACION].[PROC_INSERT_OR_UPDATE_PARAMETER]
    (
    @PAR_CODIGO VARCHAR(MAX) ,
    @PAR_NOMBRE VARCHAR(MAX),
    @PAR_VALOR VARCHAR(MAX) ,
    @PAR_DESCRIPCION VARCHAR(MAX) = NULL
)
AS
BEGIN
    SET @PAR_DESCRIPCION  =IIF(@PAR_DESCRIPCION IS NULL , @PAR_NOMBRE, @PAR_DESCRIPCION)
    IF EXISTS (
        SELECT *
    FROM [ADMINISTRACION].[PARAMETRO]
    WHERE PAR_CODIGO = @PAR_CODIGO
    )
    BEGIN
        UPDATE [ADMINISTRACION].[PARAMETRO]
SET [PAR_CODIGO] = @PAR_CODIGO,
    [PAR_NOMBRE] = @PAR_NOMBRE,
    [PAR_VALOR] = @PAR_VALOR,
    [PAR_DESCRIPCION] = @PAR_DESCRIPCION,
    [PAR_ESTADO] = 1,
    [PAR_VERSION] =  [PAR_VERSION]+1,
    [PAR_FECHA_MOFICACION] = GETDATE()
WHERE PAR_CODIGO = @PAR_CODIGO
        PRINT('Parámetro: '+@PAR_CODIGO+' Actualizado');
    END
    ELSE
    BEGIN
        INSERT INTO [ADMINISTRACION].[PARAMETRO]
            (
            [PAR_CODIGO],
            [PAR_NOMBRE],
            [PAR_VALOR],
            [PAR_DESCRIPCION],
            [PAR_ESTADO],
            [PAR_VERSION],
            [PAR_FECHA_REGISTRO],
            [PAR_FECHA_MOFICACION] 
            )
        VALUES
            (
                @PAR_CODIGO,
                @PAR_NOMBRE,
                @PAR_VALOR,
                @PAR_DESCRIPCION,
                1,
                1,
                GETDATE(),
                GETDATE()
    )
        PRINT('Parámetro: '+@PAR_CODIGO+' Ingresado');
    END
    SELECT *
    FROM [ADMINISTRACION].[PARAMETRO]
    WHERE PAR_CODIGO = @PAR_CODIGO
END
GO

