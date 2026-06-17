using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Profile;
using Common.WebCommon.Models;
using PersistenceDb.Models.Enums;

namespace LogicApi.Model.Request.Profile;

/// <summary>
/// Solicitud para crear un perfil completo
/// </summary>
public class CreateProfileRequest : IApiBaseRequest<CreateProfileResponse>
{
    /// <summary>
    /// Nombre del perfil
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Tipo de perfil (perder/ganar peso)
    /// </summary>
    public ProfileType Type { get; set; }

    /// <summary>
    /// Id del catálogo de nivel de actividad física
    /// </summary>
    public int PhysicalActivityCatalogId { get; set; }

    /// <summary>
    /// Altura en centímetros
    /// </summary>
    public decimal Height { get; set; }

    /// <summary>
    /// Id del catálogo de ritmo de progreso
    /// </summary>
    public int ProgressRateCatalogId { get; set; }

    /// <summary>
    /// Semanas estimadas del plan
    /// </summary>
    public byte EstimatedWeeks { get; set; }

    /// <summary>
    /// Proteína diaria en gramos
    /// </summary>
    public short Protein { get; set; }

    /// <summary>
    /// Carbohidratos diarios en gramos
    /// </summary>
    public short Carbohydrates { get; set; }

    /// <summary>
    /// Grasas diarias en gramos
    /// </summary>
    public short Fats { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateProfileRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateProfileRequest()
    {
    }
}
