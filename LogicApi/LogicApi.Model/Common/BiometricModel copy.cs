namespace LogicApi.Model.Common;
/// <summary>
/// Modelo de datos para la latitud y longitud
/// </summary>
public class LatitudeLongitudeModel
{
    /// <summary>
    /// Latitud
    /// </summary>
    [Required]
    [Range(-90, 90)]
    public decimal Latitude { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    public LatitudeLongitudeModel(decimal latitude, decimal longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
    /// <summary>
    /// 
    /// </summary>
    public LatitudeLongitudeModel()
    {
    }

    /// <summary>
    /// Longitud
    /// </summary>
    [Required]
    [Range(-180, 180)]
    public decimal Longitude { get; set; }


}