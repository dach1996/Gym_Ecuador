using LogicAdministratorApi.Model.Request.Administration;
using LogicCommon.Model.Request.Administration;
using LogicCommon.Model.Response.Administration;

namespace LogicAdministratorApi.BusinessLogic.AdministrationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetInitialCataloguesHandler(
    ILogger<GetInitialCataloguesHandler> logger,
    IPluginFactory pluginFactory) : AdministrationBase<GetInitialCataloguesRequest, GetInitialCataloguesResponse>(
        logger,
        pluginFactory)
{

   /// <summary>
   /// Handler
   /// </summary>
   /// <param name="request"></param>
   /// <param name="cancellationToken"></param>
   /// <returns></returns>
   public override async Task<GetInitialCataloguesResponse> Handle(GetInitialCataloguesRequest request, CancellationToken cancellationToken)
      => await Mediator.Send(new GetInitialCataloguesCommonRequest(request.ContextRequest, request.ListCatalogsTypeItemsCodes), cancellationToken).ConfigureAwait(false);
}
