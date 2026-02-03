using LogicApi.Model.Common;

namespace LogicApi.Model.Response.GymBranch;

/// <summary>
/// Respuesta de obtener detalle de sucursal de gimnasio por GUID
/// </summary>
public class GetGymBranchByGuidResponse(GymBranchDetail gymBranch) : IApiBaseResponse
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
    /// Datos de la sucursal
    /// </summary>
    public GymBranchDetail GymBranch { get; set; } = gymBranch;
}

/// <summary>
/// Detalle completo de sucursal de gimnasio
/// </summary>
public class GymBranchDetail : GymBranchItem
{
    /// <summary>
    /// Descripción de la sucursal
    /// </summary>
    /// <value></value>
    public string Description { get; set; }

    /// <summary>
    /// Horarios de la sucursal
    /// </summary>
    /// <value></value>
    public Dictionary<string, string> Schedules { get; set; }

    /// <summary>
    /// Servicios de la sucursal
    /// </summary>
    /// <value></value>
    public List<GymBranchServiceItem> Services { get; set; }

    /// <summary>
    /// Ubicación de la sucursal
    /// </summary>
    /// <value></value>
    public LatitudeLongitudeModel Location { get; set; }

    /// <summary>
    /// Reseñas de la sucursal
    /// </summary>
    public List<GymBranchReviewItem> Reviews { get; set; }

    /// <summary>
    /// Suscripciones disponibles en la sucursal
    /// </summary>
    public List<GymBranchSubscriptionItem> Subscriptions { get; set; }
}

/// <summary>
/// Item de servicio de la sucursal
/// </summary>
public class GymBranchServiceItem(string name, string iconCode)
{
    /// <summary>
    /// Nombre del servicio
    /// </summary>
    /// <value></value>
    public string Name { get; set; } = name;

    /// <summary>
    /// Código del icono del servicio
    /// </summary>
    /// <value></value>
    public string IconCode { get; set; } = iconCode;
}

/// <summary>
/// Item de reseña de la sucursal
/// </summary>
public class GymBranchReviewItem
{
    /// <summary>
    /// Nombre de la persona
    /// </summary>
    /// <value></value>
    public string PersonName { get; set; }

    /// <summary>
    /// URL de la imagen de la persona
    /// </summary>
    /// <value></value>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Calificación de la reseña
    /// </summary>
    /// <value></value>
    public byte RatingPercentage { get; set; }

    /// <summary>
    /// Comentario de la reseña
    /// </summary>
    /// <value></value>
    public string Comment { get; set; }
}

/// <summary>
/// Item de suscripción de la sucursal
/// </summary>
public class GymBranchSubscriptionItem
{
    /// <summary>
    /// Nombre de la suscripción
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción de la suscripción
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Precio de la suscripción
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Duración en días de la suscripción
    /// </summary>
    public int? DurationDays { get; set; }

    /// <summary>
    /// Características de la suscripción
    /// </summary>
    public List<SubscriptionFeatureItem> Features { get; set; }
}

/// <summary>
/// Item de característica de suscripción
/// </summary>
public class SubscriptionFeatureItem
{
    /// <summary>
    /// Nombre de la característica
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Tipo de característica (Included, Excluded)
    /// </summary>
    public SubscriptionFeatureType Type { get; set; }
}

/// <summary>
/// Tipo de característica de suscripción
/// </summary>
public enum SubscriptionFeatureType
{
    /// <summary>
    /// Característica incluida en la suscripción
    /// </summary>
    Included = 1,

    /// <summary>
    /// Característica excluida de la suscripción
    /// </summary>
    Excluded = 2
}