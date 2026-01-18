using LogicAdministratorApi.Model.Response.Gym;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.Gym;

/// <summary>
/// Solicitud para obtener gimnasios paginados
/// </summary>
public class GetGymsRequest : IApiBaseRequest<GetGymsResponse>
{
    /// <summary>
    /// Filtro por nombre de gimnasio
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// Filtro por nombre de gimnasio
    /// </summary>
    public Guid? GymGuid { get; set; }

    /// <summary>
    /// GUID del gimnasio
    /// </summary>
    public Guid? GymBranchGuid { get; set; }

    /// <summary>
    /// Página
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Tamaño de página
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

