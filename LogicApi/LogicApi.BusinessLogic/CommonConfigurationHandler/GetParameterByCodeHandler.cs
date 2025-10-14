using LogicApi.Model.Request.CommonConfiguration;
using LogicApi.Model.Response.CommonConfiguration;

namespace LogicApi.BusinessLogic.CommonConfigurationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetParameterByCodeHandler(
    ILogger<GetParameterByCodeHandler> logger,
    IPluginFactory pluginFactory) : CommonConfigurationBase<GetParameterByCodeRequest, GetParameterByCodeResponse>(
        logger,
        pluginFactory)
{
    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public override async Task<GetParameterByCodeResponse> Handle(GetParameterByCodeRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.GetParameterByCode, request, async () =>
        {
            //Guarda en cache si consigue el valor
            var parameterValue = await AdministratorCache.TryGetOrSetAsync($"{CacheCodes.PARAMETER}-{request.ParameterCode}",
                 async () =>
                 {
                     //buscamos el parámetro
                     var parameter = (await AdministrationUnitOfWork.ParameterRepository
                          .GetFirstOrDefaultGenericAsync(
                              select => new
                              {
                                  select.Status,
                                  select.Value
                              },
                              where => where.Code == request.ParameterCode
                         ).ConfigureAwait(false))
                         ?? throw new CustomException((int)MessagesCodesError.ParameterNotFound, $"El parámetro: '{request.ParameterCode}' se encontró");
                     //Verifica si el parámetro está habilitado
                     if (!parameter.Status)
                         throw new CustomException((int)MessagesCodesError.ParameterNotFound, $"El parámetro: '{request.ParameterCode}' se encuentra deshabilitado");
                     return parameter.Value;
                 }).ConfigureAwait(false);
            // retorna el valor
            return new GetParameterByCodeResponse(parameterValue);
        }, UnitOfWorkType.Administration);
}
