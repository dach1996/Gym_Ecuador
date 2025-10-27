using LogicApi.Model.Response.GymMachine;

namespace LogicApi.Model.Request.GymMachine;

/// <summary>
/// Solicitud para actualizar una máquina de gimnasio
/// </summary>
public class UpdateGymMachineRequest : IRequest<UpdateGymMachineResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid de la máquina de gimnasio
    /// </summary>
    public Guid GymMachineGuid { get; set; }

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
    /// Estado de la máquina
    /// </summary>
    public string MachineStatus { get; set; } // Disponible, En Mantenimiento

    /// <summary>
    /// Última revisión
    /// </summary>
    public DateTime? LastRevision { get; set; }

    /// <summary>
    /// Estado del registro
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public UpdateGymMachineRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateGymMachineRequest()
    {
    }
}
