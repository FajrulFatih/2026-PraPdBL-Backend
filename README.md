# PraPdBL Backend

## Description
Backend service for Project PraPdBL. This service provides REST APIs for core domain operations and data access.

## Features
- Booking management endpoints.
- Room management endpoints.
- User management endpoints.
- Database access via Entity Framework Core.
- Azure AD authentication support.

## Tech Stack
- ASP.NET Core (.NET 9)
- Entity Framework Core 9
- Pomelo MySQL provider

## Installation
1. Install .NET SDK 9.
2. Configure the database connection string.
3. Restore dependencies:
	- `dotnet restore`
4. Apply migrations (if needed):
	- `dotnet ef database update`

## Usage
- Run the API:
  - `dotnet run`
- Use the exposed endpoints in `Controllers/` for bookings, rooms, and users.

## Environment Variables
Configuration can be set via `appsettings.json`, `appsettings.Development.json`, or User Secrets.

- `ConnectionStrings__DefaultConnection`: MySQL connection string.
- `AzureAd__Scopes`: API scopes.
- `AzureAd__CallbackPath`: OIDC callback path.
- `Cors__AllowedOrigins`: Allowed CORS origins array.

## Contributing (optional)
See the contribution guide in `../PraPdBL-Docs/CONTRIBUTING.md`.

## License
TBD.
