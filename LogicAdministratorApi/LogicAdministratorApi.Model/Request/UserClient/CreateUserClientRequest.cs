using LogicAdministratorApi.Model.Response.UserClient;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicAdministratorApi.Model.Request.UserClient;

/// <summary>
/// Solicitud para crear un usuario cliente
/// </summary>
public class CreateUserClientRequest : IApiBaseRequest<CreateUserClientResponse>
{
    /// <summary>
    /// Persona cliente
    /// </summary>
    public CreatePersonClientRequest PersonClient { get; set; }

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
    /// GUID del plan de sucursal a asignar al cliente (opcional)
    /// </summary>
    [ValidateGuid]
    public Guid BranchPlanGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

/// <summary>
/// Solicitud para crear una persona cliente
/// </summary>
public class CreatePersonClientRequest
{
    /// <summary>
    /// Guid de la persona
    /// </summary>
    [ValidateGuid]
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre de la persona
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    /// <summary>
    /// Apellido de la persona
    /// </summary>
    [Required]
    [StringLength(100)]
    public string LastName { get; set; }

    /// <summary>
    /// Fecha de nacimiento de la persona
    /// </summary>
    [Required]
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Código de género de la persona
    /// </summary>
    [Required]
    [StringLength(50)]
    public string GenderItemCatalogCode { get; set; }
}