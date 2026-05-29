namespace LogicAdministratorApi.Model.Response.UserAdministrator;

/// <summary>
/// Respuesta de obtener detalle de usuario administrador por GUID
/// </summary>
public class GetUserAdministratorByGuidResponse(AdministratorUserDetail user) : IApiBaseResponse
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
    public AdministratorUserDetail User { get; set; } = user;
}

/// <summary>
/// Detalle completo de usuario administrador
/// </summary>
public class AdministratorUserDetail : AdministratorUserItem
{
    /// <summary>
    /// Alcance del rol
    /// </summary>
    public byte Scope { get; set; }
}

