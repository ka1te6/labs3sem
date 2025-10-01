using System;

namespace task2
{
    class Car : IEquatable<Car>
    {
        public string Name { get; set; }
        public string Engine { get; set; }
        public int MaxSpeed { get; set; }

        public Car(string name, string engine, int maxSpeed)
        {
            Name = name;
            Engine = engine;
            MaxSpeed = maxSpeed;
        }

        public override string ToString() => Name;

        public bool Equals(Car other)
        {
            if (other == null) return false;
            return Name == other.Name && Engine == other.Engine && MaxSpeed == other.MaxSpeed;
        }

        public override bool Equals(object obj) => Equals(obj as Car);

        public override int GetHashCode() => (Name, Engine, MaxSpeed).GetHashCode();
    }
}