using LogicApi.Model.Request.Gym;
using LogicApi.Model.Response.Gym;

namespace LogicApi.BusinessLogic.GymHandler;

/// <summary>
/// Handler para obtener gimnasios
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymsHandler(
    ILogger<GetGymsHandler> logger,
    IPluginFactory pluginFactory) : GymBase<GetGymsRequest, GetGymsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de gimnasios con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymsResponse> Handle(GetGymsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetGyms, request, async () =>
            {
                // Obtener el total de registros
                var totalRecords = await UnitOfWork.GymRepository.CountAsync().ConfigureAwait(false);

                // Aplicar paginación
                var gyms = await UnitOfWork.GymRepository
                    .GetByAsync(
                    ).ConfigureAwait(false);

                // Mapear a DTOs
                var gymItems = gyms.Select(g => new GymItem
                {
                    Guid = g.Guid,
                    Name = g.Name,
                    ShortDescription = g.ShortDescription,
                    Address = g.Address,
                    Phone = g.Phone,
                    Email = g.Email,
                    OpeningTime = g.OpeningTime,
                    ClosingTime = g.ClosingTime,
                    IsActive = g.IsActive,
                    DateTimeRegister = g.DateTimeRegister
                });

                return new GetGymsResponse(gymItems, totalRecords, request.Page, request.PageSize)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}
