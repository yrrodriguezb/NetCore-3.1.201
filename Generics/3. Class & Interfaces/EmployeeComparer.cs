using System.Collections.Generic;

namespace Class_Interfaces 
{
    public class EmployeeComparer : IEqualityComparer<Employee>, IComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return string.Equals(x.Name, y.Name);
        }

        public int GetHashCode(Employee obj)
        {   // Asegura que se devuelva el mismo código hash para empleados con el mismo nombre. 
            // Esto asegura que el HashSet indentifique los empleados duplicados.
            return obj.Name.GetHashCode();
        }

        public int Compare(Employee x, Employee y)
        {
            // Retono 0, si ambos objetos son exactos
            // Retorna menor a 0, si el primer objeto es menor que el segundo objeto
            // Retorna mayor a 0, si el primer objeto es mayor que el segundo objeto 
            return string.Compare(x.Name, y.Name);
        }
    }
}