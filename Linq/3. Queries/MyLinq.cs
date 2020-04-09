using System;
using System.Collections.Generic;

namespace Queries.Linq
{
    public static class MyLinq
    {
        public static IEnumerable<double> Random()
        {
            var random = new Random();
            while (true) 
            {
                yield return random.NextDouble();
            }
        }


        public static IEnumerable<T> Filter<T> (this IEnumerable<T> source, Func<T, bool> predicate)
        {
            // Ejecución diferida
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Filter2<T> (this IEnumerable<T> source, Func<T, bool> predicate)
        {
            var result = new List<T>();

            // Crea la estructura de datos
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}