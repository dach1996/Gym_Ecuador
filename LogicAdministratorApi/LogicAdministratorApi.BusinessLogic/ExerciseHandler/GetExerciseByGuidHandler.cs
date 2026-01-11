using LogicAdministratorApi.Model.Request.Exercise;
using LogicAdministratorApi.Model.Response.Exercise;
using LogicCommon.Model.Response.File;
using Common.WebApi.Models.Enum;

namespace LogicAdministratorApi.BusinessLogic.ExerciseHandler;

/// <summary>
/// Handler para obtener detalle de ejercicio por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetExerciseByGuidHandler(
    ILogger<GetExerciseByGuidHandler> logger,
    IPluginFactory pluginFactory) : ExerciseBase<GetExerciseByGuidRequest, GetExerciseByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de detalle de un ejercicio por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetExerciseByGuidResponse> Handle(GetExerciseByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetExerciseByGuid, request, async () =>
            {
                // Buscar el ejercicio por GUID con todas sus relaciones
                var exercise = await UnitOfWork.ExerciseRepository
                    .GetFirstOrDefaultGenericAsync(select =>
                        new ExerciseDetail
                        {
                            Guid = select.Guid,
                            Name = select.Name,
                            Description = select.Description,
                            Instructions = select.Instructions,
                            ImageUrl = select.Image != null && select.Image.State
                        ? new FileUrlResponse(select.Image.Guid, select.Image.FileBasePath.BaseUrl, select.Image.Path)
                        : null,
                            Tags = select.ExerciseTags.Select(et => new ExerciseTagItem
                            {
                                Code = et.Catalog.Code,
                                Name = et.Catalog.CatalogLanguages.FirstOrDefault().Name
                            }).ToList()
                        },
                        where => where.Guid == request.ExerciseGuid
                      )
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Ejercicio no encontrado");

                return new GetExerciseByGuidResponse
                {
                    Exercise = exercise,
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}
