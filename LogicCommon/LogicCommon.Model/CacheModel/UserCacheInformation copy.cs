namespace LogicCommon.Model.CacheModel;

/// <summary>
///     Información del gimnasio en caché
/// </summary>
public class GymCacheInformation(GymCacheItem[] gyms)
{
    /// <summary>
    ///  Información de los gimnasios
    /// </summary>
    /// <value></value>
    public GymCacheItem[] Gyms { get; set; } = gyms;
    /// <summary>
    /// Obtiene el Id del gimnasio por el Guid
    /// </summary>
    /// <param name="gymGuid"></param>
    /// <returns></returns>
    public int GetGymIdByGuid(Guid gymGuid)
        => Gyms.FirstOrDefault(first => first.Guid == gymGuid)?.Id ?? throw new ArgumentException($"No se encontró el gimnasio con el Guid: {gymGuid}", nameof(gymGuid));

}



/// <summary>
///     Información del gimnasio en caché
/// </summary>
/// <param name="Id">  Id del gimnasio </param>
/// <param name="Guid"> Guid del gimnasio </param>
/// <param name="Name"> Nombre del gimnasio </param>
/// <param name="Branches"> Lista de sucursales de gimnasio </param>
/// <remarks>
/// Constructor de la clase GymCacheItem
/// </remarks>
public record GymCacheItem(int Id, Guid Guid, string Name, GymBranchCacheItem[] Branches);


/// <summary>
/// Constructor de la clase GymBranchCacheItem
/// </summary>
/// <param name="Id"> Id de la sucursal de gimnasio </param>
/// <param name="Guid"> Guid de la sucursal de gimnasio </param>
/// <param name="Name"> Nombre de la sucursal de gimnasio </param>
public record GymBranchCacheItem(int Id, Guid Guid, string Name);
