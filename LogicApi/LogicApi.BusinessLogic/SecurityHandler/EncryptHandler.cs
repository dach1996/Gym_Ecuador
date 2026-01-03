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
        var dictionaryEncrypted = await EncryptRsaAsync(request.ListTexts, request.ApplyEncode).ConfigureAwait(false);
        return await Task.FromResult(new EncryptResponse(dictionaryEncrypted)).ConfigureAwait(false);
    });


}
