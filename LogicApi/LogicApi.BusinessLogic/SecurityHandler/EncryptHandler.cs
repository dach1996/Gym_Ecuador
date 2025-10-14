using Common.Security.Interface;
using Common.Security.Model.Enum;
using LogicApi.Model.Request.Security;
using LogicApi.Model.Response.Security;
namespace LogicApi.BusinessLogic.SecurityHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class EncryptHandler(
    ILogger<EncryptHandler> logger,
    IPluginFactory pluginFactory) : SecurityBase<EncryptRequest, EncryptResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<EncryptResponse> Handle(EncryptRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.Encrypt, request, async () =>
        {
            var rsaImplementation = PluginFactory.GetPlugin<IRsaSecurity>(RsaSecurityImplementation.ServerGeneral.ToString().ToUpper());
            var dictionaryResponse = new Dictionary<string, string>();
            foreach (var text in request.ListTexts.Distinct())
            {
                var stringResponse = rsaImplementation.Encrypt(text);
                if (request.ApplyEncode)
                    stringResponse = stringResponse.Encode();
                dictionaryResponse.Add(text, stringResponse);
            }
            return await Task.FromResult(new EncryptResponse(dictionaryResponse)).ConfigureAwait(false);
        });
}
