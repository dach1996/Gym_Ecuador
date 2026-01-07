using Common.Messages;
using Common.UserDocumentation;
using Common.Utils.ConstansCodes;
using Common.Utils.CustomExceptions;
using LogicCommon.Model.Request.Person;
using LogicCommon.Model.Response.Person;
using PersistenceDb.Models.Authentication;

namespace LogicCommon.BusinessLogic.PersonHandler;

/// <summary>
/// Handler común para obtener persona por número de cédula
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetPersonByDocumentNumberHandler(
    ILogger<GetPersonByDocumentNumberHandler> logger,
    IPluginFactory pluginFactory) : PersonBase<GetPersonByDocumentNumberCommonRequest, GetPersonByDocumentNumberResponse>(
        logger,
        pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de una persona por su número de cédula
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetPersonByDocumentNumberResponse> Handle(GetPersonByDocumentNumberCommonRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(request, async () =>
            {
                var personDetails = await AdministratorCache.TryGetOrSetAsync(CacheCodes.PersonDetailsByDocumentNumber(request.DocumentNumber), async () =>
                    {
                        var person = await UnitOfWork.PersonRepository.GetFirstOrDefaultGenericAsync(
                                            select => new PersonDetail
                                            {
                                                Id = select.Id,
                                                DocumentNumber = select.DocumentNumber,
                                                Names = select.RealNames,
                                                LastNames = select.RealLastNames,
                                                FullName = select.FullName,
                                                BirthDate = select.BirthDate,
                                                Guid = select.Guid,
                                            },
                                            where => where.DocumentNumber == request.DocumentNumber).ConfigureAwait(false);
                        //Si la persona no existe en la base de datos, se busca en el servicio de documentación
                        if (person is null)
                        {
                            var personInformation = await PluginFactory.GetPlugin<IDocumentationServices>(AppSettings.DocumentationServicesConfiguration.CurrentImplementation)
                            .GetPersonInformationAsync(new(request.DocumentNumber)).ConfigureAwait(false)
                                            ?? throw new CustomException((int)MessagesCodesError.PersonInformationNotFound, $"No se pudo encontrar información del documento: '{request.DocumentNumber}'");

                            var newPerson = await UnitOfWork.PersonRepository.AddAsync(new Person
                            {
                                DocumentNumber = request.DocumentNumber,
                                RealNames = personInformation.Names,
                                RealLastNames = personInformation.LastNames,
                                FullName = personInformation.FullName,
                                DateTimeRegister = Now,
                                BirthDate = personInformation.BirthDate?.Date,
                                Guid = Guid.NewGuid(),
                            }).ConfigureAwait(false);
                            person = new PersonDetail
                            {
                                Id = newPerson.Id,
                                DocumentNumber = newPerson.DocumentNumber,
                                Names = newPerson.RealNames,
                                LastNames = newPerson.RealLastNames,
                                FullName = newPerson.FullName,
                                BirthDate = newPerson.BirthDate?.Date,
                                Guid = newPerson.Guid,
                            };
                        }
                        return person;
                    }).ConfigureAwait(false);
                return new GetPersonByDocumentNumberResponse(personDetails);
            }).ConfigureAwait(false);
}

