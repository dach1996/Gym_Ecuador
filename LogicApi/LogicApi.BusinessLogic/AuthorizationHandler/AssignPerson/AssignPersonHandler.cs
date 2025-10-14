using Common.Utils.Cryptography.Argon2;
using LogicApi.Abstractions.Interfaces.Authorization;
using LogicApi.Model.Request.Authorization;
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
        var user = await AuthenticationUnitOfWork.UserRepository.GetFirstOrDefaultGenericAsync(
            select => new
            {
                select.HasCompleteRegistration,
            },
            where => where.UserName == request.UserName
        ).ConfigureAwait(false);
        if (user is not null && user.HasCompleteRegistration)
            throw new CustomException((int)MessagesCodesError.UserUsernameExist, $"El usuario '{request.UserName}' ya se encuentra registrado.");
        //Verifica los catálogos de la petición
        await ValidateExistValueInFileCatalogAsync(EnumLogicApi.CatalogsTypeItemsCodes.DocumentType, request.DocumentTypeCode).ConfigureAwait(false);
        await ValidateExistValueInFileCatalogAsync(EnumLogicApi.CatalogsTypeItemsCodes.Nationality, request.NationalityCode).ConfigureAwait(false);
        //Busca el perfil del usuario en la Base de Datos, este dato puede ser null ya que puede ser el primer login del usuario
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
        var person = await AuthenticationUnitOfWork.PersonRepository
            .GetByFirstOrDefaultAsync(where => where.DocumentNumber == request.DocumentNumber).ConfigureAwait(false);
        //Si la persona no existe en la base buscamos en el servicio.
        if (person is null)
        {
            //Verificar el documento
            var documentInformation = await GetPersonInformationAsync(request.DocumentNumber).ConfigureAwait(false);
            //Agrega la persona
            person = await AuthenticationUnitOfWork.PersonRepository.AddAsync(new Person
            {
                DocumentNumber = request.DocumentNumber,
                RealNames = documentInformation.Names,
                RealLastNames = documentInformation.LastNames,
                FullName = documentInformation.FullName,
            }).ConfigureAwait(false);
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
        //Verifica si se ha registrado los datos de la persona
        if (person.UserIdRegister is null)
        {
            person.DateTimeRegister = Clock.Now();
            person.NationalityCode = request.NationalityCode;
            person.DocumentTypeCode = request.DocumentTypeCode;
            person.Name = request.Name;
            person.LastName = request.LastName;
            person.UserIdRegister = user.Id;
            //Actualiza la persona
            await AuthenticationUnitOfWork.PersonRepository.UpdateAsync(person).ConfigureAwait(false);
        }
        //Actualiza los datos de usuario
        user.Phone = request.Phone;
        user.PersonId = person.Id;
        user.Companions ??= [];
        user.UserName = request.UserName;
        user.HasCompleteRegistration = true;
        user.Companions.Add(new()
        {
            PersonId = person.Id,
            UserId = user.Id,
            DateTimeRegister = Clock.Now(),
        });
        var manualUserRegistrationForm = user.ManualUserRegistrationForm;
        manualUserRegistrationForm.Password = GetPasswordEncrypted(request.NewPassword, user.Salt);
        manualUserRegistrationForm.PasswordTemporary = null;
        //Actualiza el usuario
        await AuthenticationUnitOfWork.UserRepository.UpdateAsync(user).ConfigureAwait(false);
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
