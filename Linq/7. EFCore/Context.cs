using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    public class Context : DbContext
    {     
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Cars.db");

        public DbSet<Car> Cars { get; set; }
    }
}