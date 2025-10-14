using System.Data.SqlClient;

namespace StudentManagementApp
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = "Server=LAPTOP-F7UBI3KG\\SQLEXPRESS;Database=StudentManagement;Trusted_Connection=true;TrustServerCertificate=true;";
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    Console.WriteLine("✅ Connected to database successfully!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Connection failed: {ex.Message}");
                return false;
            }
        }
    }
}