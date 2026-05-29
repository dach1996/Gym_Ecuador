using LogicAdministratorApi.Model.Request.GymBranchSchedule;
using LogicAdministratorApi.Model.Response.GymBranchSchedule;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.GymBranchScheduleHandler;

/// <summary>
/// Handler para crear horario de sucursal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateGymBranchScheduleHandler(
    ILogger<CreateGymBranchScheduleHandler> logger,
    IPluginFactory pluginFactory) : GymBranchScheduleBase<CreateGymBranchScheduleRequest, CreateGymBranchScheduleResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un horario de sucursal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateGymBranchScheduleResponse> Handle(CreateGymBranchScheduleRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateGymBranchSchedule, request, async () =>
            {
                // Validar que la sucursal existe
                var branch = await UnitOfWork.GymBranchRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.GymBranchGuid)
                    .ConfigureAwait(false);

                if (branch == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Sucursal de gimnasio no encontrada");

                // Validar que la hora de apertura sea menor que la hora de cierre
                if (request.OpeningTime >= request.ClosingTime)
                    throw new CustomException((int)MessagesCodesError.SystemError, "La hora de apertura debe ser menor que la hora de cierre");

                // Validar que no exista ya un horario para ese día en esa sucursal
                if (await UnitOfWork.GymBranchScheduleRepository
                    .ExistAnyAsync(where => where.GymBranchId == branch.Id && where.DayOfWeekCatalogId == request.DayOfWeekCatalogId)
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un horario para este día en la sucursal");

                // Crear el horario
                var newSchedule = new GymBranchSchedule
                {
                    GymBranchId = branch.Id,
                    DayOfWeekCatalogId = request.DayOfWeekCatalogId,
                    OpeningTime = request.OpeningTime,
                    ClosingTime = request.ClosingTime,
                    IsVisible = request.IsVisible
                };

                await UnitOfWork.GymBranchScheduleRepository.AddAsync(newSchedule).ConfigureAwait(false);

                // Obtener nombre del día
                var dayName = GetDayOfWeekName(request.DayOfWeekCatalogId);

                return new CreateGymBranchScheduleResponse(newSchedule.Id, dayName)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);

    private static string GetDayOfWeekName(int dayId)
    {
        return dayId switch
        {
            1 => "Lunes",
            2 => "Martes",
            3 => "Miércoles",
            4 => "Jueves",
            5 => "Viernes",
            6 => "Sábado",
            7 => "Domingo",
            _ => "Desconocido"
        };
    }
}

