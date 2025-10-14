using Common.WebCommon.Models.Enum;

namespace Common.WebCommon.Models;
/// <summary>
/// Interface para ordenamiento
/// </summary>
public interface ISorteableRequest
{
    /// <summary>
    /// Ordenamiento
    /// </summary>
    /// <value></value>
    SortableType SortableType { get; set; }
}
