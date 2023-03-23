# Reservation api

This is a simple .NET Core API that allows users to make and manage reservations. The API utilizes two tables in the database, user and reservation.

API allows to:
* register user
* authenticate user using jwt token
* authorize user over 18 y.o to make CRUD operations


## Prerequisites
* Visual Studio 2022
* .NET Core SDK
* PostgreSQL database

## Running project
1. Clone this repository<br/>
`git clone https://github.com/solracss/reservation-api.git`
2. Navigate to `./ReReservationAPI` folder
3. Run `ReservationAPI.sln`

## Using project

### Database schema
<p align="center">
 <img align ="center "src="https://i.imgur.com/JPsyHgD.png" alt="db schema">
</p>
<br>
Once you have the API running, you can interact with it using a tool like Postman or cURL. 
API will be accessible at url specified in 

`./Properties/launchSettings.json` 
by default is should be 

`https://localhost:7001` or `https://localhost:5009`

### Endpoints

The following endpoints are available in the API:
User Endpoints

    GET /api/users - Get all users.
    GET /api/users/{id} - Get user by ID.
    POST /api/users - Create a new user.
    PUT /api/users/{id} - Update user by ID.
    DELETE /api/users/{id} - Delete user by ID.

Reservation Endpoints

    GET /api/reservations - Get all reservations.
    GET /api/reservations/{id} - Get reservation by ID.
    POST /api/reservations - Create a new reservation.
    PUT /api/reservations/{id} - Update reservation by ID.
    DELETE /api/reservations/{id} - Delete reservation by ID.
