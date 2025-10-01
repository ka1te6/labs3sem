using System;

namespace task1
{
    public class ExcelentPupil : Pupil
    {
        public override void Study()
        {
            Console.WriteLine("Отличник учится очень усердно");
        }

        public override void Read()
        {
            Console.WriteLine("Отличник читает быстро и с пониманием");
        }

        public override void Write()
        {
            Console.WriteLine("Отличник пишет аккуратно и грамотно");
        }

        public override void Relax()
        {
            Console.WriteLine("Отличник отдыхает умеренно");
        }
    }
}