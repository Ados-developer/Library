# LIBRARY (ASP.NET Core Web App(Model-View-Controller))

This project is an ASP.NET Core MVC application for managing a library system.
It allows managing books, readers, and loans, with core features for borrowing and returning books.
---
### Users can:
  1. View a list of available books
  2. Borrow books for registered readers
  3. Return borrowed books
  4. Manage book and reader information

## 🖥️ Preview

![App Screenshot](./assets/banner.png)

## 🚀 How to Run the Project

### 1. Clone the Repository
```bash
git clone https://github.com/Ados-developer/ProfileEditor.git
cd ProfileEditor
```
### 2. Database
  1. In SQL Server Management Studio 20, create a new database.
  2. Run the SQL script repositories/sql/db.sql to populate the database with the sample data.
  3. Update the connection string in appsettings.json to match your SQL Server setup.

### 3. Run the app
```bash
dotnet run
```

## Requirements
1. [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
2. [SQL Server](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
