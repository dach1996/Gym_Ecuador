namespace Common.Messages;

/// <summary>
/// Message codes error for API
/// </summary>
public enum MessagesCodesError
{
    /// <summary>
    /// Error del sistema
    /// </summary>
    SystemError = 0,

    /// <summary>
    /// Token Inválido
    /// </summary>
    TokenNotAllow = 1,

    /// <summary>
    ///  Información de no encontrada en base de datos
    /// </summary>
    InformationNotFoundInDataBase = 3,

    /// <summary>
    ///  Item de catálogo no habilitado
    /// </summary>
    ItemCatalogNotEnabled = 55,

    /// <summary>
    ///  Item de catálogo no encontrado
    /// </summary>
    ItemCatalogNotFound = 56,

    /// <summary>
    ///  Catalogo no Encontrado
    /// </summary>
    CatalogNotFound = 57,

    /// <summary>
    ///  Parametro no Habilitado
    /// </summary>
    ParameterNotEnabled = 58,

    /// <summary>
    /// Parametro no encontrado
    /// </summary>
    ParameterNotFound = 59,

    /// <summary>
    /// Error de formato en el request
    /// </summary>
    FormatError = 61,

    /// <summary>
    /// Información de Dispositivo no encontrada
    /// </summary>
    DeviceInfoNotFound = 62,

    /// <summary>
    /// Información del usuario no encontrada
    /// </summary>
    InfoUserNotFound = 63,

    /// <summary>
    /// Correo no enviado
    /// </summary>
    MailDontSent = 64,

    /// <summary>
    /// Número máximo de Refresh token exedido
    /// </summary>
    MaxRefreshToken = 65,

    /// <summary>
    /// Usuario Bloqueado
    /// </summary>
    UserBlocked = 66,

    /// <summary>
    /// Mo se pudo enviar correo de notificación
    /// </summary>
    MailNotificationCantSend = 67,

    /// <summary>
    /// El rol del usuario no se encuentra configurado para realizar esta acción
    /// </summary>
    UserNotAuthorized = 68,

    /// <summary>
    /// El rol del usuario no se encuentra configurado para realizar esta acción
    /// </summary>
    RequestError = 69,

    /// <summary>
    /// Forma de acceso no registrada
    /// </summary>
    UserFormAccessNotRegister = 70,

    /// <summary>
    /// Implementación no Encontrada
    /// </summary>
    ImplementationDontFound = 77,

    /// <summary>
    ///  Enumerado no Mapeado
    /// </summary>
    EnumDontMapped = 78,

    /// <summary>
    /// Información de Persona no Encontrada
    /// </summary>
    PersonInformationNotFound = 96,

    /// <summary>
    /// Formas de Registro
    /// </summary>
    UserHasNoRegistrationForm = 97,

    /// <summary>
    /// Usuario no tiene una persona asignada
    /// </summary>
    UserHasNotAssignedPerson = 98,

    /// <summary>
    /// Usuario ya se encuentra asignado a una persona
    /// </summary>
    UserHasAssignedPerson = 99,

    /// <summary>
    /// Nombre de usuario existe
    /// </summary>
    UserUsernameExist = 100,

    /// <summary>
    ///  Correo existe
    /// </summary>
    UserEmailExist = 101,

    /// <summary>
    ///  Telefono existe
    /// </summary>
    UserPhoneExist = 102,

    /// <summary>
    ///  Telefono existe
    /// </summary>
    TermsAndConditionNotAccept = 103,

    /// <summary>
    ///  Contraseñas iguales
    /// </summary>
    EqualsPassword = 104,

    /// <summary>
    ///  Contraseña Incorrecta
    /// </summary>
    IncorrectPassword = 105,

    /// <summary>
    ///  Documento de usuario no existe
    /// </summary>
    UserDocumentNotExist = 106,

    /// <summary>
    ///  Documento de usuario ya existe
    /// </summary>
    UserDocumentExist = 107,

    /// <summary>
    /// Nombres o Apellidos no concuerdan
    /// </summary>
    NamesOrLastNameDontMatch = 108,

    /// <summary>
    ///  Acompañante Existe
    /// </summary>
    CompanionExist = 110,

    /// <summary>
    ///  Acompañante no Existe
    /// </summary>
    CompanionNotExist = 111,

    /// <summary>
    ///  Asiento Reservado
    /// </summary>
    SeatReserved = 112,

    /// <summary>
    ///  Asiento Comprado
    /// </summary>
    SeatPurchased = 113,

    /// <summary>
    ///  Tarjeta no encontrada
    /// </summary>
    CardNotFound = 114,

    /// <summary>
    ///  Tarjeta incorrecta
    /// </summary>
    CardBad = 115,

    /// <summary>
    /// Validación de Cédula
    /// </summary>
    ValidateIdentificationCard = 116,

    /// <summary>
    /// Orden no encontrada
    /// </summary>
    OrderNotFound = 117,

    /// <summary>
    /// No existe la cooperativa
    /// </summary>
    CooperativeNotExist = 118,

    /// <summary>
    /// Información de Diagramas de Piso no encontrada
    /// </summary>
    DiagramFloorNotFound = 119,

    /// <summary>
    /// Información de Bus no encontrada
    /// </summary>
    BusInformationNotFound = 120,

    /// <summary>
    /// Lugares no encontrados
    /// </summary>
    PlaceNotFound = 121,

    /// <summary>
    /// Error en validaciones del modelo de generar orden
    /// </summary>
    OrderModelError = 122,

    /// <summary>
    ///  Asiento  pre pagado o generado orden
    /// </summary>
    SeatPrePaid = 123,

    /// <summary>
    ///  Orden no pertenece al usuario
    /// </summary>
    OrderDontBelongUser = 124,

    /// <summary>
    ///  Orden no permite ser cancelada
    /// </summary>
    OrderNotAllowCancel = 125,

    /// <summary>
    ///  Asiento no encontrado
    /// </summary>
    SeatNotFound = 126,

    /// <summary>
    /// La información a utilizar no pertence al Usuario
    /// </summary>
    DataNotBelongContextUser = 127,

    /// <summary>
    /// Estado de la orden es inválido para la operación
    /// </summary>
    OrderStateInvalidOperation = 128,

    /// <summary>
    /// Máximo de reserva de asientos permitidos
    /// </summary>
    MaxSeatReserveAllow = 129,

    /// <summary>
    ///     Usuario no ha completado el registro
    /// </summary>
    UserRegisterNotComplete = 130,

    /// <summary>
    /// Ruta no encontrada
    /// </summary>
    RouteDontFound = 131,

    /// <summary>
    /// No hay rutas disponibles para los puntos seleccionados
    /// </summary>
    RouteDontAvailable = 132,

    /// <summary>
    /// Biométrico Incorrecto
    /// </summary>
    BiometricIncorrect = 133,

    /// <summary>
    /// Orden no permite ser pagada
    /// </summary>
    OrderNotAllowPay = 134,
}

