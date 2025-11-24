using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace Task1;

/// <summary>
/// Represents a change detected in the monitored directory.
/// </summary>
public record FileChangeEvent(
    FileChangeKind Kind,
    string Path,
    FileMetadata? OldState,
    FileMetadata? NewState,
    DateTimeOffset DetectedAt);

public enum FileChangeKind
{
    Created,
    Deleted,
    Modified
}

public record FileMetadata(long Length, DateTime LastWriteUtc);

public interface IDirectoryObserver
{
    void OnDirectoryChanged(FileChangeEvent change);
}

/// <summary>
/// Polls a directory on a timer and notifies observers using the Observer pattern.
/// </summary>
public sealed class DirectoryMonitor : IDisposable
{
    private readonly string _path;
    private readonly TimeSpan _interval;
    private readonly object _gate = new();
    private readonly List<IDirectoryObserver> _observers = new();
    private Timer? _timer;
    private Dictionary<string, FileMetadata> _snapshot;
    private bool _disposed;

    public DirectoryMonitor(string directoryPath, TimeSpan pollInterval)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
        {
            throw new ArgumentException("Path is required", nameof(directoryPath));
        }

        _path = Path.GetFullPath(directoryPath);
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }

        if (pollInterval < TimeSpan.FromMilliseconds(200))
        {
            throw new ArgumentException("Interval must be at least 200 ms to avoid busy-waiting.", nameof(pollInterval));
        }

        _interval = pollInterval;
        _snapshot = CaptureSnapshot();
        _timer = new Timer(PollDirectory, null, _interval, _interval);
    }

    public IDisposable Subscribe(IDirectoryObserver observer)
    {
        if (observer is null) throw new ArgumentNullException(nameof(observer));

        lock (_gate)
        {
            _observers.Add(observer);
        }

        return new Unsubscriber(_observers, observer, _gate);
    }

    private void PollDirectory(object? state)
    {
        Dictionary<string, FileMetadata> previous;
        Dictionary<string, FileMetadata> current;

        lock (_gate)
        {
            previous = _snapshot;
            current = CaptureSnapshot();
            _snapshot = current;
        }

        foreach (var change in DetectChanges(previous, current))
        {
            NotifyObservers(change);
        }
    }

    private IEnumerable<FileChangeEvent> DetectChanges(
        IReadOnlyDictionary<string, FileMetadata> previous,
        IReadOnlyDictionary<string, FileMetadata> current)
    {
        foreach (var kvp in current)
        {
            if (!previous.TryGetValue(kvp.Key, out var oldMeta))
            {
                yield return new FileChangeEvent(FileChangeKind.Created, kvp.Key, null, kvp.Value, DateTimeOffset.Now);
            }
            else if (!Equals(oldMeta, kvp.Value))
            {
                yield return new FileChangeEvent(FileChangeKind.Modified, kvp.Key, oldMeta, kvp.Value, DateTimeOffset.Now);
            }
        }

        foreach (var kvp in previous)
        {
            if (!current.ContainsKey(kvp.Key))
            {
                yield return new FileChangeEvent(FileChangeKind.Deleted, kvp.Key, kvp.Value, null, DateTimeOffset.Now);
            }
        }
    }

    private Dictionary<string, FileMetadata> CaptureSnapshot()
    {
        var map = new Dictionary<string, FileMetadata>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (var file in Directory.GetFiles(_path))
            {
                try
                {
                    var info = new FileInfo(file);
                    map[file] = new FileMetadata(info.Length, info.LastWriteTimeUtc);
                }
                catch (IOException)
                {
                    // Skip files that cannot be accessed this moment.
                }
                catch (UnauthorizedAccessException)
                {
                    // Skip inaccessible files.
                }
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // Directory not accessible. Return previous snapshot until next run.
        }

        return map;
    }

    private void NotifyObservers(FileChangeEvent change)
    {
        IDirectoryObserver[] observers;
        lock (_gate)
        {
            observers = _observers.ToArray();
        }

        foreach (var observer in observers)
        {
            observer.OnDirectoryChanged(change);
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        lock (_gate)
        {
            _timer?.Dispose();
            _timer = null;
            _observers.Clear();
        }
    }

    private sealed class Unsubscriber : IDisposable
    {
        private readonly List<IDirectoryObserver> _observers;
        private readonly IDirectoryObserver _observer;
        private readonly object _gate;
        private bool _disposed;

        public Unsubscriber(List<IDirectoryObserver> observers, IDirectoryObserver observer, object gate)
        {
            _observers = observers;
            _observer = observer;
            _gate = gate;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            lock (_gate)
            {
                _observers.Remove(_observer);
            }
        }
    }
}

