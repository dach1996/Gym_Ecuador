using LogicApi.Abstractions.Interfaces.Authorization;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Request.Person;
using LogicApi.Model.Response.Authorization;
using PersistenceDb.Models.Authentication;

namespace LogicApi.BusinessLogic.AuthorizationHandler.AssignPerson;
public abstract class AssignPersonHandler(
    ILogger<AssignPersonHandler> logger,
    IPluginFactory pluginFactory) : AuthorizationBase<AssignPersonRequest, LoginResponse>(
        logger,
        pluginFactory), IAssignPersonHandler
{
    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<LoginResponse> Handle(AssignPersonRequest request, CancellationToken cancellationToken)
        => await PluginFactory.GetPlugin<IAssignPersonHandler>($"{request.LoginType.ToString().ToUpper()}")
            .Handle(request).ConfigureAwait(false);

    public abstract Task<LoginResponse> Handle(AssignPersonRequest request);

    /// <summary>
    /// Ejecuta las validaciones de login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected async Task<LoginResponse> ExecuteLoginValidationsAsync(AssignPersonRequest request)
    {
        var user = await UnitOfWork.UserRepository.GetFirstOrDefaultGenericAsync(
            select => new
            {
                select.HasCompleteRegistration,
            },
            where => where.UserName == request.UserName
        ).ConfigureAwait(false);
        if (user is not null && user.HasCompleteRegistration)
            throw new CustomException((int)MessagesCodesError.UserUsernameExist, $"El usuario '{request.UserName}' ya se encuentra registrado.");
        //Ejecuta el proceso de asignar persona
        return await LoginResponseAsync(request, await GetUserAsync(request).ConfigureAwait(false)).ConfigureAwait(false);
    }

    /// <summary>
    /// Obtiene el usuario
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected abstract Task<User> GetUserAsync(AssignPersonRequest request);

    /// <summary>
    /// Obtiene la persona
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected async Task<Person> GetPersonAsync(AssignPersonRequest request)
    {
        //Busca la persona en la base de datos
        var person = await UnitOfWork.PersonRepository
            .GetByFirstOrDefaultAsync(where => where.DocumentNumber == request.DocumentNumber).ConfigureAwait(false);
        //Si la persona no existe en la base buscamos en el servicio.
        if (person is null)
        {
            //Verificar el documento
            _ = await Mediator.Send(new GetPersonByDocumentNumberRequest(request.DocumentNumber, ContextRequest)).ConfigureAwait(false);
            //Obtiene la persona
            person = await UnitOfWork.PersonRepository.GetByFirstOrDefaultAsync(where => where.DocumentNumber == request.DocumentNumber).ConfigureAwait(false);
        }
        return person;
    }

    /// <summary>
    /// Ejecuta el proceso de asignar persona
    /// </summary>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    protected async Task<LoginResponse> LoginResponseAsync(AssignPersonRequest request, User user)
    {
        //Obtiene las personas
        var person = await GetPersonAsync(request).ConfigureAwait(false);
        //Valida que las palabras de la petición
        if (!person.FullName.ExistString(request.Name))
            throw new CustomException((int)MessagesCodesError.NamesOrLastNameDontMatch, $"El nombre '{request.Name}' no existe en la consulta.");
        if (!person.FullName.ExistString(request.LastName))
            throw new CustomException((int)MessagesCodesError.NamesOrLastNameDontMatch, $"El apellido '{request.LastName}' no existe en la consulta.");
        var typeIdentificationId = await UnitOfWork.TypeIdentificationRepository.GetFirstOrDefaultGenericAsync(
            select => new { select.Id },
            where => where.Code == request.TypeIdentificationCode).ConfigureAwait(false);
        //Verifica si se ha registrado los datos de la persona
        if (person.UserIdRegister is null)
        {
            person.DateTimeRegister = Clock.Now();
            person.TypeIdentificationId = typeIdentificationId?.Id;
            person.Name = request.Name;
            person.LastName = request.LastName;
            person.UserIdRegister = user.Id;
        }
        var genderCatalog = await UnitOfWork.CatalogRepository.GetIdByCodeAsync(request.GenderCode).ConfigureAwait(false);
        person.BirthDate = request.Birthday;
        person.GenderCatalogId = genderCatalog;
        await UnitOfWork.PersonRepository.UpdateAsync(person).ConfigureAwait(false);

        //Actualiza los datos de usuario
        user.Phone = request.Phone;
        user.PersonId = person.Id;
        user.UserName = request.UserName;
        user.HasCompleteRegistration = true;

        var manualUserRegistrationForm = user.ManualUserRegistrationForm;
        manualUserRegistrationForm.Password = GetPasswordEncrypted(request.NewPassword, user.Salt);
        manualUserRegistrationForm.PasswordTemporary = null;
        //Actualiza el usuario
        await UnitOfWork.UserRepository.UpdateAsync(user).ConfigureAwait(false);
        //Retorna la respuesta 
        return await GetLoginAsync(request, user).ConfigureAwait(false);
    }

    /// <summary>
    /// Obtiene le Login Abstracto
    /// </summary>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    protected abstract Task<LoginResponse> GetLoginAsync(AssignPersonRequest request, User user);
}
