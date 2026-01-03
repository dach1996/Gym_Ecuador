namespace LogicCommon.Model.Response.Person;

/// <summary>
/// Detalle completo de persona
/// </summary>
public class PersonDetail
{
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
}