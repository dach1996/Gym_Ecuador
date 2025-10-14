using LogicApi.Model.Request.CommonConfiguration;
using LogicApi.Model.Response.CommonConfiguration;
using LogicApi.Model.Response.CommonConfiguration.Common;

namespace LogicApi.BusinessLogic.CommonConfigurationHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetItemsCatalogByCodeCatalogHandler(
    ILogger<GetItemsCatalogByCodeCatalogHandler> logger,
    IPluginFactory pluginFactory) : CommonConfigurationBase<GetItemsCatalogByCodeCatalogRequest, GetItemsCatalogByCodeCatalogResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetItemsCatalogByCodeCatalogResponse> Handle(GetItemsCatalogByCodeCatalogRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.GetItemsCatalogByCodeCatalog, request, async () =>
        {
            var itemsCatalog = await AdministratorCache.TryGetOrSetAsync($"{CacheCodes.CATALOGUE}-{request.CatalogueCode}",
                async () =>
                {
                    //Obtiene los items de catálogos
                    var itemCatalogs = await AdministrationUnitOfWork.CatalogRepository.
                        GetGenericAsync(
                            select => new
                            {
                                select.Code,
                                select.Value,
                                select.Status,
                            },
                            where => where.CatalogueFather.Code == request.CatalogueCode
                        ).ConfigureAwait(false);
                    // Verifica si tiene items de catálogo
                    if (itemCatalogs.IsNullOrEmpty())
                        throw new CustomException((int)MessagesCodesError.CatalogNotFound, $"El catálogo: '{request.CatalogueCode}' no posee Items.");
                    return itemCatalogs;
                }).ConfigureAwait(false);
            //Retorna el valor
            return new GetItemsCatalogByCodeCatalogResponse(itemsCatalog.Select(select => new ItemCatalogResponse
            {
                Code = select.Code,
                Status = select.Status,
                Value = select.Value
            }));
        }, UnitOfWorkType.Administration);
}
