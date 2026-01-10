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
                    .GetByFirstOrDefaultAsync(
                        where => where.Guid == request.ExerciseGuid,
                        include => include.ExerciseTags,
                        include => include.ExerciseTags.Select(et => et.Catalog),
                        include => include.ExerciseTags.Select(et => et.Catalog.CatalogLanguages),
                        include => include.Image,
                        include => include.Image != null ? include.Image.FileBasePath : null)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Ejercicio no encontrado");

                var exerciseDetail = new ExerciseDetail
                {
                    Guid = exercise.Guid,
                    Name = exercise.Name,
                    Description = exercise.Description,
                    Instructions = exercise.Instructions,
                    ImageUrl = exercise.Image != null && exercise.Image.State
                        ? new FileUrlResponse(exercise.Image.Guid, exercise.Image.FileBasePath.BaseUrl, exercise.Image.Path)
                        : null,
                    Tags = exercise.ExerciseTags.Select(et => new ExerciseTagItem
                    {
                        CatalogId = et.CatalogId,
                        Code = et.Catalog.Code,
                        Name = et.Catalog.CatalogLanguages.FirstOrDefault()?.Name ?? et.Catalog.Code
                    }).ToList()
                };

                return new GetExerciseByGuidResponse
                {
                    Exercise = exerciseDetail,
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}
