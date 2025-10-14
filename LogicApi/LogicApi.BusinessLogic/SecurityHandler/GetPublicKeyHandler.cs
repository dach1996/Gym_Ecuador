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
public class GetPublicKeyHandler(
    ILogger<GetPublicKeyHandler> logger,
    IPluginFactory pluginFactory) : SecurityBase<GetPublicKeyRequest, GetPublicKeyResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetPublicKeyResponse> Handle(GetPublicKeyRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerCacheAsync(OperationApiName.GetPublicKey,
     $"{nameof(GetPublicKeyHandler)}-{request.RsaImplementation}",
     request, async () =>
        {
            var rsaSecurityImplementation = request.RsaImplementation.ToEnum<RsaSecurityImplementation>()
            ?? throw new CustomException((int)MessagesCodesError.EnumDontMapped, $"La cadena de texto '{request.RsaImplementation}' no puede ser mapeado a un tipo '{nameof(RsaSecurityImplementation)}'");
            var rsaImplementation = PluginFactory.GetPlugin<IRsaSecurity>(rsaSecurityImplementation.ToString().ToUpper());
            return await Task.FromResult(new GetPublicKeyResponse(rsaImplementation.GetPublicKey())).ConfigureAwait(false);
        });
}
