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
    public override async Task<CreateGymResponse> Handle(
        CreateGymRequest request,
        CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(
            OperationAdministratorName.CreateGym,
            request,
            async () => {
                // Lógica de negocio aquí
                return new CreateGymResponse(...);
            }
        ).ConfigureAwait(false);
}
```

## Reglas

- Siempre usar `ExecuteHandlerAsync()` para envolver la lógica
- Especificar `OperationName` para auditoría
- Usar `.ConfigureAwait(false)` en todas las llamadas async
- Acceder a repositorios vía `UnitOfWork.{Entity}Repository`
