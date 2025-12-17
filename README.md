# ProductManagment API

---

## Prerequisites

- **.NET 10 SDK**  
- **SQL Server** (Express, Developer, or full edition)  
- Optional: **Visual Studio 2022** or **VS Code**

---

## Setup Instructions

### 1. Clone or Download the Project

Clone the repository:

git clone <repository-url>

Or download the ZIP, unzip it, and open the folder in your IDE.

---

### 2. Configure the Database

Open appsettings.json in the .WebApi project.  

Update the connection string to point to your SQL Server instance. Examples:

**Windows Authentication (Trusted Connection):**

"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-XXXX\\SQLEXPRESS;Database=ProductDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

**SQL Server Authentication:**

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=ProductDB;User Id=<Your_UserId>;Password=<YourStrongPassword>;TrustServerCertificate=True;"
}

> Make sure the database (ProductDB) exists. EF Core will create tables and seed default data automatically.

---

### 3. Restore Dependencies

Open a terminal in the project root (or use Package Manager Console in Visual Studio) and run:

dotnet restore

---

### 4. Apply Database Migrations

**Option 1: Using Package Manager Console (Visual Studio)**

- Set Default project to ProductManagment.Infrastructure  
- Run:

Update-Database

**Option 2: Using CLI**

Navigate to the Infrastructure project folder and run:

dotnet ef database update --project ProductManagment.Infrastructure --startup-project ProductManagment.WebApi

> This applies all migrations and creates tables, including seed data (users, roles, products).

---

### 5. Run the API

**In Visual Studio:**

- Set ProductManagment.WebApi as the startup project  
- Press Run (F5)

**Via CLI:**

dotnet run --project ProductManagment.WebApi

- API base URL: https://localhost:7283/api  
- Swagger UI documentation: https://localhost:7283/swagger

---

## Seeded Data

When the project runs for the first time, the DbInitializer automatically creates:

- **Roles:** Admin, User  
- **Users:**  
  - Admin: admin@example.com / Admin123!  
  - User: user@example.com / User123!  
- **Sample products** products such as
                    {
                        Title = "Shampoo",
                        Quantity = 50,
                        Price = 5.99m
                    },
                                        {
                        Title = "Car Wax",
                        Quantity = 25,
                        Price = 12.49m
                    },
                    and 
                    {
                        Title = "Microfiber Cloth",
                        Quantity = 100,
                        Price = 2.99m
                    }
