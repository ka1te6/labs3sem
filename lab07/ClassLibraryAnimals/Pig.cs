using System;

namespace ClassLibraryAnimals
{
    [Comment("Класс, представляющий свинью")]
    public class Pig : Animal
    {
        public override eFavoriteFood GetFavouriteFood() => eFavoriteFood.Everything;

        public override void SayHello() => Console.WriteLine("Хрю!");
    }
}