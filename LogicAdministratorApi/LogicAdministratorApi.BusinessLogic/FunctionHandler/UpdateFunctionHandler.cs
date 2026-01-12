using LogicAdministratorApi.Model.Request.Function;
using LogicAdministratorApi.Model.Response.Function;
using PersistenceDb.Models.Authentication;
using Common.Utils.CustomExceptions;
using Common.Messages;

namespace LogicAdministratorApi.BusinessLogic.FunctionHandler;

/// <summary>
/// Handler para actualizar una función
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateFunctionHandler(
    ILogger<UpdateFunctionHandler> logger,
    IPluginFactory pluginFactory) : FunctionBase<UpdateFunctionRequest, UpdateFunctionResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de una función
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateFunctionResponse> Handle(UpdateFunctionRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.UpdateFunction, request, async () =>
        {
            // Obtener la función por ID (sin include porque necesitamos actualizarla)
            var function = await UnitOfWork.FunctionRepository.GetByFirstOrDefaultAsync(
                where => where.Id == request.FunctionId
            ).ConfigureAwait(false)
            ?? throw new CustomException((int)MessagesCodesError.SystemError, "Función no encontrada");

            // Validar que el código de la función no exista en otra función
            if (await UnitOfWork.FunctionRepository
                .ExistAnyAsync(where => where.Code.ToLower() == request.Code.ToLower() && where.Id != request.FunctionId)
                .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe una función con este código");

            // Validar que el módulo existe
            var moduleExists = await UnitOfWork.ModuleRepository
                .ExistAnyAsync(where => where.Id == request.ModuleId)
                .ConfigureAwait(false);

            if (!moduleExists)
                throw new CustomException((int)MessagesCodesError.SystemError, "El módulo especificado no existe");

            // Actualizar propiedades de la función
            function.Code = request.Code;
            function.Name = request.Name;
            function.Description = request.Description;
            function.ModuleId = request.ModuleId;
            function.IsActive = request.IsActive;
            function.Route = request.Route;
            function.Icon = request.Icon;
            function.Order = request.Order;
            function.IsVisible = request.IsVisible;

            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await UnitOfWork.FunctionRepository.UpdateAsync(function).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);

            return new UpdateFunctionResponse
            {
                FunctionId = function.Id,
                Name = function.Name,
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }).ConfigureAwait(false);
}
