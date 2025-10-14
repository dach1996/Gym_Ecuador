using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PersistenceDb.Models.Core;
/// <summary>
/// Tabla de Pago de órdenes
/// </summary>
[Table(name: "ORDEN_PAGO", Schema = "CORE")]
public class OrderPayment
{
    /// <summary>
    /// Id de Pago
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ORD_ID")]
    [ForeignKey(nameof(Order))]
    public int Id { get; set; }

    /// <summary>
    /// Fecha de Registro del Pago
    /// </summary>
    /// <value></value>
    [Required]
    [Column("PAO_FECHA_REGISTRO")]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Precio del pago
    /// </summary>
    [Required]
    [Column("PAO_PRECIO_BASE")]
    [Precision(10, 2)]
    public decimal Price { get; set; }
    
    /// <summary>
    /// Orden Generada
    /// </summary>
    /// <value></value>
    public Order Order { get; set; }
}
