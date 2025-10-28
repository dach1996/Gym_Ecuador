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


    /// <summary>
    /// Crear Gimnasio
    /// </summary>
    [EnumMember(Value = "Crear Gimnasio")]
    CreateGym,

    /// <summary>
    /// Actualizar Gimnasio
    /// </summary>
    [EnumMember(Value = "Actualizar Gimnasio")]
    UpdateGym,

    /// <summary>
    /// Obtener Gimnasios
    /// </summary>
    [EnumMember(Value = "Obtener Gimnasios")]
    GetGyms,

    /// <summary>
    /// Obtener Gimnasio por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Gimnasio por GUID")]
    GetGymByGuid,

    // ========== OPERACIONES DE ENTRENADOR ==========

    /// <summary>
    /// Crear Entrenador
    /// </summary>
    [EnumMember(Value = "Crear Entrenador")]
    CreateTrainer,

    /// <summary>
    /// Actualizar Entrenador
    /// </summary>
    [EnumMember(Value = "Actualizar Entrenador")]
    UpdateTrainer,

    /// <summary>
    /// Obtener Entrenadores
    /// </summary>
    [EnumMember(Value = "Obtener Entrenadores")]
    GetTrainers,

    /// <summary>
    /// Obtener Entrenador por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Entrenador por GUID")]
    GetTrainerByGuid,

    // ========== OPERACIONES DE MEMBRESÍA ==========

    /// <summary>
    /// Crear Membresía
    /// </summary>
    [EnumMember(Value = "Crear Membresía")]
    CreateMembership,

    /// <summary>
    /// Actualizar Membresía
    /// </summary>
    [EnumMember(Value = "Actualizar Membresía")]
    UpdateMembership,

    /// <summary>
    /// Obtener Membresías
    /// </summary>
    [EnumMember(Value = "Obtener Membresías")]
    GetMemberships,

    /// <summary>
    /// Obtener Membresía por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Membresía por GUID")]
    GetMembershipByGuid,

    // ========== OPERACIONES DE TIPO DE MEMBRESÍA ==========

    /// <summary>
    /// Crear Tipo de Membresía
    /// </summary>
    [EnumMember(Value = "Crear Tipo de Membresía")]
    CreateMembershipType,

    /// <summary>
    /// Actualizar Tipo de Membresía
    /// </summary>
    [EnumMember(Value = "Actualizar Tipo de Membresía")]
    UpdateMembershipType,

    /// <summary>
    /// Obtener Tipos de Membresía
    /// </summary>
    [EnumMember(Value = "Obtener Tipos de Membresía")]
    GetMembershipTypes,

    /// <summary>
    /// Obtener Tipo de Membresía por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Tipo de Membresía por GUID")]
    GetMembershipTypeByGuid,

    // ========== OPERACIONES DE CLASE GRUPAL ==========

    /// <summary>
    /// Crear Clase Grupal
    /// </summary>
    [EnumMember(Value = "Crear Clase Grupal")]
    CreateGroupClass,

    /// <summary>
    /// Actualizar Clase Grupal
    /// </summary>
    [EnumMember(Value = "Actualizar Clase Grupal")]
    UpdateGroupClass,

    /// <summary>
    /// Obtener Clases Grupales
    /// </summary>
    [EnumMember(Value = "Obtener Clases Grupales")]
    GetGroupClasses,

    /// <summary>
    /// Obtener Clase Grupal por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Clase Grupal por GUID")]
    GetGroupClassByGuid,

    // ========== OPERACIONES DE HORARIO DE CLASE ==========

    /// <summary>
    /// Crear Horario de Clase
    /// </summary>
    [EnumMember(Value = "Crear Horario de Clase")]
    CreateClassSchedule,

    /// <summary>
    /// Actualizar Horario de Clase
    /// </summary>
    [EnumMember(Value = "Actualizar Horario de Clase")]
    UpdateClassSchedule,

    /// <summary>
    /// Obtener Horarios de Clase
    /// </summary>
    [EnumMember(Value = "Obtener Horarios de Clase")]
    GetClassSchedules,

    /// <summary>
    /// Obtener Horario de Clase por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Horario de Clase por GUID")]
    GetClassScheduleByGuid,

    // ========== OPERACIONES DE RESERVA DE CLASE ==========

    /// <summary>
    /// Crear Reserva de Clase
    /// </summary>
    [EnumMember(Value = "Crear Reserva de Clase")]
    CreateClassReservation,

    /// <summary>
    /// Cancelar Reserva de Clase
    /// </summary>
    [EnumMember(Value = "Cancelar Reserva de Clase")]
    CancelClassReservation,

    /// <summary>
    /// Obtener Reservas de Clase
    /// </summary>
    [EnumMember(Value = "Obtener Reservas de Clase")]
    GetClassReservations,

    /// <summary>
    /// Obtener Reserva de Clase por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Reserva de Clase por GUID")]
    GetClassReservationByGuid,

    // ========== OPERACIONES DE MÁQUINA DE GIMNASIO ==========

    /// <summary>
    /// Crear Máquina de Gimnasio
    /// </summary>
    [EnumMember(Value = "Crear Máquina de Gimnasio")]
    CreateGymMachine,

    /// <summary>
    /// Actualizar Máquina de Gimnasio
    /// </summary>
    [EnumMember(Value = "Actualizar Máquina de Gimnasio")]
    UpdateGymMachine,

    /// <summary>
    /// Obtener Máquinas de Gimnasio
    /// </summary>
    [EnumMember(Value = "Obtener Máquinas de Gimnasio")]
    GetGymMachines,

    /// <summary>
    /// Obtener Máquina de Gimnasio por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Máquina de Gimnasio por GUID")]
    GetGymMachineByGuid,

    // ========== OPERACIONES DE META PERSONAL ==========

    /// <summary>
    /// Crear Meta Personal
    /// </summary>
    [EnumMember(Value = "Crear Meta Personal")]
    CreatePersonalGoal,

    /// <summary>
    /// Actualizar Meta Personal
    /// </summary>
    [EnumMember(Value = "Actualizar Meta Personal")]
    UpdatePersonalGoal,

    /// <summary>
    /// Obtener Metas Personales
    /// </summary>
    [EnumMember(Value = "Obtener Metas Personales")]
    GetPersonalGoals,

    /// <summary>
    /// Obtener Meta Personal por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Meta Personal por GUID")]
    GetPersonalGoalByGuid,

    // ========== OPERACIONES DE RESEÑA DE GIMNASIO ==========

    /// <summary>
    /// Crear Reseña de Gimnasio
    /// </summary>
    [EnumMember(Value = "Crear Reseña de Gimnasio")]
    CreateGymReview,

    /// <summary>
    /// Obtener Reseñas de Gimnasio
    /// </summary>
    [EnumMember(Value = "Obtener Reseñas de Gimnasio")]
    GetGymReviews,

    // ========== OPERACIONES DE CALIFICACIÓN DE ENTRENADOR ==========

    /// <summary>
    /// Crear Calificación de Entrenador
    /// </summary>
    [EnumMember(Value = "Crear Calificación de Entrenador")]
    CreateTrainerRating,

    /// <summary>
    /// Obtener Calificaciones de Entrenador
    /// </summary>
    [EnumMember(Value = "Obtener Calificaciones de Entrenador")]
    GetTrainerRatings,

    /// <summary>
    /// Crear Seguimiento de Proceso
    /// </summary>
    [EnumMember(Value = "Crear Seguimiento de Proceso")]
    CreateProcessTracking,

    /// <summary>
    /// Obtener Seguimiento de Proceso por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Seguimiento de Proceso por GUID")]
    GetProcessTrackingByGuid,

    /// <summary>
    /// Obtener Seguimientos de Procesos
    /// </summary>
    [EnumMember(Value = "Obtener Seguimientos de Procesos")]
    GetProcessTrackings,

    /// <summary>
    /// Actualizar Seguimiento de Proceso
    /// </summary>
    [EnumMember(Value = "Actualizar Seguimiento de Proceso")]
    UpdateProcessTracking,
}