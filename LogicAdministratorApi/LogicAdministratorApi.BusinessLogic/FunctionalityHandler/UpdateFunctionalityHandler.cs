using LogicAdministratorApi.Model.Request.Functionality;
using LogicAdministratorApi.Model.Response.Functionality;
using PersistenceDb.Models.Authentication;
using Common.Utils.CustomExceptions;
using Common.Messages;

namespace LogicAdministratorApi.BusinessLogic.FunctionalityHandler;

/// <summary>
/// Handler para actualizar una funcionalidad
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateFunctionalityHandler(
    ILogger<UpdateFunctionalityHandler> logger,
    IPluginFactory pluginFactory) : FunctionalityBase<UpdateFunctionalityRequest, UpdateFunctionalityResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de una funcionalidad
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateFunctionalityResponse> Handle(UpdateFunctionalityRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.UpdateFunctionality, request, async () =>
        {
            // Obtener la funcionalidad por GUID (sin include porque necesitamos actualizarla)
            var functionality = await UnitOfWork.FunctionalityRepository.GetByFirstOrDefaultAsync(
                where => where.Id == request.FunctionalityGuid
            ).ConfigureAwait(false)
            ?? throw new CustomException((int)MessagesCodesError.SystemError, "Funcionalidad no encontrada");

            // Validar que el código de la funcionalidad no exista en otra funcionalidad
            if (await UnitOfWork.FunctionalityRepository
                .ExistAnyAsync(where => where.Code.ToLower() == request.Code.ToLower() && where.Id != request.FunctionalityGuid)
                .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe una funcionalidad con este código");

            // Validar que la función existe
            var functionExists = await UnitOfWork.FunctionRepository
                .ExistAnyAsync(where => where.Id == request.FunctionId)
                .ConfigureAwait(false);

            if (!functionExists)
                throw new CustomException((int)MessagesCodesError.SystemError, "La función especificada no existe");

            // Actualizar propiedades de la funcionalidad
            functionality.Code = request.Code;
            functionality.Name = request.Name;
            functionality.Description = request.Description;
            functionality.FunctionId = request.FunctionId;
            functionality.IsActive = request.IsActive;

            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await UnitOfWork.FunctionalityRepository.UpdateAsync(functionality).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);

            return new UpdateFunctionalityResponse
            {
                FunctionalityGuid = functionality.Id,
                Name = functionality.Name,
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }).ConfigureAwait(false);
}
