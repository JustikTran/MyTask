# My Task API

## Description
This is a simple RESTful API for managing tasks. It allows you to create, read, update, and delete tasks.

---
## Project Structure

* API/
	* Controllers/ 
	* Domain/
		* Entities/
		* Interfaces/
		* Enum/
	* Application/
		* DTOs/
		* Service/
		* Middleware/
	* Infrastructure/
		* Data/
			* Migrations/
		* Repositories/
---

## Endpoints
### Api Endpoints

Api endpoints for managing tasks. All endpoints require Bearer Authentication JWT. [Api Document](My-Task_API_Documentation.json)
	
### Error Handling
- **404 Not Found:** Returned when a task with the specified ID does not exist.
- **400 Bad Request:** Returned when the request body is invalid or missing required fields.
- **500 Internal Server Error:** Returned when an unexpected error occurs on the server.
- **401 Unauthorized:** Returned when the user is not authenticated.
- **403 Forbidden:** Returned when the user does not have permission to access the resource.
- **409 Conflict:** Returned when there is a conflict with the current state of the resource.
- **422 Unprocessable Entity:** Returned when the request body is well-formed but contains semantic errors.
- **429 Too Many Requests:** Returned when the user has sent too many requests in a given amount of time.
- **503 Service Unavailable:** Returned when the server is temporarily unable to handle the request.
- **504 Gateway Timeout:** Returned when the server did not receive a timely response from an upstream server.
---
## Technologies Used
- ASP.NET Core Web API (.NET 8)
- OData
- PostgreSQL
- Docker
- Bearer Authentication JWT
- Swagger for API documentation
---
## Setup Instructions

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Swagger](https://swagger.io/tools/swagger-ui/) (optional, for API documentation)
- [Git](https://git-scm.com/downloads)

### Clone the Repository
```bash
```

### Run with Docker
```bash
docker-compose up --build
```

--- 

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for details.