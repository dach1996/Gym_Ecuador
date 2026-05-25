using System.Linq.Expressions;
using LogicApi.Model.Request.PersonalGoal;
using LogicApi.Model.Response.PersonalGoal;
using PersistenceDb.Models.Core;
using PersistenceDb.Models.Enums;

namespace LogicApi.BusinessLogic.PersonalGoalHandler;

/// <summary>
/// Handler para obtener objetivos personales
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetPersonalGoalsHandler(
    ILogger<GetPersonalGoalsHandler> logger,
    IPluginFactory pluginFactory) : PersonalGoalBase<GetPersonalGoalsRequest, GetPersonalGoalsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de objetivos personales con paginación y filtros
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetPersonalGoalsResponse> Handle(GetPersonalGoalsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetPersonalGoals, request, async () =>
            {
                var filters = new List<Expression<Func<PersonalGoal, bool>>>();

                if (request.PersonId.HasValue)
                    filters.Add(goal => goal.PersonId == request.PersonId.Value);

                if (!string.IsNullOrWhiteSpace(request.GoalTypeFilter))
                    filters.Add(goal => goal.GoalType == request.GoalTypeFilter);

                if (!string.IsNullOrWhiteSpace(request.GoalStatusFilter))
                    filters.Add(goal => goal.GoalStatus == request.GoalStatusFilter);

                var paginatedResult = await UnitOfWork.PersonalGoalRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.Page,
                        select => new PersonalGoalItem
                        {
                            Guid = select.Guid,
                            PersonFullName = select.Person.FullName,
                            GoalType = select.GoalType,
                            InitialValue = select.InitialValue,
                            TargetValue = select.TargetValue,
                            StartDate = select.StartDate,
                            EstimatedEndDate = select.EstimatedEndDate,
                            GoalStatus = select.GoalStatus,
                            ProgressPercentage = select.GoalStatus == "Completado" ? 100m : 0m,
                            Description = select.Description,
                            DateTimeRegister = select.DateTimeRegister
                        },
                        where: filters,
                        orderBy: goal => goal.DateTimeRegister,
                        orderByType: OrderByType.Desc
                    ).ConfigureAwait(false);

                return new GetPersonalGoalsResponse(paginatedResult.Items, paginatedResult.TotalItems, request.Page, request.PageSize)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}
