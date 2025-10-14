namespace Common.WebCommon.Models;
public interface IPaginatorResponse<T>
{
    /// <summary>
    /// Total de Registros
    /// </summary>
    /// <value></value>
    int TotalRegister { get; set; }

    /// <summary>
    /// Companias
    /// </summary>
    /// <value></value>
    IEnumerable<T> Registers { get; set; }
}


