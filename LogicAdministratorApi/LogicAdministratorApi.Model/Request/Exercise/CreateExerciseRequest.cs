using LogicAdministratorApi.Model.Response.Exercise;
using Common.WebCommon.Models;
using LogicCommon.Model.Request.File;

namespace LogicAdministratorApi.Model.Request.Exercise;

/// <summary>
/// Solicitud para crear un ejercicio
/// </summary>
public class CreateExerciseRequest : IApiBaseRequest<CreateExerciseResponse>
{
    /// <summary>
    /// Nombre del ejercicio
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// Descripción del ejercicio
    /// </summary>
    [StringLength(1000)]
    public string Description { get; set; }

    /// <summary>
    /// Instrucciones del ejercicio
    /// </summary>
    [StringLength(2000)]
    public string Instructions { get; set; }

    /// <summary>
    /// Imagen del ejercicio (opcional)
    /// </summary>
    public RequestEncodeFile Image { get; set; }

    /// <summary>
    /// Lista de códigos de catálogo (tags/categorías) del ejercicio (opcional)
    /// </summary>
    public List<string> TagCatalogCodes { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
