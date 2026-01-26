# Tech Stack

## Backend

- **.NET 10.0** - Framework principal
- **ASP.NET Core Web API** - APIs RESTful
- **Entity Framework Core 10.0** - ORM

## Base de Datos

- **Microsoft SQL Server** - Base de datos relacional

## Librerías Clave

- **MediatR** - Patrón mediador para handlers
- **AutoMapper** - Mapeo de objetos
- **Autofac** - Inyección de dependencias
- **EFCore.BulkExtensions** - Operaciones bulk

## Arquitectura

- **API de Administrador** (`AdministratorApi`) - Gestión de gimnasios, usuarios, membresías
- **API Core** (`GatewayCoreAPI`) - Servicios para usuarios clientes
- **Patrón Repository** - Abstracción de acceso a datos
- **Arquitectura en capas** - Controllers → BusinessLogic → Persistence

## Frontend (Repositorio separado)

- Angular
- Tailwind CSS
- PrimeNG
