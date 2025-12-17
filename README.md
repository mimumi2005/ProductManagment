Running the Project

1. Download the project as ZIP;
2. Unzip  the project;
3. Open a terminal in the project folder.
Restore dependencies:
- dotnet restore
- 
Apply migrations to create the database schema:
- NuGet package manager console write "update-database"

Make sure the DefaultConnection in appsettings.json points to your SQL Server database.
Run the project

The API will be available at https://localhost:7283/api and you can see the documentation of the API's at https://localhost:7283/swagger
