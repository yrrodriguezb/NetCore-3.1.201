using System;
using System.Collections.Generic;
using System.Linq;
using Queries.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var movies = new List<Movie>
            {
                new Movie{ Title = "The Dark Hnight", Rating = 8.9f, Year = 2008 },
                new Movie{ Title = "The king's Speech", Rating = 8.0f, Year = 2010 },
                new Movie{ Title = "CasaBlanca", Rating = 8.5f, Year = 1942 },
                new Movie{ Title = "Stars Wars V", Rating = 8.7f, Year = 1980 }
            };

            // No se ejecuta, ya que es una ejecucion diferida.
            // Solo se ejecuta hasta que el objeto se enumera llamando GetEnumerator o foreach
            var query = movies.Filter(m => m.Year > 2000);

            var enumerator = query.GetEnumerator();

            while(enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            } 


            // El foreach detras de escena, utiliza un enumerador. Como se muestra anteriormente
            foreach (var movie in query)
            {
                Console.WriteLine(movie.Title);
            }


            /* 
                NOTA: Evita trampas de ejecucion doferidas.

                En este ejemplo se ejecutara la consulta para contar las peliculas de acuerdo con el criterio de busqueda.
                Y nuevamente ejecuara la consulta para iterar los datos en el bucle e imprimir el titulo de la pelcula.
                
                Para evitar esto es aconsejable crear la estructura de datos con los metodos: 
                    ToArray,
                    ToDictionary,
                    ToLookup,
                    ToList,
            */
            Console.WriteLine("\n\nQuery Diferred");

            Console.WriteLine($"Count: {query.Count()}");

            var enumeratorDiferred = query.GetEnumerator();

            while(enumeratorDiferred.MoveNext())
            {
                Console.WriteLine("Title: {enumeratorDiferred.Current.Title}");
            } 


            // Evita la ejecucion diferida
            Console.WriteLine("\n\nQuery Not Diferred");

            var queryNotDiferred = movies.Filter(m => m.Year > 2000).ToList();

            Console.Write($"Count: {queryNotDiferred.Count()}\n");

            var enumeratorNotDiferred = queryNotDiferred.GetEnumerator();

            while(enumeratorNotDiferred.MoveNext())
            {
                Console.WriteLine("Title: {enumeratorNotDiferred.Current.Title}");
            } 


            // Operadores de transmisión, ve un elemento a la vez.
            // Por lo tanto el oerador where, es un operador de transmisión
            Console.WriteLine("\n\nStreaming Operator");

            var queryStreaming = movies.Where(m => m.Year > 2000);

            var enumeratorStreaming = queryStreaming.GetEnumerator();

            while(enumeratorStreaming.MoveNext())
            {
                Console.WriteLine("Title: {enumeratorStreaming.Current.Title}");
            } 



            /*
                Orderby y OrderByDescending ofrece ejecucion diferida, pero cuando se esta iterando en el enumerador,
                Este ya debe saber cual es el primer dato que debe mostrar. 

                Por lo tanto este realiza la revison de todos los datos para saber cual es el primero a mostrar.
            */
            Console.WriteLine("\n\nStreaming Not Operator");

            var queryNotStreaming = movies.Where(m => m.Year > 2000)
                                        .OrderByDescending(m => m.Rating);

            var enumeratorNotStreaming = queryNotStreaming.GetEnumerator();

            while(enumeratorNotStreaming.MoveNext())
            {
                Console.WriteLine("Title: {enumeratorNotStreaming.Current.Title}");
            } 


            // Consulta Infinita
            // Variable para consulta infinita.
            // Take(10), evita el bucle infinito

            Console.WriteLine("\n\nEvitar consulta infinita");

            var numbers = MyLinq.Random().Where(n => n > 0.5).Take(10);

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        
        }
    }
}
