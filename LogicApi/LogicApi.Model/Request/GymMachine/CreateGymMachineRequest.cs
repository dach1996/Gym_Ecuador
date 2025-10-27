using LogicApi.Model.Response.GymMachine;

namespace LogicApi.Model.Request.GymMachine;

/// <summary>
/// Solicitud para crear una máquina de gimnasio
/// </summary>
public class CreateGymMachineRequest : IRequest<CreateGymMachineResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Nombre de la máquina
    /// </summary>
    public string MachineName { get; set; }

    /// <summary>
    /// Tipo de máquina
    /// </summary>
    public string MachineType { get; set; }

    /// <summary>
    /// URL de imagen de la máquina
    /// </summary>
    public string MachineImageUrl { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateGymMachineRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGymMachineRequest()
    {
    }
}
