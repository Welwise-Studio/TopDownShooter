using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class CollectionsExtension
    {

        /// <summary>
        /// Is array null or empty
        /// </summary>
        public static bool IsNullOrEmpty<T>(this T[] collection) => collection == null || collection.Length == 0;

        /// <summary>
        /// Is list null or empty
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IList<T> collection) => collection == null || collection.Count == 0;

        /// <summary>
        /// Is collection null or empty. IEnumerable is relatively slow. Use Array or List implementation if possible
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || !collection.Any();

        /// <summary>
        /// Collection is not null or empty
        /// </summary>
        public static bool NotNullOrEmpty<T>(this T[] collection) => !collection.IsNullOrEmpty();

        /// <summary>
        /// Collection is not null or empty
        /// </summary>
        public static bool NotNullOrEmpty<T>(this IList<T> collection) => !collection.IsNullOrEmpty();

        /// <summary>
        /// Collection is not null or empty
        /// </summary>
        public static bool NotNullOrEmpty<T>(this IEnumerable<T> collection) => !collection.IsNullOrEmpty();
    }
}
