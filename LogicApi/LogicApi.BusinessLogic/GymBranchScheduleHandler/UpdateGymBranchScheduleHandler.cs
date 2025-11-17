using LogicApi.Model.Request.GymBranchSchedule;
using LogicApi.Model.Response.GymBranchSchedule;

namespace LogicApi.BusinessLogic.GymBranchScheduleHandler;

/// <summary>
/// Handler para actualizar horario de sucursal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateGymBranchScheduleHandler(
    ILogger<UpdateGymBranchScheduleHandler> logger,
    IPluginFactory pluginFactory) : GymBranchScheduleBase<UpdateGymBranchScheduleRequest, UpdateGymBranchScheduleResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un horario de sucursal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateGymBranchScheduleResponse> Handle(UpdateGymBranchScheduleRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.UpdateGymBranchSchedule, request, async () =>
            {
                // Buscar el horario
                var schedule = await UnitOfWork.GymBranchScheduleRepository
                    .GetByFirstOrDefaultAsync(where => where.Id == request.ScheduleId)
                    .ConfigureAwait(false);

                if (schedule == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Horario no encontrado");

                // Actualizar campos si se proporcionan
                if (request.OpeningTime.HasValue)
                    schedule.OpeningTime = request.OpeningTime.Value;

                if (request.ClosingTime.HasValue)
                    schedule.ClosingTime = request.ClosingTime.Value;

                // Validar que la hora de apertura sea menor que la hora de cierre
                if (schedule.OpeningTime >= schedule.ClosingTime)
                    throw new CustomException((int)MessagesCodesError.SystemError, "La hora de apertura debe ser menor que la hora de cierre");

                if (request.IsVisible.HasValue)
                    schedule.IsVisible = request.IsVisible.Value;

                return new UpdateGymBranchScheduleResponse(schedule.Id, true)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}

