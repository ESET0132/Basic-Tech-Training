using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureDemo
{
    public class Student
    {
        public int Id;
        public string Name;
        public int Age;
        public string Grade;

        public Student(int id, string name, int age, string grade)
        {
            Id = id;
            Name = name;
            Age = age;
            Grade = grade;
        }
    }
}
