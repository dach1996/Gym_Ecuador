namespace LogicApi.Model.Response.Article;

/// <summary>
/// Respuesta de obtener artículos
/// </summary>
public class GetArticlesResponse : IPaginatorApiResponse<ArticleItem>
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Total de registros
    /// </summary>
    public int TotalRegister { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="totalRegister"></param>
    /// <param name="registers"></param>
    public GetArticlesResponse(int totalRegister, IEnumerable<ArticleItem> registers)
    {
        TotalRegister = totalRegister;
        Registers = registers;
    }

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<ArticleItem> Registers { get; set; }
}

/// <summary>
/// Item de artículo
/// </summary>
public class ArticleItem
{
    /// <summary>
    /// Guid del artículo
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Título del artículo
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Descripción o resumen del artículo
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Contenido del artículo
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Nombre del autor
    /// </summary>
    public string AuthorName { get; set; }

    /// <summary>
    /// Categoría del artículo
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// URL de la imagen principal
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Fecha de publicación
    /// </summary>
    public DateTime PublishedDate { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Cantidad de vistas
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Cantidad de likes
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Indica si está activo
    /// </summary>
    public bool IsActive { get; set; }
}

