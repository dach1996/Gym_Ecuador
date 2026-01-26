# Error Handling

## CustomException

Usar `CustomException` para errores de negocio controlados:

```csharp
throw new CustomException(
    (int)MessagesCodesError.GymBranchNotFound,
    "No se encontró la sucursal especificada");
```

## Códigos de Error

Definidos en `Common.Messages.MessagesCodesError`:

```csharp
public enum MessagesCodesError
{
    SystemError = 1,
    GymBranchNotFound = ...,
    InfoUserNotFound = ...,
    // ...
}
```

## Reglas

- Usar `CustomException` para errores de negocio (no encontrado, ya existe, etc.)
- Siempre incluir código de error + mensaje descriptivo
- Dejar que excepciones del sistema se propaguen naturalmente
- El mensaje va al usuario final, ser claro y útil

## Validaciones Comunes

```csharp
// Verificar existencia
var entity = await Repository.GetByIdAsync(id)
    ?? throw new CustomException((int)MessagesCodesError.NotFound, "...");

// Verificar duplicados
if (await Repository.ExistAnyAsync(x => x.Code == code))
    throw new CustomException((int)MessagesCodesError.AlreadyExists, "...");
```
