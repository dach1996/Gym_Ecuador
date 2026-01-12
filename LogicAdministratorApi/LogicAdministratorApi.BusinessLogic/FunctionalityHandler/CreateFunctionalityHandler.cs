using LogicAdministratorApi.Model.Request.Functionality;
using LogicAdministratorApi.Model.Response.Functionality;
using PersistenceDb.Models.Authentication;
using Common.Utils.CustomExceptions;
using Common.Messages;

namespace LogicAdministratorApi.BusinessLogic.FunctionalityHandler;

/// <summary>
/// Handler para crear una nueva funcionalidad
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateFunctionalityHandler(
    ILogger<CreateFunctionalityHandler> logger,
    IPluginFactory pluginFactory) : FunctionalityBase<CreateFunctionalityRequest, CreateFunctionalityResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de una nueva funcionalidad
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateFunctionalityResponse> Handle(CreateFunctionalityRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateFunctionality, request, async () =>
        {
            // Validar que el código de la funcionalidad no exista
            if (await UnitOfWork.FunctionalityRepository
                .ExistAnyAsync(where => where.Code.ToLower() == request.Code.ToLower())
                .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe una funcionalidad con este código");

            // Validar que la función existe
            var functionExists = await UnitOfWork.FunctionRepository
                .ExistAnyAsync(where => where.Id == request.FunctionId)
                .ConfigureAwait(false);

            if (!functionExists)
                throw new CustomException((int)MessagesCodesError.SystemError, "La función especificada no existe");

            // Crear la nueva funcionalidad
            var newFunctionality = new Functionality
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                FunctionId = request.FunctionId,
                IsActive = request.IsActive
            };

            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await UnitOfWork.FunctionalityRepository.AddAsync(newFunctionality).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);

            return new CreateFunctionalityResponse
            {
                FunctionalityGuid = newFunctionality.Id,
                Name = newFunctionality.Name,
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }).ConfigureAwait(false);
}
