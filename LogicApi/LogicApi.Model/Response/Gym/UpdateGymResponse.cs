namespace LogicApi.Model.Response.Gym;

/// <summary>
/// Respuesta de actualizar gimnasio
/// </summary>
public class UpdateGymResponse : IApiBaseResponse
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
    /// Guid del gimnasio actualizado
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="gymGuid"></param>
    /// <param name="name"></param>
    public UpdateGymResponse(Guid gymGuid, string name)
    {
        GymGuid = gymGuid;
        Name = name;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateGymResponse()
    {
    }
}
