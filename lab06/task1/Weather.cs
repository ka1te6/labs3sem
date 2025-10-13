using System;

namespace WeatherLinq
{
    public struct Weather
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public double Temp { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Country} — {Temp:F1}°C, {Description}";
        }
    }
}