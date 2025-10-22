using System;
using System.IO;
using System.Xml.Serialization;
using ClassLibraryAnimals;

namespace Deserialization
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(@"..\Serialization\bin\Debug\net9.0\animals.xml");
            Console.WriteLine($"Путь к файлу: {path}\n");

            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден. Сначала запусти проект Serialization.");
                return;
            }

            try
            {
                var serializer = new XmlSerializer(typeof(Animal[]), new Type[] {
                    typeof(Cow), typeof(Pig), typeof(Lion)
                });

                using FileStream fs = new FileStream(path, FileMode.Open);
                Animal[]? animals = (Animal[]?)serializer.Deserialize(fs);

                if (animals == null)
                {
                    Console.WriteLine("Десериализация вернула null.");
                    return;
                }

                Console.WriteLine("Десериализованные животные:\n");
                foreach (var a in animals)
                {
                    Console.WriteLine($"Тип: {a.GetType().Name}");
                    Console.WriteLine($"Имя: {a.Name}");
                    Console.WriteLine($"Возраст: {a.Age}");
                    a.Speak();
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка десериализации: " + ex.Message);
            }
        }
    }
}