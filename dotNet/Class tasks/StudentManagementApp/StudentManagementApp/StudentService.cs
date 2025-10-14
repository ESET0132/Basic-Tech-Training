using System.Data.SqlClient;

namespace StudentManagementApp
{
    public class StudentService
    {
        private readonly DatabaseHelper _dbHelper;

        public StudentService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void ViewAllStudents()
        {
            try
            {
                using (var connection = _dbHelper.CreateConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Students ORDER BY StudentId";

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("\n=== ALL STUDENTS ===");
                        Console.WriteLine("ID\tName\t\tEmail\t\t\tAge\tGrade");
                        Console.WriteLine("--\t----\t\t-----\t\t\t---\t-----");

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["StudentId"]}\t{reader["FirstName"]} {reader["LastName"]}\t\t{reader["Email"]}\t\t{reader["Age"]}\t{reader["Grade"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        
        public void AddStudent()
        {
            try
            {
                Console.WriteLine("\n=== ADD NEW STUDENT ===");

                Console.Write("Enter First Name: ");
                string firstName = Console.ReadLine() ?? "";

                Console.Write("Enter Last Name: ");
                string lastName = Console.ReadLine() ?? "";

                Console.Write("Enter Email: ");
                string email = Console.ReadLine() ?? "";

                Console.Write("Enter Age: ");
                int age = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter Grade (A/B/C/D/F): ");
                string grade = Console.ReadLine() ?? "";

                using (var connection = _dbHelper.CreateConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Students (FirstName, LastName, Email, Age, Grade) VALUES (@FirstName, @LastName, @Email, @Age, @Grade)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@Grade", grade);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("✅ Student added successfully!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

   
        public void UpdateStudent()
        {
            try
            {
                ViewAllStudents();
                Console.Write("\nEnter Student ID to update: ");
                int studentId = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter New First Name: ");
                string firstName = Console.ReadLine() ?? "";

                Console.Write("Enter New Last Name: ");
                string lastName = Console.ReadLine() ?? "";

                Console.Write("Enter New Email: ");
                string email = Console.ReadLine() ?? "";

                Console.Write("Enter New Age: ");
                int age = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter New Grade (A/B/C/D/F): ");
                string grade = Console.ReadLine() ?? "";

                using (var connection = _dbHelper.CreateConnection())
                {
                    connection.Open();
                    string query = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Age = @Age, Grade = @Grade WHERE StudentId = @StudentId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", studentId);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@Grade", grade);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("✅ Student updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("❌ Student not found!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

     
        public void DeleteStudent()
        {
            try
            {
                ViewAllStudents();
                Console.Write("\nEnter Student ID to delete: ");
                int studentId = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Are you sure? (y/n): ");
                string confirm = Console.ReadLine()?.ToLower() ?? "";

                if (confirm == "y")
                {
                    using (var connection = _dbHelper.CreateConnection())
                    {
                        connection.Open();
                        string query = "DELETE FROM Students WHERE StudentId = @StudentId";

                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@StudentId", studentId);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("✅ Student deleted successfully!");
                            }
                            else
                            {
                                Console.WriteLine("❌ Student not found!");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

   
        public void SearchStudents()
        {
            try
            {
                Console.Write("\nEnter student name to search: ");
                string searchName = Console.ReadLine() ?? "";

                using (var connection = _dbHelper.CreateConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Students WHERE FirstName LIKE @SearchName OR LastName LIKE @SearchName ORDER BY StudentId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchName", $"%{searchName}%");

                        using (var reader = command.ExecuteReader())
                        {
                            Console.WriteLine($"\n=== SEARCH RESULTS FOR '{searchName}' ===");
                            Console.WriteLine("ID\tName\t\tEmail\t\t\tAge\tGrade");
                            Console.WriteLine("--\t----\t\t-----\t\t\t---\t-----");

                            bool found = false;
                            while (reader.Read())
                            {
                                found = true;
                                Console.WriteLine($"{reader["StudentId"]}\t{reader["FirstName"]} {reader["LastName"]}\t\t{reader["Email"]}\t\t{reader["Age"]}\t{reader["Grade"]}");
                            }

                            if (!found)
                            {
                                Console.WriteLine("No students found.");
                            }
                        }
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