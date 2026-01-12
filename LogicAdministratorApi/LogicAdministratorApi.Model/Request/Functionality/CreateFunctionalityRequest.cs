using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Common.WebCommon.Models;
using LogicAdministratorApi.Model.Response.Functionality;

namespace LogicAdministratorApi.Model.Request.Functionality;

/// <summary>
/// Solicitud para crear una nueva funcionalidad
/// </summary>
public class CreateFunctionalityRequest : IApiBaseRequest<CreateFunctionalityResponse>
{
    /// <summary>
    /// Código de la funcionalidad
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Code { get; set; }

    /// <summary>
    /// Nombre de la funcionalidad
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// Descripción de la funcionalidad
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Description { get; set; }

    /// <summary>
    /// ID de la función asociada
    /// </summary>
    [Required]
    public int FunctionId { get; set; }

    /// <summary>
    /// Estado de la funcionalidad (activa/inactiva)
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
