namespace Common.WebCommon.Models;
public interface IPaginatorRequest 
{
    /// Límite
    /// </summary>
    /// <value></value>
    int PageSize { get; set; }

    /// <summary>
    /// Saltar
    /// </summary>
    /// <value></value>
    int PageNumber { get; set; }
}
