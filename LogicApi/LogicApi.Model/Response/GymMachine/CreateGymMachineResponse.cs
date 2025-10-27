namespace LogicApi.Model.Response.GymMachine;

/// <summary>
/// Respuesta de crear máquina de gimnasio
/// </summary>
public class CreateGymMachineResponse : IApiBaseResponse
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
    /// Guid de la máquina creada
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
    /// Constructor
    /// </summary>
    /// <param name="gymMachineGuid"></param>
    /// <param name="machineName"></param>
    /// <param name="machineType"></param>
    public CreateGymMachineResponse(Guid gymMachineGuid, string machineName, string machineType)
    {
        GymMachineGuid = gymMachineGuid;
        MachineName = machineName;
        MachineType = machineType;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGymMachineResponse()
    {
    }
}
