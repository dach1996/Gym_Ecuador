using LogicApi.Model.Request.SeriesRecord;
using LogicApi.Model.Response.SeriesRecord;
using PersistenceDb.Models.Core;
using Common.WebApi.Models.Enum;

namespace LogicApi.BusinessLogic.SeriesRecordHandler;

/// <summary>
/// Handler para crear registro de serie
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateSeriesRecordHandler(
    ILogger<CreateSeriesRecordHandler> logger,
    IPluginFactory pluginFactory) : SeriesRecordBase<CreateSeriesRecordRequest, CreateSeriesRecordResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un registro de serie
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateSeriesRecordResponse> Handle(CreateSeriesRecordRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.CreateSeriesRecord, request, async () =>
            {
                // Validar que el ejercicio exista
                var exercise = await UnitOfWork.ExerciseRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.ExerciseGuid)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Ejercicio no encontrado");

                // Obtener el ID del usuario por GUID
                var user = await UnitOfWork.UserRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new { select.Id },
                        where => where.Guid == UserGuid)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Usuario no encontrado");

                // Crear el registro de serie
                var newSeriesRecord = new SeriesRecord
                {
                    Guid = Guid.NewGuid(),
                    ExerciseId = exercise.Id,
                    UserId = user.Id,
                    RegistrationDate = request.RegistrationDate ?? Now,
                    Weight = request.Weight,
                    Repetitions = request.Repetitions
                };

                await UnitOfWork.SeriesRecordRepository.AddAsync(newSeriesRecord).ConfigureAwait(false);

                return new CreateSeriesRecordResponse
                {
                    SeriesRecordGuid = newSeriesRecord.Guid,
                    ExerciseGuid = exercise.Guid,
                    ExerciseName = exercise.Name,
                    RegistrationDate = newSeriesRecord.RegistrationDate,
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}
