using System;

namespace ClassLibraryAnimals
{
    [Serializable]
    public class Lion : Animal
    {
        public override void Speak()
        {
            Console.WriteLine($"{Name} говорит: Рррр!");
        }
    }
}