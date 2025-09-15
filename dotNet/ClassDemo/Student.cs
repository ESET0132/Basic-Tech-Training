using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemp
{
    internal class Student
    {
        int student_id;
        string student_name;
        int student_age;
        string student_address;


        public void initialize()
        {
            student_id = 101;
            student_name = "shivansh paliwal";
            student_age = 21;
        }


        public void display()
        {
            Console.WriteLine("student id is: " + student_id);
            Console.WriteLine("student name is: " + student_name);
            Console.WriteLine("student age is: " + student_age);
            Console.WriteLine("student address is: " + student_address);
        }


        public int get_student_id() {
            return student_id;
        }


        public double totalMarks(int m1, int m2, int m3)
        {
            int total = m1 + m2 + m3;
            return total;
        }

        public double avgMarks(int m1, int m2, int m3)
        {
            int avg = (m1 + m2 + m3) / 3;
           return avg;
        }

    }
}
