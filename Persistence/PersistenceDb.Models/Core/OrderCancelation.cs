using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Models.Core;
/// <summary>
/// Tabla Cancelaciones de Órdenes
/// </summary>
[Table(name: "ORDEN_CANCELACION", Schema = "CORE")]
public class OrderCancelation
{
    /// <summary>
    /// Id de Orden
    /// </summary>
    [Key]
    [Required]
    [Column("ORD_ID")]
    [ForeignKey(nameof(Order))]
    public int Id { get; set; }

    /// <summary>
    /// Fecha de cancelación de Orden
    /// </summary>
    /// <value></value>
    [Column("OCN_FECHA_CANCELACION")]
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Razón de cancelación
    /// </summary>
    /// <value></value>
    [Column("OCN_TIPO_CANCELACION")]
    public OrderCancelationType Type { get; set; }

    /// <summary>
    /// Razón de cancelación
    /// </summary>
    /// <value></value>
    [Column("OCN_MOTIVO_CANCELACION")]
    public string Reason { get; set; }

    /// <summary>
    /// Orden
    /// </summary>
    /// <value></value>
    public Order Order { get; set; }
}
