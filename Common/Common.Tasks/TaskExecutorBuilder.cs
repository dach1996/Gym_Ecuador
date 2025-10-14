using Microsoft.Extensions.Logging;
namespace Common.Tasks;
/// <summary>
/// Constructor de Ejecución de tarea
/// </summary>
public class TaskExecutorBuilder
{
    private readonly ILoggerFactory _loggerFactory;
    private List<object> _constructorParams = new();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="loggerFactory"></param>
    public TaskExecutorBuilder(ILoggerFactory loggerFactory)
        => _loggerFactory = loggerFactory;

    /// <summary>
    /// Con Log
    /// </summary>
    /// <typeparam name="TU"></typeparam>
    /// <returns></returns>
    public TaskExecutorBuilder WithLogger<TU>() where TU : ITaskExecutor
    {
        _constructorParams.Add(_loggerFactory.CreateLogger<TU>());
        return this;
    }
    /// <summary>
    /// Agrega parámetros
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public TaskExecutorBuilder AddConstructorParam(params object[] @params)
    {
        foreach (var param in @params)
            _constructorParams.Add(@param);
        return this;
    }

    /// <summary>
    /// Ejecutar 
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    public void Execute<T>(IExecutorModel model) where T : ITaskExecutor
    {
        object[] constructorParams = _constructorParams.ToArray();
        if (Activator.CreateInstance(typeof(T), constructorParams) is ITaskExecutor taskExecutorInstance)
            _ = Task.Factory.StartNew(async () => await taskExecutorInstance.Execute(model).ConfigureAwait(false));
        _constructorParams = new List<object>();
    }
}