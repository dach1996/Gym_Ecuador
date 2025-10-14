namespace LogicApi.Model.Common;
/// <summary>
/// Información de Lugares
/// </summary>
public class PlaceInformation
{
    /// <summary>
    /// Identificador de catálogo
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// Código de Lugar
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    /// <value></value>
    public string ShortName { get; set; }

    /// <summary>
    /// Provincia
    /// </summary>
    /// <value></value>
    public string ProvinceCode { get; set; }

    /// <summary>
    /// Provincia
    /// </summary>
    /// <value></value>
    public string ProvinceName { get; set; }

    /// <summary>
    /// Provincia
    /// </summary>
    /// <value></value>
    public int ProvinceId { get; set; }
}
