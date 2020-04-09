using System;
using System.Collections.Generic;

namespace Features.Linq
{
    public static class MyLinq
    {
        // Metodo de Extension, con la palabra clave this
        public static int MyCount<T>(this IEnumerable<T> sequence)
        {
            int count = 0;
            foreach (var item in sequence)
            {
                count += 1;
            }
            return count;
        }
    }
}