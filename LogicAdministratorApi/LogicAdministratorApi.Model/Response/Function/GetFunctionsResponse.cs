namespace LogicAdministratorApi.Model.Response.Function;

/// <summary>
/// Respuesta de obtener todas las funciones
/// </summary>
public class GetFunctionsResponse : IApiBaseResponse
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
    /// Lista de funciones
    /// </summary>
    public List<FunctionItem> Functions { get; set; } = new();
}

/// <summary>
/// Item de función
/// </summary>
public class FunctionItem
{
    /// <summary>
    /// ID de la función
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Código de la función
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Nombre de la función
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción de la función
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Nombre del módulo
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// Estado de la función (activa/inactiva)
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Ruta de la función
    /// </summary>
    public string Route { get; set; }

    /// <summary>
    /// Icono de la función
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// Orden de la función
    /// </summary>
    public byte Order { get; set; }

    /// <summary>
    /// Visibilidad de la función
    /// </summary>
    public bool IsVisible { get; set; }
}
