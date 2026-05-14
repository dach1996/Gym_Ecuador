using LogicApi.Model.Request.Forum;
using LogicApi.Model.Response.Forum;

namespace LogicApi.BusinessLogic.ForumHandler;

/// <summary>
/// Handler to get forum detail by GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetForumByGuidHandler(
    ILogger<GetForumByGuidHandler> logger,
    IPluginFactory pluginFactory) : ForumBase<GetForumByGuidRequest, GetForumByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Handles getting forum detail by GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetForumByGuidResponse> Handle(GetForumByGuidRequest request, CancellationToken cancellationToken)
            => await ExecuteHandlerAsync(OperationApiName.GetForumByGuid, request, async () =>
            {
                var forumEntity = await UnitOfWork.ForumRepository
                    .GetByFirstOrDefaultAsync(
                        forum => forum.Guid == request.ForumGuid && forum.IsActive,
                        forum => forum.User,
                        forum => forum.ForumComments
                    ).ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Forum not found");

                var forum = new ForumDetail
                {
                    Guid = forumEntity.Guid,
                    Title = forumEntity.Title,
                    Description = forumEntity.Content.Length > 200 ? forumEntity.Content.Substring(0, 200) + "..." : forumEntity.Content,
                    FullContent = forumEntity.Content,
                    Category = "General", // Default category, can be enhanced later
                    AuthorName = forumEntity.User != null && forumEntity.User.Person != null ? 
                        $"{forumEntity.User.Person.RealNames ?? string.Empty} {forumEntity.User.Person.RealLastNames ?? string.Empty}".Trim() : 
                        "Anonymous",
                    CreatedDate = forumEntity.CreationDate,
                    CreationDate = forumEntity.CreationDate,
                    CommentCount = forumEntity.ForumComments != null ? forumEntity.ForumComments.Count(c => c.IsActive) : 0,
                    IsActive = forumEntity.IsActive,
                    Comments = forumEntity.ForumComments != null && forumEntity.ForumComments.Any() ?
                        forumEntity.ForumComments
                            .Where(c => c.IsActive)
                            .OrderBy(c => c.DateTimeRegister)
                            .Select(c => new ForumCommentItem
                            {
                                Guid = c.Guid,
                                Comment = c.Comment,
                                AuthorName = c.User != null && c.User.Person != null ?
                                    $"{c.User.Person.RealNames ?? string.Empty} {c.User.Person.RealLastNames ?? string.Empty}".Trim() :
                                    "Anonymous",
                                RegistrationDate = c.DateTimeRegister
                            }).ToList() :
                        new List<ForumCommentItem>()
                };

                return new GetForumByGuidResponse(forum);
            }).ConfigureAwait(false);
}

