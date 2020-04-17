using System;
using System.Collections.Generic;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            // Funcionalidad de las Listas
            // MethodOne();

            // Como funcionan las listas detras de escena
            // MethodTwo();

            // Tipo generico Queue
            // MethodThree();

            // Con los Stack o pilas. 
            // El orden en el que los proceso es el ultimo en ingresar es el primeero en salir
            // MethodFour();

            // Tipo Generico HashSet
            // MethodFive();

            // Tipo Generico LinkedList
            // MethodSix();

            // Tipo Generico Dictionay
            // MethodSeven();

            // Tipo Generico SortedDictionary
            MethodEight();
        }

        private static void MethodEight()
        {
            // SortedDictionary, ordena sus elementos, teniendo en cuenta su propiedad Key.
            // Por defecto es un orden ascendente. Es sensible a minúsculas y mayúsculas

            var sortedEmployee = new SortedDictionary<string, List<Employee>>();

            sortedEmployee.Add("Sales", new List<Employee>() { 
                new Employee { Name = "Scott" },
                new Employee { Name = "Alex" },
                new Employee { Name = "Diego" }
            }); 
            
            sortedEmployee.Add("Engineering", new List<Employee>() { 
                new Employee { Name = "Elizabeth" },
                new Employee { Name = "Alejandra" }
            }); 

             foreach (var item in sortedEmployee)
            {
                Console.WriteLine("{0}:{0}", item.Key, item.Value.Count);
            }
        }

        private static void MethodSeven()
        {
            // Los diccionarios no pueden tener claves duplicadas

            var employeesByName = new Dictionary<string, Employee>();
            employeesByName.Add("Scott", new Employee { Name = "Scott" });
            employeesByName.Add("Alex", new Employee { Name = "Alex" });
            employeesByName.Add("Diego", new Employee { Name = "Diego" });
            employeesByName.Add("Joy", new Employee { Name = "Joy" });

            var scott = employeesByName["Scott"];

            foreach (var employee in employeesByName)
            {
                Console.WriteLine("{0}:{0}", employee.Key, employee.Value);
            }

            Console.WriteLine("\n======================\n");

            var employeesByDepartament = new Dictionary<string, List<Employee>>();

            employeesByDepartament.Add("Engineering", new List<Employee>() { new Employee { Name = "Scott" } });

            employeesByDepartament["Engineering"].Add(new Employee { Name = "Scott"});

            foreach (var item in employeesByDepartament)
            {   
                foreach (var employee in item.Value)
                {
                     Console.WriteLine("{0}", employee.Name);
                }
            }

        }

        private static void MethodSix()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(2);
            list.AddFirst(3);
            Console.WriteLine("\nLista Inicial");

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nNuevo Orden en la Lista");

            // Se agrega un nuevo elemento despues de el primer elemento
            var first = list.First;
            list.AddAfter(first, 5);
            list.AddBefore(first, 10);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nPropiedades\n");
            Console.WriteLine($"Last: {list.Last.Value}");
            Console.WriteLine($"Previous Last: {list.Last.Previous.Value}");

            Console.WriteLine("\nOtra forma de recorrer las LinkedList\n");

            var node = list.First;

            while(node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
        }

        private static void MethodFive()
        {
            HashSet<int> set = new HashSet<int>();

            set.Add(1);
            set.Add(2);
            set.Add(2); // Retorna falso, por que el elemento ya existe y no lo agrega 

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("--------------");
            
            HashSet<Employee> set2 = new HashSet<Employee>();

            var employee = new Employee { Name = "Scott" };
            set2.Add(employee);
            set2.Add(employee); // No lo agrega por que son la misma referencia

            foreach (var item in set2)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("--------------");

            HashSet<Employee> set3 = new HashSet<Employee>();

            var employee1 = new Employee { Name = "Scott" };
            var employee2 = new Employee { Name = "Scott" };
            set3.Add(employee1);
            set3.Add(employee2); // Lo agrega por que son diferentes referencia

            foreach (var item in set3)
            {
                Console.WriteLine(item.Name);
            }
        }

        private static void MethodFour()
        {
            Stack<Employee> stack = new Stack<Employee>();
            stack.Push(new Employee { Name = "Jason" });
            stack.Push(new Employee { Name = "Diego" });
            stack.Push(new Employee { Name = "Alex" });
            stack.Push(new Employee { Name = "Chris" });

            // Estructura LIFO
            // El ultimo objecto en entrar, sera el primero objecto en salir
            while (stack.Count > 0)
            {
                // Obtiene el empleado y lo elimina
                var employee = stack.Pop();
                Console.WriteLine(employee.Name);
            }
        }

        private static void MethodThree()
        {
            Queue<Employee> queue = new Queue<Employee>();
            queue.Enqueue(new Employee { Name = "Jason" });
            queue.Enqueue(new Employee { Name = "Diego" });
            queue.Enqueue(new Employee { Name = "Alex" });
            queue.Enqueue(new Employee { Name = "Chris" });


            // Estructura FIFO
            // El primer objecto en entrar, sera el primer objecto en salir
            while (queue.Count > 0)
            {
                // Obtiene el empleado y lo elimina
                var employee = queue.Dequeue();
                Console.WriteLine(employee.Name);
            }
        }

        private static void MethodTwo()
        {
            // La definicion de lista, detras de escena tiene un algotitmo que cambia el numero de tamaño de esta 
            // Cada vex que cambia, duplica su valor de capacidad dependiendo como se inicializo.

            // Este es un ejemplo para exigir la memoria a su capacidad maxima dependiendo el sistema operativo

            var numbers = new List<int>(10);
            var capacity = -1;

            while (true)
            {
                if (numbers.Capacity != capacity)
                {
                    capacity = numbers.Capacity;
                    Console.WriteLine(capacity);
                }

                numbers.Add(1);
            }
        }

        private static void MethodOne()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee { Name = "Alex" },
                new Employee { Name = "Alan" }
            };

            employees.Add(new Employee { Name = "Scott" });

            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Name);
            }
        }
    }
}
