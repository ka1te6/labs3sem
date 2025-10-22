using System;

namespace ClassLibraryAnimals
{
    [Serializable]
    public class Cow : Animal
    {
        public override void Speak()
        {
            Console.WriteLine($"{Name} говорит: Мууу!");
        }
    }
}