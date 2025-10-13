namespace ClassLibraryAnimals
{
    [Comment("Абстрактный базовый класс для всех животных")]
    public abstract class Animal
    {
        public string Name { get; set; } = "";
        public string Country { get; set; } = "";
        public bool HideFromOtherAnimals { get; set; }
        public eClassificationAnimal WhatAnimal { get; set; }

        public abstract eFavoriteFood GetFavouriteFood();
        public abstract void SayHello();
    }
}