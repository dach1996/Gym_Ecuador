using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Authentication;
/// <summary>
/// Tabla Persona
/// </summary>
[Table(name: "PERSONA", Schema = "AUTENTICACION")]
public class Person
{
    /// <summary>
    /// Id
    /// </summary>
    /// <value></value>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PNA_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    /// <value></value>
    [Required]
    [Column("PNA_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Fecha de registro 
    /// </summary>
    /// <value></value>
    [Column("PNA_FECHA_REGISTRO")]
    public DateTime? DateTimeRegister { get; set; }

    /// <summary>
    /// Código de Nacionalidad
    /// </summary>
    /// <value></value>
    [StringLength(50)]
    [Column("PNA_CODIGO_NACIONALIDAD")]
    public string NationalityCode { get; set; }

    /// <summary>
    /// Tipo de Documento
    /// </summary>
    /// <value></value>
    [StringLength(50)]
    [Column("PNA_CODIGO_TIPO_DOCUMENTO")]
    public string DocumentTypeCode { get; set; }

    /// <summary>
    /// Documento
    /// </summary>
    /// <value></value>
    [Required]
    [StringLength(50)]
    [Column("PNA_NUMERO_DOCUMENTO")]
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Nombre 
    /// </summary>
    [StringLength(150)]
    [Column("PNA_NOMBRE_INGRESADO")]
    public string Name { get; set; }

    /// <summary>
    /// Apellido
    /// </summary>
    [StringLength(150)]
    [Column("PNA_APELLIDO_INGRESADO")]
    public string LastName { get; set; }

    /// <summary>
    /// Nombres reales
    /// </summary>
    [Required]
    [StringLength(150)]
    [Column("PNA_NOMBRES_REALES")]
    public string RealNames { get; set; }

    /// <summary>
    /// Apellidos Reales
    /// </summary>
    [StringLength(150)]
    [Required]
    [Column("PNA_APELLIDOS_REALES")]
    public string RealLastNames { get; set; }

    /// <summary>
    /// Nombre completo de la persona
    /// </summary>
    [StringLength(300)]
    [Column("PNA_NOMBRE_COMPLETO")]
    public string FullName { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    /// <value></value>
    [Column("USR_ID_REGISTRADOR")]
    public int? UserIdRegister { get; set; }

    /// <summary>
    /// Id de último usuario que realizó logín
    /// </summary>
    /// <value></value>
    [Column("USR_ID_ULTIMO_LOGIN")]
    public int? UserIdLasLogin { get; set; }

    /// <summary>
    /// URL de foto de perfil del usuario
    /// </summary>
    [StringLength(500)]
    [Column("PNA_URL_FOTO_PERFIL")]
    public string ProfilePhotoUrl { get; set; }

}