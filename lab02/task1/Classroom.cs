using System;

namespace task1
{
    public class ClassRoom
    {
        private Pupil[] pupils;

        public ClassRoom(params Pupil[] pupils)
        {
            if (pupils.Length < 2 || pupils.Length > 4)
            {
                throw new ArgumentException("Класс должен содержать от 2 до 4 учеников");
            }

            this.pupils = pupils;
        }

        public void ShowClassInfo()
        {
            Console.WriteLine("Информация о классе:");
            Console.WriteLine("====================");

            for (int i = 0; i < pupils.Length; i++)
            {
                Console.WriteLine($"\nУченик {i + 1}:");
                Console.WriteLine("----------");
                pupils[i].Study();
                pupils[i].Read();
                pupils[i].Write();
                pupils[i].Relax();
            }
        }
    }
}