using Microsoft.Maui.Storage;

namespace Lab13App.Helpers;

public static class DatabasePathProvider
{
    private const string FileName = "lab.db";

    public static string GetPath()
    {
        var projectRoot = LocateProjectRoot() ?? AppContext.BaseDirectory;
        Directory.CreateDirectory(projectRoot);
        return Path.Combine(projectRoot, FileName);
    }

    private static string? LocateProjectRoot()
    {
        try
        {
            var directory = AppContext.BaseDirectory;
            while (!string.IsNullOrEmpty(directory))
            {
                if (File.Exists(Path.Combine(directory, "Lab13App.csproj")))
                {
                    return directory;
                }

                directory = Path.GetDirectoryName(directory);
            }
        }
        catch
        {
            // ignored
        }

        return null;
    }
}

