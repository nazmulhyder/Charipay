# 🌍 Charipay API – Backend (.NET 8)

The backend service for **Charipay**, a full-stack charity donation platform.  
This API is designed to support real-world workflows such as campaign management, secure donation processing, and role-based access control.

Built with **.NET 8, Clean Architecture, and CQRS**, the system emphasizes scalability, maintainability, and production-ready design.

---

## 🌐 Live API  
- 🔗 Swagger: https://charipay-web-api.azurewebsites.net/swagger  

---

## 🚀 Core Features  

### 🔐 Authentication & Authorization  
- JWT-based authentication  
- Role-based access control (**Admin, Donor, Volunteer**)  
- Secure password hashing  

### 🎯 Campaign Management  
- Create, update, and manage charity campaigns  
- Featured and active campaign filtering  
- Campaign progress tracking  

### 💳 Donation Processing  
- Support for authenticated and anonymous donations  
- Transaction tracking and validation  
- Extensible for payment gateway integration  

### 🖼️ Media Handling  
- Image upload and storage using **Azure Blob Storage**  
- Public URL generation for frontend consumption  

### 📊 Admin Capabilities  
- Manage users, charities, and campaigns  
- System-level data control and monitoring  

### 🌍 API Design  
- RESTful endpoints with versioning  
- Consistent response structure using `ApiResponse<T>`  
- Pagination, filtering, and search support  

---

## 🏗️ Tech Stack  

- **Framework:** .NET 8 Web API (C#)  
- **Architecture:** Clean Architecture  
- **Patterns:** CQRS (MediatR), Repository, Unit of Work  
- **Data Access:** Entity Framework Core, Dapper  
- **Database:** Azure SQL  
- **Cloud:** Azure App Service, Azure Blob Storage  
- **Authentication:** JWT (Role-based)  
- **Testing:** xUnit, Moq, FluentAssertions  

---

## 🏛️ Architecture Overview  

The project follows **Clean Architecture principles**:

- **Domain Layer**  
  Core business entities and interfaces  

- **Application Layer**  
  Use cases, DTOs, commands & queries (CQRS with MediatR)  

- **Infrastructure Layer**  
  EF Core, repositories, database, external services  

- **API Layer**  
  Controllers, middleware, configuration  

This structure ensures:
- Separation of concerns  
- Testability  
- Long-term maintainability  

---

## 📂 Project Structure  

```bash
Charipay/
├── Charipay.API              # Controllers, Middleware, Configuration
├── Charipay.Application      # Commands, Queries, DTOs, Handlers
├── Charipay.Domain           # Entities, Interfaces, Business rules
├── Charipay.Infrastructure   # Database, Repositories, EF Core
├── Charipay.Tests            # Unit & integration tests
