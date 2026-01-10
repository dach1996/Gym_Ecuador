using LogicApi.Model.Request.Forum;
using LogicApi.Model.Response.Forum;
using PersistenceDb.Models.Administration;

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
                // Validar que el comentario no esté vacío
                if (string.IsNullOrWhiteSpace(request.Comment))
                    throw new CustomException((int)MessagesCodesError.SystemError, "El comentario es requerido");

                // Buscar el foro por GUID
                var forum = await UnitOfWork.ForumRepository
                    .GetByFirstOrDefaultAsync(
                        where: f => f.Guid == request.ForumGuid && f.IsActive
                    ).ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Foro no encontrado o inactivo");

                // Crear el nuevo comentario
                var newComment = new ForumComment
                {
                    Guid = Guid.NewGuid(),
                    ForumId = forum.Id,
                    Comment = request.Comment,
                    IsActive = true,
                    UserIdRegister = UserId,
                    DateTimeRegister = DateTime.UtcNow
                };

                // Guardar en la base de datos
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                var comment = await UnitOfWork.ForumCommentRepository.AddAsync(newComment).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new CommentForumResponse(comment.Guid, comment.Comment)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}

