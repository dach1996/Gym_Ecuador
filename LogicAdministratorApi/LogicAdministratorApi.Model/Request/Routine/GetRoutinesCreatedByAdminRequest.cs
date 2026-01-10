using LogicAdministratorApi.Model.Response.Routine;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicAdministratorApi.Model.Request.Routine;

/// <summary>
/// Solicitud para obtener rutinas creadas por el administrador con paginación
/// </summary>
public class GetRoutinesCreatedByAdminRequest : IApiBaseRequest<GetRoutinesCreatedByAdminResponse>
{
    /// <summary>
    /// Número de página
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Tamaño de página
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Filtro por nombre de rutina (opcional)
    /// </summary>
    public string NameFilter { get; set; }

    /// <summary>
    /// Filtro por UserGuid del usuario asignado (opcional)
    /// </summary>
    [ValidateGuid]
    public Guid? UserGuidFilter { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
