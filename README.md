HPC Quota Manager

Project Overview

The HPC Quota Manager is a resource allocation and management system for High-Performance Computing (HPC) environments. Built using .NET Core, this project provides a structured API to handle tasks, users, and resource quotas with extensibility for future development.

Current Status

Features Implemented

1. API (CloudTaskManager.API)

Health Check Endpoint:

URL: /Health

Method: GET

Returns a JSON object with the application's health status (healthy) and a timestamp.

Purpose: Confirms the application is running and accessible.

2. Core Logic (CloudTaskManager.Core)

Entities:

CloudTask: Represents a task with fields such as:

Id: Unique identifier.

Title and Description.

CreatedDate and optional DueDate.

Status (using TaskStatus enum).

Priority (using TaskPriority enum).

User: Navigation property linking tasks to users.

User: Represents a user with fields like Id, Name, and relationships to tasks.

Enums:

TaskStatus: Defines task states:

Created, InProgress, OnHold, Completed, Cancelled.

TaskPriority: Categorizes tasks by priority:

Low, Medium, High, Critical.

3. Infrastructure (CloudTaskManager.Infrastructure)

Database Context (ApplicationDbContext):

Handles database operations and entity mappings.

Dependency Injection:

Infrastructure services are configured and registered in AddInfrastructure for use in the application.

Logging:

Serilog is configured to log events to the console.

4. CORS Support

Cross-Origin Resource Sharing (CORS) is enabled to allow interaction with the API from different domains.

Work In Progress

Features Under Development

Repositories and Services:

Implementation of repositories and services for CloudTask and User entities.

These components will handle business logic and interaction with the database.

Database Initialization (DbInitializer):

Currently, initialization logic for pre-seeding database data is planned but not yet complete.

Additional API Endpoints:

Endpoints for managing tasks and users (CRUD operations) are not yet implemented.

Frontend Integration:

No frontend interface or integration has been provided at this stage.

Testing:

Unit and integration tests for the implemented features are not yet in place.

Getting Started

Prerequisites

.NET 6 SDK

SQL Server or any compatible database for storing tasks and users.

Installation

Clone the repository:

git clone https://github.com/KuberCaptain/HPC-Quota-Manager.git

Navigate to the project folder:

cd HPC-Quota-Manager/src/CloudTaskManager.API

Restore dependencies:

dotnet restore

Run the application:

dotnet run

Access the API at http://localhost:5000 (or the port configured in appsettings.json).

Planned Features

Authentication and Authorization:

Add support for user roles and secure endpoints.

Advanced Task Management:

Support for task dependencies and notifications.

Quota System:

Implement logic for managing HPC resource quotas.

Dashboard:

Create a frontend dashboard for managing tasks, users, and quotas.

Contributing

Contributions are welcome! Please follow these steps:

Fork the repository.

Create a new branch for your feature or bug fix.

Commit your changes and submit a pull request.

License

This project is licensed under the MIT License. See the LICENSE file for details.

Contact

For questions or feedback, please reach out via the repository's issues section or email the project maintainer.
