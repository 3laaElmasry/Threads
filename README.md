# 🧵 Threads – ASP.NET Core API

**Threads** is a lightweight microblogging RESTful API built with ASP.NET Core and Entity Framework Core.  
It allows users to register, log in, and post short text-based messages — similar to Twitter.  
The project is structured following **Clean Architecture** principles for scalability and maintainability. Authentication and user management are handled using **Clerk.dev** with secure JWT tokens and role-based access control.

---

## 🚀 Features

- 🔐 JWT-based authentication using Clerk.dev
- 👤 Role-based access (Admin / User)
- 🧵 Create, Read, Update, Delete (CRUD) threads
- 🧼 Clean Architecture layers (Web, Business, DataAccess)
- 📄 Swagger API documentation
- 🛠️ Entity Framework Core with SQL Server
- 📦 Dependency Injection throughout the application
- 🧪 Ready for front-end integration

---

## 🧱 Tech Stack

- **Framework:** ASP.NET Core (.NET 6+)
- **ORM:** Entity Framework Core
- **Database:** SQL Server (configurable)
- **Authentication:** Clerk.dev + JWT
- **API Docs:** Swagger (Swashbuckle)
- **Architecture:** Clean Architecture
- **Other:** Dependency Injection, Middleware

---

## 📁 Project Structure

Threads.API/ --> Web Layer (Controllers, Middleware, Program.cs)
Threads.Business/ --> Business Logic (Services, Interfaces, DTOs)
Threads.DataAccess/ --> Data Layer (DbContext, Repositories)


---

## 🔐 Authentication with Clerk.dev

Authentication is handled using [Clerk.dev](https://clerk.dev), which manages user registration, login, and JWT generation.

### Example Request Header:

Authorization: Bearer <eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9>


⚙️ Configuration
Update the appsettings.Development.json file with your own credentials and connection strings:
{
  "ConnectionStrings": {
    "MyConnection": "Server=.;Database=Threads.Api;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
  },
  "Clerk": {
    "SecretKey": "sk_test_XXXXXXXXXXXXXXXXXXXXXXXX"
  },
  "JWT": {
    "Key": "s7GZ08s0fr7KVgy7DyyN+3D9li4T+VTn0xmsi+nOQqc=",
    "Issuer": "SecureApi",
    "Audience": "SecureApiUser",
    "DurationInDays": 10
  },
  "AllowedHosts": "*",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}

🛠️ Getting Started
1. Clone the Repo
git clone https://github.com/3laaElmasry/Threads.git
cd Threads

