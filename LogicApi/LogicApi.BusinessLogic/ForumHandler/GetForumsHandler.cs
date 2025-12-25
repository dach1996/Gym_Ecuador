using LogicApi.Model.Request.Forum;
using LogicApi.Model.Response.Forum;

namespace LogicApi.BusinessLogic.ForumHandler;

/// <summary>
/// Handler para obtener foros
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetForumsHandler(
    ILogger<GetForumsHandler> logger,
    IPluginFactory pluginFactory) : ForumBase<GetForumsRequest, GetForumsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de foros con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetForumsResponse> Handle(GetForumsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetForums, request, async () =>
            {
                // TODO: Implementar cuando se agregue ForumRepository al UnitOfWork
                // Por ahora retornamos datos vacíos
                var forums = new List<ForumItem>();

                return new GetForumsResponse
                (
                    totalRegister: 0,
                    registers: forums
                )
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

