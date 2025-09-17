using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
       
        Dictionary<string, object> student1 = CreateStudent("Shivansh", "Paliwal", 22, "Undeclared", 0.0);
        Dictionary<string, object> student2 = CreateStudent("Lakshay", "singh", 20, "Computer Science", 3.8);
        Dictionary<string, object> student3 = CreateStudent("Aryan", "Kumar", 22, "Mathematics", 3.5);

     
        List<Dictionary<string, object>> students = new List<Dictionary<string, object>>();

      
        students.Add(student1);
        students.Add(student2);
        students.Add(student3);

        Console.WriteLine("List of Students:");
        

        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine($"Student {i + 1}:");
            DisplayStudentInfo(students[i]);
        
        }

        Console.ReadLine();
    }


    static Dictionary<string, object> CreateStudent(string firstName, string lastName, int age, string major, double gpa)
    {
        return new Dictionary<string, object>
        {
            { "FirstName", firstName },
            { "LastName", lastName },
            { "Age", age },
            { "Major", major },
            { "GPA", gpa }
        };
    }

    static Dictionary<string, object> CreateDefaultStudent()
    {
        return CreateStudent("Shivansh", "Paliwal", 22, "Undeclared", 0.0);
    }

   
    static void DisplayStudentInfo(Dictionary<string, object> student)
    {
        Console.WriteLine($"Name: {student["FirstName"]} {student["LastName"]}");
        Console.WriteLine($"Age: {student["Age"]}");
        Console.WriteLine($"Major: {student["Major"]}");
        Console.WriteLine($"GPA: {Convert.ToDouble(student["GPA"]):F2}");
    }
}