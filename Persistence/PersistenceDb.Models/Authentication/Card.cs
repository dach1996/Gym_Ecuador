using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Attributes;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Models.Authentication;
/// <summary>
/// Tabla de Tarjetas
/// </summary>
[Table(name: "TARJETA", Schema = "AUTENTICACION")]
public class Card
{
    /// <summary>
    /// Identificador de dispositivo
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("TTA_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Código de Marca de Tarjeta
    /// </summary>
    /// <value></value>
    [Required]
    [Column("TTA_CODIGO_MARCA")]
    public CardBrand CardBrand { get; set; }

    /// <summary>
    /// Bin de Tarjeta
    /// </summary>
    /// <value></value>
    [Required]
    [StringLength(8)]
    [Column("TTA_BIN")]
    [EncryptColumn]
    public string Bin { get; set; }

    /// <summary>
    /// Pan de Tarjeta
    /// </summary>
    /// <value></value>
    [Required]
    [StringLength(5)]
    [EncryptColumn]
    [Column("TTA_PAN")]
    public string Pan { get; set; }

    /// <summary>
    /// Id de Usuario
    /// </summary>
    /// <value></value>
    [Required]
    [Column("USR_ID")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    /// <summary>
    /// Registro Eliminado?
    /// </summary>
    /// <value></value>
    [Column("TTA_ELIMINADO")]
    [Required]
    public bool IsDelete { get; set; }

    /// <summary>
    /// Código de tipo de Tarjeta
    /// </summary>
    /// <value></value>
    [Required]
    [Column("TTA_CODIGO_TIPO")]
    public CardType CardType { get; set; }

    /// <summary>
    ///  Dueño de Tarjeta
    /// </summary>
    /// <value></value>
    [Required]
    [Column("TTA_PROPIETARIO_TARJETA")]
    [StringLength(500)]
    [EncryptColumn]
    public string CardOwner { get; set; }

    /// <summary>
    /// Cantidad de nùmeros de tarjeta
    /// </summary>
    /// <value></value>
    [Required]
    [Column("TTA_LONGITUD_TARJETA")]
    public int CardLength { get; set; }

    /// <summary>
    /// Usuario
    /// </summary>
    /// <value></value>
    public User User { get; set; }
}
