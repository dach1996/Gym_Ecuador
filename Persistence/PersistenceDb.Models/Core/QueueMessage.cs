using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;
/// <summary>
/// Tabla de Mensajes Encolados
/// </summary>
[Table(name: "MENSAJES_ENCOLADOS", Schema = "CORE")]
public class QueueMessage
{
    /// <summary>
    /// Id 
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("MEC_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Fecha de Registro
    /// </summary>
    /// <value></value>
    [Column("MEC_FECHA_REGISTRO")]
    [Required]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Identificador Interno
    /// </summary>
    /// <value></value>
    [Column("MEC_IDENTIFICADOR_INTERNO")]
    [Required]
    public Guid InternlaIdentifier { get; set; }

    /// <summary>
    /// Identificador Interno
    /// </summary>
    /// <value></value>
    [Column("MEC_INFORMACION_ADICIONAL")]
    [Required]
    public string AdditionalInformation { get; set; }

    /// <summary>
    /// Tipo O funcionalidad
    /// </summary>
    /// <value></value>
    [Column("MEC_TIPO")]
    [Required]
    public byte Type { get; set; }

}
