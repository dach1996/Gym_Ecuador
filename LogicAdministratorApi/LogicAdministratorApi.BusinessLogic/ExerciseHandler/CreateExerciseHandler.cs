using LogicAdministratorApi.Model.Request.Exercise;
using LogicAdministratorApi.Model.Response.Exercise;
using PersistenceDb.Models.Core;
using PersistenceDb.Models.Administration;
using Common.WebApi.Models.Enum;
using Common.WebCommon.Models.Enum;
using LogicCommon.Model.Request.File;
using Common.WebCommon.Helper;

namespace LogicAdministratorApi.BusinessLogic.ExerciseHandler;

/// <summary>
/// Handler para crear ejercicio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateExerciseHandler(
    ILogger<CreateExerciseHandler> logger,
    IPluginFactory pluginFactory) : ExerciseBase<CreateExerciseRequest, CreateExerciseResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un ejercicio
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateExerciseResponse> Handle(CreateExerciseRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateExercise, request, async () =>
            {
                // Validar que no exista un ejercicio con el mismo nombre
                if (await UnitOfWork.ExerciseRepository
                    .ExistAnyAsync(where => where.Name.ToLower() == request.Name.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un ejercicio con este nombre");

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

                // Crear el nuevo ejercicio
                var newExercise = new Exercise
                {
                    Guid = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    Instructions = request.Instructions
                };

                // Guardar en la base de datos dentro de una transacción
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);

                var exercise = await UnitOfWork.ExerciseRepository.AddAsync(newExercise).ConfigureAwait(false);

                // Procesar la imagen si se proporcionó
                if (request.Image != null)
                {
                    await ProcessExerciseImage(request.Image, exercise.Id).ConfigureAwait(false);
                }

                // Crear los tags si se proporcionaron
                if (request.TagCatalogCodes?.Any() == true && existingCatalogs != null)
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

                return new CreateExerciseResponse(exercise.Guid, exercise.Name)
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
