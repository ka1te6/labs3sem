using System;

namespace task1
{
    struct Vector
    {
        public double X, Y, Z;

        public Vector(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }

        public static Vector operator +(Vector a, Vector b)
            => new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static double operator *(Vector a, Vector b)
            => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static Vector operator *(Vector v, double k)
            => new Vector(v.X * k, v.Y * k, v.Z * k);

        public static Vector operator *(double k, Vector v)
            => v * k;

        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        public static bool operator >(Vector a, Vector b) => a.Length > b.Length;
        public static bool operator <(Vector a, Vector b) => a.Length < b.Length;
        public static bool operator ==(Vector a, Vector b) => a.Length == b.Length;
        public static bool operator !=(Vector a, Vector b) => !(a == b);

        public override bool Equals(object obj) =>
            obj is Vector v && this == v;

        public override int GetHashCode() =>
            Length.GetHashCode();

        public override string ToString() =>
            $"({X}, {Y}, {Z}) | Length={Length:F2}";
    }
}