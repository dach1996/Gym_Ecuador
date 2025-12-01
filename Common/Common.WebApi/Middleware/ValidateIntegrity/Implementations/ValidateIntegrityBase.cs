using System.Text;
using Common.PluginFactory.Interface;
using Common.Utils.CustomExceptions;
using Common.WebApi.Middleware.ValidateIntegrity.Interface;
using Common.WebApi.Models;
using Common.WebCommon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.WebApi.Middleware.ValidateIntegrity.Implementations;
/// <summary>
/// Clase base para Middlerae
/// </summary>
/// <remarks>
/// Constructor 
/// </remarks>
/// <param name="appSettings"></param>
public abstract class ValidateIntegrityBase(ILogger<ValidateIntegrityBase> logger, IPluginFactory pluginFactory, AppSettingsCommon appSettings)
    : MiddlewareImplementationBase(logger, pluginFactory, appSettings), IValidateIntegrity
{

    /// <summary>
    /// Ejecuta la Tarea
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public abstract Task ValidateIntegrityAsync(HttpContext httpContext);

    /// <summary>
    /// Obtener cuerpo del request
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    protected static async Task<string> GetRawBodyRequest(HttpContext httpContext)
    {
        //Copia el Request para tomarlo como texto
        var bodyAsText = string.Empty;
        if (httpContext.Request.Body is null)
            return bodyAsText;
        httpContext.Request.EnableBuffering();
        httpContext.Request.Body.Position = 0;
        using var reader = new StreamReader(
            httpContext.Request.Body,
            encoding: Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false,
            leaveOpen: true);
        bodyAsText = await reader.ReadToEndAsync();
        httpContext.Request.Body.Position = 0;
        return bodyAsText;
    }


    /// <summary>
    /// Obtener query string
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    protected static string GetQueryParameters(HttpContext httpContext)
    {
        var queryString = string.Empty;
        if (httpContext.Request.QueryString.HasValue)
            queryString = httpContext.Request.QueryString.ToString().Replace("?", "");
        return queryString;
    }

    /// <summary>
    /// Obtiene la configuración de Integridad
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    protected abstract IntegrityValidation GetIntegrityConfiguration(string identifier);

    /// <summary>
    /// Obtiene el Body como un string 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected static async Task<string> GetRequestBodyAsync(HttpRequest request)
    {
        using StreamReader reader = new(request.Body, Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }

    /// <summary>
    /// Obtiene el Body como un string 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected static Dictionary<string, string> GetHeaders(HttpRequest request)
        => request.Headers.ToDictionary(h => h.Key, h => $"{h.Value}");
}