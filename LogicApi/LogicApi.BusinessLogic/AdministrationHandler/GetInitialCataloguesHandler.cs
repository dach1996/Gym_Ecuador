using Common.WebCommon.Models.Enum;
using LogicApi.Model.Request.Administration;
using LogicApi.Model.Response.Administration;

namespace LogicApi.BusinessLogic.AdministrationHandler;
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
        => await ExecuteHandlerAsync(OperationApiName.GetInitialCatalogues, request, async ()
                => await AdministratorCache.TryGetOrSetAsync(CacheCodes.GET_INITIAL_CATALOGUES, async () =>
                 {
                     var listCatalogCodes = new Dictionary<string, Dictionary<string, string>>();

                     var countries = (await UnitOfWork.CountryRepository.GetGenericAsync(
                        select => new { select.Code, select.Name }
                     ).ConfigureAwait(false)).ToDictionary(select => select.Code, select => select.Name);
                     listCatalogCodes.Add($"{CatalogsTypeItemsCodes.Nationality}", countries);
                     var typeIdentifications = (await UnitOfWork.TypeIdentificationRepository.GetGenericAsync(
                        select => new { select.Code, select.Name }
                     ).ConfigureAwait(false)).ToDictionary(select => select.Code, select => select.Name);
                     listCatalogCodes.Add($"{CatalogsTypeItemsCodes.DocumentType}", typeIdentifications);
                     var parentCodes = new[] { CatalogCodes.Gender }
                        .Select(select => select.GetEnumMember());
                     var genders = (await UnitOfWork.CatalogRepository.GetGenericAsync(
                        select => new { select.Code, select.CatalogLanguages.FirstOrDefault().Name },
                        where => parentCodes.Contains(where.CatalogueFather.Code)
                     ).ConfigureAwait(false)).ToDictionary(select => select.Code, select => select.Name);
                     listCatalogCodes.Add($"{CatalogsTypeItemsCodes.Gender}", genders);
                     return new GetInitialCataloguesResponse(listCatalogCodes);
                 }).ConfigureAwait(false)
            ).ConfigureAwait(false);
}
