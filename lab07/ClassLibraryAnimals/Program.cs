using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using ClassLibraryAnimals;

class Program
{
    static void Main()
    {
        var assembly = typeof(Animal).Assembly;
        Console.WriteLine($"Проверяем сборку: {assembly.FullName}");

        var xmlDoc = new XDocument(new XElement("LibraryDescription"));

        foreach (Type type in assembly.GetTypes())
        {
            Console.WriteLine($"Проверка типа {type.Name}");
            var attrs = type.GetCustomAttributes(typeof(CommentAttribute), false);
            Console.WriteLine($"Атрибутов: {attrs.Length}");

            var xmlType = new XElement("Type",
                new XAttribute("Name", type.Name));

            if (attrs.Length > 0)
            {
                var attr = (CommentAttribute)attrs[0];
                xmlType.Add(new XElement("Comment", attr.Comment));
            }

            xmlDoc.Root!.Add(xmlType);
        }

        string path = "LibraryDescription.xml";
        xmlDoc.Save(path);
        Console.WriteLine($"XML-файл создан: {Path.GetFullPath(path)}");
    }
}


