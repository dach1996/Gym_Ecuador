using LogicApi.Model.Request.GymBranch;
using LogicApi.Model.Response.GymBranch;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.GymBranchHandler;

/// <summary>
/// Handler para crear sucursal de gimnasio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateGymBranchHandler(
    ILogger<CreateGymBranchHandler> logger,
    IPluginFactory pluginFactory) : GymBranchBase<CreateGymBranchRequest, CreateGymBranchResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de una sucursal de gimnasio
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateGymBranchResponse> Handle(CreateGymBranchRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.CreateGymBranch, request, async () =>
            {
                // Validar que el gimnasio existe
                var gym = await UnitOfWork.GymRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.GymGuid)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el gimnasio especificado");

                // Validar que no exista una sucursal con el mismo nombre para este gimnasio
                if (await UnitOfWork.GymBranchRepository
                    .ExistAnyAsync(where => where.GymId == gym.Id && where.Name.ToLower() == request.Name.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe una sucursal con este nombre para el gimnasio");

                // Validar que el código no exista (si se proporciona)
                if (!string.IsNullOrEmpty(request.Code) && await UnitOfWork.GymBranchRepository
                        .ExistAnyAsync(where => where.Code.ToLower() == request.Code.ToLower())
                        .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe una sucursal con este código");

                // Crear la nueva sucursal
                var newGymBranch = new GymBranch
                {
                    Guid = Guid.NewGuid(),
                    GymId = gym.Id,
                    Name = request.Name,
                    Code = request.Code,
                    Description = request.Description,
                    Address = request.Address,
                    Phone = request.Phone,
                    Email = request.Email,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    MaxCapacity = request.MaxCapacity,
                    AreaSquareMeters = request.AreaSquareMeters,
                    FloorCount = request.FloorCount,
                    OpeningDate = request.OpeningDate,
                    IsActive = true,
                    DateTimeRegister = Now,
                };

                // Guardar en la base de datos
                await UnitOfWork.GymBranchRepository.AddAsync(newGymBranch).ConfigureAwait(false);

                return new CreateGymBranchResponse(newGymBranch.Guid, newGymBranch.Name, newGymBranch.Code, gym.Guid)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

