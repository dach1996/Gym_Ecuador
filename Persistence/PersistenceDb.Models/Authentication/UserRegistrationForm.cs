using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Models.Authentication;
/// <summary>
/// Tabla Forma de Registro de Usuario
/// </summary>
[Table(name: "FORMA_REGISTRO_USUARIO", Schema = "AUTENTICACION")]
[PrimaryKey(nameof(UserId), nameof(UserTypeRegister))]
public class UserRegistrationForm
{
    /// <summary>
    /// Id
    /// </summary>
    /// <value></value>
    [Key]
    [Required]
    [ForeignKey(nameof(User))]
    [Column("USR_ID")]
    public int UserId { get; set; }

    /// <summary>
    /// Código de Tipo de Registro
    /// </summary>
    /// <value></value>
    [Required]
    [Column("FRU_CODIGO_TIPO_REGISTRO")]
    public UserTypeRegister UserTypeRegister { get; set; }


    /// <summary>
    /// Fecha de Forma de REgistro
    /// </summary>
    /// <value></value>
    [Required]
    [Column("FRU_FECHA_REGISTRO")]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Contraseña
    /// </summary>
    /// <value></value>
    [StringLength(100)]
    [Column("FRU_PASSWORD")]
    public string Password { get; set; }

    /// <summary>
    /// Contraseña Temporal
    /// </summary>
    [StringLength(100)]
    [Column("FRU_PASSWORD_TEMPORAL")]
    public string PasswordTemporary { get; set; }

    /// <summary>
    /// Fecha de último Acceso
    /// </summary>
    /// <value></value>
    [Column("FRU_FECHA_ULTIMO_ACCESO")]
    public DateTime? DateTimeLastAccess { get; set; }

    /// <summary>
    /// Usuario
    /// </summary>
    /// <value></value>
    public User User { get; set; }
}