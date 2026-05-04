# 🧠 Inventory System

Sistema de gestión de usuarios, áreas, roles y auditoría basado en arquitectura por capas con integración entre:

- API (.NET Core)
- Aplicación (CQRS + MediatR)
- Infraestructura (SQL Server + SP)
- Cliente WPF (MVVM)

---

## 🏗️ Arquitectura del Sistema

```mermaid
flowchart LR

    UI["WPF Client (MVVM)"]
    API["Inventory.API Controllers"]
    APP["Application Layer (CQRS + Handlers)"]
    DOMAIN["Domain Layer (Entities + Interfaces)"]
    INFRA["Infrastructure (Repositories + ADO.NET)"]
    DB[("SQL Server (Stored Procedures)")]

    UI --> API
    API --> APP
    APP --> DOMAIN
    APP --> INFRA
    INFRA --> DB


---

# ✅ DIAGRAMA CQRS COMMAND (CORREGIDO)

```md
## 🔁 Flujo CQRS - Command

```mermaid
sequenceDiagram

    participant UI as WPF
    participant API as Controller
    participant MediatR
    participant Handler
    participant Repo
    participant DB

    UI->>API: POST /api/users
    API->>MediatR: Send(CreateUserCommand)
    MediatR->>Handler: Handle()
    Handler->>Repo: Create(user)
    Repo->>DB: Execute SP sp_CreateUser
    DB-->>Repo: OK
    Repo-->>Handler: OK
    Handler-->>API: OK
    API-->>UI: 200 OK

---

# ✅ DIAGRAMA CQRS QUERY (CORREGIDO)

```md
## 🔍 Flujo CQRS - Query

```mermaid
sequenceDiagram

    participant UI as WPF
    participant API as Controller
    participant MediatR
    participant Handler
    participant Repo
    participant DB

    UI->>API: GET /api/users
    API->>MediatR: Send(GetUsersQuery)
    MediatR->>Handler: Handle()
    Handler->>Repo: GetLast()
    Repo->>DB: Execute SP sp_GetLastUsers
    DB-->>Repo: Result
    Repo-->>Handler: Data
    Handler-->>API: DTO List
    API-->>UI: JSON

# 🧩 Tecnologías utilizadas

- .NET Core 8 (API)
- .NET Framework (WPF)
- MediatR (CQRS)
- SQL Server (Stored Procedures)
- ADO.NET
- WPF + MVVM
- Swagger (documentación API)

---

# ⚙️ Funcionalidades principales

## 👤 Usuarios

- Crear usuario
- Editar usuario
- Eliminación lógica (soft delete)
- Reactivación de usuario
- Validaciones avanzadas:
  - Nombre (sin duplicados fuzzy)
  - Contacto (numérico)
  - Email (formato válido)
  - Documento único

## 🏢 Áreas y Roles

- Consulta de áreas
- Consulta de roles
- Relación con usuarios

## 📄 Tipo de documento

- Gestión de tipos de documento
- Relación con usuarios

## 🧾 Auditoría

- Registro automático en cada operación
- Historial de cambios

---

# 🔁 Flujo de arquitectura
WPF → API → Application → Domain → Infrastructure → DB


---

# 🚀 Cómo ejecutar el proyecto

## 🔧 1. Base de datos

1. Abrir SQL Server
2. Ejecutar scripts desde: Inventory.Database


Orden recomendado:

1. Tablas
2. Funciones
3. Stored Procedures

---

## 🔧 2. API

1. Ir al proyecto: Inventory.API

2. Configurar conexión en `appsettings.json`:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=InventoryDB;User Id=sa;Password=tu_password;"
}

3. dotnet run

4. http://localhost:xxxx

5. Ir a: Inventory.WPF

6. Configurar URL API en App.config:

<appSettings>
  <add key="ApiBaseUrl" value="https://localhost:xxxx/api/" />
</appSettings>

7. Ejecutar proyecto

🔌 Endpoints principales

👤 Users
| Método | Endpoint                 |
| ------ | ------------------------ |
| GET    | /api/users               |
| POST   | /api/users               |
| PUT    | /api/users/{id}/contact  |
| DELETE | /api/users/{id}          |
| PUT    | /api/users/{id}/activate |

🏢 Areas
| Método | Endpoint   |
| ------ | ---------- |
| GET    | /api/areas |
| POST   | /api/areas |

🧾 Roles
| Método | Endpoint   |
| ------ | ---------- |
| GET    | /api/roles |
| POST   | /api/roles |

📄 TypeDocuments
| Método | Endpoint            |
| ------ | ------------------- |
| GET    | /api/type-documents |

📊 Audit
| Método | Endpoint   |
| ------ | ---------- |
| GET    | /api/audit |

🎯 Comportamiento clave (Soft Delete)
IsActive = 1 → Usuario activo
IsActive = 0 → Usuario inactivo
UI (WPF)
Usuarios inactivos:
Fila en gris
Botón editar deshabilitado
Botón activar visible

🧠 Patrones implementados
CQRS (Commands & Queries)
Repository Pattern
Dependency Injection
MVVM (WPF)
Soft Delete Pattern
Audit Logging Pattern

⚠️ Consideraciones
No se eliminan datos físicamente (soft delete)
Validaciones duplicadas:
UI
Backend
Base de datos
Uso intensivo de Stored Procedures

