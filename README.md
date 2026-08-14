#  Saraha API Clone

A robust, secure, and scalable RESTful API built with ASP.NET Core, inspired by the popular anonymous messaging app "Saraha". This project demonstrates clean architecture principles, secure authentication, and efficient database management.

##  Features

* **Secure Authentication:** Implemented JWT (JSON Web Tokens) for secure endpoints.
* **Password Protection:** Passwords are mathematically hashed and salted using `BCrypt`.
* **Anonymous Messaging:** Anyone can send a message, but only the authenticated account owner can read, update, or delete them.
* **Clean Architecture:** Built using the **Service Pattern** to decouple business logic from API Controllers.
* **IDOR Protection:** Strict endpoint authorization ensures users can only access and modify their own data.

##  Tech Stack

* **Framework:** .NET 8 / ASP.NET Core Web API
* **Language:** C#
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core (Code-First Approach)
* **Security:** JWT (JwtBearer), BCrypt.Net
* **Documentation:** Swagger / OpenAPI

##  Architecture
The project strictly follows the **Service/Repository Pattern**:
- `Controllers/`: Handles incoming HTTP requests and routing.
- `Services/`: Contains core business logic and database interactions.
- `DTOs/`: Data Transfer Objects to prevent over-posting and hide internal model structures.
- `Models/`: Database entities mapped via EF Core.

##  API Endpoints Overview

### Users
| Method | Endpoint | Auth Required | Description |
|---|---|--------------|---|
| `POST` | `/api/User/Register` | NO           | Register a new user |
| `POST` | `/api/User/Login` | NO           | Authenticate and receive JWT |
| `GET` | `/api/User/user/{id}` | NO           | Get public user profile |
| `PUT` | `/api/User` |  Yes       | Update account details |
| `DELETE` | `/api/User/{id}` |  Yes       | Delete account |

### Messages
| Method | Endpoint | Auth Required | Description |
|---|---|---|---|
| `POST` | `/api/Message/send` | NO | Send an anonymous message |
| `GET` | `/api/Message/user/{id}` |  Yes | Read received messages |
| `PUT` | `/api/Message/{id}` |  Yes | Update a specific message |
| `DELETE` | `/api/Message/{id}` |  Yes | Delete a specific message |

## API Documentation
<img width="1920" height="1856" alt="SarahaAPI Demo" src="https://github.com/user-attachments/assets/0da77860-85eb-4747-a793-a267fcfe3d16" />

##  How to Run Locally

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/YourUsername/Saraha-API.git](https://github.com/YourUsername/Saraha-API.git)
