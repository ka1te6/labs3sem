using Task1;

var directoryToWatch = args.Length > 0
    ? args[0]
    : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "lab15-watch");

Directory.CreateDirectory(directoryToWatch);

Console.WriteLine("Наблюдаем за папкой: {0}", directoryToWatch);
Console.WriteLine("Создавайте, изменяйте или удаляйте файлы, чтобы увидеть события.");

using var monitor = new DirectoryMonitor(directoryToWatch, TimeSpan.FromSeconds(2));
using var subscription = monitor.Subscribe(new ConsoleDirectoryObserver());

Console.WriteLine("Нажмите Enter для завершения работы...");
Console.ReadLine();

internal sealed class ConsoleDirectoryObserver : IDirectoryObserver
{
    public void OnDirectoryChanged(FileChangeEvent change)
    {
        var message = change.Kind switch
        {
            FileChangeKind.Created => $"Создан файл {change.Path}",
            FileChangeKind.Deleted => $"Удалён файл {change.Path}",
            FileChangeKind.Modified => $"Изменён файл {change.Path}",
            _ => $"Изменение файла {change.Path}"
        };

        Console.WriteLine($"{change.DetectedAt:HH:mm:ss} | {message}");
    }
}
