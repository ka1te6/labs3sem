using System.Collections.Generic;

public class CarCatalog
{
    private Car[] cars;

    public CarCatalog(Car[] carsArray)
    {
        cars = carsArray;
    }

    public IEnumerable<Car> Forward()
    {
        for (int i = 0; i < cars.Length; i++)
            yield return cars[i];
    }

    public IEnumerable<Car> Backward()
    {
        for (int i = cars.Length - 1; i >= 0; i--)
            yield return cars[i];
    }

    public IEnumerable<Car> FilterByYear(int year)
    {
        foreach (var car in cars)
            if (car.ProductionYear >= year)
                yield return car;
    }

    public IEnumerable<Car> FilterBySpeed(int speed)
    {
        foreach (var car in cars)
            if (car.MaxSpeed >= speed)
                yield return car;
    }
}