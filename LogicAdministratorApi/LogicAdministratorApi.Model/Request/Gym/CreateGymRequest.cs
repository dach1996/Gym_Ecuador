using LogicAdministratorApi.Model.Response.Gym;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.Gym;

/// <summary>
/// Solicitud para crear un gimnasio
/// </summary>
public class CreateGymRequest : IApiBaseRequest<CreateGymResponse>
{
    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Descripción del gimnasio
    /// </summary>  
    [Required]
    public string Description { get; set; }

    /// <summary>
    /// Descripción corta del gimnasio
    /// </summary>
    public string ShortDescription { get; set; }

    /// <summary>
    /// Teléfono del gimnasio
    /// </summary>
    [Required]
    public string Phone { get; set; }

    /// <summary>
    /// Email del gimnasio
    /// </summary>  
    [Required]
    public string Email { get; set; }

    /// <summary>
    /// Sitio web del gimnasio
    /// </summary>
    public string Website { get; set; }

    /// <summary>
    /// Código del gimnasio
    /// </summary>
    [Required]
    public string Code { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

