namespace Common.Messages;

/// <summary>
/// Message codes for API
/// </summary>
public enum MessagesCodesSucess
{
    /// <summary>
    /// Transacción exitosa
    /// </summary>
    Ok = 0,

    /// <summary>
    /// Mensaje Vacío 
    /// </summary>
    EmptyMessage = 1,

    /// <summary>
    /// Usuario creado exitosamente
    /// </summary>
    UserCreateSuccess = 2,

    /// <summary>
    /// Actualización de Datos realizados Correctamente
    /// </summary>
    UpdateUserInformationSuccess = 3,

    /// <summary>
    /// Mensaje de validación de expresiones regulares
    /// </summary>
    RegularExpressionValidationPassword = 4,

    /// <summary>
    /// Mensaje de creación de una contraseña temporal exitosa
    /// </summary>
    CreateTemporalityPasswordSuccess = 5,

    /// <summary>
    /// Contraseña actualizada Existosamente
    /// </summary>
    ChangePasswordSuccess = 6,

    /// <summary>
    /// Tarjeta eliminada exitosamente
    /// </summary>
    CardDeleteSuccess = 7,

    /// <summary>
    /// Tarjeta Registrada exitosamente
    /// </summary>
    CardRegisterSuccess = 8,

    /// <summary>
    /// Pago de asientos reservados
    /// </summary>
    OrderSeatPaid = 9,

    /// <summary>
    /// Orden Generada
    /// </summary>
    OrderGenerated = 10,

    /// <summary>
    /// Orden Cancelada
    /// </summary>
    OrderCancelada = 11,
}

