using LogicApi.Model.Request.Profile;
using LogicApi.Model.Response.Profile;

namespace LogicApi.BusinessLogic.ProfileHandler;

/// <summary>
/// Handler para calcular macros de perfil
/// </summary>
public class CalculateProfileHandler(
    ILogger<CalculateProfileHandler> logger,
    IPluginFactory pluginFactory) : ProfileBase<CalculateProfileRequest, CalculateProfileResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Calcula proteína, carbohidratos y grasas según datos del perfil
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task<CalculateProfileResponse> Handle(CalculateProfileRequest request, CancellationToken cancellationToken)
        => ExecuteHandlerAsync(OperationApiName.CalculateProfile, request, async () =>
            {
                ValidateProfileFields(
                    request.Name,
                    request.Type,
                    request.Height,
                    request.EstimatedWeeks,
                    request.PhysicalActivityCatalogId,
                    request.ProgressRateCatalogId);

                var macros = await CalculateMacrosAsync(
                    request.Type,
                    request.Weight,
                    request.Height,
                    request.PhysicalActivityCatalogId,
                    request.ProgressRateCatalogId).ConfigureAwait(false);

                return new CalculateProfileResponse
                {
                    Protein = macros.Protein,
                    Carbohydrates = macros.Carbohydrates,
                    Fats = macros.Fats,
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        );
}
