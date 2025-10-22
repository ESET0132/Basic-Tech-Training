# 🔧 Database Fix Instructions

## Problem
You're getting: `"Internal server error: Invalid object name 'Users'."`

This happens because we added the `Users` table to the code but the database hasn't been updated yet.

## 🚨 Quick Fix (Recommended)

### Step 1: Stop the API
1. Go to your terminal where the API is running
2. Press `Ctrl+C` to stop the application

### Step 2: Delete the Database Manually
1. Open **SQL Server Management Studio (SSMS)** or **Azure Data Studio**
2. Connect to your SQL Server instance: `LAPTOP-F7UBI3KG\SQLEXPRESS`
3. Find the database `MyRestaurant`
4. Right-click on `MyRestaurant` database
5. Select **"Delete"** or **"Drop"**
6. Confirm the deletion

### Step 3: Restart the API
1. Go back to your project directory
2. Run: `dotnet run`
3. The application will automatically recreate the database with all tables including `Users`

## Alternative Fix (Using Command Line)

### Step 1: Stop the API
Press `Ctrl+C` in the terminal where the API is running

### Step 2: Delete Database via Command Line
```bash
# Navigate to your project directory
cd "C:\Users\user\Codes\Basic-Tech-Training\dotNet\Class tasks\RestaurantApi\RestaurantApi"

# Drop the database
dotnet ef database drop --force
```

### Step 3: Restart the API
```bash
dotnet run
```

## What Happens Next
- The application will automatically create a new database
- All tables will be created including the new `Users` table
- You can then test the authentication endpoints

## Test After Fix
Once the API is running again, test with:

```bash
curl -X POST "https://localhost:7052/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "password123",
    "firstName": "Test",
    "lastName": "User",
    "role": "Customer"
  }' \
  -k
```

## If You Still Get Errors
1. Make sure the API is completely stopped
2. Wait a few seconds
3. Try the database deletion again
4. Restart the API

The key is to stop the running application first before making database changes!
