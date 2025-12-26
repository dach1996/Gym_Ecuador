using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Administration;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Relación Sucursal de Gimnasio con Imágenes
/// </summary>
[Table(name: "SUCURSAL_GIMNASIO_IMAGENES", Schema = "CORE")]
[PrimaryKey(nameof(GymBranchId), nameof(FilePersistenceId))]
public class GymBranchImage
{
    /// <summary>
    /// Id de la sucursal de gimnasio
    /// </summary>
    [Required]
    [Column("SGY_ID")]
    public int GymBranchId { get; set; }

    /// <summary>
    /// Id del archivo persistido
    /// </summary>
    [Required]
    [Column("ARG_ID")]
    public int FilePersistenceId { get; set; }

    /// <summary>
    /// Navegación a la sucursal de gimnasio
    /// </summary>
    [ForeignKey(nameof(GymBranchId))]
    public GymBranch GymBranch { get; set; }

    /// <summary>
    /// Navegación al archivo persistido
    /// </summary>
    [ForeignKey(nameof(FilePersistenceId))]
    public FilePersistence FilePersistence { get; set; }
}

