# Response Pattern

## Estructura de Response

Todos los responses de handlers incluyen:

```csharp
return new CreateGymResponse(gym.Guid, gym.Name)
{
    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
    ShowMessage = true  // El frontend debe mostrar este mensaje
};
```

## Propiedades

- `UserMessage`: Mensaje localizado para mostrar al usuario
- `ShowMessage`: Indica si el frontend debe mostrar el mensaje

## Cuándo usar ShowMessage

| Operación | ShowMessage | Razón |
|-----------|-------------|-------|
| Crear | `true` | Usuario debe saber que se creó |
| Actualizar | `true` | Usuario debe saber que se guardó |
| Eliminar | `true` | Usuario debe confirmar eliminación |
| Consultar | `false` | No hay acción que confirmar |
| Listar/Paginar | `false` | Solo muestra datos |

## Obtener Mensajes

```csharp
// Mensaje de éxito genérico
GetSuccessMessage(MessagesCodesSucess.Ok)

// Mensaje específico
GetSuccessMessage(MessagesCodesSucess.Created)
```
