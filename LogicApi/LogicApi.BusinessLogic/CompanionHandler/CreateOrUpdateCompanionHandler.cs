using LogicApi.Model.Request.Companion;
using PersistenceDb.Models.Core;
namespace LogicApi.BusinessLogic.CompanionHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateOrUpdateCompanionHandler(
    ILogger<CreateOrUpdateCompanionHandler> logger,
    IPluginFactory pluginFactory) : CompanionBase<CreateOrUpdateCompanionRequest, HandlerResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async override Task<HandlerResponse> Handle(CreateOrUpdateCompanionRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.CreateCompanion, request, async () =>
        {
            //Verifica si ya existe un acompañante registrado con ese número de cédula y el usuario registrador
            var personIds = await CoreUnitOfWork.CompanionRepository
                .GetGenericAsync(
                    select => select.PersonId,
                    where => where.UserId == UserId).ConfigureAwait(false);
            //Obtiene los compañeros a Crear
            var companionsToCreate = request.PersonIds.Where(where => !personIds.Exists(any => any == where));
            //Registra el nuevo acompañante
            if (!companionsToCreate.IsNullOrEmpty())
            {
                //Busca los ids de las personas que existan
                var personIdsExist = await AuthenticationUnitOfWork.PersonRepository
                    .GetGenericAsync(
                        select => select.Id,
                        where => companionsToCreate.Contains(where.Id)).ConfigureAwait(false);
                //Obtiene los compañeros que no existen
                var personIdsNotExist = companionsToCreate.Where(where => !personIdsExist.Exists(any => any == where));
                if (!personIdsNotExist.IsNullOrEmpty())
                    throw new CustomException((int)MessagesCodesError.PersonInformationNotFound, $"No existen las personas con Id: '{personIdsNotExist.Join()}'");
                //Crea los compáñeros
                var companionsAdd = personIdsExist.Select(personId => new Companion
                {
                    UserId = UserId,
                    DateTimeRegister = Clock.Now(),
                    PersonId = personId,
                }).ToList();
                await CoreUnitOfWork.CompanionRepository.AddRangeAsync(companionsAdd).ConfigureAwait(false);
            }
            //Response
            return HandlerResponse.Complete();
        }, new[] { UnitOfWorkType.Core, UnitOfWorkType.Authentication });

}