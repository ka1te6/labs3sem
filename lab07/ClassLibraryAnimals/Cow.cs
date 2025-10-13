using System;

namespace ClassLibraryAnimals
{
    [Comment("Класс, представляющий корову")]
    public class Cow : Animal
    {
        public override eFavoriteFood GetFavouriteFood() => eFavoriteFood.Grass;

        public override void SayHello() => Console.WriteLine("Мууу!");
    }
}