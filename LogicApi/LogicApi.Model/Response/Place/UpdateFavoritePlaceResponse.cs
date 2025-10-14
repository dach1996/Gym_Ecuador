namespace LogicApi.Model.Response.Place;
/// <summary>
/// Respuesta del servicio Obtener Lugares Favoritos
/// </summary>
public class UpdateFavoritePlaceResponse : IApiBaseResponse
{
    /// <summary>
    /// Lugares de Origen
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> FavoritePlaces { get; set; }

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }
}

