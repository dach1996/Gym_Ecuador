using Common.WebCommon.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Common.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models.ContextRequestModel;
using Common.WebApi.Models.AppSettingsModel;
namespace Common.WebApi.Attributes.ContextAttribute.Implementations;
/// <summary>
/// Agregar contexto en Api Gateway
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="appSettings"></param>
/// <param name="logger"></param>
public class AdministratorApiAddContext(
    AppSettingsAdministrator appSettings,
    ILogger<AdministratorApiAddContext> logger) : AddContextBase(logger, appSettings)
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var parameterDescriptor in context.ActionDescriptor.Parameters)
        {
            if (!parameterDescriptor.ParameterType.GetInterfaces().Contains(typeof(IApiBaseRequest))) continue;
            var postObject = (IApiBaseRequest)context.ActionArguments[parameterDescriptor.Name];
            postObject.ContextRequest = context.HttpContext.Items[nameof(CommonContextRequest)] as AdminContextRequest;
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
    }


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
            Logger.LogInformation("Response not OkObjectResult: {@StatusCode}", context.HttpContext.Response.StatusCode);
        }
    }
}