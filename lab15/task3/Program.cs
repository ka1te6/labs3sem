using Task3;

Console.WriteLine("SingleRandomizer — глобальный генератор чисел.");

var tasks = Enumerable.Range(1, 5)
    .Select(id => Task.Run(() =>
    {
        for (var i = 0; i < 3; i++)
        {
            var value = SingleRandomizer.Instance.NextInt(0, 100);
            Console.WriteLine($"Поток {id}: {value}");
            Thread.Sleep(100);
        }
    }))
    .ToArray();

await Task.WhenAll(tasks);

Console.WriteLine($"Случайное число double: {SingleRandomizer.Instance.NextDouble():0.0000}");
