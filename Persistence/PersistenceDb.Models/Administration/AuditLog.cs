using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla Usuario
/// </summary>
[Table(name: "LOG_AUDITORIA", Schema = "ADMINISTRACION")]
public class AuditLog
{
    /// <summary>
    /// Id
    /// </summary>
    /// <value></value>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("LOG_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Fecha
    /// </summary>
    /// <value></value>
    [Column("LOG_FECHA")]
    [Required]
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Origen de Ip
    /// </summary>
    /// <value></value>
    [Column("LOG_IP_ORIGEN")]
    [Required]
    [StringLength(50)]
    public string OriginIp { get; set; }

    /// <summary>
    /// Id de usuario
    /// </summary>
    /// <value></value>
    [Column("USR_ID")]
    [Required]
    [StringLength(120)]
    public int? UserId { get; set; }

    /// <summary>
    /// Nombre de Usuario
    /// </summary>
    /// <value></value>
    [Column("LOG_NOMBRE_USUARIO")]
    [Required]
    [StringLength(256)]
    public string UserName { get; set; }

    /// <summary>
    /// Operación
    /// </summary>
    /// <value></value>
    [Column("LOG_OPERACION")]
    [Required]
    [StringLength(10)]
    public string Operation { get; set; }

    /// <summary>
    /// Resultado
    /// </summary>
    /// <value></value>
    [Column("LOG_RESULTADO")]
    [Required]
    public bool Result { get; set; }

    /// <summary>
    /// Información de requerimiento
    /// </summary>
    /// <value></value>
    [Column("LOG_INFORMACION_REQUERIMIENTO")]
    [Required]
    public string RequestData { get; set; }

    /// <summary>
    /// Información de respuesta
    /// </summary>
    /// <value></value>
    [Column("LOG_INFORMACION_RESPUESTA")]
    [Required]
    public string ResponseData { get; set; }

}
