
using LogicApi.Model.Common;

namespace LogicApi.Model.Response.Administration;
/// <summary>
/// Respuesta de Servicio Obtener Bancos
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="cooperativeData"></param>
public class GetCooperativeInformationResponse(CooperativeData cooperativeData) : IApiBaseResponse
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
    /// Obtiene la información de cooperative
    /// </summary>
    /// <value></value>
    public CooperativeData CooperativeData { get; set; } = cooperativeData;
}

