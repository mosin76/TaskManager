Running the .NET 8.0 API and React UI
Step 1: Install or Update to .NET 8.0
The API is built using .NET 8.0. Before running it locally, ensure that you have .NET 8.0 installed on your machine. If you don't have it installed, you can download and install it from the .NET official website.

Step 2: Clone the API Repository
Clone the API repository from GitHub using the following link:
•	[TaskManager API Repository](https://github.com/mosin76/TaskManager.git)

Step 3: Configure SQL Server Express
The API uses SQL Server Express for handling Authentication and Task Management. You'll need to update the connection string for Windows Authentication in the appsettings.Development.json file. Modify the connection string to match your local SQL Server Express instance.

Step 4: Apply Entity Framework Migrations
To set up the database schema, run the following commands in the Package Manager Console:
    1.	Identity Migration: Sets up the authentication schema.
        update-database IdentityMigration -context AuthenticationDbContext
    2.	Role Seeding Migration: Seeds roles into the authentication schema.
        update-database RoleSeedMigration -context AuthenticationDbContext
    3.	Task Management Migration: Sets up the schema for task management.
        update-database TaskMigration -context ApplicationDbContext
        
Step 5: Build and Run the API
Once the migrations are applied:
1.	Clean the solution to remove any old binaries or cached files.
2.	Build the solution to ensure everything compiles correctly.
3.	Run the API. You can do this by pressing F5 in Visual Studio or using the command line:
dotnet run

Step 6: Clone and Run the React UI
The UI for this application is built using React and connects to the above API. Clone the UI repository from GitHub using the following link:
•	[TaskManager UI Repository](https://github.com/mosin76/TaskManager.UI.git)
Follow the instructions in the UI repository's README file to install dependencies and start the React application. The UI will interact with the API you just set up.

This guide provides a step-by-step process to run both the .NET 8.0 API and the React UI, ensuring everything is correctly configured and connected.


