using Common.Utils.Extensions;
using Common.WebApi.Attributes.ContextAttribute.Interface;
using Common.WebApi.Models;
using Common.WebCommon.Json;
using Common.WebCommon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.WebApi.Attributes.ContextAttribute.Implementations;
/// <summary>
/// Clase base para agregar contexto
/// </summary>
/// <remarks>
/// Constructor 
/// </remarks>
/// <param name="appSettings"></param>
/// <param name="logger"></param>
public abstract class AddContextBase(ILogger<AddContextBase> logger, AppSettingsCommon appSettings) : IAddContext
{

    protected readonly ILogger<AddContextBase> Logger = logger;
    protected readonly AppSettingsCommon AppSettings = appSettings;

    /// <summary>
    /// Obtiene el Path de del contexto
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    protected static string GetPath(HttpContext httpContext)
        => httpContext.Request.Path + (httpContext.Request.QueryString.ToString().IsNullOrEmpty() ? string.Empty : httpContext.Request.QueryString);

    /// <summary>
    /// Envia a Logear el Request 
    /// </summary>
    /// <param name="logRequestModel"></param>
    protected void SaveLoggerRequestAsync(LogRequestModel logRequestModel)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(logRequestModel.ModelToLog,
            new JsonSerializerSettings
            {
                ContractResolver = AppSettings.LogSensitiveInformation ? new SensitiveDevelopmentProperty() : new SensitiveProductionProperty(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Converters = [new StringEnumConverter()]
            });
            Logger.Log(logRequestModel.LogLevel, "{@LogMessage}: {@Path} {@Method} : {@Model}", logRequestModel.LogMessage, logRequestModel.Path, logRequestModel.Method, jsonRequest);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al registrar Log: {@Message}", ex.Message);
        }
    }

    /// <summary>
    /// Obtiene los headers sin el Token
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    protected Dictionary<string, string> GetHeaderCustom(HttpRequest request)
    {
        var headersClone = request.Headers.ToDictionary(t => t.Key.ToUpper(), t => $"{t.Value}");
        if (!AppSettings.LogHeadersRemove.IsNullOrEmpty())
            foreach (var header in AppSettings.LogHeadersRemove.Select(t => t.ToUpper()))
                headersClone.Remove(header);
        return headersClone;
    }

    public abstract void OnActionExecuting(ActionExecutingContext context);
    public abstract void OnResultExecuted(ResultExecutedContext context);
}