using System;
using System.Collections.Generic;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            reflectionGenericType();

            Console.WriteLine("\n");

            reflectionGenericMethods();
        }

        private static void reflectionGenericMethods()
        {
            // Refleccion con métodos
            var employee = new Employee();
            var employeeType = typeof(Employee);
            var methodInfo = employeeType.GetMethod("Speak");
            methodInfo = methodInfo.MakeGenericMethod(typeof(DateTime));
            methodInfo.Invoke(employee, null);
        }

        private static void reflectionGenericType()
        {
            // Refleccion con tipos genericos
            var employees = CreateCollection(typeof(List<>), typeof(Employee));
            Console.WriteLine(employees.GetType().Name);

            var genericArguments = employees.GetType().GenericTypeArguments;

            foreach (var arg in genericArguments)
            {
                Console.Write("[{0}]", arg.Name);
            }
        }

        private static object CreateCollection(Type collectionType, Type itemType)
        {
            var closedType = collectionType.MakeGenericType(itemType);

            return Activator.CreateInstance(closedType);
        }
    }

    public class Employee
    {
        public string Name { get; set; }

        public void Speak<T>()
        {
            Console.WriteLine($"Speak from: {typeof(T).Name}");
        }
    }
}
