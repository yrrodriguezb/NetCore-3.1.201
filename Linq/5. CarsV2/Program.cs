using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CarsV2
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessCar("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            Console.WriteLine("\nUNION\n");
            Console.WriteLine("\nUNION - Sintaxis de consulta\n");

            // Union - Sintaxis de consulta
            var query = 
                from car in cars
                join manufacturer in manufacturers 

                    // union de una sola propiedad por fuente de datos
                    // on car.Manufacturer equals manufacturer.Name

                    // Union por varias propiedas
                    on new { car.Manufacturer, car.Year } 
                        equals 
                            new { Manufacturer = manufacturer.Name, manufacturer.Year }
                            
                orderby car.Combined descending, car.Name ascending
                select new 
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined
                };

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }


            Console.WriteLine("\nUNION - Sintaxis de método\n");
            // Union - Sintaxia de método
            /*
                EL operador de Union recibe tres parametros para poder realizar esta unión:

                1. Recibe la fuente de datos con la cual se va a relacionar
                2. Recibe el parametro externo con el que se realciona la fuente de datos interna
                3. Recibe el parametro de llave interna con el que se relaciona en la fuente de datos externa
                4. Una funcion que sirva para realzar el mapeo de los nuevos datos que se obtienen como resultado
            */
            var query2 = 
                cars.Join(
                    manufacturers,
                    // union de una sola propiedad por fuente de datos 
                    // c => c.Manufacturer,
                    
                    // Union por varias propiedas
                    c => new { c.Manufacturer, c.Year },

                    // union de una sola propiedad por fuente de datos 
                    // m => m.Name,

                    // Union por varias propiedas
                    m => new { Manufacturer = m.Name, m.Year },

                    (c, m) => new {
                        m.Headquarters,
                        c.Name,
                        c.Combined
                    })
                    .OrderByDescending(f => f.Combined)
                    .ThenBy(f => f.Name);

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }


            Console.WriteLine("\nAGRAUPACION\n");

            Console.WriteLine("\nAGRAUPACION - Sintaxis de consulta\n");

            var query3 = 
                from car in cars
                group car by car.Manufacturer.ToUpper()
                    into manufacturer
                orderby manufacturer.Key
                select manufacturer;

            foreach (var result in query3)
            {
                Console.WriteLine($"{result.Key} tiene {result.Count()} automoviles");
            }
            Console.WriteLine("\n");

            foreach (var group in query3)
            {
                Console.WriteLine($"{group.Key}");

                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");    
                }

                Console.WriteLine("\n");
            }


            Console.WriteLine("\nAGRAUPACION - Sintaxis de método\n");

            var query4 = 
                cars.GroupBy(c => c.Manufacturer.ToUpper())
                    .OrderBy(groupby => groupby.Key);

            foreach (var group in query4)
            {
                Console.WriteLine($"{group.Key}");

                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");    
                }

                Console.WriteLine("\n");
            }

             Console.WriteLine("\nGroupJoin\n");
            Console.WriteLine("\nGroupJoin - Sintaxis de consulta\n");

            var query5 = 
                from manufacturer in manufacturers
                join car in cars
                    on manufacturer.Name equals car.Manufacturer
                // Cuando se realizan join, ya no tengo disponibles las variale de car.
                // Por esta razon se debe realiza un into, con los nuevos valores del objeto resultante
                    into carGroup
                orderby manufacturer.Name
                select new 
                {
                    Manufacturer = manufacturer,
                    Cars = carGroup
                };

            foreach (var group in query5)
            {
                Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarters}");

                foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");    
                }

                Console.WriteLine("\n");
            }

            
            
            Console.WriteLine("\nGroupJoin - Sintaxis de método\n");

            var query6 = 
                manufacturers.GroupJoin(
                    cars,
                    m => m.Name,
                    c => c.Manufacturer,
                    (m, g) => new {
                        Manufacturer = m,
                        Cars = g
                    })
                    .OrderBy(m => m.Manufacturer.Name);
            
            foreach (var group in query6)
            {
                Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarters}");

                foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");    
                }

                Console.WriteLine("\n");
            }


            Console.WriteLine("\nAGREGACIONES\n");
            Console.WriteLine("\nAGREGACIONES - Sintaxis de consulta\n");

            var query7 = 
                from car in cars
                group car by car.Manufacturer into carGroup
                // Tener en cuenta que en este metodo, se esta haciendo un buclu con cada calculo.
                // Se realizan tres bucles, uno por Maxuno por Min y uno por Average.
                // En ese caso no es muy bueno en rendimiento.
                // En la consulta de sintaxis de metodo veremos como evitar estos tres bucles y que solo haga uno.
                select new 
                {
                    Name = carGroup.Key,
                    Max = carGroup.Max(c => c.Combined),
                    Min = carGroup.Min(c => c.Combined),
                    Avg = carGroup.Average(c => c.Combined)
                } 
                into result
                orderby  result.Max descending
                select result; 

            foreach (var result in query7)
            {
                Console.WriteLine($"Name = {result.Name}, Max = {result.Max},  Min = {result.Min}, Avg = {result.Avg}");
            }


            Console.WriteLine("\nAGREGACIONES - Sintaxis de método\n");

            var query8 =
                cars.GroupBy(c => c.Manufacturer)
                .Select(g => {
                    var results = 
                        g.Aggregate(
                            new CarStatistics(),
                            (acumulator, car) => acumulator.Accumulate(car),
                            acumulator => acumulator.Compute()
                        );

                    return new 
                    {
                        Name = g.Key,
                        Avg = results.Average,
                        Min = results.Min,
                        Max = results.Max
                    };
                })
                .OrderByDescending(r => r.Max);

            foreach (var result in query8)
            {
                Console.WriteLine($"Name = {result.Name}, Max = {result.Max},  Min = {result.Min}, Avg = {result.Avg}");
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

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query = 
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .ToManufacturer();

            return query.ToList();
        }
    }
}
