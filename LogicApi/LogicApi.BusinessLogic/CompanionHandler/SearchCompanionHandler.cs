using LogicApi.Model.Request.Companion;
using LogicApi.Model.Response.Companion;
using PersistenceDb.Models.Authentication;

namespace LogicApi.BusinessLogic.CompanionHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class SearchCompanionHandler(
    ILogger<SearchCompanionHandler> logger,
    IPluginFactory pluginFactory) : CompanionBase<SearchCompanionRequest, SearchCompanionResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async override Task<SearchCompanionResponse> Handle(SearchCompanionRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.SearchCompanion, request, async () =>
        {
            //Obtiene el usuario por número de cédula
            var personInformation = await AuthenticationUnitOfWork.PersonRepository.GetFirstOrDefaultGenericAsync(
                 select => new PersonInformation(
                    select.Guid,
                    select.DocumentNumber,
                    select.FullName
                 ),
                 where => where.DocumentNumber == request.DocumentNumber
             ).ConfigureAwait(false);
            //Si la persona no existe en la base buscamos en el servicio.
            if (personInformation is null)
            {
                //Verificar el documento
                var documentInformation = await GetPersonInformationAsync(request.DocumentNumber).ConfigureAwait(false);
                //Agrega la persona
                var newPerson = await AuthenticationUnitOfWork.PersonRepository.AddAsync(new Person
                {
                    DocumentNumber = request.DocumentNumber,
                    RealNames = documentInformation.Names,
                    RealLastNames = documentInformation.LastNames,
                    FullName = documentInformation.FullName,
                }).ConfigureAwait(false);
                personInformation = new PersonInformation(newPerson.Guid, newPerson.DocumentNumber, newPerson.FullName);
            }
            //Retorna la respeusta en caso de encontrarla 
            return new SearchCompanionResponse(personInformation);
        }, UnitOfWorkType.Authentication);
}