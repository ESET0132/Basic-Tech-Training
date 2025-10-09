using System;

namespace Area
{
    
    public interface IRectangle
    {
       
        void Input();
        void CalculateArea();
        void Display();

        double Length { get; set; }
        double Width { get; set; }
        double Area { get; }

        double GetPerimeter();
    }
}