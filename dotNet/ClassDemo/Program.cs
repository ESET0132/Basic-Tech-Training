namespace ClassDemp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            Student stud = new Student();
            stud.initialize();
            stud.display();
            int id = stud.get_student_id();
            Console.WriteLine("student id from main is: " + id);
            double totalMarkss = stud.totalMarks(90, 20, 70);
            Console.WriteLine("Total marks obtained is: " + totalMarkss);
            double avgM = stud.avgMarks(90, 20, 70);
            Console.WriteLine("avg marks obtained is: " + avgM);
        }
    }
}
