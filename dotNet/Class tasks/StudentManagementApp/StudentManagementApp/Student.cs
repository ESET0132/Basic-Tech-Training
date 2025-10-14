namespace StudentManagementApp
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Grade { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}