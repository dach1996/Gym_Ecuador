using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;
using LogicCommon.Model.Response.File;

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
                CurrentProcessTrackingDetail currentProcessTrackingDetail = null;
                if (currentProcessTracking is not null)
                {
                    currentProcessTrackingDetail = new CurrentProcessTrackingDetail
                    {
                        Weight = currentProcessTracking.Weight,
                        BodyFatPercentage = currentProcessTracking.BodyFatPercentage ?? 0,
                        Bmi = CalculateBmi(currentProcessTracking.Weight, currentProcessTracking.Height),
                    };
                    var secondProcessTracking = results.Count > 1 ? results.Skip(1).FirstOrDefault() : null;
                    if (secondProcessTracking is not null)
                    {
                        var bmi = CalculateBmi(secondProcessTracking.Weight, secondProcessTracking.Height);
                        currentProcessTrackingDetail.PreviousWeight = secondProcessTracking.Weight;
                        currentProcessTrackingDetail.PreviousBodyFatPercentage = secondProcessTracking.BodyFatPercentage;
                        currentProcessTrackingDetail.PreviousBmi = bmi;
                        currentProcessTrackingDetail.WeightPercentageDifference = currentProcessTrackingDetail.Weight.CalculatePercentageDifference(secondProcessTracking.Weight);
                        if (currentProcessTracking.BodyFatPercentage.HasValue && secondProcessTracking.BodyFatPercentage.HasValue)
                            currentProcessTrackingDetail.BodyFatPercentageDifference = currentProcessTrackingDetail.BodyFatPercentage.CalculatePercentageDifference(secondProcessTracking.BodyFatPercentage);
                        currentProcessTrackingDetail.BmiDifference = currentProcessTrackingDetail.Bmi.CalculatePercentageDifference(bmi);
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
