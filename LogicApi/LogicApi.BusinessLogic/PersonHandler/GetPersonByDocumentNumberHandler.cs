using LogicApi.Model.Request.Person;
using LogicApi.Model.Response.Person;
using LogicCommon.Model.Request.Person;

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
    {
        var commonResponse = await Mediator.Send(new GetPersonByDocumentNumberCommonRequest(request.ContextRequest, request.DocumentNumber), cancellationToken).ConfigureAwait(false);
        return new GetPersonByDocumentNumberResponse(commonResponse.Person);
    }
}

