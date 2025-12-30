using Common.WebCommon.Attributes;
using LogicApi.Model.Implementations.Authorization;
using LogicApi.Model.Response.Authorization;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Authorization;
/// <summary>
/// Request para asignar persona
/// </summary>
public class AssignPersonRequest : IApiBaseRequest<LoginResponse>
{
    /// <summary>
    /// Usuario (Encriptado)
    /// </summary>
    [EncryptedField]
    [Required]
    [IgnoreSensible]
    public string UserName { get; set; }

    /// <summary>
    /// Correo
    /// </summary>
    [Required]
    [IgnoreSensible]
    public string Email { get; set; }

    /// <summary>
    /// Contraseña (Encriptada)
    /// </summary>
    [Required]
    [IgnoreSensible]
    public string Password { get; set; }

    /// <summary>
    /// Nueva Contraseña (Encriptada)
    /// </summary>
    [Required]
    [EncryptedField]
    [IgnoreSensible]
    public string NewPassword { get; set; }

    /// <summary>
    /// Número de Documento
    /// </summary>
    /// <value></value>
    [Required]
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Tipo de Documento
    /// </summary>
    /// <value></value>
    [Required]
    public string TypeIdentificationId { get; set; }

    /// <summary>
    /// Código de Género
    /// </summary>
    /// <value></value>
    [Required]
    public string GenderCode { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    /// <value></value>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Apellido
    /// </summary>
    /// <value></value>
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// Teléfono
    /// </summary>
    /// <value></value>
    [Required]
    public string Phone { get; set; }

    /// <summary>
    /// Fecha de Nacimiento
    /// </summary>
    /// <value></value>
    [Required]
    public DateTime Birthday { get; set; }

    /// <summary>
    /// Implementación de Login
    /// </summary>
    [Required]
    public LoginImplementations LoginType { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}