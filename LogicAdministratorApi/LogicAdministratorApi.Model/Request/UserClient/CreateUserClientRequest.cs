using LogicAdministratorApi.Model.Response.UserClient;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.UserClient;

/// <summary>
/// Solicitud para crear un usuario cliente
/// </summary>
public class CreateUserClientRequest : IApiBaseRequest<CreateUserClientResponse>
{
    /// <summary>
    /// Número de identificación del usuario
    /// </summary>
    [Required]
    [StringLength(50)]
    public string IdentificationNumber { get; set; }

    /// <summary>
    /// Email del usuario
    /// </summary>
    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Teléfono del usuario
    /// </summary>
    [StringLength(20)]
    public string Phone { get; set; }

    /// <summary>
    /// Código de idioma
    /// </summary>
    [StringLength(50)]
    public string LanguageCode { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

