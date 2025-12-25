using LogicApi.Model.Request.Article;
using LogicApi.Model.Response.Article;

namespace LogicApi.BusinessLogic.ArticleHandler;

/// <summary>
/// Handler para obtener artículos
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetArticlesHandler(
    ILogger<GetArticlesHandler> logger,
    IPluginFactory pluginFactory) : ArticleBase<GetArticlesRequest, GetArticlesResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de artículos con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetArticlesResponse> Handle(GetArticlesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetArticles, request, async () =>
            {
                // TODO: Implementar cuando se agregue ArticleRepository al UnitOfWork
                // Por ahora retornamos datos vacíos
                var articles = new List<ArticleItem>();

                return new GetArticlesResponse
                (
                    totalRegister: 0,
                    registers: articles
                )
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

