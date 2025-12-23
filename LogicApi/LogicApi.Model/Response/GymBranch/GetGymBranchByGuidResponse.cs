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