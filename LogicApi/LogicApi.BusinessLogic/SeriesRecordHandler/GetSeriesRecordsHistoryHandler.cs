using LogicApi.Model.Request.SeriesRecord;
using LogicApi.Model.Response.SeriesRecord;
using Common.WebApi.Models.Enum;
using System.Linq.Expressions;

namespace LogicApi.BusinessLogic.SeriesRecordHandler;

/// <summary>
/// Handler para obtener historial de registros de series
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetSeriesRecordsHistoryHandler(
    ILogger<GetSeriesRecordsHistoryHandler> logger,
    IPluginFactory pluginFactory) : SeriesRecordBase<GetSeriesRecordsHistoryRequest, GetSeriesRecordsHistoryResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención del historial de registros de series con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetSeriesRecordsHistoryResponse> Handle(GetSeriesRecordsHistoryRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetSeriesRecordsHistory, request, async () =>
            {
                // Obtener el ID del usuario por GUID
                var user = await UnitOfWork.UserRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new { select.Id },
                        where => where.Guid == UserGuid)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Usuario no encontrado");

                // Construir el filtro
                Expression<Func<PersistenceDb.Models.Core.SeriesRecord, bool>> whereFilter = 
                    record => record.UserId == user.Id;

                if (request.ExerciseGuid.HasValue)
                {
                    // Obtener el ID del ejercicio por GUID
                    var exercise = await UnitOfWork.ExerciseRepository
                        .GetFirstOrDefaultGenericAsync(
                            select => new { select.Id },
                            where => where.Guid == request.ExerciseGuid.Value)
                        .ConfigureAwait(false);

                    if (exercise != null)
                    {
                        whereFilter = record => record.UserId == user.Id && record.ExerciseId == exercise.Id;
                    }
                }

                // Obtener datos paginados de registros de series
                var paginatedResult = await UnitOfWork.SeriesRecordRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new SeriesRecordItem
                        {
                            Guid = select.Guid,
                            ExerciseGuid = select.Exercise.Guid,
                            ExerciseName = select.Exercise.Name,
                            RegistrationDate = select.RegistrationDate,
                            Weight = select.Weight,
                            Repetitions = select.Repetitions
                        },
                        whereFilter,
                        include => include.Exercise
                    ).ConfigureAwait(false);

                return new GetSeriesRecordsHistoryResponse(
                    totalRegister: paginatedResult.TotalItems,
                    registers: paginatedResult.Items
                );
            }
        ).ConfigureAwait(false);
}
