# 🌍 Charipay – Charity Donation Platform  

A backend-focused .NET Web API for managing charity campaigns, donations, and user roles. The system is designed to support real-world workflows such as secure donation processing, campaign lifecycle management, and role-based access control.

---

## 🚀 Features  
- 🔐 Role-based registration & login (JWT authentication)  
- 📊 Campaign and donation management workflows  
- 🛡️ Secure authentication and password hashing
- 🖼️ Media upload and storage using Azure Blob Storage
- 🌍 RESTful API endpoints for frontend integration  
- 🧪 Unit testing with xUnit, Moq, and FluentAssertions


---

## 🏗️ Tech Stack  
- **Backend:** .NET 8 Web API, C#  
- **Database:** Azure SQL
- **Cloud:** Azure App Service, Azure Blob Storage  
- **Authentication:** JWT, Role-based access  
- **Data Access:** EF Core (Repository & Unit of Work)  
- **Architecture:** Layered architecture with MediatR for request handling  
- **Testing:** xUnit, Moq, FluentAssertions  
- **Deployment:** Azure App Service  

---

## 📂 Project Structure  
```bash
Charipay/
├── Charipay.API              # Controllers, Middleware, Configuration
├── Charipay.Application      # Commands, Queries, DTOs, Handlers
├── Charipay.Domain           # Entities, Interfaces, Business rules
├── Charipay.Infrastructure   # Database, Repositories, EF Core
├── Charipay.Tests            # Unit & integration tests
