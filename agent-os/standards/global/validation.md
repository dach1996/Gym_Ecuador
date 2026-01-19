## Mejores prácticas de validación

- **Validar en el Servidor**: Siempre validar en el servidor; nunca confiar únicamente en la validación del cliente para seguridad o integridad de datos
- **Cliente para UX**: Usar validación del lado del cliente para proporcionar retroalimentación inmediata al usuario, pero duplicar las verificaciones en el servidor
- **Fallar Temprano**: Validar la entrada lo antes posible y rechazar datos inválidos antes del procesamiento
- **Mensajes de Error Específicos**: Proporcionar mensajes de error claros y específicos por campo que ayuden a los usuarios a corregir su entrada
- **Listas de Permitidos sobre Listas de Bloqueados**: Cuando sea posible, definir qué está permitido en lugar de intentar bloquear todo lo que no lo está
- **Validación de Tipo y Formato**: Verificar tipos de datos, formatos, rangos y campos requeridos sistemáticamente
- **Sanitizar Entrada**: Sanitizar la entrada del usuario para prevenir ataques de inyección (SQL, XSS, inyección de comandos)
- **Validación de Reglas de Negocio**: Validar reglas de negocio (ej: saldo suficiente, fechas válidas) en la capa de aplicación apropiada
- **Validación Consistente**: Aplicar validación consistentemente en todos los puntos de entrada (formularios web, endpoints de API, trabajos en segundo plano)

## Validation en el Proyecto Gym

### Data Annotations en Requests
- Usar atributos de validación en todas las propiedades de Request (`[Required]`, `[StringLength]`, `[ValidateGuid]`, etc.)
- La validación se ejecuta automáticamente por el middleware de ASP.NET Core
- Ejemplo:
```csharp
public class CreateUserRequest : IApiBaseRequest<CreateUserResponse>
{
    [Required]
    [ValidateGuid]
    public Guid UserGuid { get; set; }
    
    [Required]
    [StringLength(100)]
    public string UserName { get; set; }
}
```

### Validación en Handlers
- Validar existencia de entidades antes de operar con ellas
- Validar reglas de negocio antes de persistir cambios
- Lanzar `CustomException` con código de error desde `MessagesCodesError` enum
- Ejemplo:
```csharp
var user = await UnitOfWork.UserRepository
    .GetByFirstOrDefaultAsync(where => where.Guid == request.UserGuid)
    .ConfigureAwait(false)
    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Usuario no encontrado");
```

### Validación de Identificadores
- Usar `Guid` para identificadores externos (exposición en API)
- Usar `int` para identificadores internos (claves primarias de base de datos)
- Propiedades Guid deben terminar en `Guid` (ej: `UserGuid`, `RoleGuid`)
- Validar que los Guid no sean `Guid.Empty` antes de usarlos