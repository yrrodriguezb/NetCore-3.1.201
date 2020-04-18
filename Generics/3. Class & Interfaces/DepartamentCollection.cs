using System.Collections.Generic;

namespace Class_Interfaces
{
    public class DepartamentCollection :
        SortedDictionary<string, SortedSet<Employee>>
    {
        public DepartamentCollection Add(string departamentName, Employee employee)
        {
            if(!ContainsKey(departamentName))
            {
                Add(departamentName, new SortedSet<Employee>(new EmployeeComparer()));
            }

            this[departamentName].Add(employee);

            return this;
        }
    }
}