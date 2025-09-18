using System;
using System.Collections.Generic;

namespace Data_structure_demo
{
    internal class Student
    {
        public int id;
        public string name;
        public int marks;

        public Student(int id, string name, int marks)
        {
            this.id = id;
            this.name = name;
            this.marks = marks;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // List Example
            List<Student> students = new List<Student>();

            // Add Student objects to the list
            Student first = new Student(1, "Alice", 10);
            Student second = new Student(2, "Bob", 90);
            Student third = new Student(3, "Charlie", 78);
            students.Add(first);
            students.Add(second);
            students.Add(third);

            // Access and display each student using foreach
            Console.WriteLine("Student List:");
            foreach (Student s in students)
            {
                Console.WriteLine($"ID: {s.id}, Name: {s.name}, Marks: {s.marks}");
            }

            // Access a specific object by index
            Console.WriteLine($"\nSecond student is: {students[1].name}");

            // Dictionary Example
            Dictionary<string, Student> students_dict = new Dictionary<string, Student>();
            students_dict.Add("firstStudent", first);
            students_dict.Add("secondStudent", second); // Fixed spelling error here
            students_dict.Add("thirdStudent", third);

            foreach (KeyValuePair<string, Student> student in students_dict)
            {
                Console.WriteLine($"Key: {student.Key}, ID: {student.Value.id}");
            }

            // HashSet Example
            Console.WriteLine("\nHashSet demo");
            HashSet<Student> students_hashset = new HashSet<Student>();
            students_hashset.Add(first);
            students_hashset.Add(second);
            students_hashset.Add(third); // 'first' is already added, so it won't be added again

            foreach (Student student in students_hashset)
            {
                Console.WriteLine($"HashSet Student ID: {student.id}");
            }

            // Stack Demo
            Console.WriteLine("\nStack demo");
            Stack<Student> students_stack = new Stack<Student>();
            students_stack.Push(first);
            students_stack.Push(second);
            students_stack.Push(third);

            Student pop_stack = students_stack.Pop();
            Console.WriteLine($"Popped from stack: {pop_stack.name}");

            // Queue demo
            Console.WriteLine("\nQueue demo");
            Queue<Student> students_queue = new Queue<Student>();
            students_queue.Enqueue(first);
            students_queue.Enqueue(second);
            students_queue.Enqueue(third);

            Console.WriteLine($"Dequeued from queue: {students_queue.Dequeue().name}");
        }
    }
}
