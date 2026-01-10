using LogicApi.Model.Request.Forum;
using LogicApi.Model.Response.Forum;
using PersistenceDb.Models.Administration;

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
                // Buscar el foro por GUID
                var forum = await UnitOfWork.ForumRepository
                    .GetByFirstOrDefaultAsync(
                        where: f => f.Guid == request.ForumGuid
                    ).ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Foro no encontrado");

                // Validar que el usuario sea el creador del foro
                if (forum.UserIdRegister != UserId)
                    throw new CustomException((int)MessagesCodesError.SystemError, "No tienes permisos para actualizar este foro");

                // Actualizar los campos si se proporcionan
                if (!string.IsNullOrWhiteSpace(request.Title))
                    forum.Title = request.Title;

                if (!string.IsNullOrWhiteSpace(request.Description))
                    forum.Content = request.Description;

                if (request.IsActive.HasValue)
                    forum.IsActive = request.IsActive.Value;

                // Guardar los cambios
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await UnitOfWork.ForumRepository.UpdateAsync(forum).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new UpdateForumResponse(forum.Guid, forum.Title)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}

