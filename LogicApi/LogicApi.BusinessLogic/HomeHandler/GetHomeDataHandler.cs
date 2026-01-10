using LogicApi.Model.Request.Home;
using LogicApi.Model.Response.Home;

namespace LogicApi.BusinessLogic.HomeHandler;

/// <summary>
/// Handler para obtener datos del home/dashboard
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetHomeDataHandler(
    ILogger<GetHomeDataHandler> logger,
    IPluginFactory pluginFactory) : HomeBase<GetHomeDataRequest, GetHomeDataResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de datos del home/dashboard
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetHomeDataResponse> Handle(GetHomeDataRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetHomeData, request, async () =>
            {
                // Get latest community articles (limit 5)
                var articles = await UnitOfWork.ArticleRepository
                    .GetByAsync(
                        where: article => article.IsActive && article.PublicationDate <= DateTime.UtcNow && article.MaximumPublicationDate >= DateTime.UtcNow,
                        orderBy: article => article.PublicationDate,
                        orderByType: PersistenceDb.Models.Enums.OrderByType.Desc,
                        top: 5
                    ).ConfigureAwait(false);

                var communityArticles = articles.Select(article => new CommunityArticleItem
                {
                    Guid = article.Guid,
                    ImageUrl = article.ImageUrl ?? string.Empty,
                    Category = "Fitness", // Default category, can be enhanced later
                    Title = article.Title,
                    Description = article.Summary ?? string.Empty
                }).ToList();

                // Get latest forums with comment count (limit 5)
                var forumsData = await UnitOfWork.ForumRepository
                    .GetByAsync(
                        where: forum => forum.IsActive,
                        orderBy: forum => forum.CreationDate,
                        orderByType: PersistenceDb.Models.Enums.OrderByType.Desc,
                        top: 5,
                        includes: forum => forum.ForumComments
                    ).ConfigureAwait(false);

                var forums = forumsData.Select(forum => new ForumItem
                {
                    Guid = forum.Guid,
                    Category = "General", // Default category, can be enhanced later
                    Title = forum.Title,
                    Description = forum.Content.Length > 200 ? forum.Content.Substring(0, 200) + "..." : forum.Content,
                    CommentCount = forum.ForumComments != null ? forum.ForumComments.Count(c => c.IsActive) : 0
                }).ToList();

                return new GetHomeDataResponse
                {
                    DashboardMeasurementData = new DashboardMeasurementData
                    {
                        CurrentWeight = 0,
                        GoalWeight = 0,
                        PreviousWeight = 0,
                    },
                    MealPlan = new MealPlan
                    {
                        Name = "Plan de alimentación",
                        TotalCalories = 0,
                        MaxCarbohydrates = 0,
                        MaxProtein = 0,
                        MaxFats = 0,
                        UsedCarbohydrates = 0,
                        UsedProtein = 0,
                        UsedFats = 0,
                    },
                    CommunityArticles = communityArticles.ToList(),
                    Forums = forums.ToList()
                };
            }
        ).ConfigureAwait(false);
}

