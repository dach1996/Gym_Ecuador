using Common.WebCommon.Attributes;
using LogicApi.Model.Implementations.Authorization;
using LogicApi.Model.Response.Authorization;

namespace LogicApi.Model.Request.Authorization;
/// <summary>
/// Request para asignar persona
/// </summary>
public class AssignPersonRequest : IRequest<LoginResponse>, IApiBaseRequest
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
    public string DocumentTypeCode { get; set; }

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
    /// Código de Nacionalidad
    /// </summary>
    /// <value></value>
    [Required]
    public string NationalityCode { get; set; }

    /// <summary>
    /// Implementación de Login
    /// </summary>
    [Required]
    public LoginImplementations LoginType { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}