using LogicApi.Model.Request.GymBranchSchedule;
using LogicApi.Model.Response.GymBranchSchedule;
using Microsoft.EntityFrameworkCore;

namespace LogicApi.BusinessLogic.GymBranchScheduleHandler;

/// <summary>
/// Handler para obtener horarios de sucursal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymBranchSchedulesHandler(
    ILogger<GetGymBranchSchedulesHandler> logger,
    IPluginFactory pluginFactory) : GymBranchScheduleBase<GetGymBranchSchedulesRequest, GetGymBranchSchedulesResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de horarios de una sucursal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymBranchSchedulesResponse> Handle(GetGymBranchSchedulesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetGymBranchSchedules, request, async () =>
            {
                // Buscar la sucursal
                var branch = await UnitOfWork.GymBranchRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.GymBranchGuid)
                    .ConfigureAwait(false);

                if (branch == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Sucursal de gimnasio no encontrada");

                // Obtener los horarios de la sucursal
                var schedules = await UnitOfWork.GymBranchScheduleRepository
                    .GetByAsync(where => where.GymBranchId == branch.Id)
                    .ConfigureAwait(false);

                // Mapear a DTOs
                var scheduleItems = schedules.Select(s => new GymBranchScheduleItem
                {
                    Id = s.Id,
                    DayOfWeekCatalogId = s.DayOfWeekCatalogId,
                    DayOfWeekName = GetDayOfWeekName(s.DayOfWeekCatalogId),
                    OpeningTime = s.OpeningTime,
                    ClosingTime = s.ClosingTime,
                    IsVisible = s.IsVisible
                });

                return new GetGymBranchSchedulesResponse(branch.Name, scheduleItems)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);

    private string GetDayOfWeekName(int dayId)
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

