namespace LogicAdministratorApi.Model.Response.UserAdministrator;

/// <summary>
/// Respuesta de obtener detalle de usuario administrador por GUID
/// </summary>
public class GetUserAdministratorByGuidResponse(UserDetail user) : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Datos del usuario
    /// </summary>
    public UserDetail User { get; set; } = user;
}

/// <summary>
/// Detalle completo de usuario administrador
/// </summary>
public class UserDetail
{
    /// <summary>
    /// Guid del usuario
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre de usuario
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Email del usuario
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Teléfono del usuario
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Código de idioma
    /// </summary>
    public string LanguageCode { get; set; }

    /// <summary>
    /// Indica si el usuario está bloqueado
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Indica si tiene registro completo
    /// </summary>
    public bool HasCompleteRegistration { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Fecha del primer login
    /// </summary>
    public DateTime? FirstLoginDate { get; set; }

    /// <summary>
    /// URL de la imagen del usuario
    /// </summary>
    public string ImageUrl { get; set; }
}

