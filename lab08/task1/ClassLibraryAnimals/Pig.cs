using System;

namespace ClassLibraryAnimals
{
    [Serializable]
    public class Pig : Animal
    {
        public override void Speak()
        {
            Console.WriteLine($"{Name} говорит: Хрю-хрю!");
        }
    }
}