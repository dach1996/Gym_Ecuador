using Common.WebApi.Models.ContextRequestModel;
using LogicAdministratorApi.Model.Response.UserClient;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.UserClient;

/// <summary>
/// Solicitud para obtener detalle de usuario cliente por GUID
/// </summary>
public class GetUserClientByGuidRequest : IApiBaseRequest<GetUserClientByGuidResponse>
{
    /// <summary>
    /// Guid del cliente (ClientGymBranch)
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid ClientGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
