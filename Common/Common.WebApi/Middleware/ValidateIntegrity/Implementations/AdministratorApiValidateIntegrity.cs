using Common.Messages;
using Common.PluginFactory.Interface;
using Common.Security.Interface;
using Common.Security.Model.Enum;
using Common.Utils.CustomExceptions;
using Common.Utils.Extensions;
using Common.WebApi.Models;
using Common.WebApi.Models.AppSettingsModel;
using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.WebApi.Middleware.ValidateIntegrity.Implementations;

/// <summary>
/// Constructor
/// </summary>
/// <param name="appSettings"></param>
public class AdministratorApiValidateIntegrity(ILogger<AdministratorApiValidateIntegrity> logger, IPluginFactory pluginFactory, AppSettingsAdministrator appSettings)
    : ValidateIntegrityBase(logger, pluginFactory, appSettings)
{
    public override async Task ValidateIntegrityAsync(HttpContext httpContext)
    {
        var contextRequest = httpContext.Items[nameof(CommonContextRequest)] as AdminContextRequest;
        var integrityMode = GetIntegrityConfiguration(contextRequest.Headers?.Channel.ToString());
        if (integrityMode.Enable && (!integrityMode.PathsExclude?.Any(t => t.Equals(httpContext.Request.Path.ToString(), StringComparison.CurrentCultureIgnoreCase)) ?? true))
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
            var rsaImplementation = PluginFactory.GetPlugin<IRsaSecurity>($"{RsaSecurityImplementation.ServerGeneral}");
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
    }

    /// <summary>
    /// Obtiene la integridad
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    protected override IntegrityValidation GetIntegrityConfiguration(string identifier) => appSettings.IntegrityValidationConfig.Find(t => t.Identifier == identifier)
        ?? throw new CustomException((int)MessagesCodesError.UserNotAuthorized, $"No se encuentra ningún identificador de integridad con nombre '{identifier}'");
}