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
public class DecryptHandler(
    ILogger<DecryptHandler> logger,
    IPluginFactory pluginFactory) : SecurityBase<DecryptRequest, DecryptResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<DecryptResponse> Handle(DecryptRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.Decrypt, request, async () =>
        {
            var rsaImplementation = PluginFactory.GetPlugin<IRsaSecurity>(RsaSecurityImplementation.ServerGeneral.ToString().ToUpper());
            var dictionaryResponse = new Dictionary<string, string>();
            foreach (var text in request.ListTextsEncrypt)
            {
                var valueEncrypt = text;
                if (request.HasEncode)
                    valueEncrypt = text.Decode();
                var stringResponse = rsaImplementation.Decrypt(valueEncrypt);
                dictionaryResponse.Add(text, stringResponse);
            }
            return await Task.FromResult(new DecryptResponse(dictionaryResponse)).ConfigureAwait(false);
        });
}
