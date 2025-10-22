using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using ClassLibraryAnimals;

namespace Serialization
{
    class Program
    {
        static void Main()
        {
            List<Animal> animals = new()
            {
                new Cow { Name = "Бурёнка", Age = 5 },
                new Pig { Name = "Пятачок", Age = 2 },
                new Lion { Name = "Симба", Age = 4 }
            };

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "animals.xml");

            XmlSerializer serializer = new(typeof(List<Animal>));
            using (FileStream fs = new(path, FileMode.Create))
            {
                serializer.Serialize(fs, animals);
            }

            Console.WriteLine($"Файл успешно создан: {path}");
        }
    }
}