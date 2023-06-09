![](https://img.shields.io/badge/version-1.0-Green)<br /><br />

# Reservation api

This is a simple .NET Core API that allows users to make and manage reservations.

Main features:

- register new user
- authenticate user using jwt token
- authorize logged user over 18 y.o. to make CRUD operations
- admin user can view all users and reservations
- all records are presented as paginated results (by default 10 items per page, max 50)
- records can be filtered 
    - `reservations` by id, email and start date
    - `users` by email, first and last name
- and sorted
    - `reservations` by id, email and start date
    - `users` by id, email and date of birth

## Prerequisites

- Visual Studio 2022
- .NET Core 7.0
- PostgreSQL database

## Running project

1. Clone this repository<br/>
   `git clone https://github.com/solracss/reservation-api.git`
2. Run `ReservationAPI.sln`
3. Set [environment variables](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows) in `appsettings.json` for 
    - connection string to database
    - jwt key

## Using project

### Database schema

<p align="center">
 <img align ="center "src="https://i.imgur.com/8DcBQEt.png" alt="db schema">
</p>
<br>
Once you have the API running, you can interact with it using a tool like Postman or cURL. 
API will be accessible at url specified in

`./Properties/launchSettings.json`

### Endpoints

The following endpoints are available in the API:

User Endpoints

    GET /api/users - Get all users.
    GET /api/users/{id} - Get user by ID.

Account Endpoints

    GET /api/account/register - Register new user.
    GET /api/account/login - Login to app.

Reservation Endpoints

    GET /api/reservations - Get all reservations.
    GET /api/reservations/{id} - Get reservation by ID.
    POST /api/reservations - Create a new reservation.
    PUT /api/reservations/{id} - Update reservation by ID.
    DELETE /api/reservations/{id} - Delete reservation by ID.

Full endpoints documentation available at `https://localhost:PORT_NUMBER/swagger/index.html`

## Tech Stack

- [.NetCore 7.0](https://learn.microsoft.com/en-us/dotnet/core/introduction)
- [Auto Mapper](https://automapper.org/)
- [Entity FrameworkCore](https://learn.microsoft.com/en-us/ef/core/)
- [PostgreSQL](https://www.postgresql.org/)
- [Fluent Validation](https://docs.fluentvalidation.net/en/latest/)
