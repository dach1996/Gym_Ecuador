using LogicAdministratorApi.Model.Request.Function;
using LogicAdministratorApi.Model.Response.Function;

namespace LogicAdministratorApi.BusinessLogic.FunctionHandler;

/// <summary>
/// Handler para obtener todas las funciones
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetFunctionsHandler(
    ILogger<GetFunctionsHandler> logger,
    IPluginFactory pluginFactory) : FunctionBase<GetFunctionsRequest, GetFunctionsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de todas las funciones
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetFunctionsResponse> Handle(GetFunctionsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetFunctions, request, async () =>
        {
            var functions = await UnitOfWork.FunctionRepository.GetGenericAsync(
                select => new FunctionItem
                {
                    Id = select.Id,
                    Code = select.Code,
                    Name = select.Name,
                    Description = select.Description,
                    ModuleName = select.Module.Name,
                    IsActive = select.IsActive,
                    Route = select.Route,
                    Icon = select.Icon,
                    Order = select.Order,
                    IsVisible = select.IsVisible
                }
            ).ConfigureAwait(false);

            return new GetFunctionsResponse
            {
                Functions = functions.ToList(),
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = false
            };
        }).ConfigureAwait(false);
}
