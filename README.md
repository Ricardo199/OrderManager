# OrderManager

OrderManager is a Windows desktop application for viewing basket contents and adding products to customer orders.

## Overview

The application provides a focused workflow for order maintenance:

- List basket details for a selected shopper
- Add products to an existing basket
- Update basket quantity and subtotal in SQL Server
- Refresh basket line items after changes

## Technology

- **Framework:** .NET 8 (`net8.0-windows`)
- **UI:** WPF
- **Data Access:** Entity Framework Core + SQL Server
- **Packages:** `Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.Data.SqlClient`, `MvvmLightLibs`

## Domain Model

- `Shopper`
- `Product`
- `Basket`
- `BasketItem`

Mappings and table configuration are defined in `Connection.cs`.

## Prerequisites

- Windows environment
- .NET 8 SDK
- SQL Server / SQL Server Express
- `OMS` database with tables:
  - `Shopper`
  - `Product`
  - `Basket`
  - `BasketItem`

## Configuration

Database connection is configured in `Connection.cs`.

Current value in source:

`Server=localhost\SQLEXPRESS; Database=OMS; Trusted_Connection=True; Encrypt=False`

Update this value to match your SQL Server instance.

Recommended production baseline:

`Server=YOUR_SERVER;Database=OMS;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=False`

Use a valid server certificate and keep `TrustServerCertificate=False` to enforce certificate validation.

## Run the Application

From the repository root:

```bash
dotnet build OrderManager.sln
dotnet run --project OrderManager.csproj
```

Or open `OrderManager.sln` in Visual Studio and run the project.

## Repository Layout

- `MainWindow.xaml` – main UI layout
- `MainWindow.xaml.cs` – UI event handlers and order workflows
- `Connection.cs` – EF Core `DbContext` and entity mapping
- `*.cs` – domain entities

## License

See [LICENSE.txt](LICENSE.txt).
