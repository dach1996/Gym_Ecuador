using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;
/// <summary>
/// Tabla de Asientas Reservados a Personas
/// </summary>
[Table(name: "ORDEN_ASIENTO_PERSONA", Schema = "CORE")]
[PrimaryKey(nameof(OrderId), nameof(ReserveSeatId), nameof(PersonId))]
public class OrderSeatPerson
{
    /// <summary>
    /// Id de Orden
    /// </summary>
    /// <value></value>
    [Required]
    [ForeignKey(nameof(Order))]
    [Column("ORD_ID")]
    public int OrderId { get; set; }

    /// <summary>
    /// Id de reserva de Asiento
    /// </summary>
    /// <value></value>
    [Required]
    [Column("RSA_ID")]
    [ForeignKey(nameof(ReserveSeat))]
    public int ReserveSeatId { get; set; }

    /// <summary>
    /// Id de Persona
    /// </summary>
    /// <value></value>
    [Required]
    [Column("PNA_ID")]
    [ForeignKey(nameof(PersonId))]
    public int PersonId { get; set; }

    /// <summary>
    /// Precio de reserva de Asiento
    /// </summary>
    [Required]
    [Column("OAP_PRECIO")]
    [Precision(10, 2)]
    public decimal Price { get; set; }

    /// <summary>
    /// Orden
    /// </summary>
    /// <value></value>
    public Order Order { get; set; }

    /// <summary>
    /// Reserva de Asiento
    /// </summary>
    /// <value></value>
    public ReserveSeat ReserveSeat { get; set; }

    /// <summary>
    /// Persona
    /// </summary>
    /// <value></value>
    public Person Person { get; set; }
}
