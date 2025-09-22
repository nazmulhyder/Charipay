# 🌍 Charipay – Charity Donation Platform  

_A .NET Web API application for managing charity donations, built with Clean Architecture, MediatR, Repository Pattern, and Unit of Work._  

---

## 🚀 Features  
- 🔐 Role-based registration & login (JWT authentication)  
- 🏛️ Clean Architecture with MediatR, Repository, and Unit of Work  
- 🗂️ Domain-driven design (DDD) principles  
- 🛡️ Secure password hashing  
- 📦 Unit tests with xUnit, Moq, FluentAssertions  
- 🌍 RESTful API endpoints  

---

## 🏗️ Tech Stack  
- **Backend:** .NET 8 Web API, C#, MediatR  
- **Database:** Azure SQL 
- **Authentication:** JWT, Role-based  
- **ORM/Data Access:** EF Core (Repository & Unit of Work), Dapper (optional)  
- **Testing:** xUnit, Moq, FluentAssertions  
- **Other:** AutoMapper, ILogger
- **Deployment & Hosting:** Azure Web App

---

## 📂 Project Structure  
```bash
Charipay/
├── Charipay.API              # Presentation layer (Controllers, Middlewares, Startup)
├── Charipay.Application      # Application layer (Commands, Queries, DTOs, Handlers)
├── Charipay.Domain           # Entities, Interfaces, Business rules
├── Charipay.Infrastructure   # Database, Repositories, EF Core
├── Charipay.Tests            # Unit & integration tests
```
## 📖 API Documentation

Swagger UI:
- **Production** → [https://charipay-web-api.azurewebsites.net/swagger/index.html](https://charipay-web-api.azurewebsites.net/swagger/index.html)
