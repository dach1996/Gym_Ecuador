using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Administration;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Relación Equipamiento con Imágenes
/// </summary>
[Table(name: "EQUIPAMIENTO_IMAGENES", Schema = "CORE")]
[PrimaryKey(nameof(EquipmentId), nameof(FilePersistenceId))]
public class EquipmentImage
{
    /// <summary>
    /// Id del equipamiento
    /// </summary>
    [Required]
    [Column("EQP_ID")]
    public int EquipmentId { get; set; }

    /// <summary>
    /// Id del archivo persistido
    /// </summary>
    [Required]
    [Column("ARG_ID")]
    public int FilePersistenceId { get; set; }

    /// <summary>
    /// Navegación al equipamiento
    /// </summary>
    [ForeignKey(nameof(EquipmentId))]
    public Equipment Equipment { get; set; }

    /// <summary>
    /// Navegación al archivo persistido
    /// </summary>
    [ForeignKey(nameof(FilePersistenceId))]
    public FilePersistence FilePersistence { get; set; }
}

