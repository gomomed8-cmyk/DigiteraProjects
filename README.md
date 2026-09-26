# Job Application Management System

A backend Web API for managing job applications, candidates, and job postings.

The project is built with **ASP.NET Core** and follows **Clean Architecture** principles with **CQRS** and **MediatR**.

## Features

- Job Management
- Candidate Management
- Job Applications
- Application Status Tracking
- JWT Authentication & Authorization
- Role-based Access Control
- CQRS Pattern
- MediatR
- Entity Framework Core
- SQL Server
- Hangfire Background Jobs
- Swagger API Documentation

## Architecture

The solution is organized using Clean Architecture:

```text
JobApplication
│
├── JobApplication.API
├── JobApplication.Application
├── JobApplication.Domain
└── JobApplication.Infrastructure
