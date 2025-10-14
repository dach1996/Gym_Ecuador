namespace LogicApi.Model.Response.Companion;
/// <summary>
/// Respuesta de crear compañero de viaje
/// </summary>
public class CreateOrUpdateCompanionResponse : IApiBaseResponse
{
    /// <summary>
    /// Id de Nuevo acompañante
    /// </summary>
    /// <value></value>
    public long CompanionId { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="companionId"></param>
    public CreateOrUpdateCompanionResponse(long companionId)
    {
        CompanionId = companionId;
    }

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }
}