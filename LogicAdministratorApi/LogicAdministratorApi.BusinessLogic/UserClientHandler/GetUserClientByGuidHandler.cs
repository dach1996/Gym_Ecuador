using LogicAdministratorApi.Model.Request.UserClient;
using LogicAdministratorApi.Model.Response.UserClient;

namespace LogicAdministratorApi.BusinessLogic.UserClientHandler;

/// <summary>
/// Handler para obtener detalle de usuario cliente por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetUserClientByGuidHandler(
    ILogger<GetUserClientByGuidHandler> logger,
    IPluginFactory pluginFactory) : UserClientBase<GetUserClientByGuidRequest, GetUserClientByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de detalle de un usuario cliente por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetUserClientByGuidResponse> Handle(GetUserClientByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetUserClientByGuid, request, async () =>
            {
                // Buscar el cliente por GUID usando GetFirstOrDefaultGenericAsync con proyección directa
                var client = await UnitOfWork.PersonRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new PersonClientDetail
                        {
                            Guid = select.Guid,
                            RegistrationDate = select.DateTimeRegister,
                        },
                        where => where.Guid == request.ClientGuid
                    ).ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Cliente no encontrado");

                return new GetUserClientByGuidResponse(client)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}
