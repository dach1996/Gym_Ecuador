namespace LogicApi.Model.Response.Gym;

/// <summary>
/// Respuesta de crear gimnasio
/// </summary>
public class CreateGymResponse : IApiBaseResponse
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
    /// Guid del gimnasio creado
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
    public CreateGymResponse(Guid gymGuid, string name)
    {
        GymGuid = gymGuid;
        Name = name;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGymResponse()
    {
    }
}
