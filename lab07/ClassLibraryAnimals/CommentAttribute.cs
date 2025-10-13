using System;

namespace ClassLibraryAnimals
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class CommentAttribute : Attribute
    {
        public string Comment { get; }

        public CommentAttribute(string comment)
        {
            Comment = comment;
        }
    }
}