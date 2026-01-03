using LogicCommon.Model.Response.Person;

namespace LogicApi.Model.Response.Person;

/// <summary>
/// Respuesta de obtener persona por número de cédula
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="person"></param>
public class GetPersonByDocumentNumberResponse(PersonDetail person) : IApiBaseResponse
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
    /// Datos de la persona
    /// </summary>
    public PersonDetail Person { get; set; } = person;
}
