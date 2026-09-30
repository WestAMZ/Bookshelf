# Bookshelf

A full-stack digital library and book catalog application designed to manage authors, books, genres, and user interaction in a clean, modern web experience.

This project highlights a production-style architecture that combines a .NET backend with a React frontend, demonstrating REST API development, relational database modeling, authentication, and component-driven UI design.

## Project Summary

Bookshelf is a personal full-stack application built to simulate a modern reading platform where users can browse books, search by metadata, view author details, and authenticate securely. The solution is organized as a monorepo-style project with a dedicated API layer and a separate frontend application, making it easy to evolve, test, and maintain.

## Architecture

### Backend
- ASP.NET Core 6 Web API
- C# and .NET Web application fundamentals
- Entity Framework Core for ORM and migrations
- SQL Server (LocalDB configuration in development)
- ASP.NET Core Identity for user management
- JWT-based authentication and authorization
- Swagger / OpenAPI for API documentation
- AutoMapper for DTO mapping
- Custom exception handling and HTTP response middleware

### Frontend
- React 19
- TypeScript
- Vite for fast development and build tooling
- React Router for client-side navigation
- Tailwind CSS for styling
- Axios for HTTP requests
- React Icons for interface elements

### Data Model
The application is centered around a relational library domain, including entities such as:
- Books
- Authors
- Genres
- Comments
- User accounts and identity data
- Join tables for many-to-many relationships between books, authors, and genres

This creates a scalable and structured foundation for catalog, search, and content management features.

## Features

- Browse and view book details
- Search books by title, author, genre, and date range
- Manage author and book relationships
- View author detail pages with associated books
- User registration and login
- JWT authentication for protected routes
- Role-based access patterns with admin claims
- API documentation with Swagger UI
- Seeded sample data for a ready-to-run library experience

## Project Structure

```text
Bookshelf/
├── backend/
│   └── BookshelfAPI/
│       ├── Controllers/
│       ├── Data/
│       ├── DTOs/
│       ├── Entities/
│       ├── Filters/
│       ├── Migrations/
│       ├── Middlewares/
│       ├── Utilities/
│       ├── Validations/
│       ├── ApplicationDbContext.cs
│       ├── Program.cs
│       ├── appsettings.json
│       ├── appsettings.Development.json
│       └── BookshelfAPI.csproj
├── frontend/
│   ├── public/
│   ├── src/
│   ├── package.json
│   ├── vite.config.ts
│   ├── tsconfig.json
│   └── index.html
├── AGENTS.MD
├── README.md
└── .gitignore
```

## Tech Stack

| Layer | Technologies |
| --- | --- |
| API | ASP.NET Core Web API, C#, JWT, Swagger |
| Data Access | Entity Framework Core, SQL Server / LocalDB |
| Identity | ASP.NET Core Identity |
| Frontend | React, TypeScript, Vite |
| Styling | Tailwind CSS |
| Routing | React Router |
| HTTP Client | Axios |
| Development | .NET SDK, Node.js, npm/pnpm |

## Getting Started

### Prerequisites
- .NET 6 SDK
- Node.js 18+ or later
- SQL Server LocalDB (configured by default for development)
- npm or pnpm

### Backend
From the project root, run:

```bash
dotnet restore
cd backend/BookshelfAPI
dotnet run
```

The API will initialize the database and seed default data on startup when the development environment is used.

### Frontend
From the project root, run:

```bash
cd frontend
npm install
npm run dev
```

The frontend is configured to run on:

```text
http://localhost:5001
```

> The backend and frontend are intentionally configured to work together in a local development setup, with CORS enabled for the Vite frontend.

## Notes for Development

- The default local database connection is defined in the development app settings and uses SQL Server LocalDB.
- If you switch to another database provider or connection string, update the configuration in the backend settings files.
- API documentation can usually be accessed through Swagger when the backend is running in development mode.

## Why This Project Matters

This project demonstrates practical full-stack development skills across multiple layers of a modern web application:
- API design and controller architecture
- Database modeling and migrations
- Authentication and security patterns
- Client-side state and routing
- Integration between frontend and backend services

It is a strong example of a portfolio-ready application that balances clean structure, real-world functionality, and modern software engineering practices.

## License

This project is intended for learning and portfolio demonstration purposes.
