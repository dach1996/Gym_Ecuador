using Newtonsoft.Json;

namespace Common.UserDocumentation.Implementation.BusPlace.Models.Response;

/// <summary>
/// Respuesta interna de BusPlace
/// </summary>
public class InternalBusPlaceResponse
{
    /// <summary>
    /// Estado de la respuesta
    /// </summary>
    /// <value></value>
    [JsonProperty("estado")]
    public int Status { get; set; }

    /// <summary>
    /// Información del referente
    /// </summary>
    /// <value></value>
    [JsonProperty("treferente")]
    public Referent Referent { get; set; }

    /// <summary>
    /// Indica si existe el referente
    /// </summary>
    /// <value></value>
    [JsonProperty("existe")]
    public bool Exists { get; set; }
}

/// <summary>
/// Información del referente de BusPlace
/// </summary>
public class Referent
{
    /// <summary>
    /// Código del referente
    /// </summary>
    /// <value></value>
    [JsonProperty("ref_codigo")]
    public long ReferentCode { get; set; }

    /// <summary>
    /// Cédula del referente
    /// </summary>
    /// <value></value>
    [JsonProperty("ref_cedula")]
    public string ReferentIdentification { get; set; }

    /// <summary>
    /// Nombre completo del referente
    /// </summary>
    /// <value></value>
    [JsonProperty("ref_nombre")]
    public string FullName { get; set; }

    /// <summary>
    /// Fecha de nacimiento del referente
    /// </summary>
    /// <value></value>
    [JsonProperty("ref_fecnac")]
    public string BirthDate { get; set; }
}

