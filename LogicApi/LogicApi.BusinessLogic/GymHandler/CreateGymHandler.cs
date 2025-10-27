using LogicApi.Model.Request.Gym;
using LogicApi.Model.Response.Gym;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.GymHandler;

/// <summary>
/// Handler para crear gimnasio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateGymHandler(
    ILogger<CreateGymHandler> logger,
    IPluginFactory pluginFactory) : GymBase<CreateGymRequest, CreateGymResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un gimnasio
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateGymResponse> Handle(CreateGymRequest request, CancellationToken cancellationToken)
    {
        return await ExecuteHandlerAsync(
            OperationApiName.CreateGym,
            request,
            async () =>
            {
                // Validar que el nombre no esté vacío
                if (string.IsNullOrWhiteSpace(request.Name))
                    throw new CustomException((int)MessagesCodesError.SystemError, "El nombre del gimnasio es requerido");

                // Validar que no exista otro gimnasio con el mismo nombre
                var existingGym = await UnitOfWork.GymRepository
                    .GetByFirstOrDefaultAsync(where => where.Name.ToLower() == request.Name.ToLower())
                    .ConfigureAwait(false);

                if (existingGym != null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un gimnasio con este nombre");

                // Crear el nuevo gimnasio
                var newGym = new Gym
                {
                    Guid = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    ShortDescription = request.ShortDescription,
                    Address = request.Address,
                    Phone = request.Phone,
                    Email = request.Email,
                    Website = request.Website,
                    OpeningTime = request.OpeningTime,
                    ClosingTime = request.ClosingTime,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    IsActive = true,
                    DateTimeRegister = Now,
                    UserIdRegister = UserId
                };

                // Guardar en la base de datos
                await UnitOfWork.GymRepository.AddAsync(newGym).ConfigureAwait(false);

                return new CreateGymResponse(newGym.Guid, newGym.Name)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
    }
}
