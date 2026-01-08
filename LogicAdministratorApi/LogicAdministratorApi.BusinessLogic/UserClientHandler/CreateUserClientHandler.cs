using Common.Templates.Models.Mail;
using Common.Utils.Cryptography.Argon2;
using LogicAdministratorApi.Model.Request.UserClient;
using LogicAdministratorApi.Model.Response.UserClient;
using LogicCommon.Model.Request.Mail;
using LogicCommon.Model.Request.Person;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.UserClientHandler;

/// <summary>
/// Handler para crear usuario cliente
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateUserClientHandler(
    ILogger<CreateUserClientHandler> logger,
    IPluginFactory pluginFactory) : UserClientBase<CreateUserClientRequest, CreateUserClientResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un usuario cliente
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateUserClientResponse> Handle(CreateUserClientRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateUserClient, request, async () =>
            {
                // Obtener el plan por GUID
                var branchPlan = await UnitOfWork.BranchPlanRepository
                    .GetFirstOrDefaultGenericAsync(select => new
                    {
                        select.Id,
                        select.GymBranchId,
                        select.DurationDays,
                        select.IsActive
                    }, where => where.Guid == request.BranchPlanGuid)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Plan de sucursal no encontrado");
                // Validar que el plan de sucursal está activo
                if (!branchPlan.IsActive)
                    throw new CustomException((int)MessagesCodesError.SystemError, "El plan de sucursal no está activo");
                // Validar que el usuario no exista
                var currentUser = await UnitOfWork.UserRepository
                    .GetFirstOrDefaultGenericAsync(select => new
                    {
                        select.Id,
                        select.Guid,
                        select.Email,
                        select.UserName,
                        select.PersonId,
                        Names = select.Person.RealNames,
                        LastNames = select.Person.RealLastNames,
                    },
                    where => where.Email.ToLower() == request.Email.ToLower()
                    || where.UserName.ToLower() == request.Email.ToLower())
                    .ConfigureAwait(false);
                var password = GeneratePassword();
                var isNewUser = currentUser is null;
                if (isNewUser)
                {
                    // Obtener la persona por el número de identificación
                    var personDetails = await Mediator.Send(new GetPersonByDocumentNumberCommonRequest(request.ContextRequest, request.IdentificationNumber), cancellationToken).ConfigureAwait(false);

                    // Validar que el email no exista
                    if (await UnitOfWork.UserRepository
                        .ExistAnyAsync(where => where.Email.ToLower() == request.Email.ToLower()
                        || where.UserName.ToLower() == request.Email.ToLower())
                        .ConfigureAwait(false))
                        throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un usuario con este email");

                    // Generar salt y contraseña
                    var salt = Argon2.GenerateRandomSecretBytes();
                    var passwordEncrypted = GetPasswordEncrypted(password, salt);

                    // Obtener el rol de cliente móvil
                    var roleId = await UnitOfWork.RoleRepository.GetIdByScopeAndPlatformAsync(
                            RoleType.Client, RolePlatformType.Mobile).ConfigureAwait(false);
                    // Crear el nuevo usuario
                    var newUser = new User
                    {
                        Guid = Guid.NewGuid(),
                        UserName = request.Email,
                        Email = request.Email,
                        Phone = request.Phone,
                        PersonId = personDetails.Person.Id,
                        LanguageCode = request.LanguageCode ?? "es",
                        DateTimeRegister = Now,
                        IsBlocked = false,
                        HasCompleteRegistration = true,
                        Salt = salt,
                        UserRegistrationForms =
                        [
                            new ()
                        {
                            DateTimeRegister = Now,
                            DateTimeLastAccess = Now,
                            UserTypeRegister = UserTypeRegister.Manual,
                            Password = passwordEncrypted,
                            PasswordTemporary = passwordEncrypted
                        }
                        ],
                        UserRoleScopes = [
                            new ()
                        {
                            RoleId = roleId,
                        }
                        ]
                    };
                    newUser = await UnitOfWork.UserRepository.AddAsync(newUser).ConfigureAwait(false);
                    currentUser = new
                    {
                        newUser.Id,
                        newUser.Guid,
                        newUser.Email,
                        newUser.UserName,
                        newUser.PersonId,
                        personDetails.Person.Names,
                        personDetails.Person.LastNames,
                    };
                }

                // Guardar en la base de datos
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);

                // Verificar si ya existe un ClientGymBranch para este usuario y sucursal
                var clientGymBranch = await UnitOfWork.ClientGymBranchRepository
                    .GetFirstOrDefaultGenericAsync(select => new
                    {
                        select.Id,
                    }, where => where.UserId == currentUser.Id && where.GymBranchId == branchPlan.GymBranchId)
                    .ConfigureAwait(false);
                if (clientGymBranch is null)
                {
                    // Crear nuevo ClientGymBranch
                    var newClientGymBranch = new ClientGymBranch
                    {
                        Guid = Guid.NewGuid(),
                        UserId = currentUser.Id,
                        GymBranchId = branchPlan.GymBranchId,
                        RegistrationDate = Now,
                        Status = true
                    };
                    newClientGymBranch = await UnitOfWork.ClientGymBranchRepository.AddAsync(newClientGymBranch).ConfigureAwait(false);
                    clientGymBranch = new
                    {
                        newClientGymBranch.Id,
                    };
                }

                // Crear la membresía (ClientMembership)
                await UnitOfWork.ClientMembershipRepository.AddAsync(new ClientMembership
                {
                    Guid = Guid.NewGuid(),
                    ClientGymBranchId = clientGymBranch.Id,
                    BranchPlanId = branchPlan.Id,
                    StartDate = Now,
                    EndDate = branchPlan.DurationDays.HasValue ? Now.AddDays(branchPlan.DurationDays.Value) : null,
                    IsActive = true,
                    RegistrationDate = Now
                }).ConfigureAwait(false);

                await UnitOfWork.CommitAsync().ConfigureAwait(false);
                await Mediator.Send(new SendMailRequest
                {
                    MailTemplateModel = new NewUserManualRegisterMailTemplateModel
                    {
                        PersonName = currentUser.Names.Split(' ').FirstOrDefault() + " " + currentUser.LastNames?.Split(' ').FirstOrDefault(),
                        Email = currentUser.Email,
                        Password = password,
                        SupportEmail = "soporte@fitecenter.fit",
                        Link = "https://fitcenter-administrator-app-service.azurewebsites.net/"
                    },
                    To = [currentUser.Email]
                }).ConfigureAwait(false);
                return new CreateUserClientResponse(currentUser.Guid, currentUser.UserName, currentUser.Email)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

