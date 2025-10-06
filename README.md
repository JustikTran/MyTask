# My Task API

## Description
This is a simple RESTful API for managing tasks. It allows you to create, read, update, and delete tasks.

---

## Endpoints
### 1. Get All Tasks
- **URL:** `/task-item/{userId}`
- **Method:** `GET`
- **Description:** Retrieve a list of all tasks of user.
- **Response:**
  - **Status Code:** `200 OK`
  - **Body:**
	```json
	[
	  {
		"id": 1,
		"title": "Sample Task",
		"description": "This is a sample task.",
		"completed": false
	  }
	]
	```
### 2. Get Task by ID
- **URL:** `/task-item/id={id}`
- **Method:** `GET`
- **Description:** Retrieve a specific task by its ID.
- **Response:**
  - **Status Code:** `200 OK`
  - **Body:**
	```json
	{
	  "id": 1,
	  "title": "Sample Task",
	  "description": "This is a sample task.",
	  "completed": false
	}
	```
### 3. Create a New Task
- **URL:** `/task-item/create`
- **Method:** `POST`
- **Description:** Create a new task.
- **Request Body:**
	```json
	{
	  "title": "New Task",
	  "description": "This is a new task."
	}
	```

### 4. Update a Task
- **URL:** `/task-item/update/id={id}`
- **Method:** `PUT`
- **Description:** Update an existing task.
- **Request Body:**
	```json
	{
	  "title": "Updated Task",
	  "description": "This task has been updated.",
	  "completed": true
	}
	```
### 5. Delete a Task
- **URL:** `/task-item/delete/id={id}`
- **Method:** `DELETE`
- **Description:** Delete a task by its ID.
- **Response:**
  - **Status Code:** `204 No Content`
	
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