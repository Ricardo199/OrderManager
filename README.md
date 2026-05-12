# OrderManager

OrderManager is a Windows desktop application for viewing basket contents and adding products to customer orders.

## Overview

The application provides a focused workflow for order maintenance:

- list basket details for a selected shopper
- add products to an existing basket
- update basket quantity and subtotal in SQL Server
- refresh basket line items after changes

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

Mappings and table configuration are defined in `/home/runner/work/OrderManager/OrderManager/Connection.cs`.

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

Database connection is configured in `/home/runner/work/OrderManager/OrderManager/Connection.cs`:

`Server=localhost\SQLEXPRESS; Database=OMS; Trusted_Connection=True; Encrypt=False`

Update this value to match your SQL Server instance.

## Run the Application

From `/home/runner/work/OrderManager/OrderManager`:

```bash
dotnet build OrderManager.sln
dotnet run --project OrderManager.csproj
```

Or open `OrderManager.sln` in Visual Studio and run the project.

## Repository Layout

- `/home/runner/work/OrderManager/OrderManager/MainWindow.xaml` – main UI layout
- `/home/runner/work/OrderManager/OrderManager/MainWindow.xaml.cs` – UI event handlers and order workflows
- `/home/runner/work/OrderManager/OrderManager/Connection.cs` – EF Core `DbContext` and entity mapping
- `/home/runner/work/OrderManager/OrderManager/*.cs` – domain entities

## License

See [LICENSE.txt](LICENSE.txt).
