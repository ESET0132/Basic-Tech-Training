using System;

namespace Area
{
    
    public class Rectangle : IRectangle
    {
        
        public double Length { get; set; }
        public double Width { get; set; }
        public double Area { get; private set; }

     
        public void Input()
        {
            Console.Write("Enter length: ");
            Length = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter width: ");
            Width = Convert.ToDouble(Console.ReadLine());
        }

        public void CalculateArea()
        {
            Area = Length * Width;
        }

        public double GetPerimeter()
        {
            return 2 * (Length + Width);
        }

        public void Display()
        {
            Console.WriteLine($"Rectangle: Length={Length}, Width={Width}, Area={Area}, Perimeter={GetPerimeter()}");
        }
    }
}