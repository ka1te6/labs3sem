public class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }

    public Car(string name, int year, int speed)
    {
        Name = name;
        ProductionYear = year;
        MaxSpeed = speed;
    }

    public override string ToString()
    {
        return $"{Name} ({ProductionYear}), MaxSpeed: {MaxSpeed}";
    }
}