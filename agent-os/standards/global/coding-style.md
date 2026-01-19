## Mejores prácticas de estilo de código

- **Convenciones de Nomenclatura Consistente**: Establecer y seguir convenciones de nomenclatura para variables, funciones, clases y archivos en todo el código base
- **Formato Automatizado**: Mantener estilo de código consistente (sangría, saltos de línea, etc.)
- **Nombres Significativos**: Elegir nombres descriptivos que revelen la intención; evitar abreviaciones y variables de una sola letra excepto en contextos estrechos
- **Funciones Pequeñas y Enfocadas**: Mantener funciones pequeñas y enfocadas en una sola tarea para mejor legibilidad y capacidad de prueba
- **Sangría Consistente**: Usar sangría consistente (espacios o tabs) y configurar tu editor/linter para hacerla cumplir
- **Eliminar Código Muerto**: Eliminar código no utilizado, bloques comentados e importaciones en lugar de dejarlos como desorden

## Estilo de Código en el Proyecto Gym

### Convenciones de Nomenclatura

#### Clases y Archivos
- **Handlers**: `{Accion}{Entidad}Handler` (ej: `CreateUserAdministratorHandler`)
- **Requests**: `{Accion}{Entidad}Request` (ej: `CreateUserAdministratorRequest`)
- **Responses**: `{Accion}{Entidad}Response` (ej: `CreateUserAdministratorResponse`)
- **Controllers**: `{Entidad}Controller` (ej: `UserAdministratorController`)
- **Clases Base**: `{Entidad}Base` (ej: `UserAdministratorBase`)

#### Propiedades y Métodos
- Usar **PascalCase** para propiedades públicas
- Usar **camelCase** para parámetros y variables locales
- Usar verbos en infinitivo para métodos (ej: `GetUserAsync`, `CreateUserAsync`)

#### Identificadores
- Usar `Guid` para identificadores externos (exposición en API)
- Usar `int` para identificadores internos (claves primarias de base de datos)
- Propiedades Guid deben terminar en `Guid` (ej: `UserGuid`, `RoleGuid`)

### Primary Constructors
- Usar primary constructors (C# 12) para inyección de dependencias
- Ejemplo:
```csharp
public class CreateUserAdministratorHandler(
    ILogger<CreateUserAdministratorHandler> logger,
    IPluginFactory pluginFactory) 
    : UserAdministratorBase<CreateUserAdministratorRequest, CreateUserAdministratorResponse>(logger, pluginFactory)
```

### Null Safety
- Validar nulls antes de usar objetos
- Usar null-conditional operators (`?.`) cuando sea apropiado
- Usar `??` para valores por defecto
- Ejemplo:
```csharp
var user = await GetUserAsync().ConfigureAwait(false)
    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Usuario no encontrado");
```

### LINQ
- Preferir métodos de LINQ sobre loops cuando sea posible
- Usar `Any()` en lugar de `Count() > 0` para verificar existencia
- Usar `FirstOrDefault()` cuando se espera un solo resultado

### Organización
- Agrupar código relacionado en regiones cuando sea apropiado
- Usar `#region` para métodos del controller, propiedades, etc.