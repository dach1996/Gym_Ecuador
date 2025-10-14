using LogicApi.Model.Response.Common;

namespace LogicApi.Model.Response.Administration;
/// <summary>
/// Respuesta de Servicio Obtener Bancos
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="listCatalogCodes"></param>
/// <param name="controlValidations"></param>
public class GetInitialCataloguesResponse(
    IEnumerable<CatalogCodes> listCatalogCodes,
    Dictionary<string, ControlValidationItem> controlValidations) : IApiBaseResponse
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
    public IEnumerable<CatalogCodes> ListCatalogCodes { get; set; } = listCatalogCodes;

    /// <summary>
    /// Validaciones de control para Input
    /// </summary>
    /// <value></value>
    public Dictionary<string, ControlValidationItem> InputControlValidations { get; set; } = controlValidations;
}



/// <summary>
/// Items de Catálogo
/// </summary>
public class ItemsCatalogue
{
    /// <summary>
    /// Código
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    /// <value></value>
    public string Description { get; set; }

    /// <summary>
    /// Información Adicional
    /// </summary>
    /// <value></value>
    public object AdditionalInformation { get; set; }

}


/// <summary>
/// Item de Expresión Regutlar
/// </summary>
public class ControlValidationItem
{
    /// <summary>
    /// Validaciones conformado por una expresión regular y un mensaje a mostrar
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> Validations { get; set; }

    /// <summary>
    /// Restricciones que no muestran mensaje
    /// </summary>
    /// <value></value>
    public IEnumerable<string> Restrictions { get; set; }
}