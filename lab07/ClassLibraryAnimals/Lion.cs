using System;

namespace ClassLibraryAnimals
{
    [Comment("Класс, представляющий льва")]
    public class Lion : Animal
    {
        public override eFavoriteFood GetFavouriteFood() => eFavoriteFood.Meat;

        public override void SayHello() => Console.WriteLine("Рррр!");
    }
}