# Restaurant API - cURL Commands (Your Local URLs)

## Your API URLs:
- **HTTPS**: https://localhost:7052
- **HTTP**: http://localhost:5281

## Base URLs (choose one)
```bash
BASE_URL="https://localhost:7052"  # HTTPS (recommended)
# OR
BASE_URL="http://localhost:5281"   # HTTP (if HTTPS issues)
```

## 🔥 QUICK START COMMANDS

### 1. Register a Customer
```bash
curl -X POST "https://localhost:7052/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "customer@example.com",
    "password": "password123",
    "firstName": "John",
    "lastName": "Doe",
    "phoneNumber": "+1234567890",
    "role": "Customer"
  }' \
  -k
```

### 2. Register a Restaurant Owner
```bash
curl -X POST "https://localhost:7052/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "owner@restaurant.com",
    "password": "password123",
    "firstName": "Jane",
    "lastName": "Smith",
    "phoneNumber": "+1234567891",
    "role": "RestaurantOwner"
  }' \
  -k
```

### 3. Login as Customer
```bash
curl -X POST "https://localhost:7052/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "customer@example.com",
    "password": "password123"
  }' \
  -k
```

### 4. Login as Restaurant Owner
```bash
curl -X POST "https://localhost:7052/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "owner@restaurant.com",
    "password": "password123"
  }' \
  -k
```

## 📋 COMPLETE TESTING WORKFLOW

### Step 1: Register Users
```bash
# Register Customer
curl -X POST "https://localhost:7052/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{"email": "customer@example.com", "password": "password123", "firstName": "John", "lastName": "Doe", "role": "Customer"}' \
  -k

# Register Restaurant Owner
curl -X POST "https://localhost:7052/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{"email": "owner@restaurant.com", "password": "password123", "firstName": "Jane", "lastName": "Smith", "role": "RestaurantOwner"}' \
  -k
```

### Step 2: Login and Get Tokens
```bash
# Login as Customer (copy the token from response)
curl -X POST "https://localhost:7052/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email": "customer@example.com", "password": "password123"}' \
  -k

# Login as Restaurant Owner (copy the token from response)
curl -X POST "https://localhost:7052/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email": "owner@restaurant.com", "password": "password123"}' \
  -k
```

### Step 3: Test Authentication
```bash
# Get current user info (replace YOUR_TOKEN_HERE with actual token)
curl -X GET "https://localhost:7052/api/auth/me" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -k
```

### Step 4: Create Restaurant (as Owner)
```bash
# Replace YOUR_OWNER_TOKEN with the actual token from step 2
curl -X POST "https://localhost:7052/api/restaurants" \
  -H "Authorization: Bearer YOUR_OWNER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test Restaurant",
    "address": "123 Test Street, Test City, TC 12345",
    "phone": "+1234567890",
    "email": "info@testrestaurant.com",
    "openingDate": "2024-01-01T00:00:00Z"
  }' \
  -k
```

### Step 5: Create Menu Item (as Owner)
```bash
# Replace YOUR_OWNER_TOKEN with actual token and restaurant ID from step 4
curl -X POST "https://localhost:7052/api/menus" \
  -H "Authorization: Bearer YOUR_OWNER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Margherita Pizza",
    "description": "Classic pizza with tomato, mozzarella, and basil",
    "price": 15.99,
    "category": "Pizza",
    "isAvailable": true,
    "restaurantId": 1
  }' \
  -k
```

### Step 6: Place Order (as Customer)
```bash
# Replace YOUR_CUSTOMER_TOKEN with actual token and menu ID from step 5
curl -X POST "https://localhost:7052/api/orders" \
  -H "Authorization: Bearer YOUR_CUSTOMER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "customerName": "John Doe",
    "customerPhone": "+1234567890",
    "customerEmail": "customer@example.com",
    "orderDate": "2024-01-15T12:00:00Z",
    "totalAmount": 15.99,
    "status": "Pending",
    "restaurantId": 1,
    "orderItems": [
      {
        "quantity": 1,
        "unitPrice": 15.99,
        "menuId": 1
      }
    ]
  }' \
  -k
```

## 🔍 PUBLIC ENDPOINTS (No Authentication Required)

### Get All Restaurants
```bash
curl -X GET "https://localhost:7052/api/restaurants" -k
```

### Get All Menus
```bash
curl -X GET "https://localhost:7052/api/menus" -k
```

### Get Menus by Restaurant
```bash
curl -X GET "https://localhost:7052/api/menus/restaurant/1" -k
```

## 👤 CUSTOMER ENDPOINTS

### Get My Orders
```bash
curl -X GET "https://localhost:7052/api/orders/my-orders" \
  -H "Authorization: Bearer YOUR_CUSTOMER_TOKEN" \
  -k
```

## 🏪 RESTAURANT OWNER ENDPOINTS

### Get My Restaurants
```bash
curl -X GET "https://localhost:7052/api/restaurants/my-restaurants" \
  -H "Authorization: Bearer YOUR_OWNER_TOKEN" \
  -k
```

### Get Orders for My Restaurant
```bash
curl -X GET "https://localhost:7052/api/orders/restaurant/1" \
  -H "Authorization: Bearer YOUR_OWNER_TOKEN" \
  -k
```

## 🧪 TESTING ERROR SCENARIOS

### Try to Access Protected Endpoint Without Token
```bash
curl -X GET "https://localhost:7052/api/orders/my-orders" -k
```

### Try to Access Admin Endpoint with Customer Token
```bash
curl -X GET "https://localhost:7052/api/orders" \
  -H "Authorization: Bearer YOUR_CUSTOMER_TOKEN" \
  -k
```

### Try to Create Restaurant with Customer Token
```bash
curl -X POST "https://localhost:7052/api/restaurants" \
  -H "Authorization: Bearer YOUR_CUSTOMER_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"name": "Unauthorized Restaurant", "address": "123 Fake St", "phone": "+1234567890", "email": "fake@restaurant.com", "openingDate": "2024-01-01T00:00:00Z"}' \
  -k
```

## 🚀 ONE-LINER COMMANDS (Copy & Paste Ready)

### Quick Registration
```bash
curl -X POST "https://localhost:7052/api/auth/register" -H "Content-Type: application/json" -d '{"email": "test@example.com", "password": "password123", "firstName": "Test", "lastName": "User", "role": "Customer"}' -k
```

### Quick Login
```bash
curl -X POST "https://localhost:7052/api/auth/login" -H "Content-Type: application/json" -d '{"email": "test@example.com", "password": "password123"}' -k
```

### Quick Public Test
```bash
curl -X GET "https://localhost:7052/api/restaurants" -k
```

## 📝 NOTES:
- **`-k` flag**: Skips SSL certificate verification (needed for localhost HTTPS)
- **Replace tokens**: Copy the actual JWT tokens from login responses
- **Use HTTPS**: https://localhost:7052 (recommended)
- **Alternative HTTP**: http://localhost:5281 (if HTTPS issues, remove `-k` flag)

## 🔧 TROUBLESHOOTING:

### If HTTPS doesn't work, try HTTP:
```bash
curl -X GET "http://localhost:5281/api/restaurants"
```

### For verbose output (debugging):
```bash
curl -v -X GET "https://localhost:7052/api/restaurants" -k
```

### Save response to file:
```bash
curl -X GET "https://localhost:7052/api/restaurants" -k -o restaurants.json
```
