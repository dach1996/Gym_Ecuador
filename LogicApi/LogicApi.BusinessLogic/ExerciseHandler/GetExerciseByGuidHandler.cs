using LogicApi.Model.Request.Exercise;
using LogicApi.Model.Response.Exercise;
using LogicCommon.Model.Response.File;
using Common.WebApi.Models.Enum;

namespace LogicApi.BusinessLogic.ExerciseHandler;

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
        => await ExecuteHandlerAsync(OperationApiName.GetExerciseByGuid, request, async () =>
            {


                var exerciseDetail = await UnitOfWork.ExerciseRepository
                    .GetFirstOrDefaultGenericAsync(select => new ExerciseDetail
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
                            CatalogId = et.CatalogId,
                            Code = et.Catalog.Code,
                            Name = et.Catalog.CatalogLanguages.FirstOrDefault().Name
                        }).ToList()
                    }, where => where.Guid == request.ExerciseGuid)
                .ConfigureAwait(false)
                ?? throw new CustomException((int)MessagesCodesError.SystemError, "Ejercicio no encontrado");

                return new GetExerciseByGuidResponse
                {
                    Exercise = exerciseDetail
                };
            }
        ).ConfigureAwait(false);
}
