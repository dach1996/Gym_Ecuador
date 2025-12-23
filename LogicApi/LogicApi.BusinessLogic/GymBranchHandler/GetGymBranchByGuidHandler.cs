using LogicApi.Model.Request.GymBranch;
using LogicApi.Model.Response.GymBranch;

namespace LogicApi.BusinessLogic.GymBranchHandler;

/// <summary>
/// Handler para obtener detalle de sucursal de gimnasio por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymBranchByGuidHandler(
    ILogger<GetGymBranchByGuidHandler> logger,
    IPluginFactory pluginFactory) : GymBranchBase<GetGymBranchByGuidRequest, GetGymBranchByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de detalle de una sucursal por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymBranchByGuidResponse> Handle(GetGymBranchByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetGymBranchByGuid, request, async () =>
            {
                // Buscar la sucursal por GUID
                var branch = (await UnitOfWork.GymBranchRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new GymBranchDetail
                        {
                            Guid = select.Guid,
                            Name = select.Name,
                            Description = select.Description,
                            Address = select.Address,
                            Location = new(select.Latitude, select.Longitude),

                        },
                        where => where.Guid == request.GymBranchGuid)
                    .ConfigureAwait(false))
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Sucursal de gimnasio no encontrada");
                branch.CalificationPercentage = 90;
                branch.ImageUrls = ["https://img.freepik.com/premium-photo/modern-gym-interior-with-sport-fitness-equipment-fitness-center-inteior-inteior-crossfit-workout-gym_875746-17605.jpg", "https://img.freepik.com/premium-photo/dark-empty-gym-interior-with-training-equipment-free-space_1028938-42151.jpg"];
                branch.Services = [
                                new("Entrenamiento personal", "PERSONAL_TRAINING"),
                                new("Clases grupales", "GROUP_CLASSES"),
                                new("Duchas y vestidores", "SHOWERS_AND_LOCKERS"),
                                new("Área de nutrición", "NUTRITION_AREA"),
                                new("Wifi gratuito", "FREE_WIFI"),
                                new("Sauna", "SAUNA"),
                            ];
                branch.Schedules = new Dictionary<string, string> {
                                { "Lunes a Viernes", "08:00 - 20:00" },
                                { "Sábado", "09:00 - 13:00" },
                                { "Domingo", "Cerrado" },
                            };
                branch.Description = "El mejor gimnasio de la ciudad, Excelente atención y equipos de última generación.";
                branch.Reviews = [
                                new (){
                                    PersonName = "Juan Pérez",
                                    ImageUrl = "https://img.freepik.com/free-photo/portrait-beautiful-redhead-woman_23-2148339229.jpg?semt=ais_hybrid&w=740&q=80",
                                    RatingPercentage = 90,
                                    Comment = "El mejor gimnasio de la ciudad, Excelente atención y equipos de última generación."
                                },
                                new (){
                                    PersonName = "Maria García",
                                    ImageUrl = "https://thumbs.dreamstime.com/b/portrait-handsome-smiling-young-man-folded-arms-smiling-joyful-cheerful-men-crossed-hands-isolated-studio-shot-172869765.jpg",
                                    RatingPercentage = 80,
                                    Comment = "El gimnasio tiene un ambiente muy acogedor y el personal es muy amable."
                                },
                            ];
                return new GetGymBranchByGuidResponse(branch);
            }).ConfigureAwait(false);
}

