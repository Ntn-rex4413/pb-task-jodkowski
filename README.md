pb-task-jodkowski
# MovieCatalogAPI
Programming task submitted as part of recruitment process for 2023 summer internship.

Applicant, author: Natan Jodkowski

# Contents
- /MovieCatalogAPI - source code for Movie Catalog web API program and .sln file,
- /MovieCatalogAPI.Tests - source code for unit tests for MovieCatalogAPI project,

# Functionality
MovieCatalogAPI exposes following endpoints for given HTTP request types:
- (POST) /api/movies,
- (GET) /api/movies,
- (GET) /api/movies/{genre},
- (GET) /api/movies/{year},

performing actions specified in task requirements.
Additionally, API specification available @ /swagger

# Implementation overview
Made with .NET 7.0.

Some of additional dependencies:
- Entity Framework Core - InMemory database,
- AutoMapper package,

Tests written with xUnit.
