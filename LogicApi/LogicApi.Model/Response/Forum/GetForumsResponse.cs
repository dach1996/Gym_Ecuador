namespace LogicApi.Model.Response.Forum;

/// <summary>
/// Respuesta de obtener foros
/// </summary>
public class GetForumsResponse : IPaginatorApiResponse<ForumItem>
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
    public GetForumsResponse(int totalRegister, IEnumerable<ForumItem> registers)
    {
        TotalRegister = totalRegister;
        Registers = registers;
    }

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<ForumItem> Registers { get; set; }
}

/// <summary>
/// Item de foro
/// </summary>
public class ForumItem
{
    /// <summary>
    /// Guid del foro
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Título del foro
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Descripción del foro
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Nombre del autor
    /// </summary>
    public string AuthorName { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Cantidad de comentarios
    /// </summary>
    public int CommentCount { get; set; }

    /// <summary>
    /// Indica si está activo
    /// </summary>
    public bool IsActive { get; set; }
}

