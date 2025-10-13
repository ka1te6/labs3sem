using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherLinq
{
    class Program
    {
        static async Task Main()
        {
            string apiKey = "d047da4d32d4128da72dce7dd1a4530f";
            int targetCount = 50;
            int totalRequests = 200;

            Console.WriteLine("Параллельная загрузка данных о погоде...\n");

            var httpClient = new HttpClient();
            var random = new Random();
            var results = new ConcurrentBag<Weather>();

            var coords = Enumerable.Range(0, totalRequests)
                .Select(_ => (
                    lat: random.NextDouble() * 180 - 90,
                    lon: random.NextDouble() * 360 - 180
                ))
                .ToList();

            await Parallel.ForEachAsync(coords, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async (coord, _) =>
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={coord.lat:F4}&lon={coord.lon:F4}&appid={apiKey}&units=metric";

                try
                {
                    var response = await httpClient.GetStringAsync(url);
                    using var doc = JsonDocument.Parse(response);

                    if (!doc.RootElement.TryGetProperty("sys", out var sys) ||
                        !sys.TryGetProperty("country", out var countryEl) ||
                        !doc.RootElement.TryGetProperty("name", out var nameEl) ||
                        string.IsNullOrWhiteSpace(nameEl.GetString()))
                        return;

                    string country = countryEl.GetString();
                    string name = nameEl.GetString();
                    double temp = doc.RootElement.GetProperty("main").GetProperty("temp").GetDouble();
                    string description = doc.RootElement.GetProperty("weather")[0].GetProperty("description").GetString();

                    results.Add(new Weather
                    {
                        Country = country,
                        Name = name,
                        Temp = temp,
                        Description = description
                    });
                }
                catch { }
            });

            var weathers = results
                .GroupBy(w => (w.Name, w.Country))
                .Select(g => g.First())
                .Take(targetCount)
                .ToList();

            Console.WriteLine($"Получено {weathers.Count} уникальных записей\n");

            if (weathers.Count == 0)
            {
                Console.WriteLine("Не удалось получить данные. Проверь API key или интернет.");
                return;
            }

            Console.WriteLine("-Анализ данных-\n");

            var maxTemp = weathers.Max(w => w.Temp);
            var minTemp = weathers.Min(w => w.Temp);
            var avgTemp = weathers.Average(w => w.Temp);

            var maxCountry = weathers.First(w => w.Temp == maxTemp);
            var minCountry = weathers.First(w => w.Temp == minTemp);
            var countryCount = weathers.Select(w => w.Country).Distinct().Count();

            var descriptions = new[] { "clear sky", "rain", "few clouds" };
            var match = weathers.FirstOrDefault(w => descriptions.Contains(w.Description));

            Console.WriteLine($"Максимальная температура: {maxCountry.Temp:F1}°C — {maxCountry.Name}, {maxCountry.Country}");
            Console.WriteLine($"Минимальная температура: {minCountry.Temp:F1}°C — {minCountry.Name}, {minCountry.Country}");
            Console.WriteLine($"Средняя температура: {avgTemp:F1}°C");
            Console.WriteLine($"Количество уникальных стран: {countryCount}");

            if (match.Name != null)
                Console.WriteLine($"Первая найденная погода ('{match.Description}'): {match.Name}, {match.Country}");
            else
                Console.WriteLine("Не найдено мест с погодой 'clear sky', 'rain' или 'few clouds'.");

            await System.IO.File.WriteAllLinesAsync("weather_results.txt", weathers.Select(w => w.ToString()));
            Console.WriteLine("\nРезультаты сохранены в weather_results.txt");
        }
    }
}

