namespace LogicAdministratorApi.Model.Response.Functionality;

/// <summary>
/// Respuesta de obtener todas las funcionalidades
/// </summary>
public class GetFunctionalitiesResponse : IApiBaseResponse
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
    /// Lista de funcionalidades
    /// </summary>
    public List<FunctionalityItem> Functionalities { get; set; } = new();
}

/// <summary>
/// Item de funcionalidad
/// </summary>
public class FunctionalityItem
{
    /// <summary>
    /// Guid de la funcionalidad
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nombre de la funcionalidad
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Código de la funcionalidad
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Descripción de la funcionalidad
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Nombre de la función
    /// </summary>
    public string FunctionName { get; set; }

    /// <summary>
    /// Nombre del módulo
    /// </summary>
    public string ModuleName { get; set; }
}
