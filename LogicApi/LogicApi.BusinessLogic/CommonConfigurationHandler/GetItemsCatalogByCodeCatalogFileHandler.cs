using LogicApi.Model.Request.CommonConfiguration;
using LogicApi.Model.Response.CommonConfiguration;
using LogicApi.Model.Response.CommonConfiguration.Common;
namespace LogicApi.BusinessLogic.CommonConfigurationHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetItemsCatalogByCodeCatalogFileHandler(
    ILogger<GetItemsCatalogByCodeCatalogFileHandler> logger,
    IPluginFactory pluginFactory) : CommonConfigurationBase<GetItemsCatalogByCodeCatalogFileRequest, GetItemsCatalogByCodeCatalogFileResponse>(
        logger,
        pluginFactory)
{


    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetItemsCatalogByCodeCatalogFileResponse> Handle(GetItemsCatalogByCodeCatalogFileRequest request, CancellationToken cancellationToken)
   => await ExecuteHandlerAsync(OperationApiName.GetItemsCatalogByCodeCatalogFile, request, async () =>
        {
            var response = await AdministratorCache.TryGetOrSetAsync($"{CacheCodes.CATALOGUE_FILE}-{request.CatalogueCode}",
             () => CataloguesType.GetCatalogByCodeAsync(request.CatalogueCode)).ConfigureAwait(false);
            var items = response?.ListItemCatalog.Select(where => new ItemCatalogFileResponse
            {
                Code = where.Code,
                Value = where.LanguageValue?.FirstOrDefault(t => t.Key == request.Language).Value,
                Enum = where.Enum
            }).ToList();
            return new GetItemsCatalogByCodeCatalogFileResponse(items);
        });
}
