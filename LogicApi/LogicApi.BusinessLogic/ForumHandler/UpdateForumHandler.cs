using LogicApi.Model.Request.Forum;
using LogicApi.Model.Response.Forum;

namespace LogicApi.BusinessLogic.ForumHandler;

/// <summary>
/// Handler para actualizar foro
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateForumHandler(
    ILogger<UpdateForumHandler> logger,
    IPluginFactory pluginFactory) : ForumBase<UpdateForumRequest, UpdateForumResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un foro
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateForumResponse> Handle(UpdateForumRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.UpdateForum, request, async () =>
            {
                // TODO: Implementar cuando se agregue ForumRepository al UnitOfWork
                // Por ahora retornamos el GUID del request
                return new UpdateForumResponse(request.ForumGuid, request.Title ?? string.Empty)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}

