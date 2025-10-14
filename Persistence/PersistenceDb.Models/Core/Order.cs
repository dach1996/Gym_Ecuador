using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Enums;
using PersistenceDb.Models.Models;

namespace PersistenceDb.Models.Core;
/// <summary>
/// Tabla Reserva Orden
/// </summary>
[Table(name: "ORDEN", Schema = "CORE")]
public class Order : IEntityControlRow
{
    /// <summary>
    /// Id de Orden
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ORD_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid de Orden
    /// </summary>
    [Required]
    [Column("ORD_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id de la ruta
    /// </summary>
    [Column("RUT_ID")]
    [ForeignKey(nameof(CooperativeRoute))]
    public int CooperativeRouteId { get; set; }

    /// <summary>
    /// Id de Usuario
    /// </summary>
    [Required]
    [Column("USR_ID")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    /// <summary>
    /// Fecha de Registro
    /// </summary>
    /// <value></value>
    [Column("ORD_FECHA_REGISTRO")]
    [Required]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Id de Cooperative
    /// </summary>
    /// <value></value>
    [Required]
    [Column("ORD_ESTADO")]
    public OrderState State { get; set; }

    /// <summary>
    /// Última fecha de actualización de Estado
    /// </summary>
    /// <value></value>
    [Required]
    [Column("ORD_ULTIMA_FECHA_ACTUALIZACION")]
    public DateTime LastDateTimeUpdate { get; set; }

    /// <summary>
    /// Fecha de expiración de orden
    /// </summary>
    /// <value></value>
    [Required]
    [Column("ORD_FECHA_EXPIRACION")]
    public DateTime DateTimeExpiration { get; set; }

    /// <summary>
    /// Identificador de mensaje de Queue
    /// </summary>
    /// <value></value>
    [Column("MEC_ID")]
    [ForeignKey(nameof(QueueMessage))]
    public int? QueueMessageId { get; set; }

    /// <summary>
    /// Control de Versión
    /// </summary>
    [Column("ORD_CONTROL_VERSION")]
    public DateTime RowControl { get; set; }

    /// <summary>
    /// Usuario asociado
    /// </summary>
    /// <value></value>
    public User User { get; set; }

    /// <summary>
    /// Reserva de asiento y personas
    /// </summary>
    /// <value></value>
    public ICollection<OrderSeatPerson> OrderSeatPeople { get; set; }

    /// <summary>
    /// Cancelación de Orden
    /// </summary>
    /// <value></value>
    public OrderCancelation OrderCancelation { get; set; }

    /// <summary>
    /// Pago de Órden
    /// </summary>
    /// <value></value>
    public OrderPayment OrderPayment { get; set; }

    /// <summary>
    /// Queue de Mensaje
    /// </summary>
    /// <value></value>
    public QueueMessage QueueMessage { get; set; }

    /// <summary>
    /// Ruta de Cooperativa
    /// </summary>
    /// <value></value>
    public CooperativeRoute CooperativeRoute { get; set; }
}
