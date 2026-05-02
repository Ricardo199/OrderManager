# OrderManager

A Windows desktop application for managing customer orders and shopping baskets, built with C# and WPF on .NET 8.

---

## Features

- **View Baskets** — Select any shopper's basket by email and instantly see all items, unit prices, and quantities in a structured data grid.
- **Add Items to Basket** — Add products to an existing basket with a specified quantity. If the product already exists in the basket, the quantity is incremented automatically.
- **Automatic Totals** — Basket subtotal and total item count are recalculated and persisted to the database whenever items are added.
- **Live Data Refresh** — The basket grid refreshes immediately after any change, keeping the view in sync with the database.

---

## Tech Stack

| Layer | Technology |
|---|---|
| UI Framework | WPF (.NET 8, Windows) |
| ORM | Entity Framework Core 9 |
| Database | SQL Server (SQL Server Express) |
| MVVM Utilities | MvvmLightLibs |

---

## Data Model

| Entity | Key Fields |
|---|---|
| `Shopper` | `IdShopper`, `Email`, `FirstName`, `LastName`, `Address`, `City`, `StateProvince`, `Country`, `ZipCode` |
| `Product` | `IdProduct`, `ProductName`, `Description`, `Price` |
| `Basket` | `IdBasket`, `IdShopper`, `OrderDate`, `Quantity`, `SubTotal` |
| `BasketItem` | `IdBasketItem`, `IdBasket`, `IdProduct`, `Quantity` |

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or SQL Server Express with the `OMS` database configured

---

## Getting Started

1. **Clone the repository**

   ```bash
   git clone https://github.com/Ricardo199/OrderManager.git
   cd OrderManager
   ```

2. **Configure the database connection**

   The connection string is defined in `Connection.cs`. By default it targets a local SQL Server Express instance:

   ```
   Server=localhost\SQLEXPRESS; Database=OMS; Trusted_Connection=True; Encrypt=False
   ```

   Update the `connectionString` field if your SQL Server instance name or credentials differ.

3. **Set up the database**

   Ensure the `OMS` database exists and contains the following tables matching the entity definitions: `Product`, `Shopper`, `Basket`, `BasketItem`.

4. **Build and run**

   Open `OrderManager.sln` in Visual Studio 2022+ and press **F5**, or build from the command line:

   ```bash
   dotnet build
   dotnet run --project OrderManager.csproj
   ```

---

## Usage

| Action | How |
|---|---|
| Load a basket | Select a shopper email + basket ID from the **Basket** dropdown, then click **Load**. |
| Add an item | Click **Add Item to Order**, choose a basket and product, enter a quantity, then click **Save**. |
| Cancel | Click **Cancel** and confirm the prompt to discard the current add-item form. |
| Exit | Click **Exit** to close the application. |

---

## Project Structure

```
OrderManager/
├── App.xaml / App.xaml.cs        # Application entry point
├── MainWindow.xaml / .cs         # Main UI and interaction logic
├── Connection.cs                 # EF Core DbContext and model configuration
├── Product.cs                    # Product entity
├── Shopper.cs                    # Shopper entity
├── Basket.cs                     # Basket entity
├── BasketItem.cs                 # BasketItem entity
└── OrderManager.csproj           # Project file
```

---

## License

This project is licensed under the terms found in [LICENSE.txt](LICENSE.txt).