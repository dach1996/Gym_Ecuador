namespace LogicApi.Model.Response.Companion;
/// <summary>
/// Respuesta de buscar compañero de viaje
/// </summary>
/// <remarks>
/// Item de compañero
/// </remarks>
/// <param name="person"></param>
public class SearchCompanionResponse(PersonInformation person) : IApiBaseResponse
{
    /// <summary>
    /// Compañero encontrado
    /// </summary>
    /// <value></value>
    public PersonInformation Person { get; set; } = person;

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

/// <summary>
/// Información de Persona
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="personGuid"></param>
/// <param name="documentNumber"></param>
/// <param name="fullName"></param>
public class PersonInformation(Guid personGuid, string documentNumber, string fullName)
{
    /// <summary>
    /// Id de Persona
    /// </summary>
    /// <value></value>
    public Guid PersonGuid { get; set; } = personGuid;

    /// <summary>
    /// Número de Documento
    /// </summary>
    /// <value></value>
    public string DocumentNumber { get; set; } = documentNumber;

    /// <summary>
    /// Nombre Completo
    /// </summary>
    /// <value></value>
    public string FullName { get; set; } = fullName;
}