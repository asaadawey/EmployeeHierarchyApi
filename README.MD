# Employee Hierarchy Management API

A robust ASP.NET Core Web API for managing employee hierarchies and organizational structures. This backend service provides comprehensive endpoints for employee management and hierarchical relationships.

## 🌟 Features

- **RESTful API**: Clean and intuitive REST endpoints
- **Hierarchical Data Management**: Support for complex organizational structures
- **Entity Framework Core**: Code-first approach with SQL Server
- **Authentication & Authorization**: ASP.NET Core Identity with JWT tokens
- **Data Validation**: Comprehensive input validation and error handling
- **Database Seeding**: Pre-populated data for development and testing
- **CORS Support**: Cross-origin resource sharing for frontend integration

## 🏗️ Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Documentation**: Swagger/OpenAPI
- **Logging**: Built-in .NET logging with Serilog (optional)

## 📋 Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (LocalDB, Express, or full version)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/employee-hierarchy-api.git
cd employee-hierarchy-api
2. Restore Dependencies
bashdotnet restore
3. Configure Database
Update the connection string in appsettings.json:
json{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeHierarchyDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

4. Run Database Migrations
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
5. Run the Application
```bash
dotnet run
```

The API will be available at:

HTTPS: https://localhost:5111
HTTP: http://localhost:5111
Swagger UI: https://localhost:5111/swagger

📁 Project Structure
EmployeeHierarchyAPI/
├── Controllers/
│   ├── EmployeesController.cs
│   ├── AuthController.cs
│   └── HierarchyController.cs
├── Models/
│   ├── Employee.cs
│   ├── ApplicationUser.cs
│   └── DTOs/
│       ├── EmployeeDto.cs
│       └── HierarchyDto.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   └── Migrations/
├── Services/
│   ├── IEmployeeService.cs
│   ├── EmployeeService.cs
│   └── HierarchyService.cs
├── Authorization/
│   ├── Requirements/
│   └── Handlers/
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
🔧 Configuration
Database Configuration
The application uses Entity Framework Core with SQL Server. Update your connection string in appsettings.json:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your-SQL-Server-Connection-String"
  },
  "JWT": {
    "SecretKey": "your-super-secret-key-here-make-it-long-and-complex",
    "Issuer": "EmployeeHierarchyAPI",
    "Audience": "EmployeeHierarchyClient",
    "ExpirationInMinutes": 60
  }
}
```

Environment Variables
For production, consider using environment variables:
```bash
export ConnectionStrings__DefaultConnection="your-production-connection-string"
export JWT__SecretKey="your-production-secret-key"
```

📚 API Endpoints
Authentication

POST /api/auth/login - User login
POST /api/auth/register - User registration
POST /api/auth/refresh - Refresh JWT token

Employees

GET /api/employees - Get all employees
GET /api/employees/{id} - Get employee by ID
POST /api/employees - Create new employee
PUT /api/employees/{id} - Update employee
DELETE /api/employees/{id} - Delete employee

Hierarchy

GET /api/hierarchy - Get complete hierarchy
GET /api/hierarchy/{id}/subordinates - Get employee's direct reports
GET /api/hierarchy/{id}/managers - Get employee's management chain
PUT /api/hierarchy/{id}/manager - Update employee's manager

🔒 Authentication & Authorization
The API uses JWT Bearer tokens for authentication. Include the token in your requests:
httpAuthorization: Bearer your-jwt-token-here
Default Seeded Users
Email: test2@example.com
Password: 1234


Email: test@example.com
Password: 1234

API Testing with curl
```bash
# Login
curl -X POST https://localhost:5111/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@company.com","password":"Admin123!"}'

# Get employees (with token)
curl -X GET https://localhost:5111/api/employees \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```
🔄 Database Migrations
Create Migration
```bash
dotnet ef migrations add MigrationName
```
Update Database
```bash
dotnet ef database update

Remove Last Migration
```bash
dotnet ef migrations remove
```
📦 Dependencies
```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
```
🐛 Troubleshooting
Common Issues

Database Connection Issues

Verify SQL Server is running
Check connection string format
Ensure database exists


Migration Errors

Delete Migrations folder and recreate
Check for model conflicts


Authentication Issues

Verify JWT configuration
Check token expiration
Ensure proper CORS setup


Built with ❤️ using ASP.NET Core