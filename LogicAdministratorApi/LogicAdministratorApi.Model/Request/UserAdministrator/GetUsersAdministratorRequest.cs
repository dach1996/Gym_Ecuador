using Common.WebApi.Models.ContextRequestModel;
using LogicAdministratorApi.Model.Response.UserAdministrator;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.UserAdministrator;

/// <summary>
/// Solicitud para obtener usuarios administradores paginados
/// </summary>
public class GetUsersAdministratorRequest : IPaginatorApiRequest<GetUsersAdministratorResponse>
{
    /// <summary>
    /// Filtro por nombre de usuario
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// Página
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    /// <summary>
    /// Tamaño de página
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

