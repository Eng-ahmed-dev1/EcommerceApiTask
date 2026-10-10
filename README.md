# E-Commerce API (Enterprise Architecture)

An Enterprise-grade ASP.NET Core Web API built with Clean Architecture / N-Tier Architecture, Entity Framework Core, and ASP.NET Core Identity.

## 🏗 Architecture Layers

- **`Ecomerec.API`**: Presentation layer exposing RESTful endpoints, API configurations, and Dependency Injection setup.
- **`Ecomerec.BLL`**: Business Logic Layer containing Services, DTOs (Data Transfer Objects), and Business Exceptions.
  - **Cart Services**: Cart container operations (`GetByIdAsync`, `GetByUserIdAsync`, `ClearAsync`).
  - **CartItem Services**: Granular shopping cart item management (`AddAsync` with stock validation, `RemoveAsync` with price deduction, `UpdateQuantityAsync` with total price recalculation).
  - **Product Services**: Full catalog management with search, category filtering, min/max price range, and pagination.
- **`Ecomerec.DAL`**: Data Access Layer containing Entity Framework Core DbContext, Migrations, Repositories, and Entities (`Product`, `Cart`, `CartItem`, `ApplicationUser`).

## 🛠 Tech Stack

- **.NET 8 / C#**
- **ASP.NET Core Web API**
- **Entity Framework Core (SQL Server)**
- **ASP.NET Core Identity**

## 🚀 Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/Eng-ahmed-dev1/EcommerceApiTask.git
   ```
2. Restore and Build:
   ```bash
   dotnet restore
   dotnet build
   ```
3. Update connection string in `appsettings.json` and run migrations:
   ```bash
   dotnet ef database update --project Ecomerec.DAL --startup-project Ecomerec.API
   ```
