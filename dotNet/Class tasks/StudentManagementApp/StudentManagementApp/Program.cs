using StudentManagementApp;

class Program
{
    static void Main(string[] args)
    {
        var dbHelper = new DatabaseHelper();
        var authService = new AuthenticationService(dbHelper);
        var studentService = new StudentService(dbHelper);

        if (!dbHelper.TestConnection())
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
            return;
        }

    
        authService.ShowLoginMenu();

        Console.WriteLine("\n STUDENT MANAGEMENT SYSTEM");
        Console.WriteLine("============================\n");

        ShowMenu(studentService);
    }

    static void ShowMenu(StudentService studentService)
    {
        while (true)
        {
            Console.WriteLine(" MAIN MENU:");
            Console.WriteLine("1. View All Students");
            Console.WriteLine("2. Add New Student");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Search Students");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option (1-6): ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    studentService.ViewAllStudents();
                    break;
                case "2":
                    studentService.AddStudent();
                    break;
                case "3":
                    studentService.UpdateStudent();
                    break;
                case "4":
                    studentService.DeleteStudent();
                    break;
                case "5":
                    studentService.SearchStudents();
                    break;
                case "6":
                    Console.WriteLine("Thank you for using Student Management System. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}