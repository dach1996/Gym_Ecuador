using Common.WebCommon.Attributes;
using LogicCommon.Model.Response.Administration;
namespace LogicApi.Model.Response.Authorization;
/// <summary>
/// Objeto de respuesta para ingreso de sistema
/// </summary>
public class LoginResponse : IApiBaseResponse
{
    /// <summary>
    /// Información del Usuario
    /// </summary>
    public UserInfo InfoUser { get; set; }

    /// <summary>
    /// Token de acceso
    /// </summary>
    [IgnoreSensible]
    public string AccessToken { get; set; }

    /// <summary>
    /// Configuración
    /// </summary>
    public UserConfiguration Configuration { get; set; }

    /// <summary>
    /// Lista de códigos de Catálogo
    /// </summary>
    /// <value></value>
    public GetInitialCataloguesResponse GetInitialCataloguesResponse { get; set; }

    /// <summary>
    /// Configuración de Filtro
    /// </summary>
    public FilterConfiguration FilterConfiguration { get; set; }

    /// <summary>
    /// Calificación máxima de los gimnasios
    /// </summary>
    public byte MaxCalification { get; set; }

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }
}

/// <summary>
/// Configuración de Filtro
/// </summary>
public class FilterConfiguration
{
    /// <summary>
    /// Tipos de Gimnasio
    /// </summary>
    /// <value></value>
    public List<FilterItem> GymTypes { get; set; }

    /// <summary>
    /// Servicios
    /// </summary>
    /// <value></value>
    public List<FilterItem> Services { get; set; }
}


/// <summary>
/// Item de Filtro
/// </summary>
public class FilterItem(string code, string name)
{
    /// <summary>
    /// Código de Catálogo
    /// </summary>
    public string Code { get; set; } = code;

    /// <summary>
    ///  Name
    /// </summary>
    public string Name { get; set; } = name;
}

/// <summary>
/// Configuraciones
/// </summary>
public class UserConfiguration
{
    /// <summary>
    /// Lenguage
    /// </summary>
    public string Language { get; set; }

    /// <summary>
    /// Url Terminos y Condiciones
    /// </summary>
    public string UrlTermsAndConditions { get; set; }

    /// <summary>
    /// Forzar a cambiar password
    /// </summary>
    public bool ForceChangePassword { get; set; }
}



/// <summary>
/// Modelo de Información de Usuario
/// </summary>
public class UserInfo
{
    /// <summary>
    /// Identificador de Usuario
    /// </summary>
    /// <value></value>
    public Guid GuidIdentifier { get; set; }

    /// <summary>
    /// Primer Nombre
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Apellido
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Sobrenombre del usuario
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Tipo de identificación
    /// </summary>
    public string IdentificationType { get; set; }

    /// <summary>
    /// Número de documento (CI / RUC / Pasaporte)
    /// </summary>
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Número de teléfono
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Correo electrónico
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Imagen
    /// </summary>
    public string UrlImage { get; set; }

    /// <summary>
    /// Ha verificado datos
    /// </summary>
    public bool HasVerifiedData { get; set; }

    /// <summary>
    /// Ha realizado seguimiento
    /// </summary>
    public bool HasAnyProcessTracking { get; set; }
}