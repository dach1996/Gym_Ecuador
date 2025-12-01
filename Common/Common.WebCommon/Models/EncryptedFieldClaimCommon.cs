namespace Common.WebCommon.Models;
/// <summary>
/// Datos encritados en claim
/// </summary>
public class EncryptedFieldClaimCommon
{
    /// <summary>
    /// Correo Electrónico
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Nombre de Usuario
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Primer Nombre
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Apellido
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    public Guid UserGuid { get; set; }

}