using LogicApi.Model.Common;
using LogicApi.Model.Enum;
using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para obtener el seguimiento de proceso más reciente del usuario
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetCurrentProcessTrackingHandler(
    ILogger<GetCurrentProcessTrackingHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<GetCurrentProcessTrackingRequest, GetCurrentProcessTrackingResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención del seguimiento de proceso más reciente del usuario autenticado
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetCurrentProcessTrackingResponse> Handle(GetCurrentProcessTrackingRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetCurrentProcessTracking, request, async () =>
            {
                var results = await UnitOfWork.ProcessTrackingRepository
                    .GetGenericAsync(
                        select => new
                        {
                            select.Weight,
                            select.BodyFatPercentage,
                            select.Height,
                        },
                        tracking => tracking.UserId == UserId,
                        orderBy => orderBy.Id,
                        OrderByType.Desc,
                        top: 2
                    ).ConfigureAwait(false);

                var currentProcessTracking = results.FirstOrDefault();
                var currentProcessTrackingDetail = new CurrentProcessTrackingDetail();
                if (currentProcessTracking is not null)
                {
                    var weight = new StatisticComparisonModel
                    {
                        Code = "WEIGHT",
                        Label = "Peso",
                        Value = currentProcessTracking.Weight.Round(2),
                        DifferenceValueType = DifferenceValueType.Positive,
                    };

                    var bodyFatPercentage = new StatisticComparisonModel
                    {
                        Code = "BODY_FAT_PERCENTAGE",
                        Label = "Porcentaje de Grasa Corporal",
                        Value = currentProcessTracking.BodyFatPercentage.Round(2),
                        DifferenceValueType = DifferenceValueType.Negative,
                    };

                    var bmi = new StatisticComparisonModel
                    {
                        Code = "BMI",
                        Label = "IMC",
                        Value = CalculateBmi(currentProcessTracking.Weight, currentProcessTracking.Height),
                        DifferenceValueType = DifferenceValueType.Positive,
                    };
                    currentProcessTrackingDetail.Statistics.AddRange([weight, bodyFatPercentage, bmi]);

                    var secondProcessTracking = results.Count > 1 ? results.Skip(1).FirstOrDefault() : null;
                    if (secondProcessTracking is not null)
                    {
                        weight.PreviousValue = secondProcessTracking.Weight.Round(2);
                        bodyFatPercentage.PreviousValue = secondProcessTracking.BodyFatPercentage.Round(2);
                        bmi.PreviousValue = CalculateBmi(secondProcessTracking.Weight, secondProcessTracking.Height);
                    }
                }

                return new GetCurrentProcessTrackingResponse(currentProcessTrackingDetail)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}
