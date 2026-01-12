using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Common.WebCommon.Models;
using LogicAdministratorApi.Model.Response.Function;

namespace LogicAdministratorApi.Model.Request.Function;

/// <summary>
/// Solicitud para crear una nueva función
/// </summary>
public class CreateFunctionRequest : IApiBaseRequest<CreateFunctionResponse>
{
    /// <summary>
    /// Código de la función
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Code { get; set; }

    /// <summary>
    /// Nombre de la función
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// Descripción de la función
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Description { get; set; }

    /// <summary>
    /// ID del módulo asociado
    /// </summary>
    [Required]
    public short ModuleId { get; set; }

    /// <summary>
    /// Estado de la función (activa/inactiva)
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    /// Ruta de la función
    /// </summary>
    [StringLength(64)]
    public string Route { get; set; }

    /// <summary>
    /// Icono de la función
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Icon { get; set; }

    /// <summary>
    /// Orden de la función
    /// </summary>
    [Required]
    public byte Order { get; set; }

    /// <summary>
    /// Visibilidad de la función
    /// </summary>
    [Required]
    public bool IsVisible { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
