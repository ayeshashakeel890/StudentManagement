# BackEnd Project C.C.P - Student Event Management API

## Overview

The Student Event Management API is a robust and scalable backend solution designed to handle all aspects of event coordination for a student body. Built on a modern .NET stack, this RESTful API provides comprehensive endpoints for managing events, handling participant registrations, and collecting user feedback.

The project is architected following **Clean Architecture** principles, ensuring a clear separation of concerns, high maintainability, and testability. This makes it an ideal foundation for any complex web or mobile application.

---

## Core Features

*   **Event Management (CRUD):** Full support for creating, reading, updating, and deleting events.
*   **Participant Registration:** A secure endpoint for students to register for events, implementing a many-to-many relationship between students and events.
*   **Feedback System:** Allows registered attendees to submit ratings and comments for events they have attended, with business logic to prevent submissions for future events.
*   **Advanced Querying:** Dynamic filtering and sorting capabilities for the event list, allowing users to search by keywords (name, venue) and sort by various criteria (date, name).
*   **RESTful Design:** Follows REST API design standards, using proper HTTP verbs, status codes, and a logical resource-based URL structure.
*   **API Documentation:** Automatically generated and interactive API documentation using **Swagger (OpenAPI)**.

---

## Technical Stack & Architecture

This project demonstrates proficiency in modern backend development practices and technologies.

*   **Framework:** .NET 9.0 / ASP.NET Core
*   **Data Access:** Entity Framework Core 8.0+
*   **Database:** Microsoft SQL Server
*   **Architecture:**
    *   **Clean Architecture:** The solution is logically separated into four distinct layers:
        1.  **Domain:** Contains the core business entities and has no external dependencies.
        2.  **Application:** Holds the business logic, services, DTOs, and interfaces.
        3.  **Infrastructure:** Manages external resources, primarily the database connection and data persistence via EF Core.
        4.  **API (Presentation):** The ASP.NET Core project that exposes the functionality via RESTful endpoints.
    *   **Dependency Injection:** Extensively used to decouple services and promote modular, testable code.
    *   **Repository & Unit of Work Pattern (Implicit):** Leveraged through EF Core's `DbContext` and `DbSet` to abstract data access logic.
    *   **Data Transfer Objects (DTOs):** Used to decouple the API's public-facing data contracts from the internal domain models, improving security and stability.

---

## Getting Started

Follow these instructions to get a local copy up and running for development and testing purposes.

### Prerequisites

*   [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
*   [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or another compatible IDE
*   [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express, Developer, or full version)

### Installation & Setup

1.  **Clone the Repository**
    ```sh
    git clone https://github.com/[Your-GitHub-Username]/StudentManagementCCP.git
    cd StudentManagementCCP
    ```

2.  **Configure Database Connection**
    *   Open the solution (`StudentEventManagement.sln`) in Visual Studio 2022.
    *   In the `StudentEventManagement.API` project, open the `appsettings.Development.json` file.
    *   Update the `DefaultConnection` string to point to your SQL Server instance. The default is configured for SQL Express:
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=StudentEventDb_Portfolio;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
        }
        ```

3.  **Apply Database Migrations**
    *   In Visual Studio, open the **Package Manager Console** (`Tools > NuGet Package Manager > Package Manager Console`).
    *   Set the **Default project** dropdown to `StudentEventManagement.Infrastructure`.
    *   Run the following command to create the database and its schema:
        ```powershell
        Update-Database
        ```

4.  **Run the Application**
    *   Set `StudentEventManagement.API` as the startup project.
    *   Press `F5` or click the "Play" button to launch the API.
    *   The application will start, and a browser window will automatically open to the **Swagger UI** (`/swagger/index.html`), where you can explore and test all available API endpoints.

---

