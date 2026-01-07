namespace Common.PushNotification.Implementations.Indigitall.Model.Request;
/// <summary>
/// Request para Enviar notificación
/// </summary>
public class UpdateCampaignRequest
{
    /// <summary>
    /// Filtros
    /// </summary>
    /// <value></value>
    public Filters Filters { get; set; }

    /// <summary>
    /// Propiedades
    /// </summary>
    /// <value></value>
    public Properties Properties { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="data"></param>
    /// <param name="platforms"></param>
    /// <param name="codeTopics"></param>
    public UpdateCampaignRequest(
        string title,
        string body,
        string data,
        IEnumerable<string> platforms,
        IEnumerable<int> codeTopics = null)
    {
        Properties = new(title, body, data);
        Filters = new(platforms, codeTopics ?? Enumerable.Empty<int>());
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="data"></param>
    /// <param name="platforms"></param>
    /// <param name="codeTopics"></param>
    public UpdateCampaignRequest(
        string title,
        string body,
        string data,
        IEnumerable<string> platforms,
        int codeTopics)
    {
        Properties = new(title, body, data);
        Filters = new(platforms, codeTopics);
    }
}

/// <summary>
/// Acciones
/// </summary>
public class Action
{
    /// <summary>
    /// Tipo
    /// </summary>
    /// <value></value>
    public string Type { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public Action() => Type = "app";
}

/// <summary>
/// Filtros
/// </summary>
public class Filters
{
    /// <summary>
    /// Dipositivos
    /// </summary>
    /// <value></value>
    public List<object> DeviceCodes { get; set; }

    /// <summary>
    /// Conmstructor
    /// </summary>
    public Filters(IEnumerable<string> platforms, IEnumerable<int> codeTopics)
    {
        DeviceCodes = new();
        Platforms = platforms;
        Topics = new(codeTopics);
    }

    /// <summary>
    /// Conmstructor
    /// </summary>
    public Filters(IEnumerable<string> platforms, int codeTopics)
    {
        DeviceCodes = new();
        Platforms = platforms;
        Topics = new(codeTopics);
    }

    /// <summary>
    /// Tópicos
    /// </summary>
    /// <value></value>
    public Topics Topics { get; set; }

    /// <summary>
    /// Plataformas
    /// </summary>
    /// <value></value>
    public IEnumerable<string> Platforms { get; set; }
}

/// <summary>
/// Propiedades
/// </summary>
public class Properties
{

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    public Properties(string title, string body, string data = null)
    {
        Title = title;
        Body = body;
        Action = new();
        Data = data;
    }

    /// <summary>
    /// Tíutlo
    /// </summary>
    /// <value></value>
    public string Title { get; set; }

    /// <summary>
    /// Información Adicional
    /// </summary>
    /// <value></value>
    public string Data { get; set; }

    /// <summary>
    /// Cuerpo
    /// </summary>
    /// <value></value>
    public string Body { get; set; }

    /// <summary>
    /// Acción
    /// </summary>
    /// <value></value>
    public Action Action { get; set; }
}

/// <summary>
/// Tópicos
/// </summary>
public class Topics
{
    /// <summary>
    /// Suscritos
    /// </summary>
    /// <value></value>
    public IEnumerable<IEnumerable<int>> Subscribed { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="subscribed"></param>
    public Topics(IEnumerable<int> subscribed)
    => Subscribed = new List<IEnumerable<int>>
        {
            subscribed
        };

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="subscribed"></param>
    public Topics(int subscribed)
    => Subscribed = new List<IEnumerable<int>>
        {
            new List<int>(){
                subscribed
            }
        };
}