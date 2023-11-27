using System;

namespace FlexibleSaveSystem.Exceptions
{
    public class NotSupportedTypeException : Exception
    {
        public NotSupportedTypeException() => new Exception("A field with this type not supprorted to save.");
        public NotSupportedTypeException(Type type) => new Exception($"A field with type '{type}' not supproted to save");
    }
}
