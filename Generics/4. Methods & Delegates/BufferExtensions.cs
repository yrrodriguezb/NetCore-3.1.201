using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace Methods_Delegates
{
    public static class BufferExtensions
    {
        public delegate void Printer<T>(T data);

        /// <summary>Metodo de estensión para imprimir por pantalla los datos ingresados.
        /// <seealso cref="Methods_Delegates.BufferExtensions"/>
        /// </summary>
        public static void Dump<T>(this IBuffer<T> buffer)
        {
            foreach (var item in buffer)
            {
                Console.WriteLine(item);
            }
        }

        public static IEnumerable<TOutput> AsEnumerableOf<T, TOutput>(this IBuffer<T> buffer)
        {
            // Convierte cualquier tipo primitivo en C# en un TOutput.
            // Cuando hablamos de primitivo, nos referimos a los tipos: int, string, double, etc.
            var converter = TypeDescriptor.GetConverter(typeof(T));

            foreach (var item in buffer)
            {
                TOutput result = (TOutput)converter.ConvertTo(item, typeof(TOutput));
                yield return result;
            }
        }

        public static void Dump<T>(this IBuffer<T> buffer, Printer<T> print)
        {
            foreach (var item in buffer)
            {
                print(item);
            }
        }

        public static IEnumerable<TOutput> Map<T, TOutput>(this IBuffer<T> buffer, Converter<T, TOutput> converter)
        {
            return buffer.Select(i => converter(i));
        }
    }
}