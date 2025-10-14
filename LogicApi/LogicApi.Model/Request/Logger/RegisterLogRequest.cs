using LogicApi.Model.Response;

namespace LogicApi.Model.Request.Logger;

/// <summary>
/// Request de registro de Log
/// </summary>
public class RegisterLogRequest : IRequest<HandlerResponse>, IApiBaseRequest
{
    /// <summary>
    /// Log Type
    /// </summary>
    [Required]
    public string LogType { get; set; }

    /// <summary>
    /// String Log
    /// </summary>
    [Required]
    public string LogJson { get; set; }

    /// <summary>
    /// Verifica si el log enviado fue solicitado por el Logín
    /// </summary>
    /// <value></value>
    public bool RequiredLog { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
