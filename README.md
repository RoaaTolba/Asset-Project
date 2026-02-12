# HR System

## Description
The HR System helps organizations manage employees by tracking attendance, calculating salaries based on attendance, and managing users and roles.



## Tech Stack
- **Backend:** .NET Core, ASP.NET MVC, EF Core, Identity, Repository & Service Layer, Middleware, Security
- **Database:** SQL Server (via EF Core)  
- **Authentication & Authorization:** ASP.NET Identity with role-based access control, claims-based permissions, and secure access management
  


## Requirements
- **.NET 8 SDK**  
- NuGet packages:
  - `Microsoft.EntityFrameworkCore`  
  - `Microsoft.EntityFrameworkCore.SqlServer`  
  - `Microsoft.EntityFrameworkCore.Tools`  
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore`  



## Features
- Track daily employee attendance  
- Automatic salary calculation based on attendance  
- Add, edit, and manage employees  
- User management with role assignment  
- Role creation and permission management  
- View salary reports  
- Display employee salaries  



## Usage
- Admin users can log in and manage employees, users, roles, and permissions.  
- HR staff can record attendance and generate salary reports.  
