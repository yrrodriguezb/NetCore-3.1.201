using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFileTransformToCar("cars.csv");

            var queryMethod = 
                cars.Where(c => c.Origin == "Japan" && c.Model >= 80)
                    .OrderByDescending(c => c.Model)
                    .ThenBy(c => c.Name)
                    .Select(c => c);

            // or
            var querySintax = 
                from car in cars
                where car.Origin == "Japan" && car.Model  >= 80
                orderby car.Model descending, car.Name ascending
                select new {
                    Name = car.Name,
                    Model = car.Model,
                    Origin = car.Origin
                };

            foreach (Car car in queryMethod.Take(10))
            {
                Console.WriteLine($"{car.Name}, Modelo: {car.Model}");
            }


            // Seleccionar el primer elemento
            var top = 
                cars.OrderByDescending(c => c.Model)
                    .ThenBy(c => c.Name)
                    .Select(c => c)
                    .First(c => c.Origin == "Japan" && c.Model >= 80);

            Console.WriteLine($"Primer automovil: {top.Name}\n");

            // Verificar si existe un elemento
            var Exists = cars.Any();
            // or
            var Exists2 = cars.Any(car => car.Origin == "Colombia");
            Console.WriteLine($"Exists: {Exists}, Exists2: {Exists2}\n");


            // Verificar si todos los carros son de Japon
            var all = cars.All(c => c.Origin is "Japan");
            Console.WriteLine($"Todos los autos son de Japon: {all}\n");

            
            // Verificar si contiene un auto de Japon de modelo mayor o igual al año 80
            var constains = cars.Contains(top);
            Console.WriteLine($"Contiene auto de Japon Modelo mayor o igual al año 80: {constains}\n");

            // Operador SelectMany
            var result = 
                cars.Take(2)
                    .SelectMany(c => c.Name);

            Console.WriteLine("\nSelectMany ============>\n");
            foreach (var character in result)
            {
                Console.WriteLine(character);
            }

            // Similar a lo siguiente
            var result2 = 
                cars.Take(2)
                    .Select(c => c.Name);

            Console.WriteLine("\nComprendiendo SelectMany ============>\n");
            foreach (var car in result2)
            {
                for (int j = 0; j < car.Length; j++)
                {
                    Console.WriteLine(car[j]);
                }
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            // Consulta, Por extension de método
            return File.ReadAllLines(path)
                .Skip(2) // Omite las dos primeras lineas
                .Where(line => line.Length > 1)
                .Select(Car.ParseFromCsv)
                .ToList();
        }

        private static List<Car> ProcessFileSyntaxQuery(string path)
        {
            // Consulta, Por sintaxis de consulta
            var query = 
                from line in File.ReadAllLines(path).Skip(2)
                where line.Length > 1
                select Car.ParseFromCsv(line);

            return query.ToList();
        }

        private static List<Car> ProcessFileTransformToCar(string path)
        {
            // Consulta, con metodo de transformacion personalizado a carro
            var query = File.ReadAllLines(path)
                .Skip(2) // Omite las dos primeras lineas
                .Where(line => line.Length > 1)
                .ToCar();

            return query.ToList();
        }
    }
}
