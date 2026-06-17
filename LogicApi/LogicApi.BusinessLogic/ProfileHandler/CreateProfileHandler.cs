using LogicApi.Model.Request.Profile;
using LogicApi.Model.Response.Profile;
using ProfileEntity = PersistenceDb.Models.Core.Profile;

namespace LogicApi.BusinessLogic.ProfileHandler;

/// <summary>
/// Handler para crear perfil
/// </summary>
public class CreateProfileHandler(
    ILogger<CreateProfileHandler> logger,
    IPluginFactory pluginFactory) : ProfileBase<CreateProfileRequest, CreateProfileResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Crea un perfil completo y desactiva otros perfiles activos de la persona
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task<CreateProfileResponse> Handle(CreateProfileRequest request, CancellationToken cancellationToken)
        => ExecuteHandlerAsync(OperationApiName.CreateProfile, request, async () =>
            {
                ValidateProfileFields(
                    request.Name,
                    request.Type,
                    request.Height,
                    request.EstimatedWeeks,
                    request.PhysicalActivityCatalogId,
                    request.ProgressRateCatalogId);

                await ValidateCatalogReferencesAsync(
                    request.PhysicalActivityCatalogId,
                    request.ProgressRateCatalogId).ConfigureAwait(false);

                ValidateMacros(request.Protein, request.Carbohydrates, request.Fats);

                var newProfile = new ProfileEntity
                {
                    Guid = Guid.NewGuid(),
                    PersonId = PersonId,
                    Name = request.Name.Trim(),
                    Type = request.Type,
                    PhysicalActivityCatalogId = request.PhysicalActivityCatalogId,
                    Height = request.Height,
                    ProgressRateCatalogId = request.ProgressRateCatalogId,
                    EstimatedWeeks = request.EstimatedWeeks,
                    Protein = request.Protein,
                    Carbohydrates = request.Carbohydrates,
                    Fats = request.Fats,
                    IsActive = true,
                    DateTimeRegister = Now,
                    UserIdRegister = UserId
                };

                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await DeactivateActiveProfilesAsync().ConfigureAwait(false);
                await UnitOfWork.ProfileRepository.AddAsync(newProfile).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new CreateProfileResponse(
                    newProfile.Guid,
                    newProfile.Name,
                    newProfile.Protein,
                    newProfile.Carbohydrates,
                    newProfile.Fats)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        );
}
