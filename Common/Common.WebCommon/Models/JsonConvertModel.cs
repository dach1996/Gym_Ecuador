
using Newtonsoft.Json;

namespace Common.WebCommon.Models;

/// <summary>
/// Clase Abstracta que devuelve 
/// </summary>
public abstract class JsonConvertModel
{
    /// <summary>
    /// Clase Abstracta que permite obtner la información Serializada
    /// </summary>
    /// <returns></returns>
    [JsonIgnore]
    public string Json => JsonConvert.SerializeObject(this);
}