# SC601_2C_VN_GRUPO-4

## Integrantes del grupo
- CANALES HERRERA JEFFRY MOISES [jcanales20717@ufide.ac.cr]
- ULATE ESQUIVEL KENDALL ADRIEL [kulate00788@ufide.ac.cr]
- PARDO GARCIA EDWIN CAMILO [epardo32120@ufide.ac.cr]

## Enlace del repositorio
- https://github.com/JeffryCH/SC601_2C_VN_GRUPO-4

## Especificación básica del proyecto

### Arquitectura del proyecto
- Clean Architecture (por capas):
  - TaskQueue.APP (ASP.NET MVC, vistas Razor, controladores)
  - TaskQueue.BLL (servicios, interfaces de negocio)
  - TaskQueue.DAL (contexto EF, repositorios, UnitOfWork)
  - TaskQueue.ML (entidades del modelo)
- **Solo el módulo de tareas está incluido en el repositorio, pero la estructura completa del proyecto MVC/.NET se mantiene.**

### Libraries o paquetes NuGet utilizados
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- AutoMapper
- (otros según necesidades)

### Principios SOLID y patrones de diseño utilizados
- S: Single Responsibility (cada clase tiene una única responsabilidad)
- O: Open/Closed (interfaces y servicios extensibles)
- L: Liskov Substitution (interfaces bien definidas)
- I: Interface Segregation (interfaces separadas por funcionalidad)
- D: Dependency Inversion (inyección de dependencias en controladores y servicios)
- Patrón Repository
- Patrón Unit of Work
- Patrón Service Layer

## Especificaciones técnicas
- Framework: .NET 9.0 (estructura equivalente a .NET Framework 4.8)
- Base de datos: SQL Server
- Entity Framework Core
- ASP.NET Razor

## Funcionalidades principales del módulo de tareas
- Gestión de tareas (CRUD, prioridad, estado, reintento)
- Queue de tareas con prioridad y FIFO
- Ejecución de tareas por Worker
- Monitoreo y notificación (logs, panel de estado, reintentos)
- Dashboard y reportes

## Validaciones implementadas
- No se repiten IDs
- Validación de modelos en controladores
- Convenciones de nombres y estilo
- Menús y submenús en vistas

## Observaciones
- Todo el código del módulo de tareas fue desarrollado por el grupo, siguiendo el patrón de MiApp visto en clases.
- El resto de la estructura del proyecto se mantiene para facilitar futuras extensiones y cumplir con la arquitectura solicitada.

---

