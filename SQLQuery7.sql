-- Create a new database
CREATE DATABASE Store;
USE Store;


-- Create a table for products
CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    price DECIMAL(10, 2)
);

-- Insert data
INSERT INTO products (product_id, product_name, price) VALUES
(1, 'Laptop', 1200.00),
(2, 'Mouse', 25.50),
(3, 'Keyboard', 75.00);

-- Use sp_help to see the table details, columns, and constraints
EXEC sp_help 'products';

-- Rename the 'price' column to 'unit_price' using sp_rename
-- The 'COLUMN' argument is important to specify what you are renaming
EXEC sp_rename 'products.price', 'unit_price', 'COLUMN';

-- Use sp_help again to see the updated table structure with the new column name
EXEC sp_help 'products';

-- Select all data to show the table and its columns
SELECT * FROM products;

