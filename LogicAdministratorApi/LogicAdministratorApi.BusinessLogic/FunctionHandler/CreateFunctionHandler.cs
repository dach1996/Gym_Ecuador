using LogicAdministratorApi.Model.Request.Function;
using LogicAdministratorApi.Model.Response.Function;
using PersistenceDb.Models.Authentication;
using Common.Utils.CustomExceptions;
using Common.Messages;

namespace LogicAdministratorApi.BusinessLogic.FunctionHandler;

/// <summary>
/// Handler para crear una nueva función
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateFunctionHandler(
    ILogger<CreateFunctionHandler> logger,
    IPluginFactory pluginFactory) : FunctionBase<CreateFunctionRequest, CreateFunctionResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de una nueva función
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateFunctionResponse> Handle(CreateFunctionRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateFunction, request, async () =>
        {
            // Validar que el código de la función no exista
            if (await UnitOfWork.FunctionRepository
                .ExistAnyAsync(where => where.Code.ToLower() == request.Code.ToLower())
                .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe una función con este código");

            // Validar que el módulo existe
            var moduleExists = await UnitOfWork.ModuleRepository
                .ExistAnyAsync(where => where.Id == request.ModuleId)
                .ConfigureAwait(false);

            if (!moduleExists)
                throw new CustomException((int)MessagesCodesError.SystemError, "El módulo especificado no existe");

            // Crear la nueva función
            var newFunction = new Function
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                ModuleId = request.ModuleId,
                IsActive = request.IsActive,
                Route = request.Route,
                Icon = request.Icon,
                Order = request.Order,
                IsVisible = request.IsVisible
            };

            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await UnitOfWork.FunctionRepository.AddAsync(newFunction).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);

            return new CreateFunctionResponse
            {
                FunctionId = newFunction.Id,
                Name = newFunction.Name,
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }).ConfigureAwait(false);
}
