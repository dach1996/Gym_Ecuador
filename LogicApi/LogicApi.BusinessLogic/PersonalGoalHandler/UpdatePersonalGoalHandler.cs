using LogicApi.Model.Request.PersonalGoal;
using LogicApi.Model.Response.PersonalGoal;

namespace LogicApi.BusinessLogic.PersonalGoalHandler;

/// <summary>
/// Handler para actualizar objetivo personal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdatePersonalGoalHandler(
    ILogger<UpdatePersonalGoalHandler> logger,
    IPluginFactory pluginFactory) : PersonalGoalBase<UpdatePersonalGoalRequest, UpdatePersonalGoalResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un objetivo personal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdatePersonalGoalResponse> Handle(UpdatePersonalGoalRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.UpdatePersonalGoal, request, async () =>
            {
                var personalGoal = await UnitOfWork.PersonalGoalRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.PersonalGoalGuid)
                    .ConfigureAwait(false);

                if (personalGoal == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "El objetivo personal no existe");

                if (request.TargetValue <= 0)
                    throw new CustomException((int)MessagesCodesError.SystemError, "El valor objetivo debe ser mayor a cero");

                if (request.EstimatedEndDate.HasValue && personalGoal.StartDate >= request.EstimatedEndDate.Value)
                    throw new CustomException((int)MessagesCodesError.SystemError, "La fecha de inicio debe ser anterior a la fecha de fin estimada");

                personalGoal.TargetValue = request.TargetValue;
                personalGoal.EstimatedEndDate = request.EstimatedEndDate;
                personalGoal.GoalStatus = request.GoalStatus;
                personalGoal.Description = request.Description;

                return new UpdatePersonalGoalResponse(personalGoal.Guid, personalGoal.GoalStatus)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}
