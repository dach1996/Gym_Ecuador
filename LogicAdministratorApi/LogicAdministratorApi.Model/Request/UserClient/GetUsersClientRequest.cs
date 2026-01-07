using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.UserClient;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.UserClient;

/// <summary>
/// Solicitud para obtener usuarios clientes paginados
/// </summary>
public class GetUsersClientRequest : IPaginatorApiRequest<GetUsersClientResponse>
{
    /// <summary>
    /// Filtro por email
    /// </summary>
    public string EmailFilter { get; set; }

    /// <summary>
    /// Filtro por nombre de usuario
    /// </summary>
    public string UserNameFilter { get; set; }

    /// <summary>
    /// Filtro por estado bloqueado
    /// </summary>
    public bool? IsBlockedFilter { get; set; }

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

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public GetUsersClientRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetUsersClientRequest()
    {
    }
}

