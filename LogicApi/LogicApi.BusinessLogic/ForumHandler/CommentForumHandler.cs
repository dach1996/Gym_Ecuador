using LogicApi.Model.Request.Forum;
using LogicApi.Model.Response.Forum;

namespace LogicApi.BusinessLogic.ForumHandler;

/// <summary>
/// Handler para comentar en foro
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CommentForumHandler(
    ILogger<CommentForumHandler> logger,
    IPluginFactory pluginFactory) : ForumBase<CommentForumRequest, CommentForumResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un comentario en un foro
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CommentForumResponse> Handle(CommentForumRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.CommentForum, request, async () =>
            {
                // TODO: Implementar cuando se agregue ForumRepository y ForumCommentRepository al UnitOfWork
                // Por ahora retornamos un GUID temporal
                var commentGuid = Guid.NewGuid();

                return new CommentForumResponse(commentGuid, request.Comment)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}

