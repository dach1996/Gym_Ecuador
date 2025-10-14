namespace Common.Card.Models.Request;
/// <summary>
/// Request para verificar Bin
/// </summary>
public class VerifyBinRequest
{
    /// <summary>
    /// Bin de petición
    /// </summary>
    /// <value></value>
    public string Bin { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bin"></param>
    public VerifyBinRequest(string bin)
    {
        Bin = bin;
    }
}