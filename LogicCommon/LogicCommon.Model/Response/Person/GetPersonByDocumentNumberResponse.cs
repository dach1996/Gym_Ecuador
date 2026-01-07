namespace LogicCommon.Model.Response.Person;

/// <summary>
/// Respuesta común de obtener persona por número de cédula
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="person"></param>
public class GetPersonByDocumentNumberResponse(PersonDetail person)
{
    /// <summary>
    /// Datos de la persona
    /// </summary>
    public PersonDetail Person { get; set; } = person;
}

