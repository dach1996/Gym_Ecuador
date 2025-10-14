using Common.WebCommon.Models;

namespace Common.WebJob.Model;

/// <summary>
/// Contexto de auditoría del request
/// </summary>
public class WebJobContext : CommonContextRequest
{
    /// <summary>
    /// Identificador Externo
    /// </summary>
    public Guid InternalIdentifier { get; set; }
}
