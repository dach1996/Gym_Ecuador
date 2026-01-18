using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.Gym;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.Gym;

/// <summary>
/// Solicitud para obtener detalle de un gimnasio por GUID
/// </summary>
public class GetGymDetailRequest : IApiBaseRequest<GetGymDetailResponse>
{
    /// <summary>
    /// GUID del gimnasio a obtener
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
