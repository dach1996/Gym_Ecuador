# Handler Structure

## Jerarquía de Herencia

```
{Entity}Handler (CreateGymHandler, GetEquipmentsHandler)
    └─ {Entity}Base<TRequest, TResponse> (GymBase, EquipmentBase)
        └─ BusinessLogicAdministratorBase / BusinessLogicBase
            └─ BusinessLogicCommonBase
```

## Crear un Handler

1. Crear clase base si no existe: `{Entity}Base<TRequest, TResponse>`
2. Crear handler específico heredando de la base
3. Implementar `Handle()` usando `ExecuteHandlerAsync()`

```csharp
public class CreateGymHandler(
    ILogger<CreateGymHandler> logger,
    IPluginFactory pluginFactory)
    : GymBase<CreateGymRequest, CreateGymResponse>(logger, pluginFactory)
{
    public override Task<CreateGymResponse> Handle(
        CreateGymRequest request,
        CancellationToken cancellationToken)
        => ExecuteHandlerAsync(
            OperationAdministratorName.CreateGym,
            request,
            async () =>
            {
                var entity = await UnitOfWork.GymRepository
                    .GetByFirstOrDefaultAsync(where => where.Id == request.Id)
                    .ConfigureAwait(false);

                return new CreateGymResponse(...);
            });
}
```

## Reglas

- Siempre usar `ExecuteHandlerAsync()` para envolver la lógica
- Especificar `OperationName` para auditoría
- Acceder a repositorios vía `UnitOfWork.{Entity}Repository`
- Códigos de catálogo, ítems y parámetros: ver [`catalog-and-parameter-codes.md`](catalog-and-parameter-codes.md) — **nunca** literales inline en handlers

### Async en `Handle()`

- **No** marcar `Handle()` como `async` si solo delega en `ExecuteHandlerAsync`: devolver el `Task` directamente.
- **Sí** usar lambda `async` dentro de `ExecuteHandlerAsync` cuando la lógica contiene `await`.
- Usar `.ConfigureAwait(false)` en cada `await` **dentro** del lambda de `ExecuteHandlerAsync`, no en el `return` externo de `Handle()`.

```csharp
// Correcto
public override Task<MyResponse> Handle(MyRequest request, CancellationToken cancellationToken)
    => ExecuteHandlerAsync(OperationApiName.MyOperation, request, async () =>
    {
        await UnitOfWork.MyRepository.AddAsync(entity).ConfigureAwait(false);
        return new MyResponse();
    });

// Incorrecto — async/await redundante en Handle()
public override async Task<MyResponse> Handle(MyRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(...).ConfigureAwait(false);
```

### Cuándo sí usar `async` en `Handle()`

Solo si hay lógica **fuera** de `ExecuteHandlerAsync` que requiera `await` antes o después de invocarlo.

### Documentación XML

En clases con **primary constructor** (`ILogger<T>`, `IPluginFactory`), el analizador no resuelve `<param name="logger">` ni `<param name="pluginFactory">` en la documentación de la clase.

- **Handlers concretos y `{Entity}Base`**: usar solo `/// <summary>`.
- **No** documentar con `<param>` los parámetros del constructor primario (`logger`, `pluginFactory`); la inyección es estándar en todo el proyecto.
- **Sí** documentar con `<param>` y `<returns>` los métodos públicos (`Handle`, helpers expuestos).

```csharp
/// <summary>
/// Handler para crear gimnasio
/// </summary>
public class CreateGymHandler(
    ILogger<CreateGymHandler> logger,
    IPluginFactory pluginFactory)
    : GymBase<CreateGymRequest, CreateGymResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Crea un gimnasio
    /// </summary>
    /// <param name="request">Datos del gimnasio</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Respuesta con el gimnasio creado</returns>
    public override Task<CreateGymResponse> Handle(...)
        => ExecuteHandlerAsync(...);
}
```
