using LogicAdministratorApi.Model.Request.Exercise;
using LogicAdministratorApi.Model.Response.Exercise;
using PersistenceDb.Models.Core;
using PersistenceDb.Models.Administration;
using Common.WebCommon.Models.Enum;
using LogicCommon.Model.Request.File;
using Common.WebCommon.Helper;

namespace LogicAdministratorApi.BusinessLogic.ExerciseHandler;

/// <summary>
/// Handler para actualizar ejercicio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateExerciseHandler(
    ILogger<UpdateExerciseHandler> logger,
    IPluginFactory pluginFactory) : ExerciseBase<UpdateExerciseRequest, UpdateExerciseResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un ejercicio
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateExerciseResponse> Handle(UpdateExerciseRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.UpdateExercise, request, async () =>
            {
                // Buscar el ejercicio por GUID
                var exercise = await UnitOfWork.ExerciseRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.ExerciseGuid)
                    .ConfigureAwait(false);

                if (exercise == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "El ejercicio no existe");

                // Validar que no exista otro ejercicio con el mismo nombre (excepto el que estamos actualizando)
                if (await UnitOfWork.ExerciseRepository
                    .ExistAnyAsync(where => where.Name.ToLower() == request.Name.ToLower() && where.Id != exercise.Id)
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe otro ejercicio con este nombre");

                // Validar que los tags existan si se proporcionaron y obtener los catálogos
                List<Catalog> existingCatalogs = null;
                if (request.TagCatalogCodes?.Any() == true)
                {
                    existingCatalogs = await UnitOfWork.CatalogRepository
                        .GetByAsync(where => request.TagCatalogCodes.Contains(where.Code))
                        .ConfigureAwait(false);

                    if (existingCatalogs.Count != request.TagCatalogCodes.Count)
                    {
                        var missingCodes = request.TagCatalogCodes.Except(existingCatalogs.Select(c => c.Code)).ToList();
                        throw new CustomException((int)MessagesCodesError.SystemError, $"Los siguientes códigos de catálogo no existen: {string.Join(", ", missingCodes)}");
                    }
                }

                // Iniciar transacción
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);

                // Actualizar los campos del ejercicio
                exercise.Name = request.Name;
                exercise.Description = request.Description;
                exercise.Instructions = request.Instructions;

                await UnitOfWork.ExerciseRepository.UpdateAsync(exercise).ConfigureAwait(false);

                // Procesar la imagen si se proporcionó
                if (request.Image != null)
                {
                    await ProcessExerciseImage(request.Image, exercise.Id).ConfigureAwait(false);
                }

                await UnitOfWork.ExerciseTagRepository.DeleteAsync(where => where.ExerciseId == exercise.Id).ConfigureAwait(false);

                // Crear los nuevos tags si se proporcionaron
                if (request.TagCatalogCodes?.Count > 0 && existingCatalogs?.Count > 0)
                {
                    var catalogCodeToId = existingCatalogs.ToDictionary(c => c.Code, c => c.Id);

                    var exerciseTags = request.TagCatalogCodes.Select(catalogCode => new ExerciseTag
                    {
                        ExerciseId = exercise.Id,
                        CatalogId = catalogCodeToId[catalogCode]
                    }).ToList();

                    await UnitOfWork.ExerciseTagRepository.AddRangeAsync(exerciseTags).ConfigureAwait(false);
                }

                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new UpdateExerciseResponse(exercise.Guid, exercise.Name)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);

    /// <summary>
    /// Procesa la imagen del ejercicio
    /// </summary>
    /// <param name="image"></param>
    /// <param name="exerciseId"></param>
    /// <returns></returns>
    private async Task ProcessExerciseImage(RequestEncodeFile image, int exerciseId)
    {
        var fileBasePaths = await GetFileBasePathCacheInformationByPathCodeAsync(PathCode.ExerciseImage).ConfigureAwait(false);
        var folderPath = HelperPathName.GetExercisePathName(fileBasePaths.FileDirectoryPath, exerciseId);
        await ProcessImagesAsync(
            [image],
            PathCode.ExerciseImage,
            processCreateImagesAsync: async (images, response) =>
            {
                var imageId = response.Items.FirstOrDefault()?.Id;
                if (imageId.HasValue)
                {
                    await UnitOfWork.ExerciseRepository.UpdateByAsync(
                        (exercise => exercise.ImageId, imageId.Value),
                        where => where.Id == exerciseId).ConfigureAwait(false);
                }
            },
            getFileExtension: (fileExtension) => HelperFileName.GetExerciseImageName(fileExtension.Replace(".", ""), exerciseId),
            folderPath: folderPath
        ).ConfigureAwait(false);
    }
}
