using System;
using System.Collections.Generic;
using System.Linq;
using Features.Linq;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            // Recibe un parametro entero, retorno un valor entero
            Func<int, int> f = x => x * x;

            // Recibe dos parametros enteros, retorno un valor entero
            Func<int,int, int> add = (x, y) => x + y;
            Console.WriteLine($"Funcion lambda 'Add' => Resultado: {add(1, 3)}");

            Action<string> Print = str => Console.Write(str);

            /*
                NOTA:  Siempre el último tipo de expresion es el tipo de retorno

                Ejemplos:
                Func<string, int>      => Recibe un parametro de tipo string y retorna un valor de tipo entero
                Func<int, int, string> => Recibe dos parametros de tipo entero y devuelve un valor de tipo string
            */

            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 2, Name = "Chris" }
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };

            foreach (var person in sales)
            {
                Print(person.Name);
            }

            Console.WriteLine();

            IEnumerator<Employee> enumerator = developers.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Print(enumerator.Current.Name);
            }

            // Aplicando mi metodo de extensión de la clase MyLinq
            Console.WriteLine(developers.MyCount());

            
            // Delegados
            foreach (var employee in developers.Where(delegate (Employee employee) {
                return employee.Name.StartsWith("S");
            }))
            {
                Print($"Con delegados: {employee.Name}");
            }

            // Empresiones lambda
            foreach (var employee in developers.Where(NamesStarsWithS))
            {
                Print($"Con empresion lambda: {employee.Name}");
            }

            // Sintaxis de consulta
            string[] cities = 
            {
                "Boston",
                "Los Angeles",
                "Seatle",
                "London",
                "Hyderabad"
            };

            IEnumerable<string> filteredCities = 
                from city in cities
                where city.StartsWith("L") && city.Length < 15
                orderby city
                select city;

                Print("\nCiudadas ==========>\n");

                foreach (var city in filteredCities)
                {
                    Print($"La ciudad de {city}\n");
                }
        }

        private static bool NamesStarsWithS(Employee employye)
        {
            return employye.Name.StartsWith("S");
        }
    }
}
