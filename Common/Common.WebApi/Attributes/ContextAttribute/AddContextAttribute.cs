using Common.Utils.Extensions;
using Common.WebApi.Models;
using Common.WebCommon.Json;
using Common.WebCommon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.WebApi.Attributes.ContextAttribute;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="webHostEnvironment"></param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class AddContextAttribute(ILogger<AddContextAttribute> logger, AppSettingsApi appSettings) : ActionFilterAttribute
{
    private readonly ILogger<AddContextAttribute> _logger = logger;
    private readonly AppSettingsApi _appSettings = appSettings;

    /// <summary>
    /// Ejecución de acción
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var parameterDescriptor in context.ActionDescriptor.Parameters)
        {
            if (!parameterDescriptor.ParameterType.GetInterfaces().AsEnumerable().Any(t => t == typeof(IApiBaseRequest))) continue;
            var postObject = (IApiBaseRequest)context.ActionArguments[parameterDescriptor.Name];
            postObject.ContextRequest = context.HttpContext.Items[nameof(ContextRequest)] as ContextRequest;
            Task.Run(() =>
           {
               var header = GetHeaderCustom(context.HttpContext.Request);
               var method = context.HttpContext.Request.Method;
               var path = GetPath(context.HttpContext);
               SaveLoggerRequestAsync(new LogRequestModel
               {
                   LogMessage = $"Request Headers: {postObject.ContextRequest.RequestId}",
                   ModelToLog = header,
                   Method = method,
                   Path = path,
               });
               SaveLoggerRequestAsync(new LogRequestModel
               {
                   LogMessage = $"Request Body: {postObject.ContextRequest.RequestId}",
                   ModelToLog = postObject,
                   Method = method,
                   Path = path,
               });
           });
        }
        base.OnActionExecuting(context);
    }


     /// <summary>
    /// Ejecución tras resultado
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        if (context.Result is OkObjectResult result)
        {
            var statusCode = context.HttpContext.Response.StatusCode;
            var path = GetPath(context.HttpContext);
            var method = context.HttpContext.Request.Method;
            Task.Run(() =>
                SaveLoggerRequestAsync(new LogRequestModel
                {
                    LogMessage = $"Response: {statusCode}",
                    ModelToLog = result.Value,
                    Path = path,
                    Method = method
                })
            );
        }
        else
        {
            _logger.LogInformation("Response not OkObjectResult: {@StatusCode}", context.HttpContext.Response.StatusCode);
        }
    }

    /// <summary>
    /// Obtiene el Path de del contexto
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private static string GetPath(HttpContext httpContext)
        => httpContext.Request.Path + (httpContext.Request.QueryString.ToString().IsNullOrEmpty() ? string.Empty : httpContext.Request.QueryString);




    /// <summary>
    /// Envia a Logear el Request 
    /// </summary>
    /// <param name="logRequestModel"></param>
    private void SaveLoggerRequestAsync(LogRequestModel logRequestModel)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(logRequestModel.ModelToLog,
            new JsonSerializerSettings { ContractResolver = _appSettings.LogSensitiveInformation ? new SensitiveDevelopmentProperty() : new SensitiveProductionProperty(), ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            _logger.Log(logRequestModel.LogLevel, "{@LogMessage}: {@Path} {@Method} : {@Model}", logRequestModel.LogMessage, logRequestModel.Path, logRequestModel.Method, jsonRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar Log: {@Message}", ex.Message);
        }
    }

    /// <summary>
    /// Obtiene los headers sin el Token
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private Dictionary<string, string> GetHeaderCustom(HttpRequest request)
    {
        var headersClone = request.Headers.ToDictionary(t => t.Key.ToUpper(), t => $"{t.Value}");
        if (!_appSettings.LogHeadersRemove.IsNullOrEmpty())
            foreach (var header in _appSettings.LogHeadersRemove.Select(t => t.ToUpper()))
                headersClone.Remove(header);
        return headersClone;
    }
}

