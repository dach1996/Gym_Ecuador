using LogicAdministratorApi.Model.Request.Functionality;
using LogicAdministratorApi.Model.Response.Functionality;

namespace LogicAdministratorApi.BusinessLogic.FunctionalityHandler;

/// <summary>
/// Handler para obtener todas las funcionalidades
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetFunctionalitiesHandler(
    ILogger<GetFunctionalitiesHandler> logger,
    IPluginFactory pluginFactory) : FunctionalityBase<GetFunctionalitiesRequest, GetFunctionalitiesResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de todas las funcionalidades
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetFunctionalitiesResponse> Handle(GetFunctionalitiesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetFunctionalities, request, async () =>
        {
            var functionalities = await UnitOfWork.FunctionalityRepository.GetGenericAsync(
                select => new FunctionalityItem
                {
                    Id = select.Id,
                    Name = select.Name + " - " + select.Function.Name,
                    Code = select.Code,
                    Description = select.Description,
                    FunctionName = select.Function.Name,
                    ModuleName = select.Function.Module.Name
                }
            ).ConfigureAwait(false);

            return new GetFunctionalitiesResponse
            {
                Functionalities = functionalities.ToList(),
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = false
            };
        }).ConfigureAwait(false);
}
