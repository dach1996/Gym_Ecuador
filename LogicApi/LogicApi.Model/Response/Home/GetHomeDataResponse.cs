namespace LogicApi.Model.Response.Home;

/// <summary>
/// Respuesta de obtener datos del home/dashboard
/// </summary>
public class GetHomeDataResponse : IApiBaseResponse
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
    /// Datos de medición del dashboard
    /// </summary>
    public DashboardMeasurementData DashboardMeasurementData { get; set; }

    /// <summary>
    /// Plan de alimentación
    /// </summary>
    public MealPlan MealPlan { get; set; }

    /// <summary>
    /// Community articles section
    /// </summary>
    public List<CommunityArticleItem> CommunityArticles { get; set; }

    /// <summary>
    /// Forum section
    /// </summary>
    public List<HomeForumItem> Forums { get; set; }

}

/// <summary>
/// Datos de medición del dashboard
/// </summary>
public class DashboardMeasurementData
{
    /// <summary>
    /// Peso actual
    /// </summary>
    /// <value></value>
    public decimal CurrentWeight { get; set; }

    /// <summary>
    /// Peso objetivo
    /// </summary>
    /// <value></value>
    public decimal GoalWeight { get; set; }

    /// <summary>
    /// Peso anterior
    /// </summary>
    /// <value></value>
    public decimal PreviousWeight { get; set; }

    /// <summary>
    /// Cambio de peso
    /// </summary>
    /// <value></value>
    public decimal CurrentHeight { get; set; }

    /// <summary>
    /// Días de racha (consecutivos)
    /// </summary>
    public int StreakDays { get; set; }

    /// <summary>
    /// Calorías usadas
    /// </summary>
    public int CaloriesUsed { get; set; }

    /// <summary>
    /// Calorías totales
    /// </summary>
    public int TotalCalories { get; set; }

}
/// <summary>
/// Plan de alimentación
/// </summary>
public class MealPlan
{
    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Total de calorías
    /// </summary>
    public int TotalCalories { get; set; }

    /// <summary>
    /// Total de carbohidratos
    /// </summary>
    public int CaloriesUsed { get; set; }

    /// <summary>
    /// Total de proteínas
    /// </summary>
    public int MaxProtein { get; set; }

    /// <summary>
    /// Total de grasas
    /// </summary>
    public int MaxFats { get; set; }

    /// <summary>
    /// Total de carbohidratos
    /// </summary>
    public int MaxCarbohydrates { get; set; }

    /// <summary>
    /// Carbohidratos usados
    /// </summary>
    public int UsedCarbohydrates { get; set; }

    /// <summary>
    /// Proteínas usadas
    /// </summary>
    public int UsedProtein { get; set; }

    /// <summary>
    /// Grasas usadas
    /// </summary>
    public int UsedFats { get; set; }
}

/// <summary>
/// Community article item for home
/// </summary>
public class CommunityArticleItem
{
    /// <summary>
    /// Article GUID
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Article image URL
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Article category
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Article title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Article description
    /// </summary>
    public string Description { get; set; }
}

/// <summary>
/// Forum item for home
/// </summary>
public class HomeForumItem
{
    /// <summary>
    /// Forum GUID
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Forum category
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Forum title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Forum description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Number of comments
    /// </summary>
    public int CommentCount { get; set; }
}
