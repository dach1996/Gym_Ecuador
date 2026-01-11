namespace LogicCommon.Model.CacheModel;
/// <summary>
///     Información del gimnasio en caché
/// </summary>
public class GymCacheInformation
{
    /// <summary>
    ///  Id del gimnasio
    /// </summary>
    /// <value></value>
    public int GymId { get; set; }

    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    /// <value></value>
    public Guid GymGuid { get; set; }
    /// <summary>
    /// Id de la sucursal de gimnasio
    /// </summary>
    /// <value></value>
    public int GymBranchId { get; set; }
    
    /// <summary>
    /// Guid de la sucursal de gimnasio
    /// </summary>
    /// <value></value>
    public Guid GymBranchGuid { get; set; }
}