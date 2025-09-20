using System;
using System.Data.SqlClient;

namespace TestDBMSConnectionApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SQL Server Connection Test");

            // Try connecting to the likely instance name
            string connString = @"Server=.\SQLEXPRESS;Database=master;Integrated Security=True;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    Console.WriteLine("Trying to connect to SQL Server Express...");
                    conn.Open();

                    Console.WriteLine("SUCCESS! Connected to SQL Server.");
                    Console.WriteLine($"Server version: {conn.ServerVersion}");

                    // Create our database and table
                    CreateDatabaseAndTable(conn);

                    // Close and reopen connection to our specific database
                    conn.Close();
                }

                // Now connect to our specific database
                string myDbConnString = @"Server=.\SQLEXPRESS;Database=mydatabase;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(myDbConnString))
                {
                    conn.Open();
                    Console.WriteLine("Connected to 'mydatabase'.");

                    // Show menu of operations
                    DisplayMenu(conn);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine("\nThis usually means:");
                Console.WriteLine("1. SQL Server is not installed");
                Console.WriteLine("2. SQL Server is not running");
                Console.WriteLine("3. The instance name is incorrect");

                Console.WriteLine("\nPlease install SQL Server Express from:");
                Console.WriteLine("https://www.microsoft.com/en-us/sql-server/sql-server-downloads");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void CreateDatabaseAndTable(SqlConnection conn)
        {
            // Create database if it doesn't exist
            string createDbSql = @"
            IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'mydatabase')
            BEGIN
                CREATE DATABASE mydatabase;
                PRINT 'Database created successfully.';
            END";

            using (SqlCommand cmd = new SqlCommand(createDbSql, conn))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Database checked/created.");
            }

            // Switch to the new database to create table
            string useDbSql = "USE mydatabase";
            using (SqlCommand cmd = new SqlCommand(useDbSql, conn))
            {
                cmd.ExecuteNonQuery();
            }

            // Create table if it doesn't exist
            string createTableSql = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='testtable' AND xtype='U')
            BEGIN
                CREATE TABLE testtable (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Name VARCHAR(50) NOT NULL
                );
                PRINT 'Table created successfully.';
            END";

            using (SqlCommand cmd = new SqlCommand(createTableSql, conn))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table checked/created.");
            }
        }

        static void DisplayMenu(SqlConnection conn)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n===== DATABASE MENU =====");
                Console.WriteLine("1. Add staff member");
                Console.WriteLine("2. View all staff");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option (1-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        InsertStaff(conn);
                        break;
                    case "2":
                        DisplayStaff(conn);
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void InsertStaff(SqlConnection conn)
        {
            Console.Write("Enter staff name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            string sql = "INSERT INTO testtable (Name) VALUES (@name)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        Console.WriteLine("Staff added successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DisplayStaff(SqlConnection conn)
        {
            string sql = "SELECT * FROM testtable";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine("\nStaff List:");
                        Console.WriteLine("ID\tName");
                        Console.WriteLine("--\t----");

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Id"]}\t{reader["Name"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No staff members found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}