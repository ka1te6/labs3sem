using System;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {

            Pupil excellent = new ExcelentPupil();
            Pupil good = new GoodPupil();
            Pupil bad = new BadPupil();
       
            ClassRoom classRoom = new ClassRoom(excellent, good, bad);
            classRoom.ShowClassInfo();
        }
    }
}