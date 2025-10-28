using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Administration;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Relación Seguimiento de Procesos con Imágenes
/// </summary>
[Table(name: "SEGUIMIENTO_PROCESOS_IMAGENES", Schema = "CORE")]
[PrimaryKey(nameof(ProcessTrackingId), nameof(FilePersistenceId))]
public class ProcessTrackingImage
{
    /// <summary>
    /// Id del seguimiento de proceso
    /// </summary>
    [Required]
    [Column("SPR_ID")]
    public int ProcessTrackingId { get; set; }

    /// <summary>
    /// Id del archivo persistido
    /// </summary>
    [Required]
    [Column("ARG_ID")]
    public int FilePersistenceId { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("SPI_FECHA_REGISTRO")]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int UserIdRegister { get; set; }

    /// <summary>
    /// Navegación al seguimiento de proceso
    /// </summary>
    [ForeignKey(nameof(ProcessTrackingId))]
    public ProcessTracking ProcessTracking { get; set; }

    /// <summary>
    /// Navegación al archivo persistido
    /// </summary>
    [ForeignKey(nameof(FilePersistenceId))]
    public FilePersistence FilePersistence { get; set; }
}

