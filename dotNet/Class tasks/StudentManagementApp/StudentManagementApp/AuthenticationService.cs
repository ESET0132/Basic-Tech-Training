using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace StudentManagementApp
{
    public class AuthenticationService
    {
        private readonly DatabaseHelper _dbHelper;

        public AuthenticationService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public User? Login(string username, string password)
        {
            try
            {
                using (var connection = _dbHelper.CreateConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Users WHERE Username = @Username AND IsActive = 1";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPassword = reader["Password"].ToString() ?? "";
                                
                                
                                if (VerifyPassword(password, storedPassword))
                                {
                                    return new User
                                    {
                                        UserId = (int)reader["UserId"],
                                        Username = reader["Username"].ToString() ?? "",
                                        Email = reader["Email"].ToString() ?? "",
                                        Role = reader["Role"].ToString() ?? "User",
                                        CreatedDate = (DateTime)reader["CreatedDate"],
                                        IsActive = (bool)reader["IsActive"]
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
            }

            return null;
        }

        public bool Register(string username, string password, string email)
        {
            try
            {
               
                if (UserExists(username))
                {
                    Console.WriteLine("❌ Username already exists!");
                    return false;
                }

                using (var connection = _dbHelper.CreateConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Username, Password, Email, Role, CreatedDate, IsActive) VALUES (@Username, @Password, @Email, @Role, @CreatedDate, @IsActive)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", HashPassword(password));
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Role", "User");
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        command.Parameters.AddWithValue("@IsActive", true);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("✅ Registration successful!");
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Registration error: {ex.Message}");
            }

            return false;
        }

        private bool UserExists(string username)
        {
            try
            {
                using (var connection = _dbHelper.CreateConnection())
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking user existence: {ex.Message}");
                return true; 
            }
        }

        private string HashPassword(string password)
        {
            
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput == storedPassword;
        }

        public void ShowLoginMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n🔐 LOGIN SYSTEM");
                Console.WriteLine("================\n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option (1-3): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        if (PerformLogin())
                        {
                            return; 
                        }
                        break;
                    case "2":
                        PerformRegistration();
                        break;
                    case "3":
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private bool PerformLogin()
        {
            Console.WriteLine("\n=== LOGIN ===");
            Console.Write("Enter Username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Enter Password: ");
            string password = ReadPassword();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("❌ Username and password are required!");
                return false;
            }

            User? user = Login(username, password);
            if (user != null)
            {
                Console.WriteLine($"\n✅ Welcome, {user.Username}!");
                Console.WriteLine($"Role: {user.Role}");
                return true;
            }
            else
            {
                Console.WriteLine("❌ Invalid username or password!");
                return false;
            }
        }

        private void PerformRegistration()
        {
            Console.WriteLine("\n=== REGISTER ===");
            Console.Write("Enter Username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Enter Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Enter Password: ");
            string password = ReadPassword();

            Console.Write("Confirm Password: ");
            string confirmPassword = ReadPassword();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("❌ All fields are required!");
                return;
            }

            if (password != confirmPassword)
            {
                Console.WriteLine("❌ Passwords do not match!");
                return;
            }

            if (password.Length < 6)
            {
                Console.WriteLine("❌ Password must be at least 6 characters long!");
                return;
            }

            Register(username, password, email);
        }

        private string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[0..^1];
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
    }
}
