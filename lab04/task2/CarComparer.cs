using System.Collections.Generic;

public class CarComparer : IComparer<Car>
{
    public enum CompareType { Name, Year, Speed }
    private CompareType compareBy;

    public CarComparer(CompareType type)
    {
        compareBy = type;
    }

    public int Compare(Car x, Car y)
    {
        return compareBy switch
        {
            CompareType.Name => string.Compare(x.Name, y.Name),
            CompareType.Year => x.ProductionYear.CompareTo(y.ProductionYear),
            CompareType.Speed => x.MaxSpeed.CompareTo(y.MaxSpeed),
            _ => 0
        };
    }
}