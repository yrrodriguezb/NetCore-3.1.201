using System;
using System.Collections.Generic;

namespace Class_Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ejemplo de buffer
            // bufferSample();

            // Evita que se dupliquen empleados en los HashSet y los ordena por empleados.
            // Adémas ordena los empleados en un SortedSet

            Console.WriteLine("\ncompareEmployeeHashSet ===================\n");
            compareEmployeeHashSet();

            Console.WriteLine("\ncompareEmployeeSortedSet ===================\n");
            compareEmployeeSortedSet();

            Console.WriteLine("\ncompareEmployeeOptimized ===================\n");
            compareEmployeeOptimized();
        }

        public static void compareEmployeeOptimized()
        {
             var departaments = new DepartamentCollection();

            departaments
                .Add("Engineering", new Employee { Name = "Scott" })
                .Add("Engineering", new Employee { Name = "Alex" })
                .Add("Engineering", new Employee { Name = "Diego" })
                .Add("Engineering", new Employee { Name = "Diego" });

            departaments.Add("Sales", new Employee { Name = "Joy" })
                .Add("Sales", new Employee { Name = "Joy" })
                .Add("Sales", new Employee { Name = "Alan" })
                .Add("Sales", new Employee { Name = "Alan" });

            foreach (var departament in departaments)
            {   
                Console.WriteLine(departament.Key);

                foreach (var employee in departament.Value)
                {
                    Console.WriteLine($"\t{employee.Name}");
                }
            }
        }

        private static void compareEmployeeHashSet()
        {
            var departaments = new Dictionary<string, HashSet<Employee>>();

            // EmployeeComparer, Evita empleados duplicados teniendo en cuenta solo el nombre.

            departaments.Add("Engineering", new HashSet<Employee>(new EmployeeComparer()));
            departaments["Engineering"].Add(new Employee { Name = "Scott" });
            departaments["Engineering"].Add(new Employee { Name = "Alex" });
            departaments["Engineering"].Add(new Employee { Name = "Diego" });
            departaments["Engineering"].Add(new Employee { Name = "Diego" });

            departaments.Add("Sales", new HashSet<Employee>(new EmployeeComparer()));
            departaments["Sales"].Add(new Employee { Name = "Joy" });
            departaments["Sales"].Add(new Employee { Name = "Alan" });
            departaments["Sales"].Add(new Employee { Name = "Alan" });

            foreach (var departament in departaments)
            {   
                Console.WriteLine(departament.Key);

                foreach (var employee in departament.Value)
                {
                    Console.WriteLine($"\t{employee.Name}");
                }
            }
        }

        public static void compareEmployeeSortedSet()
        {
            var departaments = new Dictionary<string, SortedSet<Employee>>();

            // EmployeeComparer, Evita empleados duplicados teniendo en cuenta solo el nombre.

            departaments.Add("Engineering", new SortedSet<Employee>(new EmployeeComparer()));
            departaments["Engineering"].Add(new Employee { Name = "Scott" });
            departaments["Engineering"].Add(new Employee { Name = "Alex" });
            departaments["Engineering"].Add(new Employee { Name = "Diego" });
            departaments["Engineering"].Add(new Employee { Name = "Diego" });

            departaments.Add("Sales", new SortedSet<Employee>(new EmployeeComparer()));
            departaments["Sales"].Add(new Employee { Name = "Joy" });
            departaments["Sales"].Add(new Employee { Name = "Alan" });
            departaments["Sales"].Add(new Employee { Name = "Alan" });

            foreach (var departament in departaments)
            {   
                Console.WriteLine(departament.Key);

                foreach (var employee in departament.Value)
                {
                     Console.WriteLine($"\t{employee.Name}");
                }
            }
        }

        private static void bufferSample()
        {
            var buffer = new Buffer<double>();

            ProcessInput(buffer);

            foreach (var item in buffer)
            {
                Console.WriteLine($"item in buffer {item}");
            }

            ProcessBuffer(buffer);
        }

        private static void ProcessBuffer(IBuffer<double> buffer)
        {
            var sum = 0.0;

            Console.WriteLine("Buffer: ");

            while (!buffer.IsEmpty)
            {
                sum += buffer.Read();
            }

            Console.WriteLine(sum);
        }

        private static void ProcessInput(IBuffer<double> buffer)
        {
            while (true)
            {
                var value = 0.0;
                var input = Console.ReadLine();

                if (double.TryParse(input, out value))
                {
                    buffer.Write(value);
                    continue;
                }

                break;
            }
        }
    }
}
