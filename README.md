# Library Management System

## Overview
This project is a Library Management System built using **ASP.NET Core** following the **Onion Architecture** with the **Repository** and **Unit of Work** patterns. The system separates library management from system user management while implementing role-based authorization using JWT authentication.

## Requirements

### 1. Separate Databases
- Create **two separate databases**:
  - **System Users Database** (Authentication & Authorization)
  - **Library Management Database** (Books, Members, Borrowing, Returns, etc.)

### 2. Database Indexing
Create indexes on the following columns to improve query performance:
-Performance-critical properties are indexed to prevent full table scans

### 3. Data Seeding
Seed the database with the **first Administrator** account.

The initial administrator will have permission to:
- Create other administrators.
- Create librarians.
- Create staff members.

### 4. Data Access Layer
Each database should have its own:

- DbContext
- Repository Layer
- Unit of Work

This ensures complete separation of concerns between authentication and library management.

### 5. Base Controller
Create a **Base Controller** that contains shared endpoints and common functionality used by both:

- Members
- System Users

### 6. Admin Controllers
Create dedicated Admin Controllers containing operations that are accessible only by:

- Administrator
- Librarian

### 7. Administrator Permissions
Only an **Administrator** can:

- Create another Administrator.
- Delete any system user.

### 8. Administrator & Librarian Permissions
Administrators and Librarians can:

- Create Librarian accounts.
- Create Staff accounts.
- Add,Update,Delete => Books,Authors,Publishers,Categories,and Languages

### 9. Default Registration Role
Any user who registers through the system will automatically receive the **Member** role.

### 10. JWT Authentication
During login:

- Generate a JWT token.
- Store the user's role as an **encrypted claim** within the JWT token.

### 11. System Roles
The system supports four roles:

- Admin
- Librarian
- Staff
- Member

### 12. Architecture
The project follows:

- Onion Architecture
- Repository Pattern
- Unit of Work Pattern

These architectural patterns provide:

- Separation of concerns
- Maintainability
- Testability
- Scalability
- Clean dependency management

## Role Permissions Summary

| Operation | Admin | Librarian | Staff | Member |
|----------|:-----:|:---------:|:-----:|:------:|
| Create Admin | ✅ | ❌ | ❌ | ❌ |
| Create Librarian | ✅ | ✅ | ❌ | ❌ |
| Create Staff | ✅ | ✅ | ❌ | ❌ |
| Delete Users | ✅ | ❌ | ❌ | ❌ |
| Library Management Operations | ✅ | ✅ | ✅* | ❌ |
| Register | ✅ | ✅ | ✅ | ✅ |

> *Staff permissions are limited to the operations assigned by the system requirements.

## Tech Stack

- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT Authentication
- Onion Architecture
- Repository Pattern
- Unit of Work Pattern
