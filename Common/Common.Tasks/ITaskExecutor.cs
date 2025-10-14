
namespace Common.Tasks;
/// <summary>
/// Interfáz para ejecutador
/// </summary>
public interface ITaskExecutor
{
    /// <summary>
    /// Ejecutor
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task Execute(IExecutorModel model);
}