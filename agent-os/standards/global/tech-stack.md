## Stack tecnológico

Define tu stack técnico a continuación. Esto sirve como referencia para todos los miembros del equipo y ayuda a mantener la consistencia en todo el proyecto.

### Framework & Runtime
- **Application Framework:** ASP.NET Core (Web API)
- **Language/Runtime:** C# 12 (.NET)
- **Package Manager:** NuGet

### Arquitectura
- **Patrón CQRS:** MediatR para separar comandos y consultas
- **Dependency Injection:** Autofac como contenedor de DI
- **Mapping:** AutoMapper para mapeo entre entidades y DTOs
- **Arquitectura en Capas:** Controllers → BusinessLogic → Persistence → Model

### Database & Storage
- **Database:** SQL Server
- **ORM:** Entity Framework Core
- **Patrón Repository:** UnitOfWork con repositorios por entidad

### Logging & Monitoring
- **Logging:** Serilog con structured logging
- **ILogger<T>:** Inyección de logger tipado en todos los handlers y controllers

### Security
- **Encriptación de Contraseñas:** Argon2
- **Autenticación:** JWT (implementación personalizada)

### API
- **API Versioning:** ASP.NET Core API Versioning
- **Formato de Respuesta:** GenericResponse<T> para todas las respuestas
- **Mensajes Internacionalizados:** Sistema de mensajes por código (MessagesCodesSucess, MessagesCodesError)

### Testing & Quality
- **Test Framework:** xUnit, NUnit o MSTest
- **Proyecto de Tests:** Proyecto separado `Test`

### Configuration
- **Configuración:** appsettings.json con variantes por ambiente (Development, Production, Debug)
- **IConfiguration:** Acceso a configuración tipada
