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
                var paginatedResult = await UnitOfWork.ForumRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new ForumItem
                        {
                            Guid = select.Guid,
                            Title = select.Title,
                            Description = select.Content.Length > 200 ? select.Content.Substring(0, 200) + "..." : select.Content,
                            AuthorName = select.Creator != null && select.Creator.Person != null ?
                                $"{select.Creator.Person.RealNames ?? string.Empty} {select.Creator.Person.RealLastNames ?? string.Empty}".Trim() :
                                "Anonymous",
                            CreatedDate = select.CreationDate,
                            CommentCount = select.ForumComments != null ? select.ForumComments.Count(c => c.IsActive) : 0,
                            IsActive = select.IsActive
                        },
                        where: forum => forum.IsActive,
                        orderBy: forum => forum.Title,
                        orderByType: PersistenceDb.Models.Enums.OrderByType.Desc
                    ).ConfigureAwait(false);

                return new GetForumsResponse
                (
                    totalRegister: paginatedResult.TotalItems,
                    registers: paginatedResult.Items
                )
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

