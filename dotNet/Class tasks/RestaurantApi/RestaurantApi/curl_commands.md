# Restaurant API - cURL Commands for JWT Authentication

## Base URL
Replace `https://localhost:7000` with your actual API URL if different.

```bash
BASE_URL="https://localhost:7000"
```

## 1. AUTHENTICATION ENDPOINTS

### Register a Customer
```bash
curl -X POST "$BASE_URL/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "customer@example.com",
    "password": "password123",
    "firstName": "John",
    "lastName": "Doe",
    "phoneNumber": "+1234567890",
    "role": "Customer"
  }'
```

### Register a Restaurant Owner
```bash
curl -X POST "$BASE_URL/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "owner@restaurant.com",
    "password": "password123",
    "firstName": "Jane",
    "lastName": "Smith",
    "phoneNumber": "+1234567891",
    "role": "RestaurantOwner"
  }'
```

### Register an Admin
```bash
curl -X POST "$BASE_URL/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@restaurant.com",
    "password": "password123",
    "firstName": "Admin",
    "lastName": "User",
    "phoneNumber": "+1234567892",
    "role": "Admin"
  }'
```

### Login as Customer
```bash
curl -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "customer@example.com",
    "password": "password123"
  }'
```

### Login as Restaurant Owner
```bash
curl -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "owner@restaurant.com",
    "password": "password123"
  }'
```

### Login as Admin
```bash
curl -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@restaurant.com",
    "password": "password123"
  }'
```

### Get Current User Info
```bash
# Replace YOUR_TOKEN_HERE with the actual JWT token from login response
curl -X GET "$BASE_URL/api/auth/me" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

## 2. PUBLIC ENDPOINTS (No Authentication Required)

### Get All Restaurants
```bash
curl -X GET "$BASE_URL/api/restaurants"
```

### Get Specific Restaurant
```bash
curl -X GET "$BASE_URL/api/restaurants/1"
```

### Get All Menus
```bash
curl -X GET "$BASE_URL/api/menus"
```

### Get Specific Menu
```bash
curl -X GET "$BASE_URL/api/menus/1"
```

### Get Menus by Restaurant
```bash
curl -X GET "$BASE_URL/api/menus/restaurant/1"
```

## 3. CUSTOMER ENDPOINTS

### Create a New Order (Customer Only)
```bash
curl -X POST "$BASE_URL/api/orders" \
  -H "Authorization: Bearer YOUR_CUSTOMER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "customerName": "John Doe",
    "customerPhone": "+1234567890",
    "customerEmail": "customer@example.com",
    "orderDate": "2024-01-15T12:00:00Z",
    "totalAmount": 25.50,
    "status": "Pending",
    "restaurantId": 1,
    "orderItems": [
      {
        "quantity": 2,
        "unitPrice": 12.75,
        "menuId": 1
      }
    ]
  }'
```

### Get My Orders (Customer Only)
```bash
curl -X GET "$BASE_URL/api/orders/my-orders" \
  -H "Authorization: Bearer YOUR_CUSTOMER_TOKEN"
```

## 4. RESTAURANT OWNER ENDPOINTS

### Create a New Restaurant (Restaurant Owner or Admin Only)
```bash
curl -X POST "$BASE_URL/api/restaurants" \
  -H "Authorization: Bearer YOUR_OWNER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "My Restaurant",
    "address": "123 Main St, City, State 12345",
    "phone": "+1234567890",
    "email": "info@myrestaurant.com",
    "openingDate": "2024-01-01T00:00:00Z"
  }'
```

### Get My Restaurants (Restaurant Owner or Admin Only)
```bash
curl -X GET "$BASE_URL/api/restaurants/my-restaurants" \
  -H "Authorization: Bearer YOUR_OWNER_TOKEN"
```

### Create a New Menu Item (Restaurant Owner or Admin Only)
```bash
curl -X POST "$BASE_URL/api/menus" \
  -H "Authorization: Bearer YOUR_OWNER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Pizza Margherita",
    "description": "Classic pizza with tomato, mozzarella, and basil",
    "price": 12.99,
    "category": "Pizza",
    "isAvailable": true,
    "restaurantId": 1
  }'
```

### Get Orders for My Restaurant (Restaurant Owner or Admin Only)
```bash
curl -X GET "$BASE_URL/api/orders/restaurant/1" \
  -H "Authorization: Bearer YOUR_OWNER_TOKEN"
```

## 5. ADMIN ENDPOINTS

### Get All Orders (Admin Only)
```bash
curl -X GET "$BASE_URL/api/orders" \
  -H "Authorization: Bearer YOUR_ADMIN_TOKEN"
```

## 6. TESTING ERROR SCENARIOS

### Try to Access Protected Endpoint Without Token
```bash
curl -X GET "$BASE_URL/api/orders/my-orders"
```

### Try to Access Admin Endpoint with Customer Token
```bash
curl -X GET "$BASE_URL/api/orders" \
  -H "Authorization: Bearer YOUR_CUSTOMER_TOKEN"
```

### Try to Create Restaurant with Customer Token
```bash
curl -X POST "$BASE_URL/api/restaurants" \
  -H "Authorization: Bearer YOUR_CUSTOMER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Unauthorized Restaurant",
    "address": "123 Fake St",
    "phone": "+1234567890",
    "email": "fake@restaurant.com",
    "openingDate": "2024-01-01T00:00:00Z"
  }'
```

### Try to Register with Existing Email
```bash
curl -X POST "$BASE_URL/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "customer@example.com",
    "password": "password123",
    "firstName": "Duplicate",
    "lastName": "User",
    "role": "Customer"
  }'
```

### Try to Login with Wrong Credentials
```bash
curl -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "customer@example.com",
    "password": "wrongpassword"
  }'
```

### Try to Create Menu for Restaurant You Don't Own
```bash
curl -X POST "$BASE_URL/api/menus" \
  -H "Authorization: Bearer YOUR_CUSTOMER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Unauthorized Menu",
    "description": "This should fail",
    "price": 9.99,
    "category": "Test",
    "isAvailable": true,
    "restaurantId": 1
  }'
```

## 7. COMPLETE TESTING WORKFLOW

### Step 1: Register Users
```bash
# Register Customer
curl -X POST "$BASE_URL/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{"email": "customer@example.com", "password": "password123", "firstName": "John", "lastName": "Doe", "role": "Customer"}'

# Register Restaurant Owner
curl -X POST "$BASE_URL/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{"email": "owner@restaurant.com", "password": "password123", "firstName": "Jane", "lastName": "Smith", "role": "RestaurantOwner"}'
```

### Step 2: Login and Get Tokens
```bash
# Login as Customer (copy the token from response)
curl -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email": "customer@example.com", "password": "password123"}'

# Login as Restaurant Owner (copy the token from response)
curl -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email": "owner@restaurant.com", "password": "password123"}'
```

### Step 3: Create Restaurant (as Owner)
```bash
# Replace YOUR_OWNER_TOKEN with the actual token from step 2
curl -X POST "$BASE_URL/api/restaurants" \
  -H "Authorization: Bearer YOUR_OWNER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"name": "Test Restaurant", "address": "123 Test St", "phone": "+1234567890", "email": "test@restaurant.com", "openingDate": "2024-01-01T00:00:00Z"}'
```

### Step 4: Create Menu Item (as Owner)
```bash
# Replace YOUR_OWNER_TOKEN with the actual token and restaurant ID from step 3
curl -X POST "$BASE_URL/api/menus" \
  -H "Authorization: Bearer YOUR_OWNER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"name": "Test Pizza", "description": "A test pizza", "price": 15.99, "category": "Pizza", "isAvailable": true, "restaurantId": 1}'
```

### Step 5: Place Order (as Customer)
```bash
# Replace YOUR_CUSTOMER_TOKEN with the actual token and menu ID from step 4
curl -X POST "$BASE_URL/api/orders" \
  -H "Authorization: Bearer YOUR_CUSTOMER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"customerName": "John Doe", "customerPhone": "+1234567890", "customerEmail": "customer@example.com", "orderDate": "2024-01-15T12:00:00Z", "totalAmount": 15.99, "status": "Pending", "restaurantId": 1, "orderItems": [{"quantity": 1, "unitPrice": 15.99, "menuId": 1}]}'
```

## 8. USEFUL CURL OPTIONS

### Save Response to File
```bash
curl -X GET "$BASE_URL/api/restaurants" -o restaurants.json
```

### Show Headers in Response
```bash
curl -X GET "$BASE_URL/api/restaurants" -i
```

### Verbose Output (for debugging)
```bash
curl -X GET "$BASE_URL/api/restaurants" -v
```

### Skip SSL Certificate Verification (for development)
```bash
curl -X GET "$BASE_URL/api/restaurants" -k
```

## Notes:
- Replace `YOUR_TOKEN_HERE`, `YOUR_CUSTOMER_TOKEN`, `YOUR_OWNER_TOKEN`, `YOUR_ADMIN_TOKEN` with actual JWT tokens from login responses
- Replace `https://localhost:7000` with your actual API URL
- The JWT tokens expire after 60 minutes (configurable)
- Use `-k` flag if you're using self-signed certificates in development
- Use `-v` flag for verbose output to see request/response details
