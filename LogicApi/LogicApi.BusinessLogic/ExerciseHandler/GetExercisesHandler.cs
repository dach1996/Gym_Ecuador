using LogicApi.Model.Request.Exercise;
using LogicApi.Model.Response.Exercise;
using LogicCommon.Model.Response.File;
using Common.WebApi.Models.Enum;
using System.Linq.Expressions;

namespace LogicApi.BusinessLogic.ExerciseHandler;

/// <summary>
/// Handler para obtener ejercicios
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetExercisesHandler(
    ILogger<GetExercisesHandler> logger,
    IPluginFactory pluginFactory) : ExerciseBase<GetExercisesRequest, GetExercisesResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de ejercicios con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetExercisesResponse> Handle(GetExercisesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetExercises, request, async () =>
            {
                // Construir el filtro
                Expression<Func<PersistenceDb.Models.Core.Exercise, bool>> whereFilter = exercise => true;

                if (request.CatalogId.HasValue)
                {
                    // Filtrar por tag/categoría
                    whereFilter = exercise => exercise.ExerciseTags.Any(et => et.CatalogId == request.CatalogId.Value);
                }

                // Obtener datos paginados de ejercicios
                var paginatedResult = await UnitOfWork.ExerciseRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new ExerciseItem
                        {
                            Guid = select.Guid,
                            Name = select.Name,
                            Description = select.Description,
                            ImageUrl = select.Image != null && select.Image.State
                                ? new FileUrlResponse(select.Image.Guid, select.Image.FileBasePath.BaseUrl, select.Image.Path)
                                : null,
                            Tags = select.ExerciseTags
                                .Select(et => et.Catalog.CatalogLanguages.FirstOrDefault().Name)
                                .ToList()
                        },
                        whereFilter
                    ).ConfigureAwait(false);

                return new GetExercisesResponse(
                    totalRegister: paginatedResult.TotalItems,
                    registers: paginatedResult.Items
                );
            }
        ).ConfigureAwait(false);
}
