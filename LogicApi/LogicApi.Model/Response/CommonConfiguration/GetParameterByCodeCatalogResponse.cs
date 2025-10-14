using Common.Utils.Extensions;

namespace LogicApi.Model.Response.CommonConfiguration;

/// <summary>
/// Response Obtiene una lista de items de catálogo
/// </summary>
public class GetParameterByCodeResponse
{
    /// <summary>
    /// Valor del parámetro del sistema
    /// </summary>
    /// <value></value>
    private readonly string _originalValue;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="value"></param>
    public GetParameterByCodeResponse(string value) => _originalValue = value;

    /// <summary>
    /// Obtiene el valor en string
    /// </summary>
    /// <value></value>
    public string StringValue { get => _originalValue; }
    
    /// <summary>
    /// Obtiene el valor en int
    /// </summary>
    /// <returns></returns>
    public int IntValue { get => _originalValue.ToInt(); }
}
