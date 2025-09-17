namespace DataStructureDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> student_list = new List<Student>
            {
                new Student(101, "Shivansh", 20, "A"),
                new Student(102, "Lakshay", 21, "B"),
                new Student(103, "Piyush", 19, "C")
            };

            foreach (Student s in student_list)
            {
                Console.WriteLine($"Student: {s.Name}, ID: {s.Id}, Age: {s.Age}, Grade: {s.Grade}");
            }

            Console.ReadKey();
        }
    }
}
