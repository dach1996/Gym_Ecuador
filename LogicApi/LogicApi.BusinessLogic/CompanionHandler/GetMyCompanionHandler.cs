using LogicApi.Model.Common;
using LogicApi.Model.Request.Companion;
using LogicApi.Model.Response.Companion;
namespace LogicApi.BusinessLogic.CompanionHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetMyCompanionHandler(
    ILogger<GetMyCompanionHandler> logger,
    IPluginFactory pluginFactory) : CompanionBase<GetMyCompanionRequest, GetMyCompanionResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async override Task<GetMyCompanionResponse> Handle(GetMyCompanionRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetMyCompanion, request, async () =>
        {
            //Busca los compañeros de usuario en contexto
            var items = await CoreUnitOfWork.CompanionRepository.GetGenericAsync(
                select => new CompanionItem(
                    select.Person.Guid,
                    select.Person.DocumentNumber,
                    select.Person.FullName
                ),
                where => where.UserId == UserId
            ).ConfigureAwait(false);
            //Retorna la respuesta
            return new GetMyCompanionResponse(items);
        }, UnitOfWorkType.Core);
}