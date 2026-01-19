using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Core;

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
    public DateTime DateTimeRegister { get; set; }

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
    /// Fecha de nacimiento
    /// </summary>
    /// <value></value>
    [Column("PNA_FECHA_NACIMIENTO")]
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Id de género
    /// </summary>
    /// <value></value>
    [Column("CAT_ID_GENERO")]
    [ForeignKey(nameof(GenderCatalog))]
    public int? GenderCatalogId { get; set; }

    /// <summary>
    /// Id de tipo de identificación
    /// </summary>
    /// <value></value>
    [Column("TID_ID")]
    [ForeignKey(nameof(TypeIdentification))]
    public byte? TypeIdentificationId { get; set; }

    /// <summary>
    /// Género de la persona
    /// </summary>
    /// <value></value>
    public Catalog GenderCatalog { get; set; }

    /// <summary>
    /// Tipo de identificación
    /// </summary>
    /// <value></value>
    public TypeIdentification TypeIdentification { get; set; }
    /// <summary>
    /// Lista de usuarios registrados
    /// </summary>
    /// <value></value>
    public ICollection<User> Users { get; set; }


    /// <summary>
    /// Lista de clientes de sucursales de gimnasio
    /// </summary>
    /// <value></value>
    public ICollection<ClientGymBranch> ClientGymBranches { get; set; }
}