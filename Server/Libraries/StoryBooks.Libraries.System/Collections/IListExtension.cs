namespace System.Linq
{
    using System.Collections.Generic;

    public static class IListExtensions
    {
        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            if (items.IsNotEmpty())
            {
                foreach (var item in items)
                {
                    collection.Add(item);
                }
            }
        }

        /// Convert list of object into another type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">Items to be converted</param>
        /// <returns></returns>
        public static IList<T> To<T>(this IList<IConvertible> items)
        { 
            var result = new List<T>();

            items.ForEach(item => result.Add(item.To<T>()));

            return result;
        }
    }
}