using System.Runtime.Serialization;

namespace Common.WebApi.Models.Enum;
/// <summary>
/// Operaciones
/// </summary>
public enum OperationApiName
{

    /// <summary>
    /// Consultar Catálogos Iniciales
    /// </summary>
    [EnumMember(Value = "Consultar Catálogos Iniciales")]
    GetInitialCatalogues,

    /// <summary>
    /// Operaciones
    /// </summary>
    [EnumMember(Value = "Inicio de Sesión")]
    Login,

    /// <summary>
    /// Inicio de Sesión Biométrico
    /// </summary>
    [EnumMember(Value = "Inicio de Sesión Biométrico")]
    BiometricLogin,

    /// <summary>
    /// Inicio de Sesión por Google
    /// </summary>
    [EnumMember(Value = "Inicio de Sesión Google")]
    GoogleLogin,

    /// <summary>
    /// Inicio de Sesión Usuario/Contraseña
    /// </summary>
    [EnumMember(Value = "Inicio de Sesión Usuario/Contraseña")]
    UserPasswordLogin,

    /// <summary>
    /// Obtener llave pública
    /// </summary>
    [EnumMember(Value = "Obtener llave pública")]
    GetPublicKey,

    /// <summary>
    /// Crear Usuario
    /// </summary>
    [EnumMember(Value = "Crear Usuario")]
    CreateUser,

    /// <summary>
    /// Cierre de Sesión
    /// </summary>
    [EnumMember(Value = "Cierre de Sesión")]
    Logout,

    /// <summary>
    /// Cambiar Contraseña
    /// </summary>
    [EnumMember(Value = "Cambiar Contraseña")]
    PasswordChange,

    /// <summary>
    /// Olvidó Contraseña
    /// </summary>
    [EnumMember(Value = "Olvidó Contraseña")]
    PasswordForgotten,

    /// <summary>
    /// Regenerar Token
    /// </summary>
    [EnumMember(Value = "Regenerar Token")]
    RefreshToken,

    /// <summary>
    /// Registro Biométrico
    /// </summary>
    [EnumMember(Value = "Registro Biométrico")]
    RegisterBiometric,

    /// <summary>
    /// Eliminar Registro Biométrico
    /// </summary>
    [EnumMember(Value = "Eliminar Registro Biométrico")]
    RemoveBiometric,

    /// <summary>
    /// Actualizar Usuario
    /// </summary>
    [EnumMember(Value = "Actualizar Usuario")]
    UpdateUser,

    /// <summary>
    /// Consultar Items de Catálogo por Archivo
    /// </summary>
    [EnumMember(Value = "Consultar Items de Catálogo por Archivo")]
    GetItemsCatalogByCodeCatalogFile,

    /// <summary>
    /// Consultar Item de Catálogo por Código
    /// </summary>
    [EnumMember(Value = "Consultar Item de Catálogo por Código")]
    GetItemsCatalogByCodeCatalog,

    /// <summary>
    /// Consultar Parámetro
    /// </summary>
    [EnumMember(Value = "Consultar Parámetro")]
    GetParameterByCode,

    /// <summary>
    /// Consultar Expresiones Regulares
    /// </summary>
    [EnumMember(Value = "Consultar Expresiones Regulares")]
    GetPasswordValidateRegularExpression,

    /// <summary>
    /// Enviar Mail de Notificación
    /// </summary>
    [EnumMember(Value = "Enviar Mail de Notificación")]
    SendMailNotification,

    /// <summary>
    /// Registrar Dispositivo
    /// </summary>
    [EnumMember(Value = "Registrar Dispositivo")]
    RegisterDevice,

    /// <summary>
    /// Eliminar Archivo
    /// </summary>
    [EnumMember(Value = "Eliminar Archivo")]
    DeleteBlobFile,

    /// <summary>
    /// Descargar Archivo
    /// </summary>
    [EnumMember(Value = "Descargar Archivo")]
    DownloadBlobFile,

    /// <summary>
    /// Actualizar Archivo
    /// </summary>
    [EnumMember(Value = "Actualizar Archivo")]
    UpdateBlobFile,

    /// <summary>
    /// Registrar Log de Auditoría
    /// </summary>
    [EnumMember(Value = "Registrar Log de Auditoría")]
    RegisterLog,

    /// <summary>
    /// Desencriptar Texto
    /// </summary>
    [EnumMember(Value = "Desencriptar Texto")]
    Decrypt,

    /// <summary>
    /// Encriptar Texto
    /// </summary>
    [EnumMember(Value = "Encriptar Texto")]
    Encrypt,

    /// <summary>
    /// Actualiza un lugar Favorito
    /// </summary>
    [EnumMember(Value = "Actualiza un lugar Favorito")]
    UpdateFavoritePlace,

    /// <summary>
    /// Obtiene los tickets disponibles
    /// </summary>
    [EnumMember(Value = "Obtiene los tickets disponibles")]
    GetTicketsAvailable,

    /// <summary>
    /// Obtener compañeros de viaje
    /// </summary>
    [EnumMember(Value = "Obtener compañeros de viaje")]
    SearchCompanion,

    /// <summary>
    /// Actualiza un compañero
    /// </summary>
    [EnumMember(Value = "Actualiza un compañero")]
    UpdateCompanion,

    /// <summary>
    /// Crea un compañero
    /// </summary>
    [EnumMember(Value = "Registrar compañero")]
    CreateCompanion,

    /// <summary>
    /// Obtiene mis compañeros de viaje
    /// </summary>
    [EnumMember(Value = "Obtiene mis compañeros de viaje")]
    GetMyCompanion,

    /// <summary>
    /// Reservar Asiento
    /// </summary>
    [EnumMember(Value = "Reservar Asiento")]
    ReserveSeat,

    /// <summary>
    /// Registra una Tarjeta
    /// </summary>
    [EnumMember(Value = "Registra una Tarjeta")]
    RegisterCard,

    /// <summary>
    /// Obtiene mis tarjetas
    /// </summary>
    [EnumMember(Value = "Obtiene mis tarjetas")]
    GetMyCards,

    /// <summary>
    /// Elimina una tarjeta
    /// </summary>
    [EnumMember(Value = "Elimina una Tarjeta")]
    DeleteCard,

    /// <summary>
    /// Asigna la Persona
    /// </summary>
    [EnumMember(Value = "Asigna Persona")]
    AssignPerson,

    /// <summary>
    /// Validación de Cédula
    /// </summary>
    [EnumMember(Value = "Validación de Cédula")]
    IdentificationCardValidation,

    /// <summary>
    /// Validación de Tarjeta
    /// </summary>
    [EnumMember(Value = "Validación de Tarjeta")]
    ValidateCard,

    /// <summary>
    /// Registrar Push Token
    /// </summary>
    [EnumMember(Value = "Registrar Push Token")]
    RegisterPushToken,

    /// <summary>
    /// Genera orden de Compra
    /// </summary>
    [EnumMember(Value = "Genera Orden")]
    GenerateOrder,

    /// <summary>
    /// Pagar Orden
    /// </summary>
    [EnumMember(Value = "Pagar Orden")]
    PaymentOrder,

    /// <summary>
    /// Obtiene los asientos disponibles
    /// </summary>
    [EnumMember(Value = "Obtiene los asientos disponibles")]
    GetSeatAvailable,

    /// <summary>
    /// Cancelación manual de Órden
    /// </summary>
    [EnumMember(Value = "Cancelación Manual de Orden")]
    ManualCancelOrder,

    /// <summary>
    /// Obtiene Información de Cooperativa
    /// </summary>
    [EnumMember(Value = "Obtiene información de Cooperativa")]
    GetCooperativeInformation,

    /// <summary>
    /// Verificar valores de Órden
    /// </summary>
    [EnumMember(Value = "Verificar Valores de Orden")]
    VerifyOrderValues,

    /// <summary>
    /// Obtener mis ordenes
    /// </summary>
    [EnumMember(Value = "Obtener mis Ordenes")]
    GetMyOrdersPaginated,

    /// <summary>
    /// Obtener mis ordenes inicio
    /// </summary>
    [EnumMember(Value = "Obtener mis Ordenes Inicio")]
    GetInitialMyOrdersPaginated,

    /// <summary>
    /// Obtener detalle de Orden
    /// </summary>
    [EnumMember(Value = "Obtener detalle de Orden")]
    GetOrderDetails,

    /// <summary>
    /// Obtiene los lugares favoritos
    /// </summary>
    [EnumMember(Value = "Obtiene los lugares favoritos")]
    GetMyFavoritePlace,

    /// <summary>
    /// Obtiene los lugares paginados
    /// </summary>
    [EnumMember(Value = "Obtiene los lugares paginados")]
    GetPlacesPaginated,
    
    
}