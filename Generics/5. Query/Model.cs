using System;

namespace Query
{
    public interface IEntity
    {
        bool IsValid();
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public class Employee : Person, IEntity
    {
        public int Id { get; set; }

        public bool IsValid()
        {
            return true;
        }

        public virtual void DoWork()
        {
            Console.WriteLine("Doind real work");
        }
    }

    public class Manager : Employee
    {
        public override void DoWork()
        {
            Console.WriteLine("Crate a meeting");
        }
    }
}