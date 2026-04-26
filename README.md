# 🌍 Charipay – Backend API (.NET 8)

A backend API for managing charity campaigns, donations, and volunteer participation.  
Charipay is a UK-first platform designed to support real-world workflows such as campaign management, secure donations, and volunteer coordination.

---

## 🚀 Live Demo

- 🌐 Frontend: https://charipay.azurewebsites.net  
- 📡 API (Swagger): https://charipay-web-api.azurewebsites.net/swagger/index.html  

---

## 🎯 Overview

Charipay provides a structured backend system for:

- Managing charities and campaigns  
- Supporting anonymous and authenticated donations  
- Handling volunteer applications and lifecycle  
- Enforcing role-based access control (Admin, Donor, Volunteer)  

The system is built with scalability and maintainability in mind using Clean Architecture principles.

---

## ✨ Features

### 🔐 Authentication & Authorization
- JWT-based authentication  
- Role-based access (Admin, Donor, Volunteer)  
- Secure API endpoints  

### 💰 Donation System
- Campaign-based donations  
- Support for anonymous donations  
- GBP (£) as base currency  
- Donation tracking for registered users  

### 🙋 Volunteer System
- View volunteer opportunities  
- Apply / cancel application  
- Status tracking (Pending, Approved, Cancelled, Completed)  
- Reapply support after cancellation/rejection  

### 🛠️ Admin Features
- Charity management  
- Campaign creation & updates  
- Volunteer application approval/rejection  
- User management  

---

## 🏗️ Backend Architecture Structure

Charipay backend follows a Clean Architecture style, keeping business logic separate from infrastructure and API concerns.

```bash
Charipay/
 ├── Charipay.API/
 │   ├── Controllers/        # API endpoints
 │   ├── Middleware/         # Exception handling, request pipeline
 │   └── Program.cs          # Dependency injection and app configuration
 │
 ├── Charipay.Application/
 │   ├── Features/           # CQRS commands and queries
 │   ├── DTOs/               # Request/response models
 │   ├── Interfaces/         # Application contracts
 │   ├── Validators/         # FluentValidation rules
 │   └── Behaviors/          # MediatR pipeline behaviours
 │
 ├── Charipay.Domain/
 │   ├── Entities/           # Core business entities
 │   ├── Enums/              # Domain enums/statuses
 │   └── Interfaces/         # Domain abstractions
 │
 └── Charipay.Infrastructure/
     ├── Persistence/        # EF Core DbContext and migrations
     ├── Repositories/       # Repository implementations
     ├── Services/           # External/internal service implementations
     └── UnitOfWork/         # Unit of Work implementation
```

### Layer Responsibilities

- **API Layer** handles HTTP requests, controllers, middleware, and dependency injection.
- **Application Layer** contains CQRS handlers, DTOs, validation, and business use cases.
- **Domain Layer** contains core entities, enums, and business rules.
- **Infrastructure Layer** handles database access, repositories, Azure services, and external integrations.
