## Mejores prácticas de comentarios en código

- **Código Autoexplicativo**: Escribir código que se explique a sí mismo a través de estructura clara y nomenclatura
- **Comentarios Mínimos y Útiles**: Agregar comentarios concisos y mínimos para explicar secciones grandes de lógica de código
- **No Comentar Cambios o Correcciones**: No dejar comentarios de código que hablen de cambios o correcciones recientes o temporales. Los comentarios deben ser textos informativos perennes que sean relevantes en el futuro

## Comentarios en el Proyecto Gym

### Comentarios XML
- Documentar todas las clases públicas con comentarios XML
- Documentar todos los métodos públicos y propiedades importantes
- Usar español para la documentación
- Incluir descripción de parámetros con `<param name="...">`
- Incluir descripción de retornos con `<returns>`

### Ejemplo de Documentación XML
```csharp
/// <summary>
/// Handler para crear usuario administrador
/// </summary>
/// <param name="logger">Logger para registro de eventos</param>
/// <param name="pluginFactory">Factory para obtener dependencias</param>
public class CreateUserAdministratorHandler(
    ILogger<CreateUserAdministratorHandler> logger,
    IPluginFactory pluginFactory) 
    : UserAdministratorBase<CreateUserAdministratorRequest, CreateUserAdministratorResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un usuario administrador
    /// </summary>
    /// <param name="request">Request con los datos del usuario a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Response con el resultado de la operación</returns>
    public override async Task<CreateUserAdministratorResponse> Handle(...)
}
```

### Propiedades Documentadas
- Documentar propiedades públicas de Requests y Responses
- Incluir descripción del propósito y formato esperado
- Ejemplo:
```csharp
/// <summary>
/// Identificador único del usuario
/// </summary>
[Required]
[ValidateGuid]
public Guid UserGuid { get; set; }
```