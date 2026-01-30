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

## Sincronización de Mensajes JSON

**IMPORTANTE**: Cuando se agregue un nuevo código de error al enum `MessagesCodesError`, se **DEBE** actualizar también el archivo JSON correspondiente.

### Archivos de Mensajes

| Tipo | Enum | Archivo JSON |
|------|------|--------------|
| Errores | `MessagesCodesError` | `Common/Common.Messages/MessagesJson/UserMessageError.json` |
| Éxito | `MessagesCodesSuccess` | `Common/Common.Messages/MessagesJson/UserMessageSuccess.json` |

### Formato del Archivo JSON

```json
{
  "Messages": [
    {
      "Code": 139,
      "Message": {
        "Spanish": "El cliente ya tiene una membresía en esta sucursal",
        "English": "The client already has a membership at this branch"
      }
    }
  ]
}
```

### Pasos Obligatorios al Crear un Nuevo Código

1. Agregar el código al enum correspondiente con su `<summary>`:
   ```csharp
   /// <summary>
   /// Descripción del nuevo error
   /// </summary>
   NuevoCodigoError = 148,
   ```

2. Agregar la entrada en el archivo JSON con mensajes en **Spanish** e **English**:
   ```json
   {
     "Code": 148,
     "Message": {
       "Spanish": "Mensaje descriptivo en español",
       "English": "Descriptive message in English"
     }
   }
   ```

### Reglas de Sincronización

- El `Code` en el JSON debe coincidir exactamente con el valor del enum
- Siempre proporcionar mensajes en ambos idiomas (Spanish e English)
- Los mensajes deben ser claros y útiles para el usuario final
- Mantener consistencia en el tono y estilo de los mensajes existentes

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
