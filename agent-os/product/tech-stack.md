# Product Tech Stack

## Framework & Runtime
- **Target Framework:** .NET 10.0
- **Application Framework:** ASP.NET Core (Web API)
- **Language:** C# 12 con Nullable Reference Types habilitado
- **Package Manager:** NuGet

## Arquitectura
- **Patrón CQRS:** MediatR para separar comandos y consultas
- **Dependency Injection:** Autofac como contenedor de DI
- **Mapping:** AutoMapper para mapeo entre entidades y DTOs
- **Arquitectura en Capas:** Controllers → BusinessLogic → Persistence → Model
- **Primary Constructors:** Uso de primary constructors (C# 12) para inyección de dependencias

## Database & Storage
- **Database:** SQL Server
- **ORM:** Entity Framework Core 10.0.1
- **Patrón Repository:** UnitOfWork con repositorios por entidad
- **Code First:** Entity Framework Core con Code First migrations
- **Bulk Operations:** EFCore.BulkExtensions para operaciones masivas en base de datos

## Cloud Storage & Messaging
- **File Storage:** Azure Blob Storage para almacenamiento de archivos (imágenes, videos)
- **Message Queue:** Azure Queue Storage para mensajería asíncrona
- **Background Jobs:** Azure WebJobs para procesamiento en segundo plano

## Real-Time Communication
- **WebSockets:** SignalR para comunicación en tiempo real (WebSockets)
- **Real-Time Updates:** Actualizaciones instantáneas de disponibilidad, reservas y notificaciones

## API
- **API Framework:** ASP.NET Core Web API
- **API Versioning:** Asp.Versioning.Mvc 8.1.0 para versionado de APIs
- **API Documentation:** Swagger/OpenAPI para documentación de APIs
- **Formato de Respuesta:** GenericResponse<T> para todas las respuestas de API
- **Mensajes Internacionalizados:** Sistema de mensajes por código (MessagesCodesSucess, MessagesCodesError)

## Security & Authentication
- **Autenticación:** Sistema de autenticación personalizado con JWT
- **Encriptación de Contraseñas:** Argon2 para encriptación de contraseñas
- **Control de Acceso:** Roles y funcionalidades con control de acceso basado en permisos
- **Roles Multi-Nivel:** Super Administrador, Administrador de Gimnasio, Administrador de Sucursal, Cliente, Entrenador

## Logging & Monitoring
- **Logging:** Serilog con structured logging
- **ILogger<T>:** Inyección de logger tipado en todos los handlers y controllers
- **Log Aggregation:** Seq para agregación de logs
- **Telemetría:** Application Insights para telemetría y monitoreo

## Time Management
- **Time Library:** NodaTime para manejo de tiempo y zonas horarias

## Testing & Quality
- **Test Framework:** xUnit, NUnit o MSTest
- **Proyecto de Tests:** Proyecto separado `Test`
- **Code Analysis:** SonarAnalyzer para análisis estático de código

## Configuration
- **Configuración:** appsettings.json con variantes por ambiente (Development, Production, Debug)
- **IConfiguration:** Acceso a configuración tipada
- **Environment Variables:** Variables de entorno para configuración sensible

## Mobile & Notifications
- **Push Notifications:** Sistema de notificaciones push a dispositivos móviles
- **Notification Management:** Gestión de preferencias de notificaciones por usuario

## Data Validation
- **Data Annotations:** Atributos de validación en Requests (`[Required]`, `[StringLength]`, `[ValidateGuid]`, etc.)
- **Server-Side Validation:** Validación automática por middleware de ASP.NET Core
- **Business Rules Validation:** Validación de reglas de negocio en handlers

## Error Handling
- **Custom Exceptions:** CustomException para errores de negocio
- **Exception Middleware:** ExceptionHandlingMiddleware para manejo centralizado de excepciones
- **Error Codes:** Sistema de códigos de error (MessagesCodesError enum)
- **User-Friendly Messages:** Mensajes de error amigables para el usuario internacionalizados

## Code Quality & Standards
- **Documentation:** Comentarios XML en español para todas las clases públicas
- **Naming Conventions:** Convenciones de nomenclatura consistentes (PascalCase para propiedades, camelCase para parámetros)
- **Async/Await:** Uso de `.ConfigureAwait(false)` en todas las llamadas async
- **Null Safety:** Validación de nulls y uso de null-conditional operators
- **LINQ:** Preferencia por métodos LINQ sobre loops
