## Virtual Bag System

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET_Core-MVC-5C2D91?style=for-the-badge)
![Entity Framework Core](https://img.shields.io/badge/Entity_Framework-Core-68217A?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-Database-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)

VirtualBag System is a web-based academic management platform built with ASP.NET Core MVC. The project is designed to support students, teachers, and administrators through one organized system where learning resources, homework, attendance, notes, and class information can be managed digitally.

This project was created as a student-friendly learning project, but it follows a layered structure that is commonly used in real-world .NET applications.

# Table of Contents

• [Overview](#overview)
• [Features](#features)
• [User Roles](#user-roles)
• [Tech Stack](#tech-stack)
• [Project Architecture](#project-architecture)
• [Getting Started](#getting-started)
• [Database Configuration](#database-configuration)
• [Running the Application](#running-the-application)
• [Screenshots](#screenshots)
• [Learning Outcomes](#learning-outcomes)
• [Known Limitations](#known-limitations)
• [Future Improvements](#future-improvements)
• [Repository Hygiene](#repository-hygiene)
• [Author](#author)
• [License](#license)

# Overview

VirtualBag System helps educational institutions manage daily academic activities in a simple digital environment. Instead of keeping homework, attendance, study notes, and class information in separate places, the system brings them together in one web application.

The main goal of this project is to practice ASP.NET Core MVC, Entity Framework Core, SQL Server, layered architecture, session-based authentication, and role-based workflows in a practical academic scenario.

# Features

• Role-based login system for Admin, Teacher, and Student users
• Separate dashboards for each role
• User management for teachers and students
• Class and subject management
• Teacher assignment to specific classes and subjects
• Homework creation and deadline tracking
• Homework submission and teacher review workflow
• Attendance session creation and attendance marking
• Digital book/library management
• Student note management
• Student study activity tracking
• Notification system for new homework updates
• SQL Server database integration using Entity Framework Core
• Layered application structure with MVC, BLL, and DAL separation

# User Roles

#Admin

Admin users manage the core academic setup of the system.

• Manage users
• Manage classes
• Manage subjects
• Assign teachers to classes and subjects

#Teacher

Teacher users manage learning activities for the classes and subjects assigned to them.

• View assigned classes and subjects
• Create, update, and delete homework
• Create attendance sessions
• Mark student attendance
• Review homework submissions

#Student

Student users can access academic resources and track their own learning activities.

• View personal dashboard
• View assigned homework
• Submit homework
• View homework submission status
• Access digital books
• Create and manage notes
• View notifications
• Track attendance summary

# Tech Stack

This project was built using the following technologies:

• **Framework:** ASP.NET Core MVC
• **Language:** C#
• **Runtime:** .NET 10
• **Database:** Microsoft SQL Server
• **ORM:** Entity Framework Core
• **Architecture:** Layered Architecture
• **Mapping Tool:** AutoMapper
• **Frontend:** Razor Views, Bootstrap, jQuery
• **IDE:** Visual Studio Code

# Project Architecture

The project is organized into separate layers to keep responsibilities clean and easy to maintain.

```text
VirtualBagSystem/
|-- VirtualBag/          # ASP.NET Core MVC web application
|   |-- Controllers/     # Request handling and page flow
|   |-- Views/           # Razor UI pages
|   |-- wwwroot/         # Static files such as CSS, JS, and libraries
|   `-- Program.cs       # Application startup and dependency injection
|
|-- BLL/                 # Business Logic Layer
|   |-- DTOs/            # Data Transfer Objects
|   |-- Services/        # Business services
|   `-- MapperConfig.cs  # AutoMapper configuration
|
|-- DAL/                 # Data Access Layer
|   |-- EF/              # Entity Framework DbContext and table models
|   `-- Repository/      # Repository classes for data operations
|
`-- VirtualBagSystem.slnx
```

# Getting Started

Follow these steps to run the project locally.

# Prerequisites

Make sure the following tools are installed:

• .NET 10 SDK
• SQL Server or SQL Server Express
• SQL Server Management Studio, Azure Data Studio, or another SQL client
• Visual Studio, Visual Studio Code, or JetBrains Rider

# Clone the Repository

```bash
git clone https://github.com/eyamin-000/Virtual-Bag-System.git
cd Virtual-Bag-System
```

# Restore Dependencies

```bash
dotnet restore
```

# Build the Solution

```bash
dotnet build VirtualBagSystem.slnx
```

# Database Configuration

The application uses SQL Server and reads the database connection from `VirtualBag/appsettings.json`.

Update the connection string based on your local SQL Server setup:

```json
{
  "ConnectionStrings": {
    "DbConn": "Server=YOUR_SERVER_NAME\\SQLEXPRESS;Database=VirtualBagDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Recommended database steps:

1. Create a SQL Server database named `VirtualBagDB`.
2. Update `DbConn` in `VirtualBag/appsettings.json`.
3. Add a database script or EF Core migrations before sharing the repository publicly.
4. Run the application after the database schema is ready.

If migrations are added later, the database can be updated with a command similar to:

```bash
dotnet ef database update --project DAL/DAL.csproj --startup-project VirtualBag/VirtualBag.csproj
```

## Running the Application

Run the MVC project from the solution root:

```bash
dotnet run --project VirtualBag/VirtualBag.csproj
```

The application should open on one of the configured local URLs:

```text
https://localhost:7019
http://localhost:5243
```

## Screenshots

## Learning Outcomes

While building this project, the following concepts were practiced:

• ASP.NET Core MVC project structure
• Razor views and controller-based routing
• Session-based login flow
• Role-based dashboard design
• Entity Framework Core with SQL Server
• Repository and service layer separation
• DTO-based data transfer
• AutoMapper usage
• Basic CRUD operations
• Building a portfolio-ready academic project

## Known Limitations

• The current project depends on an existing SQL Server database schema.
• Database migrations or a SQL setup script should be added for easier installation.
• Passwords are currently hashed with MD5 for learning simplicity. For production use, replace it with ASP.NET Core Identity or a modern password hashing approach.
• Some nullable reference warnings may appear during build and can be improved with stronger validation.
• The project is suitable for academic and portfolio use, but it should be hardened before production deployment.

## Future Improvements

• Add complete EF Core migrations or a SQL database script
• Add ASP.NET Core Identity for stronger authentication and authorization
• Add file upload support for books and homework submissions
• Add search, filtering, and pagination for large data lists
• Add dashboard charts for attendance and homework analytics
• Add email or real-time notifications
• Add unit tests and integration tests
• Add deployment support for cloud hosting
• Improve UI consistency and responsiveness

## Author

**Eyamin Khan Emon**

- GitHub: [eyamin-000](https://github.com/eyamin-000)
- LinkedIn: [Eyamin Khan Emon](https://www.linkedin.com/in/eyamin-khan-emon)
- Email: eyaminkhanemon0000@gmail.com

## License

This project is currently prepared for academic and portfolio purposes. Add a license file before using or distributing it as an open-source project.
