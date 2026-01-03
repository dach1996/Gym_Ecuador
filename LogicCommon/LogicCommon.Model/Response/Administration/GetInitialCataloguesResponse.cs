
namespace LogicCommon.Model.Response.Administration;
/// <summary>
/// Respuesta de Servicio Obtener Bancos
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="listCatalogCodes"></param>
public class GetInitialCataloguesResponse(
    Dictionary<string, Dictionary<string, string>> listCatalogCodes) 
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    /// <value></value>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Lista de códigos de catálgo
    /// </summary>
    /// <value></value>
    public Dictionary<string, Dictionary<string, string>> ListCatalogCodes { get; set; } = listCatalogCodes;

}

