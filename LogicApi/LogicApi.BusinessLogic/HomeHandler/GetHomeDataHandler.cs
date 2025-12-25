using LogicApi.Model.Request.Home;
using LogicApi.Model.Response.Home;

namespace LogicApi.BusinessLogic.HomeHandler;

/// <summary>
/// Handler para obtener datos del home/dashboard
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetHomeDataHandler(
    ILogger<GetHomeDataHandler> logger,
    IPluginFactory pluginFactory) : HomeBase<GetHomeDataRequest, GetHomeDataResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de datos del home/dashboard
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetHomeDataResponse> Handle(GetHomeDataRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetHomeData, request, async () =>
            {
                return new GetHomeDataResponse
                {
                    DashboardMeasurementData = new DashboardMeasurementData
                    {
                        CurrentWeight = 0,
                        GoalWeight = 0,
                        PreviousWeight = 0,
                    },
                    MealPlan = new MealPlan
                    {
                        Name = "Plan de alimentación",
                        TotalCalories = 0,
                        MaxCarbohydrates = 0,
                        MaxProtein = 0,
                        MaxFats = 0,
                        UsedCarbohydrates = 0,
                        UsedProtein = 0,
                        UsedFats = 0,
                    }
                };
            }
        ).ConfigureAwait(false);
}

