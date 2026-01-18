using Common.WebApi.Models.ContextRequestModel;
using LogicAdministratorApi.Model.Response.UserClient;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.UserClient;

/// <summary>
/// Solicitud para obtener usuarios clientes paginados
/// </summary>
public class GetUsersClientRequest : IPaginatorApiRequest<GetUsersClientResponse>
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid? GymGuid { get; set; }

    /// <summary>
    /// Guid de la sucursal
    /// </summary>
    public Guid? GymBranchGuid { get; set; }
    
    /// <summary>
    /// Filtro por email
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// Tamaño de página
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; }

    /// <summary>
    /// Página
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

}

