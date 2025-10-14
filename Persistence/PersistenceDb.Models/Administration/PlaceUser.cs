using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Authentication;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de Lugar Usuario
/// </summary>
[Table(name: "LUGAR_USUARIO", Schema = "ADMINISTRACION")]
[PrimaryKey(nameof(UserId), nameof(PlaceId))]
public class PlaceUser
{
    /// <summary>
    /// Id de Usuario
    /// </summary>
    /// <value></value>
    [Column("USR_ID")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    /// <summary>
    /// Id de Lugar
    /// </summary>
    /// <value></value>
    [Column("LOD_ID")]
    [ForeignKey(nameof(Place))]
    public int PlaceId { get; set; }

    /// <summary>
    ///  Es Favorito en Origen
    /// </summary>
    /// <value></value>
    [Column("LUS_FAVORITO_ORIGEN")]
    public bool IsFavoriteOrigin { get; set; }

    /// <summary>
    ///  Es Favorito en Origen
    /// </summary>
    /// <value></value>
    [Column("LUS_FAVORITO_DESTINO")]
    public bool IsFavoriteDestination { get; set; }

    /// <summary>
    ///Usuario
    /// </summary>
    public User User { get; set; }

    /// <summary>
    ///Lugar
    /// </summary>
    public Place Place { get; set; }
}
