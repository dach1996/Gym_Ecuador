using LogicApi.Model.Request.Membership;
using LogicApi.Model.Response.Membership;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.MembershipHandler;

/// <summary>
/// Handler para crear membresía
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateMembershipHandler(
    ILogger<CreateMembershipHandler> logger,
    IPluginFactory pluginFactory) : MembershipBase<CreateMembershipRequest, CreateMembershipResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de una membresía
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateMembershipResponse> Handle(CreateMembershipRequest request, CancellationToken cancellationToken)
    {
        return await ExecuteHandlerAsync(
            OperationApiName.CreateMembership,
            request,
            async () =>
            {
                // Validar que la persona exista
                var person = await UnitOfWork.PersonRepository
                    .GetByFirstOrDefaultAsync(where => where.Id == request.PersonId)
                    .ConfigureAwait(false);

                if (person == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "La persona especificada no existe");

                // Validar que el gimnasio exista
                var gym = await UnitOfWork.GymRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.GymGuid)
                    .ConfigureAwait(false);

                if (gym == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "El gimnasio especificado no existe");

                // Validar que el tipo de membresía exista
                var membershipType = await UnitOfWork.MembershipTypeRepository
                    .GetByFirstOrDefaultAsync(where => where.Id == request.MembershipTypeId)
                    .ConfigureAwait(false);

                if (membershipType == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "El tipo de membresía especificado no existe");

                // Validar fechas
                if (request.StartDate >= request.EndDate)
                    throw new CustomException((int)MessagesCodesError.SystemError, "La fecha de inicio debe ser anterior a la fecha de fin");

                // Verificar si ya existe una membresía activa para esta persona en este gimnasio
                var existingMembership = await UnitOfWork.MembershipRepository
                    .GetByFirstOrDefaultAsync(where => 
                        where.PersonId == request.PersonId && 
                        where.GymId == gym.Id && 
                        where.Status == "Activa" &&
                        where.EndDate > DateTime.UtcNow)
                    .ConfigureAwait(false);

                if (existingMembership != null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Esta persona ya tiene una membresía activa en este gimnasio");

                // Crear la nueva membresía
                var newMembership = new Membership
                {
                    Guid = Guid.NewGuid(),
                    PersonId = request.PersonId,
                    GymId = gym.Id,
                    MembershipTypeId = request.MembershipTypeId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Status = "Activa",
                    GymRole = request.GymRole,
                    DateTimeRegister = Now,
                    UserIdRegister = UserId
                };

                // Guardar en la base de datos
                await UnitOfWork.MembershipRepository.AddAsync(newMembership).ConfigureAwait(false);

                return new CreateMembershipResponse(newMembership.Guid, newMembership.Status, newMembership.EndDate)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
    }
}
