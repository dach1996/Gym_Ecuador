using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicApi.Model.Response.Place;
using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Place;
/// <summary>
/// Request para actualizar un lugar favorito de Usuari
/// </summary>
public class UpdateFavoritePlaceRequest : IApiBaseRequest<UpdateFavoritePlaceResponse>
{
    /// <summary>
    /// Código de Lugar
    /// </summary>
    /// <value></value>
    [Required]
    public string PlaceCode { get; set; }

    /// <summary>
    /// Tipo de Lugar
    /// </summary>
    /// <value></value>

    [ValidateEnum] 
    public PlaceFavoriteType PlaceFavoriteType { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

/// <summary>
/// Tipo de lugar favorito
/// </summary>
public enum PlaceFavoriteType
{
    /// <summary>
    /// Origen
    /// </summary>
    Origin = 1,

    /// <summary>
    /// Destino
    /// </summary>
    Destination = 2
}