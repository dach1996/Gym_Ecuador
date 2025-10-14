namespace LogicApi.Model.Common;
/// <summary>
/// Item de Compañero
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="personGuid"></param>
/// <param name="documentNumber"></param>
/// <param name="fullName"></param>
public class CompanionItem(Guid personGuid, string documentNumber, string fullName)
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
    /// Nombre de Acompañante
    /// </summary>
    /// <value></value>
    public string FullName { get; set; } = fullName;
}