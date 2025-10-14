using LogicApi.Model.Request.Administration;
using LogicApi.Model.Response.Administration;
using LogicApi.Model.Response.Common;
using Common.Utils;

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
                     var codeCatalogsExist = Util.GetListEnumMember<EnumLogicApi.CatalogsTypeItemsCodes>();
                     //Si la lista está vacía obtiene todos los catálogos
                     if (request.ListCatalogsTypeItemsCodes.IsNullOrEmpty())
                         request.ListCatalogsTypeItemsCodes = codeCatalogsExist;
                     var listNotMatch = request.ListCatalogsTypeItemsCodes.ContainsIn(codeCatalogsExist);
                     if (!listNotMatch.IsNullOrEmpty())
                         throw new CustomException((int)MessagesCodesError.CatalogNotFound, $"Los cógidos de catálogos: '{listNotMatch.Join()}' no existen.");
                     //Crea los Objetos de Respuesta
                     var listCatalogResponse = Enumerable.Empty<CatalogCodes>().ToList();
                     var tasks = Enumerable.Empty<Task>().ToList();
                     foreach (var catalog in request.ListCatalogsTypeItemsCodes)
                         tasks.Add(GetCatalogCodesResponseByFile(catalog));
                     if (tasks.Count != 0)
                     {
                         await Task.WhenAll(tasks).ConfigureAwait(false);
                         foreach (var task in tasks)
                             listCatalogResponse.Add(((Task<CatalogCodes>)task).Result);
                     }
                     return new GetInitialCataloguesResponse(
                        listCatalogResponse,
                        Mapper.Map<Dictionary<string, Model.Response.Administration.ControlValidationItem>>(AppSettingsApi.InputControlValidations));
                 }).ConfigureAwait(false)
            ).ConfigureAwait(false);
}
