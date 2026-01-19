## Convenciones generales de desarrollo

- **Estructura de Proyecto Consistente**: Organizar archivos y directorios en una estructura predecible y lógica que los miembros del equipo puedan navegar fácilmente
- **Documentación Clara**: Mantener archivos README actualizados con instrucciones de configuración, descripción general de arquitectura y pautas de contribución
- **Mejores Prácticas de Control de Versiones**: Usar mensajes de commit claros, ramas de características y solicitudes de pull/merge significativas con descripciones
- **Configuración de Entorno**: Usar variables de entorno para configuración; nunca comprometer secretos o claves de API al control de versiones
- **Gestión de Dependencias**: Mantener dependencias actualizadas y mínimas; documentar por qué se usan dependencias principales
- **Proceso de Revisión de Código**: Establecer un proceso consistente de revisión de código con expectativas claras para revisores y autores
- **Mantenimiento de Changelog**: Mantener un changelog o notas de versión para rastrear cambios e mejoras significativas

## Convenciones del Proyecto Gym

### Arquitectura en Capas
- **Controllers**: Capa de presentación (API REST)
- **BusinessLogic**: Lógica de negocio (Handlers con MediatR)
- **Model**: Modelos de Request/Response separados por dominio
- **Persistence**: Acceso a datos (Repositories, UnitOfWork, Entity Framework)
- **Common**: Librerías compartidas y utilidades

### Patrón CQRS con MediatR
- Cada operación tiene un Request y un Response específico
- Los Handlers implementan `IRequestHandler<TRequest, TResponse>`
- Handlers heredan de clases base por dominio (ej: `UserAdministratorBase<TRequest, TResponse>`)
- Siempre usar `ExecuteHandlerAsync` para ejecutar la lógica del handler

### Estructura de Archivos
- **Handlers**: `{Accion}{Entidad}Handler` (ej: `CreateUserAdministratorHandler`)
- **Requests**: `{Accion}{Entidad}Request` (ej: `CreateUserAdministratorRequest`)
- **Responses**: `{Accion}{Entidad}Response` (ej: `CreateUserAdministratorResponse`)
- **Controllers**: `{Entidad}Controller` (ej: `UserAdministratorController`)

### Transacciones y UnitOfWork
- Acceder a repositorios a través de `UnitOfWork.{Entity}Repository`
- Usar `UnitOfWork.BeginTransactionAsync()` antes de múltiples operaciones
- Usar `UnitOfWork.CommitAsync()` para confirmar cambios
- Siempre usar transacciones para operaciones que modifican múltiples entidades

### Async/Await
- **SIEMPRE** usar `.ConfigureAwait(false)` en llamadas async
- Esto mejora el rendimiento y evita deadlocks