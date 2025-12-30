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

/// <summary>
/// Detalle completo de persona
/// </summary>
public class PersonDetail
{
    /// <summary>
    /// Número de documento
    /// </summary>
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    public string Names { get; set; }

    /// <summary>
    /// Apellidos
    /// </summary>
    public string LastNames { get; set; }

    /// <summary>
    /// Nombre completo
    /// </summary>
    public string FullName { get; set; }
}

