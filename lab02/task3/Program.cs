using System;

namespace task3
{
    class Program
    {
        static void Main(string[] args)
        {
          
            Console.WriteLine("Введите ключ доступа (pro/exp) или нажмите Enter для бесплатной версии:");
            string key = Console.ReadLine();

            DocumentWorker documentWorker;

            switch (key?.ToLower())
            {
                case "pro":
                    documentWorker = new ProDocumentWorker();
                    break;
                case "exp":
                    documentWorker = new ExpertDocumentWorker();
                    break;
                default:
                    documentWorker = new DocumentWorker();
                    break;
            }

            Console.WriteLine("\nРабота с документом:");
            documentWorker.OpenDocument();
            documentWorker.EditDocument();
            documentWorker.SaveDocument();
            
        }
    }
}