using LogicApi.Model.Request.Forum;
using LogicApi.Model.Response.Forum;
using PersistenceDb.Models.Administration;

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
                // Validar que el título no esté vacío
                if (string.IsNullOrWhiteSpace(request.Title))
                    throw new CustomException((int)MessagesCodesError.SystemError, "El título del foro es requerido");

                // Validar que la descripción no esté vacía
                if (string.IsNullOrWhiteSpace(request.Description))
                    throw new CustomException((int)MessagesCodesError.SystemError, "La descripción del foro es requerida");

                // Crear el nuevo foro
                var newForum = new Forum
                {
                    Guid = Guid.NewGuid(),
                    Title = request.Title,
                    Content = request.Description,
                    CreationDate = DateTime.UtcNow,
                    IsActive = true,
                    UserIdRegister = UserId
                };

                // Guardar en la base de datos
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                var forum = await UnitOfWork.ForumRepository.AddAsync(newForum).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new CreateForumResponse(forum.Guid, forum.Title)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}

