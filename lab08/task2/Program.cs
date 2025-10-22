using System;
using System.IO;
using System.IO.Compression;

namespace FileSearchAndCompress
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к директории:");
            string directoryPath = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Введите имя файла для поиска (например, data.txt):");
            string fileName = Console.ReadLine() ?? string.Empty;

            try
            {
                string? foundFile = FindFile(directoryPath, fileName);

                if (foundFile == null)
                {
                    Console.WriteLine("Файл не найден.");
                    return;
                }

                Console.WriteLine($"\nФайл найден: {foundFile}");
                Console.WriteLine("\n--- Содержимое файла ---\n");

                using (FileStream fs = new FileStream(foundFile, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    string content = reader.ReadToEnd();
                    Console.WriteLine(content);
                }

                Console.WriteLine("\nХотите сжать этот файл? (y/n):");
                string answer = Console.ReadLine()?.ToLower() ?? "n";

                if (answer == "y")
                {
                    string compressedPath = foundFile + ".gz";
                    CompressFile(foundFile, compressedPath);
                    Console.WriteLine($"Файл успешно сжат: {compressedPath}");
                }
                else
                {
                    Console.WriteLine("Сжатие пропущено.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        static string? FindFile(string directory, string targetFile)
        {
            try
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    if (Path.GetFileName(file).Equals(targetFile, StringComparison.OrdinalIgnoreCase))
                        return file;
                }

                foreach (var subDir in Directory.GetDirectories(directory))
                {
                    string? found = FindFile(subDir, targetFile);
                    if (found != null)
                        return found;
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            return null;
        }

        static void CompressFile(string sourceFile, string compressedFile)
        {
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
            using (FileStream targetStream = new FileStream(compressedFile, FileMode.Create, FileAccess.Write))
            using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
            {
                sourceStream.CopyTo(compressionStream);
            }
        }
    }
}
