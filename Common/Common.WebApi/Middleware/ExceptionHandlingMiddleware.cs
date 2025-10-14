
using Common.Messages;
using Common.Utils.CustomExceptions;
using Common.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using Common.WebApi.Models.Enum;
using Common.PluginFactory.Interface;
using Common.Utils.Extensions;

namespace Common.WebApi.Middleware;

/// <summary>
/// Constructor
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<MiddlewareBase> logger,
    IPluginFactory pluginFactory) : MiddlewareBase(
        next,
        logger,
        pluginFactory)
{

    /// <summary>
    /// Invoke
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await Next(httpContext).ConfigureAwait(false);
        }
        catch (CustomException ex)
        {
            var message = UserMessages.GetErrorMessageByCode(ex.Code);
            var messageResponse = $"{message.Message} ({message.Code})";
            if (WebHostEnvironment.IsDevelopmentOrDebug() && !string.IsNullOrEmpty(ex.AdditionalInfoError))
                messageResponse = $"{messageResponse} - {ex.AdditionalInfoError}";
            Logger.LogError("CustomException (Code: {@Code} - HTTP: {@CodeHttp} - Message: {@Message} - Reason: {@AdditionalInfoError})", ex.Code, ex.CodeHttp, message.Message, ex.AdditionalInfoError);
            await SetMessageResponse(httpContext, (int)HttpStatusCode.OK, message.Code, messageResponse);
        }
        catch (Exception ex)
        {
            var code = (int)MessagesCodesError.SystemError;
            var message = UserMessages.GetErrorMessageByCode(code);
            var messageResponse = $"{message.Message} ({message.Code})";
            if (WebHostEnvironment.IsDevelopmentOrDebug())
                messageResponse = $"{messageResponse} - {ex.Message}";
            Logger.LogError(ex, "Exception (HTTP: {@CodeHttp} - {@Message}) ", (int)HttpStatusCode.InternalServerError, ex.Message);
            await SetMessageResponse(httpContext, (int)HttpStatusCode.OK, message.Code, messageResponse).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Controlador Genérico 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="customException"></param>
    /// <returns></returns>
    private async Task SetMessageResponse(HttpContext context, int httpCode, int codeResponse, string messageResponse)
    {
        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.StatusCode = httpCode;
        var response = new GenericResponse<string>
        {
            Code = codeResponse,
            ResponseType = nameof(ResponseType.Error),
            Message = messageResponse,
        }.ToString();
        Logger.LogError("Response Error: {@Path} {@Method} : {@Model}", context.Request.Path.Value, context.Request.Method, response);
        await context.Response.WriteAsync(response).ConfigureAwait(false);
    }

}
