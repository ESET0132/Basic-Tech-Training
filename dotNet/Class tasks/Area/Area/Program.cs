using System;

namespace Area
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IRectangle rect1 = new Rectangle();
            rect1.Input();
            rect1.CalculateArea();
            rect1.Display();

            Console.WriteLine();

            IRectangle rect2 = new Rectangle();
            rect2.Input();
            rect2.CalculateArea();
            rect2.Display();
        }
    }
}

