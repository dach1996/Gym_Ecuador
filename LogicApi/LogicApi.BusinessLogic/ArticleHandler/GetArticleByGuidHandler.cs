using LogicApi.Model.Request.Article;
using LogicApi.Model.Response.Article;

namespace LogicApi.BusinessLogic.ArticleHandler;

/// <summary>
/// Handler to get article detail by GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetArticleByGuidHandler(
    ILogger<GetArticleByGuidHandler> logger,
    IPluginFactory pluginFactory) : ArticleBase<GetArticleByGuidRequest, GetArticleByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Handles getting article detail by GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetArticleByGuidResponse> Handle(GetArticleByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetArticleByGuid, request, async () =>
            {
                var article = await UnitOfWork.ArticleRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new ArticleDetail
                        {
                            Guid = select.Guid,
                            Title = select.Title,
                            Description = select.Summary ?? string.Empty,
                            Content = select.Content,
                            FullContent = select.Content,
                            AuthorName = "Admin", // Can be enhanced with author relationship
                            Category = "Fitness", // Default category, can be enhanced later
                            ImageUrl = select.ImageUrl ?? string.Empty,
                            PublishedDate = select.PublicationDate,
                            PublicationDate = select.PublicationDate,
                            CreationDate = select.CreationDate
                        },
                        where => where.Guid == request.ArticleGuid && where.IsActive && 
                                 where.PublicationDate <= DateTime.UtcNow && 
                                 where.MaximumPublicationDate >= DateTime.UtcNow
                    ).ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Article not found");

                return new GetArticleByGuidResponse(article);
            }).ConfigureAwait(false);
}

