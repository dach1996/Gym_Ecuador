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
        var dictionaryDecrypted = await DecryptRsaAsync(request.ListTextsEncrypt, request.HasEncode).ConfigureAwait(false);
        return await Task.FromResult(new DecryptResponse(dictionaryDecrypted)).ConfigureAwait(false);
    });

   
}
