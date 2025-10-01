using System;
using Xunit;
using task3;

namespace task3.Tests;

public class FigureTests
{
    [Fact]
    public void LengthSide_CorrectCalculation()
    {
        Point p1 = new Point(0, 0);
        Point p2 = new Point(3, 4);
        Figure f = new Figure("Test", p1, p2, new Point(0, 1));
        Assert.Equal(5, f.LengthSide(p1, p2));
    }

    [Fact]
    public void PerimeterCalculator_Triangle()
    {
        Point p1 = new Point(0, 0);
        Point p2 = new Point(0, 3);
        Point p3 = new Point(4, 0);

        Figure triangle = new Figure("Треугольник", p1, p2, p3);

        double perimeter = triangle.PerimeterCalculator();
        Assert.Equal(12, perimeter, 3); 
    }
}
