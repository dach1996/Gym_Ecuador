using LogicAdministratorApi.Model.Request.Gym;
using LogicAdministratorApi.Model.Response.Gym;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.GymHandler;

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
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateGym, request, async () =>
            {
                if (await UnitOfWork.GymRepository
                    .ExistAnyAsync(where => where.Name.ToLower() == request.Name.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un gimnasio con este nombre");

                // Crear el nuevo gimnasio
                var newGym = new Gym
                {
                    Guid = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    ShortDescription = request.ShortDescription,
                    Phone = request.Phone,
                    Email = request.Email,
                    Website = request.Website,
                    IsActive = GymStatus.Active,
                    DateTimeRegister = Now,
                };

                // Guardar en la base de datos
                await UnitOfWork.GymRepository.AddAsync(newGym).ConfigureAwait(false);

                return new CreateGymResponse(newGym.Guid, newGym.Name)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

