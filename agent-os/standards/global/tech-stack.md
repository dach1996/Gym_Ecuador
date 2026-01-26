# Tech Stack

## Backend

- **.NET 10.0** - Framework principal
- **ASP.NET Core Web API** - APIs RESTful
- **C#** - Lenguaje de programación

## Base de Datos

- **Microsoft SQL Server** - Base de datos relacional
- **Entity Framework Core 10.0** - ORM

## Librerías Principales

| Librería | Versión | Uso |
|----------|---------|-----|
| MediatR | 13.1.0 | Patrón mediador para handlers |
| AutoMapper | 15.1.0 | Mapeo de objetos |
| Autofac | 9.0.0 | Inyección de dependencias |
| EFCore.BulkExtensions | 10.0.0 | Operaciones bulk en BD |
| BCrypt.Net-Next | 4.0.3 | Hash de contraseñas |
| Asp.Versioning.Mvc | 8.1.0 | Versionado de API |

## Monitoreo y Calidad

- **Application Insights** - Telemetría y monitoreo
- **SonarAnalyzer** - Análisis estático de código

## Arquitectura

```
├── AdministratorApi/        # API para administradores
├── GatewayCoreAPI/          # API para usuarios clientes
├── LogicAdministratorApi/   # Lógica de negocio admin
├── LogicApi/                # Lógica de negocio core
├── LogicCommon/             # Lógica compartida
├── Persistence/             # Acceso a datos (Repository)
├── Common/                  # Utilidades compartidas
├── WebJobs/                 # Trabajos en background
└── WebSocketsApp/           # Comunicación tiempo real
```

## Patrones

- **MediatR** - Handlers desacoplados de controllers
- **Repository Pattern** - Abstracción de acceso a datos
- **Unit of Work** - Gestión de transacciones
- **Arquitectura en capas** - Controllers → BusinessLogic → Persistence

## Frontend (Repositorio separado)

- Angular
- Tailwind CSS
- PrimeNG
