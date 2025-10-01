using System;

namespace task1
{
    public class GoodPupil : Pupil
    {
        public override void Study()
        {
            Console.WriteLine("Хорошист учится старательно");
        }

        public override void Read()
        {
            Console.WriteLine("Хорошист читает уверенно");
        }

        public override void Write()
        {
            Console.WriteLine("Хорошист пишет хорошо");
        }

        public override void Relax()
        {
            Console.WriteLine("Хорошист отдыхает достаточно");
        }
    }
}