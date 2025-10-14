using Common.Messages;
using Common.PluginFactory.Interface;
using Common.Security.Interface;
using Common.Security.Model.Enum;
using Common.Utils.CustomExceptions;
using Common.Utils.Extensions;
using Common.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace Common.WebApi.Middleware;
/// <summary>
/// Constructor
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
/// <returns></returns>
public class ValidateIntegrityMiddleware(
    RequestDelegate next,
    ILogger<ValidateIntegrityMiddleware> logger,
    IPluginFactory pluginFactory) : MiddlewareBase(next, logger, pluginFactory)
{
    private readonly AppSettingsApi _appSettings = pluginFactory.GetType<AppSettingsApi>();



    /// <summary>
    /// Invoke
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var contextRequest = httpContext.Items[nameof(ContextRequest)] as ContextRequest;
        var integrityMode = GetIntegrityConfiguration(contextRequest);
        if (integrityMode.Enable && (!integrityMode.PathsExclude?.Any(t => t.ToUpper() == httpContext.Request.Path.ToString().ToUpper()) ?? true))
        {
            //Valida que estén presentes el resto de headers
            if (string.IsNullOrEmpty(contextRequest.Headers?.Content))
                throw new CustomException((int)MessagesCodesError.UserNotAuthorized, "No esta presente el header X-Content");
            if (string.IsNullOrEmpty(contextRequest.Headers?.Time))
                throw new CustomException((int)MessagesCodesError.UserNotAuthorized, "No esta presente el header X-Time");
            if (string.IsNullOrEmpty(contextRequest.Headers?.Secret))
                throw new CustomException((int)MessagesCodesError.UserNotAuthorized, "No esta presente el header X-Secret");

            //Arma el nounce

            string bodyAsText = await GetRawBodyRequest(httpContext);
            string hashBody = string.IsNullOrEmpty(bodyAsText) ? string.Empty : bodyAsText.ToSha256();
            string queryString = GetQueryParameters(httpContext);
            var nounce = $"{httpContext.Request.Method}||{queryString}||{hashBody}||{contextRequest.Headers?.Time}";
            //Encripta
            var rsaImplementation = PluginFactory.GetPlugin<IRsaSecurity>(RsaSecurityImplementation.ServerGeneral.ToString().ToUpper());
            var secretDecode = contextRequest.Headers.Secret.Decode();
            var secretDecrypt = rsaImplementation.Decrypt(secretDecode);
            //Calcula el hash de integridad
            var hmacToken = nounce.ToSha256(secretDecrypt);
            //Comparar el hash
            if (hmacToken != contextRequest.Headers?.Content)
            {
                var body = await GetRequestBodyAsync(httpContext.Request).ConfigureAwait(false);
                Logger.LogCritical("El hash de integridad de datos del request:'{@XContent}' es distinto al calculado: '{@Hmac}'" +
                                 "'Nounce': {@Nounce} " +
                                 "'Secret': {@SecretKey} " +
                                 "'Headers': {@Headers} " +
                                 "'Body': {@Body} " +
                                 "'Context:':{@Context} ",
                                 contextRequest.Headers?.Content,
                                  hmacToken,
                                  nounce,
                                  secretDecrypt,
                                  GetHeaders(httpContext.Request),
                                  body,
                                  contextRequest);
                throw new CustomException((int)MessagesCodesError.UserNotAuthorized, "Error comparando la integridad de datos");
            }
        }
        await Next(httpContext).ConfigureAwait(false);
    }

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
    /// Obtener header
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="headerName"></param>
    /// <returns></returns>
    protected static KeyValuePair<string, StringValues> GetHeaderValue(HttpContext httpContext, string headerName) =>
        httpContext.Request.Headers.FirstOrDefault(p => p.Key.Equals(headerName, StringComparison.InvariantCultureIgnoreCase));

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
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    protected IntegrityValidation GetIntegrityConfiguration(ContextRequest contextRequest)
        => _appSettings.IntegrityValidationConfig.Find(t => t.Identifier == contextRequest.Headers?.Channel.ToString())
            ?? throw new CustomException((int)MessagesCodesError.UserNotAuthorized, $"No se encuentra ningún identificador de integridad con nombre '{contextRequest.Headers?.Channel.ToString()}'");


    /// <summary>
    /// Obtiene el Body como un string 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private static async Task<string> GetRequestBodyAsync(HttpRequest request)
    {
        using StreamReader reader = new(request.Body, Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }

    /// <summary>
    /// Obtiene el Body como un string 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private static IDictionary<string, string> GetHeaders(HttpRequest request)
        => request.Headers.ToDictionary(h => h.Key, h => $"{h.Value}");
}