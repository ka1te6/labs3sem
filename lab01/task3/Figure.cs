using System;
namespace task3
{
	public class Figure
	{
    private Point[] points;
    public string Name { get; set; }

    public Figure(string name, Point p1, Point p2, Point p3)
    {
        Name = name;
        points = new Point[] { p1, p2, p3 };
    }

    public Figure(string name, Point p1, Point p2, Point p3, Point p4)
        : this(name, p1, p2, p3)
    {
        points = new Point[] { p1, p2, p3, p4 };
    }

    public Figure(string name, Point p1, Point p2, Point p3, Point p4, Point p5)
        : this(name, p1, p2, p3, p4)
    {
        points = new Point[] { p1, p2, p3, p4, p5 };
    }

    public double LengthSide(Point A, Point B)
    {
        return Math.Sqrt(Math.Pow(B.X - A.X, 2) + Math.Pow(B.Y - A.Y, 2));
    }

    public double PerimeterCalculator()
    {
        double perimeter = 0;
        for (int i = 0; i < points.Length; i++)
        {
            Point current = points[i];
            Point next = points[(i + 1) % points.Length];
            perimeter += LengthSide(current, next);
        }
        return perimeter;
    }
}
}