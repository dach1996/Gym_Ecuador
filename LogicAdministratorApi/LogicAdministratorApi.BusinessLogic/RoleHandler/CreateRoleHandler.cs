using LogicAdministratorApi.Model.Request.Role;
using LogicAdministratorApi.Model.Response.Role;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Enums;

namespace LogicAdministratorApi.BusinessLogic.RoleHandler;

/// <summary>
/// Handler para crear un nuevo rol
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateRoleHandler(
    ILogger<CreateRoleHandler> logger,
    IPluginFactory pluginFactory) : RoleBase<CreateRoleRequest, CreateRoleResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un nuevo rol
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateRoleResponse> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateRole, request, async () =>
        {
            // Validar que el nombre del rol no exista
            if (await UnitOfWork.RoleRepository
                .ExistAnyAsync(where => where.Code.ToLower() == request.Name.ToLower())
                .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un rol con este nombre");

            // Validar que la plataforma existe
         
            var rolePlatform = RolePlatformType.Web.GetEnumMember();
            var rolePlatformId = (await UnitOfWork.PlatformRepository
                .GetFirstOrDefaultGenericAsync(select => (byte?)select.Id, where => where.Name == rolePlatform)
                .ConfigureAwait(false))
                ?? throw new CustomException((int)MessagesCodesError.SystemError, "Plataforma no encontrada");

            // Validar que las funcionalidades existen
            if (request.FunctionalityGuids.Any())
            {
                var functionalitiesCount = await UnitOfWork.FunctionalityRepository
                    .CountAsync(where => request.FunctionalityGuids.Contains(where.Id))
                    .ConfigureAwait(false);

                if (functionalitiesCount != request.FunctionalityGuids.Count)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Una o más funcionalidades no fueron encontradas");
            }

            // Crear el nuevo rol
            var newRole = new Role
            {
                Guid = Guid.NewGuid(),
                Code = request.Name,
                Name = request.Description,
                PlatformId = rolePlatformId,
                Scope = request.ScopeCode.ToEnum<RoleScope>()
            };

            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await UnitOfWork.RoleRepository.AddAsync(newRole).ConfigureAwait(false);

            // Asignar funcionalidades al rol
            if (request.FunctionalityGuids.Any())
            {
                var roleFunctionalities = request.FunctionalityGuids.Select(functionalityId => new RoleFunctionality
                {
                    RoleId = newRole.Id,
                    FunctionalityId = functionalityId
                }).ToList();

                await UnitOfWork.RoleFunctionalityRepository.AddRangeAsync(roleFunctionalities).ConfigureAwait(false);
            }

            await UnitOfWork.CommitAsync().ConfigureAwait(false);

            return new CreateRoleResponse
            {
                RoleGuid = newRole.Guid,
                Name = newRole.Code,
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }).ConfigureAwait(false);
}
