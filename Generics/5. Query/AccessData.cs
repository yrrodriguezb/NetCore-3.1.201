using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Query
{
    public class Context : DbContext
    {     
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Employees.db");

        public DbSet<Employee> Employees { get; set; }
    }

    /*
        IDisposable => Limpia y Desecha Adecuadamente
        out => Es un modificador genérico, esto lo hace un parámetro covariante.
               Permite que erepositorio se pueda compartar como IRepository<Employee> o IRepositort<Person>, 
               ya que Employee deriva o hereda de Person.
            => La covarianza solo funciona en delegados e interfaces
    */
    public interface IReadOnlyRepository<out T> : IDisposable
    {
        T FindById(int id);
        IQueryable<T> FindAll();
    }

    public interface IWriteOnlyRepository<in T> : IDisposable
    {
        void Add(T newEntity);
        void Delete(T entity);
        int Commit();
    }

    public interface IRepository<T> : IReadOnlyRepository<T>, IWriteOnlyRepository<T>
    {
        bool Any();
    }


    /*
        Ejemplo de un genérico con mas restricciones

        public class SQLRepo<T, T2> : where T  : class
                                      where T2 : IRepository<T2>  
        {}
    */

    public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        // Restricción new(), verifica que el tipo T tenga un constructor sin parametros, para poder realizar una instacia de T
        private DbContext _context;
        private DbSet<T> _set;

        public SqlRepository(DbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public bool Any()
        {
            return _set.Any();
        }

        public void Add(T newEntity)
        {
            if (newEntity.IsValid())
            {
                _set.Add(newEntity);
            }
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
        }
        
        public T FindById(int id)
        {
            return _set.Find(id);
        }

        public IQueryable<T> FindAll()
        {
            return _set;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}