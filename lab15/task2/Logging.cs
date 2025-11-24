using System.Text;
using System.Text.Json;

namespace Task2;

public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error,
    Critical
}

public record LogEntry(DateTimeOffset Timestamp, LogLevel Level, string Message, string? Details = null);

public interface ILogRepository
{
    void Write(LogEntry entry);
}

public sealed class TextFileLogRepository : ILogRepository
{
    private readonly string _filePath;
    private readonly object _gate = new();

    public TextFileLogRepository(string filePath)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
    }

    public void Write(LogEntry entry)
    {
        var line = $"{entry.Timestamp:O} [{entry.Level}] {entry.Message}";
        if (!string.IsNullOrWhiteSpace(entry.Details))
        {
            line += $" ({entry.Details})";
        }

        lock (_gate)
        {
            File.AppendAllText(_filePath, line + Environment.NewLine, Encoding.UTF8);
        }
    }
}

public sealed class JsonFileLogRepository : ILogRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };
    private readonly object _gate = new();

    public JsonFileLogRepository(string filePath)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
    }

    public void Write(LogEntry entry)
    {
        lock (_gate)
        {
            List<LogEntry> items = [];
            if (File.Exists(_filePath))
            {
                var content = File.ReadAllText(_filePath, Encoding.UTF8);
                if (!string.IsNullOrWhiteSpace(content))
                {
                    items = JsonSerializer.Deserialize<List<LogEntry>>(content, _options) ?? [];
                }
            }

            items.Add(entry);
            var json = JsonSerializer.Serialize(items, _options);
            File.WriteAllText(_filePath, json, Encoding.UTF8);
        }
    }
}

public sealed class MyLogger
{
    private readonly IReadOnlyCollection<ILogRepository> _repositories;

    public MyLogger(params ILogRepository[] repositories)
    {
        if (repositories is null || repositories.Length == 0)
        {
            throw new ArgumentException("Provide at least one repository.", nameof(repositories));
        }

        _repositories = repositories;
    }

    public void Log(LogLevel level, string message, Exception? exception = null)
    {
        var entry = new LogEntry(DateTimeOffset.Now, level, message, exception?.ToString());
        foreach (var repository in _repositories)
        {
            repository.Write(entry);
        }
    }

    public void Info(string message) => Log(LogLevel.Info, message);
    public void Warning(string message) => Log(LogLevel.Warning, message);
    public void Error(string message, Exception? ex = null) => Log(LogLevel.Error, message, ex);
    public void Debug(string message) => Log(LogLevel.Debug, message);
}

