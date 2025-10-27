using LogicApi.Model.Request.Gym;
using LogicApi.Model.Response.Gym;

namespace LogicApi.BusinessLogic.GymHandler;

/// <summary>
/// Handler para actualizar gimnasio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateGymHandler(
    ILogger<UpdateGymHandler> logger,
    IPluginFactory pluginFactory) : GymBase<UpdateGymRequest, UpdateGymResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un gimnasio
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateGymResponse> Handle(UpdateGymRequest request, CancellationToken cancellationToken)
    {
        return await ExecuteHandlerAsync(
            OperationApiName.UpdateGym,
            request,
            async () =>
            {
                // Buscar el gimnasio por GUID
                var gym = await UnitOfWork.GymRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.GymGuid)
                    .ConfigureAwait(false);

                if (gym == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Gimnasio no encontrado");

                // Validar que el nombre no esté vacío
                if (string.IsNullOrWhiteSpace(request.Name))
                    throw new CustomException((int)MessagesCodesError.SystemError, "El nombre del gimnasio es requerido");

                // Validar que no exista otro gimnasio con el mismo nombre (excluyendo el actual)
                var existingGym = await UnitOfWork.GymRepository
                    .GetByFirstOrDefaultAsync(where => where.Name.ToLower() == request.Name.ToLower() && where.Id != gym.Id)
                    .ConfigureAwait(false);

                if (existingGym != null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe otro gimnasio con este nombre");

                // Actualizar los campos
                gym.Name = request.Name;
                gym.Description = request.Description;
                gym.ShortDescription = request.ShortDescription;
                gym.Address = request.Address;
                gym.Phone = request.Phone;
                gym.Email = request.Email;
                gym.Website = request.Website;
                gym.OpeningTime = request.OpeningTime;
                gym.ClosingTime = request.ClosingTime;
                gym.Latitude = request.Latitude;
                gym.Longitude = request.Longitude;
                gym.IsActive = request.IsActive;

                return new UpdateGymResponse(gym.Guid, gym.Name)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
    }
}
