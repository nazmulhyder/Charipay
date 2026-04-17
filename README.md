# 🌍 Charipay – Full-Stack Charity Platform  

A **full-stack charity donation platform** built with **.NET 8 and Angular**, designed to support real-world workflows such as campaign management, secure donation processing, and role-based access control.

The system demonstrates **scalable architecture, clean design patterns, and cloud deployment on Azure**, making it suitable for production-grade applications.

---

## 🌐 Live Demo  
- 🔗 Web App: https://charipay.azurewebsites.net/  
- 🔗 API Swagger: https://charipay-web-api.azurewebsites.net/swagger  

---


## 🚀 Features  

### 🔐 Authentication & Authorization  
- JWT-based authentication  
- Role-based access control (**Admin, Donor, Volunteer**)  
- Secure password hashing  

### 🎯 Campaign Management  
- Create, update, and manage charity campaigns  
- Featured and active campaign listings  
- Real-time campaign progress tracking  

### 💳 Donation Workflow  
- Support for **authenticated and anonymous donations**  
- Transaction tracking and history  
- Secure backend validation  

### 🧑‍💼 Admin Dashboard  
- Manage users, charities, and campaigns  
- View system-wide data and activities  

### 🙋 Volunteer Module *(In Progress)*  
- Volunteer opportunity listing  
- Application and tracking system  

### 🖼️ Media Handling  
- Image upload and storage using **Azure Blob Storage**  

### 🌍 Full-Stack Integration  
- Seamless communication between Angular frontend and .NET API  
- RESTful APIs designed for scalability  

### 🧪 Testing  
- Unit testing with **xUnit, Moq, FluentAssertions**  

---

## 🏗️ Tech Stack  

### Frontend  
- Angular 20 (Standalone Components)  
- TypeScript, RxJS  
- Bootstrap 5  

### Backend  
- .NET 8 Web API (C#)  
- MediatR (CQRS pattern)  
- Entity Framework Core  
- Repository & Unit of Work  

### Database  
- Azure SQL  

### Cloud & DevOps  
- Azure App Service  
- Azure Blob Storage  
- GitHub  

### Authentication  
- JWT (Role-based access control)  

---

## 🏛️ Architecture  

The project follows **Clean Architecture principles**, ensuring separation of concerns and maintainability.

- **Domain Layer** → Core business logic and entities  
- **Application Layer** → Use cases, DTOs, CQRS handlers  
- **Infrastructure Layer** → Data access, EF Core, external services  
- **API Layer** → Controllers, middleware, configuration  

CQRS is implemented using **MediatR**, separating read and write operations for better scalability.

---

## 📂 Project Structure  

```bash
Charipay/
├── Charipay.API              # Controllers, Middleware, Configuration
├── Charipay.Application      # Commands, Queries, DTOs, Handlers
├── Charipay.Domain           # Entities, Interfaces, Business rules
├── Charipay.Infrastructure   # Database, Repositories, EF Core
├── Charipay.Tests            # Unit & integration tests
