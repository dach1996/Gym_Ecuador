using System.Text.Json.Serialization;

namespace LogicCommon.Model.Response.Person;
/// <summary>
/// Detalle completo de persona
/// </summary>
public class PersonDetail
{
    /// <summary>
    /// Id de la persona
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>
    /// Guid de la persona
    /// </summary>
    public Guid Guid { get; set; }

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

    /// <summary>
    /// Fecha de nacimiento
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Código del género
    /// </summary>
    public string GenderItemCatalogCode { get; set; }

    /// <summary>
    ///  Teléfono
    /// </summary>
    /// <value></value>
    public string Phone { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }
}