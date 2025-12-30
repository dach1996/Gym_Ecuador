using LogicApi.Model.Request.Person;
using LogicApi.Model.Response.Person;
using PersistenceDb.Models.Authentication;

namespace LogicApi.BusinessLogic.PersonHandler;

/// <summary>
/// Handler para obtener persona por número de cédula
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetPersonByDocumentNumberHandler(
    ILogger<GetPersonByDocumentNumberHandler> logger,
    IPluginFactory pluginFactory)
    : PersonBase<GetPersonByDocumentNumberRequest, GetPersonByDocumentNumberResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de una persona por su número de cédula
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetPersonByDocumentNumberResponse> Handle(GetPersonByDocumentNumberRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerCacheAsync(OperationApiName.GetPersonByDocumentNumber, CacheCodes.PersonByDocumentNumber(request.DocumentNumber), request, async () =>
            {
                var person = await UnitOfWork.PersonRepository.GetFirstOrDefaultGenericAsync(
                    select => new PersonDetail
                    {
                        DocumentNumber = select.DocumentNumber,
                        Names = select.RealNames,
                        LastNames = select.RealLastNames,
                        FullName = select.FullName,
                    },
                    where => where.DocumentNumber == request.DocumentNumber).ConfigureAwait(false);
                //Si la persona no existe en la base de datos, se busca en el servicio de documentación
                if (person is null)
                {
                    var personInformation = await DocumentationServices.GetPersonInformationAsync(new(request.DocumentNumber)).ConfigureAwait(false)
                                    ?? throw new CustomException((int)MessagesCodesError.PersonInformationNotFound, $"No se pudo encontrar información del documento: '{request.DocumentNumber}'");

                    var newPerson = await UnitOfWork.PersonRepository.AddAsync(new Person
                    {
                        DocumentNumber = request.DocumentNumber,
                        RealNames = personInformation.Names,
                        RealLastNames = personInformation.LastNames,
                        FullName = personInformation.FullName,
                        BirthDate = personInformation.BirthDate?.Date,
                    }).ConfigureAwait(false);
                    person = new PersonDetail
                    {
                        DocumentNumber = newPerson.DocumentNumber,
                        Names = newPerson.RealNames,
                        LastNames = newPerson.RealLastNames,
                        FullName = newPerson.FullName,
                        BirthDate = newPerson.BirthDate?.Date,
                    };
                }
                return new GetPersonByDocumentNumberResponse(person);
            }).ConfigureAwait(false);
}

