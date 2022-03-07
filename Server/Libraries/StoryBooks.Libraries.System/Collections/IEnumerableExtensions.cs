namespace System.Linq
{
    using System.Collections.Generic;

    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Indicates whether the specified collection is null or has no elements
        /// </summary>
        /// <typeparam name="T">Type of the elements in the collection</typeparam>
        /// <param name="collection">Source collection</param>
        /// <returns>True if the collection is null or empty; otherwise false</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection is null || !collection.Any();
        }

        /// <summary>
        /// Check if IEnumerable is not empty
        /// </summary>
        /// <typeparam name="T">Type of the elements in the list</typeparam>
        /// <param name="collection">List to be checked</param>
        public static bool IsNotEmpty<T>(this IEnumerable<T> collection)
        {
            return !collection.IsNullOrEmpty();
        }

        /// <summary>
        /// Foreach LINQ extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        /// <summary>
        /// Checks if value is in given list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsIn<T>(this T source, params T[] list)
        {
            if (source is null) 
            {
                throw new ArgumentException("Cannot search for null value!");
            }

            return list.Contains(source);
        }

        /// <summary>
        /// Convert list of object into another type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<T> To<T>(this IEnumerable<IConvertible> items)
        {
            var result = new List<T>();

            items.ForEach(item => result.Add(item.To<T>()));

            return result;
        }
    }
}