using System;

namespace task1
{
    public class BadPupil : Pupil
    {
        public override void Study()
        {
            Console.WriteLine("Двоечник учится неохотно");
        }

        public override void Read()
        {
            Console.WriteLine("Двоечник читает медленно");
        }

        public override void Write()
        {
            Console.WriteLine("Двоечник пишет с ошибками");
        }

        public override void Relax()
        {
            Console.WriteLine("Двоечник отдыхает часто и много");
        }
    }
}