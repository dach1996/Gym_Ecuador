namespace LogicApi.Model.Response.GymMachine;

/// <summary>
/// Respuesta de actualizar máquina de gimnasio
/// </summary>
public class UpdateGymMachineResponse : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Guid de la máquina actualizada
    /// </summary>
    public Guid GymMachineGuid { get; set; }

    /// <summary>
    /// Estado de la máquina
    /// </summary>
    public string MachineStatus { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="gymMachineGuid"></param>
    /// <param name="machineStatus"></param>
    public UpdateGymMachineResponse(Guid gymMachineGuid, string machineStatus)
    {
        GymMachineGuid = gymMachineGuid;
        MachineStatus = machineStatus;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateGymMachineResponse()
    {
    }
}
