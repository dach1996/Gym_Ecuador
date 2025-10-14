using Common.Utils.Extensions;

namespace Common.WebCommon.TaskOperation;
public class MultipleTask
{
    private readonly IDictionary<string, Task> _dictionaryTasks;

    /// <summary>
    /// Iniciador Estático
    /// </summary>
    /// <param name="dictionaryTasks"></param>
    /// <returns></returns>
    public static MultipleTask Init(IDictionary<string, Task> dictionaryTasks)
        => new(dictionaryTasks);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dictionaryTasks"></param>
    private MultipleTask(IDictionary<string, Task> dictionaryTasks)
    {
        _dictionaryTasks = dictionaryTasks;
    }

    /// <summary>
    /// Espera a todas las tareas
    /// </summary>
    /// <returns></returns>
    public async Task WaitAllTask()
    {
        var tasks = _dictionaryTasks.Select(t => t.Value);
        if (tasks.Any())
            await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    /// <summary>
    /// Obtiene el Resultado
    /// </summary>
    /// <param name="identifierTask"></param>
    /// <typeparam name="TTaskResult"></typeparam>
    /// <returns></returns>
    public TTaskResult Get<TTaskResult>(string identifierTask)
    {
        var task = _dictionaryTasks.FirstOrDefault(t => t.Key == identifierTask).Value
            ?? throw new ArgumentException($"No se encuentra la tarea con Identificación: '{identifierTask}'", nameof(identifierTask));
        return ((Task<TTaskResult>)task).Result;
    }

    /// <summary>
    /// Obtiene el Resultado con el nombre del tipo 
    /// </summary>
    /// <typeparam name="TTaskResult"></typeparam>
    /// <returns></returns>
    public TTaskResult Get<TTaskResult>()
        => Get<TTaskResult>(typeof(TTaskResult).Name);

    /// <summary>
    /// Intenta obtener el Resultado si existe
    /// </summary>
    /// <param name="identifierTask"></param>
    /// <param name="result"></param>
    /// <typeparam name="TTaskResult"></typeparam>
    /// <returns></returns>
    public bool TryGet<TTaskResult>(string identifierTask, out TTaskResult result)
    {
        var keyValueTask = _dictionaryTasks.FirstOrNull(t => t.Key == identifierTask);
        result = keyValueTask is not null ? ((Task<TTaskResult>)keyValueTask.Value.Value).Result : default;
        return result is not null;
    }

    /// <summary>
    /// Intenta obtener el Resultado si existe con el nombre del tipo
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="TTaskResult"></typeparam>
    /// <returns></returns>
    public bool TryGet<TTaskResult>(out TTaskResult result)
        => TryGet(typeof(TTaskResult).Name, out result);

}