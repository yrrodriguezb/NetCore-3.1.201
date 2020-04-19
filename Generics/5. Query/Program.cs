using System;
using System.Linq;

namespace Query
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IRepository<Employee> employeeRepository = 
                new SqlRepository<Employee>(new Context()))
            {
                if (!employeeRepository.Any())   
                {
                    AddEmployees(employeeRepository);
                    AddManagers(employeeRepository);
                }
                
                CountEmployees(employeeRepository);
                QueryEmployee(employeeRepository);
                DumpPeople(employeeRepository);
            }
        }

        private static void AddManagers(IWriteOnlyRepository<Manager> employeeRepository)
        {
            employeeRepository.Add(new Manager { Name = "Diego" });
            employeeRepository.Commit();
        }

        private static void DumpPeople(IReadOnlyRepository<Person> employeeRepository)
        {
            var employees = employeeRepository.FindAll();

            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Name);
            }
        }

        private static void QueryEmployee(IRepository<Employee> employeeRepository)
        {
            var employee = employeeRepository.FindById(1);
            Console.WriteLine(employee.Name);
        }

        private static void CountEmployees(IRepository<Employee> employeeRepository)
        {
            var count = employeeRepository.FindAll().Count();
            Console.WriteLine($"Count {count}");
        }

        private static void AddEmployees(IRepository<Employee> employeeRepository)
        {
            employeeRepository.Add(new Employee { Name = "Scott" });
            employeeRepository.Add(new Employee { Name = "Alan" });
            
            employeeRepository.Commit();
        }
    }
}
