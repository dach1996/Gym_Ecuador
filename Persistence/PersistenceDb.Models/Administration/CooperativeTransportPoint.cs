using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de cooperativa y punto de transporte
/// </summary>
[Table(name: "COOPERATIVA_PUNTO_TRANSPORTE", Schema = "ADMINISTRACION")]
public class CooperativeTransportPoint
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CPT_ID")]
    /// <summary>
    /// Id de Punto de Transporte
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [Column("PTT_ID")]
    /// <summary>
    /// Id de Punto de Transporte
    /// </summary>
    /// <value></value>
    public int TransportPointId { get; set; }

    [Required]
    [Column("COP_ID")]
    /// <summary>
    /// Id de Cooperativa
    /// </summary>
    /// <value></value>
    public int CooperativeId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("CPT_CODIGO")]
    /// <summary>
    /// Código del Punto de Transporte
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    /// <summary>
    /// Cooperativa
    /// </summary>
    /// <value></value>
    public Cooperative Cooperative { get; set; }

    /// <summary>
    /// Punto de Transporte
    /// </summary>
    /// <value></value>
    public TransportPoint TransportPoint { get; set; }
}
