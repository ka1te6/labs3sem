using System;

namespace task2
{
    public class Rectangle
    {
        private double sideA;
        private double sideB;

        public Rectangle(double sideA, double sideB)
        {
            this.sideA = sideA;
            this.sideB = sideB;
        }

        private double CalculateArea() => sideA * sideB;

        private double CalculatePerimeter() => 2 * (sideA + sideB);

        public double Area => CalculateArea();

        public double Perimeter => CalculatePerimeter();
    }
}