using System;

namespace FlexibleSaveSystem.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SaveDataAttribute attribute is applied to a field with an id that has already been used.
    /// </summary>
    public class EngagedIdException : Exception
    {
        public EngagedIdException() => new Exception("A field with this id is already initialized, try another id");
        public EngagedIdException(string id) => new Exception($"A field with id '{id}' is already initialized, try another id");
    }
}
