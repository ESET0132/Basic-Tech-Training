# Restaurant API - JWT Authentication System

## Overview

This Restaurant API now includes a comprehensive JWT (JSON Web Token) authentication system with role-based authorization. The system supports three user roles: **Customer**, **RestaurantOwner**, and **Admin**.

## Features

- **User Registration & Login** with email/password
- **JWT Token Authentication** with configurable expiry
- **Role-based Authorization** (Customer, RestaurantOwner, Admin)
- **Password Hashing** using BCrypt
- **Secure API Endpoints** with proper authorization
- **User Management** with profile information

## User Roles & Permissions

### Customer
- View restaurants and menus (public)
- Place orders
- View own orders
- Manage own profile

### RestaurantOwner
- All Customer permissions
- Create and manage restaurants
- Create and manage menu items
- View orders for owned restaurants

### Admin
- All permissions
- View all orders across the system
- Full system access

## API Endpoints

### Authentication Endpoints

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Register new user | None |
| POST | `/api/auth/login` | Login user | None |
| GET | `/api/auth/me` | Get current user info | Authenticated |

### Public Endpoints (No Authentication Required)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/restaurants` | Get all restaurants |
| GET | `/api/restaurants/{id}` | Get specific restaurant |
| GET | `/api/menus` | Get all menus |
| GET | `/api/menus/{id}` | Get specific menu |
| GET | `/api/menus/restaurant/{restaurantId}` | Get menus by restaurant |

### Customer Endpoints

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| POST | `/api/orders` | Create new order | Customer |
| GET | `/api/orders/my-orders` | Get own orders | Customer |

### Restaurant Owner Endpoints

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| POST | `/api/restaurants` | Create restaurant | RestaurantOwner, Admin |
| GET | `/api/restaurants/my-restaurants` | Get owned restaurants | RestaurantOwner, Admin |
| POST | `/api/menus` | Create menu item | RestaurantOwner, Admin |
| GET | `/api/orders/restaurant/{restaurantId}` | Get restaurant orders | RestaurantOwner, Admin |

### Admin Endpoints

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| GET | `/api/orders` | Get all orders | Admin |

## Configuration

### JWT Settings (appsettings.json)

```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "RestaurantApi",
    "Audience": "RestaurantApiUsers",
    "ExpiryMinutes": 60
  }
}
```

### Database Schema

The system includes a new `Users` table with the following structure:

- **Id**: Primary key
- **Email**: Unique email address
- **PasswordHash**: BCrypt hashed password
- **FirstName**: User's first name
- **LastName**: User's last name
- **PhoneNumber**: Optional phone number
- **Role**: User role (Customer, RestaurantOwner, Admin)
- **CreatedAt**: Account creation timestamp
- **IsActive**: Account status

## Usage Examples

### 1. Register a New User

```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "customer@example.com",
  "password": "password123",
  "firstName": "John",
  "lastName": "Doe",
  "phoneNumber": "+1234567890",
  "role": "Customer"
}
```

### 2. Login

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "customer@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "customer@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "role": "Customer",
  "expiresAt": "2024-01-15T13:00:00Z"
}
```

### 3. Use Token in API Calls

```http
GET /api/orders/my-orders
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### 4. Create a Restaurant (Restaurant Owner)

```http
POST /api/restaurants
Authorization: Bearer <restaurant_owner_token>
Content-Type: application/json

{
  "name": "My Restaurant",
  "address": "123 Main St, City, State 12345",
  "phone": "+1234567890",
  "email": "info@myrestaurant.com",
  "openingDate": "2024-01-01T00:00:00Z"
}
```

## Security Features

1. **Password Hashing**: All passwords are hashed using BCrypt
2. **JWT Tokens**: Secure token-based authentication
3. **Role-based Access**: Endpoints are protected based on user roles
4. **Token Expiry**: Tokens expire after 60 minutes (configurable)
5. **Input Validation**: All inputs are validated using Data Annotations
6. **SQL Injection Protection**: Entity Framework provides protection
7. **HTTPS**: Configured for secure communication

## Error Handling

The API returns appropriate HTTP status codes and error messages:

- **400 Bad Request**: Invalid input data
- **401 Unauthorized**: Missing or invalid token
- **403 Forbidden**: Insufficient permissions
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Server-side errors

## Testing

Use the provided `RestaurantApi_Authentication.http` file to test all endpoints. The file includes examples for:

- User registration and login
- All API endpoints with proper authorization
- Error scenarios and invalid requests
- Different user roles and permissions

## Getting Started

1. **Install Dependencies**: The required packages are already added to the project
2. **Configure JWT Settings**: Update the JWT settings in `appsettings.json`
3. **Run Database Migration**: The system will automatically create the database schema
4. **Register Users**: Use the registration endpoint to create test users
5. **Test Endpoints**: Use the provided HTTP test file to verify functionality

## Database Migration

The system will automatically create the Users table when the application starts. If you need to recreate the database:

1. Delete the existing database
2. Run the application - it will recreate all tables including Users
3. Register new users through the API

## Production Considerations

1. **Change JWT Secret Key**: Use a strong, unique secret key in production
2. **Use Environment Variables**: Store sensitive configuration in environment variables
3. **Enable HTTPS**: Ensure HTTPS is enabled in production
4. **Database Security**: Use proper database security measures
5. **Token Refresh**: Consider implementing token refresh for better user experience
6. **Rate Limiting**: Add rate limiting to prevent abuse
7. **Logging**: Implement proper logging for security monitoring
