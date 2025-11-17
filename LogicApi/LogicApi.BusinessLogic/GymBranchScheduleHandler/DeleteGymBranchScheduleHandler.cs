using LogicApi.Model.Request.GymBranchSchedule;
using LogicApi.Model.Response.GymBranchSchedule;

namespace LogicApi.BusinessLogic.GymBranchScheduleHandler;

/// <summary>
/// Handler para eliminar horario de sucursal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class DeleteGymBranchScheduleHandler(
    ILogger<DeleteGymBranchScheduleHandler> logger,
    IPluginFactory pluginFactory) : GymBranchScheduleBase<DeleteGymBranchScheduleRequest, DeleteGymBranchScheduleResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la eliminación de un horario de sucursal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<DeleteGymBranchScheduleResponse> Handle(DeleteGymBranchScheduleRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.DeleteGymBranchSchedule, request, async () =>
            {
                // Buscar el horario
                var schedule = await UnitOfWork.GymBranchScheduleRepository
                    .GetByFirstOrDefaultAsync(where => where.Id == request.ScheduleId)
                    .ConfigureAwait(false);

                if (schedule == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Horario no encontrado");

                // Eliminar el horario
                await UnitOfWork.GymBranchScheduleRepository.DeleteAsync(schedule).ConfigureAwait(false);

                return new DeleteGymBranchScheduleResponse(true)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}

