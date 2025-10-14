namespace LogicApi.Model.Response.CommonConfiguration.Common;
/// <summary>
/// Items de Catálogo
/// </summary>
public class ItemCatalogResponse
{
    /// <summary>
    /// Nombre de item de catálogo
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    /// <summary>
    /// Valor de item de catálogo
    /// </summary>
    /// <value></value>
    public string Value { get; set; }

    /// <summary>
    /// Indica si esta Activo o Inactivo
    /// </summary>
    /// <value></value>
    public bool Status { get; set; }

}

/// <summary>
/// Archivo Item de Catálogo 
/// </summary>
public class ItemCatalogFileResponse : ItemCatalogResponse
{
    /// <summary>
    /// Enum 
    /// </summary>
    /// <value></value>
    public string Enum { get; set; }
}