using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Utilities.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// return not null values from enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereIsNotNull<T>(this IEnumerable<T> enumerable)
        {
            bool isValueType = typeof(T).IsValueType;
            return enumerable.Where(x => isValueType || x != null);
        }
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.ToList().ForEach(action);
        }
    }
}
