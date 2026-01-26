# Controller Pattern

## Herencia

```
SecurityControllerBase (requiere [Authorize])
    └─ ApiControllerBase (filtros de contexto, token, scope)
        └─ GenericControlerBase (métodos Success(), Mediator)
            └─ ControllerBase
```

## MediatR

Toda lógica de negocio pasa por MediatR para desacoplar controllers:

```csharp
// Correcto
public async Task<IActionResult> GetGyms([FromQuery] GetGymsRequest request)
    => Success(await Mediator.Send(request).ConfigureAwait(false));

// Incorrecto - no llamar servicios directamente
public async Task<IActionResult> GetGyms()
    => Ok(await _gymService.GetGyms());
```

## Reglas

- Heredar de `SecurityControllerBase` para endpoints autenticados
- Usar `Mediator.Send()` para toda lógica de negocio
- Usar `Success()` para respuestas exitosas (aplica envelope automáticamente)
- Usar `.ConfigureAwait(false)` en llamadas async
