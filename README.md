# 🏬 Smart Stock - Intelligent Inventory Control & Demand Forecasting API

## 📘 Overview

**Smart Stock** is a **.NET 8 Web API** designed to manage and predict inventory behavior using real business logic and data-driven insights.  
It provides complete CRUD operations for products, inventory movements, and user management — and features an **AI-based demand forecasting module** powered by **ML.NET** (under development).

This project aims to support small and medium-sized businesses in optimizing their stock levels, reducing losses, and improving operational efficiency through automation and data prediction.

---

## ⚙️ Features

- ✅ **Product Management:** Register, update, and track products in real-time  
- 🔄 **Stock Movements:** Record all inflows and outflows of inventory items  
- 📊 **Demand Forecasting (ML.NET):** Predict product depletion based on consumption history  
- ⚠️ **Low Stock Notifications:** Automatic alerts when items are predicted to run out soon  
- 🔐 **Authentication & Authorization:** JWT-based access control  
- 📈 **Reports & Dashboards:** Ready for integration with frontend dashboards (React.js)

---

## 🧩 Architecture

Smart Stock follows the principles of **Clean Architecture**, ensuring scalability, maintainability, and separation of concerns.

- **SmartStock.API** -> Controllers & Endpoints
- **SmartStock.Application** -> Business rules & services
- **SmartStock.Core** -> Entities & Interfaces
- **SmartStock.Infrastructure** -> Database context & repositories

---

## 🛠️ Technologies Used

| Category | Tools & Frameworks |
|-----------|--------------------|
| **Language** | C# (.NET 8) |
| **Framework** | ASP.NET Core Web API |
| **ORM** | Entity Framework Core |
| **Database** | SQL Server |
| **Authentication** | JWT (JSON Web Token) |
| **Machine Learning** | ML.NET *(for future AI forecasting module)* |
| **Architecture** | Clean Architecture + Repository Pattern |
| **Testing** | xUnit |
| **Documentation** | Swagger / OpenAPI |

---

## 🧱 Entities Overview

### Product
Represents a registered product in the inventory.
```csharp
public class Product {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int MinimumQuantity { get; set; }
    public decimal Price { get; set; }
    public DateTime LastRestockDate { get; set; }
}
```

### StockMovement
Tracks all product inflows and outflows.
```csharp
public class StockMovement {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public MovementType Type { get; set; }
    public int Quantity { get; set; }
    public DateTime MovementDate { get; set; }

    public Product Product { get; set; }
}

public enum MovementType {
    Entry = 1,
    Exit = 2
}
```

### DemandPrediction
Stores ML.NET prediction results for product depletion.
```csharp
public class DemandPrediction {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public DateTime PredictionDate { get; set; }
    public int PredictedDaysToDepletion { get; set; }

    public Product Product { get; set; }
}
```

### User
Represents users registered in the system
```csharp
public class User {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string ImageUrl { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } = "User";
}
```

---

## 🧠 Demand Forecasting Logic
The **AI module** (built with **ML.NET**) will analyze product consumption history to predict when each product will run out of stock.

**Simplified workflow:**

1. Retrieve product movement history (type = Exit)
2. Calculate average daily usage
3. Train ML.NET regression model based on daily consumption
4. Predict future depletion days
5. Save results in **DemandPrediction** and generate notifications

---

## 🧰 Project Setup

### Prerequisites
- .NET 8 SDK
- SQL Server
- Visual Studio or VS Code

### Steps to Run
```bash
C:\seu\caminho> git clone https://github.com/Gabriel-Steps/Smart-Stock-API
C:\seu\caminho> cd Smart-Stock-API
C:\seu\caminho\Smart-Stock-API> dotnet restore
C:\seu\caminho\Smart-Stock-API> dotnet ef database update
C:\seu\caminho\Smart-Stock-API> cd SmartStockBackend.API
C:\seu\caminho\Smart-Stock-API\SmartStockBackend.API> dotnet run
```
