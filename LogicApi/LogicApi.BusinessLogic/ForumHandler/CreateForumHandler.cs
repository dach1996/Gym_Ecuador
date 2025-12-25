using LogicApi.Model.Request.Forum;
using LogicApi.Model.Response.Forum;

namespace LogicApi.BusinessLogic.ForumHandler;

/// <summary>
/// Handler para crear foro
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateForumHandler(
    ILogger<CreateForumHandler> logger,
    IPluginFactory pluginFactory) : ForumBase<CreateForumRequest, CreateForumResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un foro
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateForumResponse> Handle(CreateForumRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.CreateForum, request, async () =>
            {
                // TODO: Implementar cuando se agregue ForumRepository al UnitOfWork
                // Por ahora retornamos un GUID temporal
                var forumGuid = Guid.NewGuid();

                return new CreateForumResponse(forumGuid, request.Title)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}

