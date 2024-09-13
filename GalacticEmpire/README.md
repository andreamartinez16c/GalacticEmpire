# Galactic Empire API

This project is an ASP.NET API that allows for the management of the registration of inhabitants of the planet Tatooine, including the identification of potential rebels. 
It is built with ASP.NET Core, Entity Framework Core, and SQL Server.

## Features

- **Inhabitant Registration**: allows you to store information about the inhabitants.
- **Inhabitant Query**: retrieve the full record of inhabitants or query by ID.
- **Rebel Identification**: query all inhabitants suspected of being rebels.
- **Planet and Species Query**: allows you to obtain the planets of origin and registered species.

## Prerequisites

Before running the project, make sure you have the following requirements installed:

- **.NET SDK 6.0 or higher**: [Install from here](https://dotnet.microsoft.com/download/dotnet/6.0)
- **SQL Server**: Ensure you have a running instance of SQL Server.
- **SQL Server Management Studio (SSMS)**: You can download it from [here](https://aka.ms/ssmsfullsetup).
- **Visual Studio 2022**: [Download from here](https://visualstudio.microsoft.com/).

## Database Setup

### Step 1: Create the Database in SQL Server

1. Open **SQL Server Management Studio (SSMS)**.
2. Connect to your SQL server.
3. Right-click on the `Databases` folder and select **New Database**.
4. Name the database `GalacticEmpireDB` and click **OK**.

### Step 2: Run the Migration Script

The project is set up to use **Entity Framework Core**, which will handle the automatic creation of tables in the database. However, make sure that the database connection file is correctly configured.

Open the `appsettings.json` file in Visual Studio and ensure that the connection string points to your local SQL Server instance:
   ```json
   {
     "ConnectionStrings": {
       "GalacticEmpireDB": "Server=server;Database=databaseName;Trusted_Connection=True;"
     }
   }