using System;
using System.Xml.Serialization;

namespace ClassLibraryAnimals
{
    [Serializable]
    [XmlInclude(typeof(Cow))]
    [XmlInclude(typeof(Pig))]
    [XmlInclude(typeof(Lion))]
    public abstract class Animal
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        public abstract void Speak();
    }
}

