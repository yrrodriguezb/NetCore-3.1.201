using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // Func<int, int> square = x => x * x; // Codigo que se ejecuta y se puede incovar
            // Expression<Func<int, int, int>> add = (x, y) => x + y; // Ya no es codigo ejecutable

            // LINQ Cuando trabaja con entity framework trabaja y ve que trabajan con IQueryable 
            // y a su vez adoptan una expresion de funccion "Empression<Func<Object, bool>>",
            // EF tiene la oportunidad de inspeccionar su código y traducirlos a declaraciones SQL

            // Cuando se trabaja con EF y este trabaja con IEnumerable, quiere decir que esta operacion se realiza en memoria


            InsertData();
            QueryData();
        }

        private static void QueryData()
        {
            var db = new Context();

            var query = 
                db.Cars.Where(c => c.Manufacturer == "BMW")
                    .Take(10)
                    .OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Name);

            foreach (var car in query)
            {
                Console.WriteLine($"{car.Name}: {car.Combined}");
            }


            Console.WriteLine("\nSintaxis de Consulta");

            var query2 =
                from car in db.Cars
                group car by car.Manufacturer into Manufacturer
                select new 
                {
                    Name = Manufacturer.Key,
                    Cars = (
                        from car in Manufacturer
                        orderby car.Combined descending
                        select car
                    ).Take(2)
                };

            // No soportado por SQLite

            /* foreach (var group in query2)
            {
                Console.WriteLine(group.Name);

                foreach (var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Name}: {car.Combined}");
                }
            } */


            Console.WriteLine("\nSintaxis de método");

            var query3 = 
                db.Cars.GroupBy(c => c.Manufacturer)
                    .Select(g => new {
                        Name = g.Key,
                        Cars = g.OrderByDescending(c => c.Combined).Take(2)
                    });

            // No soportado por SQLite

            /*foreach (var group in query3)
            {
                Console.WriteLine(group.Name);

                foreach (var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Name}: {car.Combined}");
                }
            } */

        }

        private static void InsertData()
        {
            var cars = ProcessCar("fuel.csv");

            var db = new Context();
        
            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }

                var result = db.SaveChanges();

                Console.WriteLine($"Datos insertados {result}");
            }
        }

         private static List<Car> ProcessCar(string path)
        {
            // Consulta, Por extension de método
            return File.ReadAllLines(path)
                .Skip(2) // Omite las dos primeras lineas
                .Where(line => line.Length > 1)
                .ToCar()
                .ToList();
        }
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar (this IEnumerable<string> source)
        {

            foreach (var line in source)
            {
                var columns = line.Split(';');

                yield return new Car 
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };   
            }
        }
    }
}
