using LogicApi.Model.Common;

namespace LogicApi.Model.Response.Companion;
/// <summary>
/// Respuesta de obtener mis compañero de viaje
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="companions"></param>
public class GetMyCompanionResponse(IEnumerable<CompanionItem> companions) : IApiBaseResponse
{
    /// <summary>
    /// Compañeros
    /// </summary>
    /// <value></value>
    public IEnumerable<CompanionItem> Companions { get; set; } = companions;

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
