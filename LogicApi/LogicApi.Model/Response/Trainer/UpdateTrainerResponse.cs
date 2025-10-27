namespace LogicApi.Model.Response.Trainer;

/// <summary>
/// Respuesta de actualizar entrenador
/// </summary>
public class UpdateTrainerResponse : IApiBaseResponse
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
    /// Guid del entrenador actualizado
    /// </summary>
    public Guid TrainerGuid { get; set; }

    /// <summary>
    /// Especialidad del entrenador
    /// </summary>
    public string Specialty { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="trainerGuid"></param>
    /// <param name="specialty"></param>
    public UpdateTrainerResponse(Guid trainerGuid, string specialty)
    {
        TrainerGuid = trainerGuid;
        Specialty = specialty;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateTrainerResponse()
    {
    }
}
