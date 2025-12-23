using Common.WebApi.Models.ContextRequestModel;
using LogicAdministratorApi.Model.Response.Gym;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.Gym;

/// <summary>
/// Solicitud para actualizar un gimnasio
/// </summary>
public class UpdateGymRequest : IRequest<UpdateGymResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción del gimnasio
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Descripción corta del gimnasio
    /// </summary>
    public string ShortDescription { get; set; }

    /// <summary>
    /// Teléfono del gimnasio
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Email del gimnasio
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Sitio web del gimnasio
    /// </summary>
    public string Website { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public UpdateGymRequest(AdminContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateGymRequest()
    {
    }
}

