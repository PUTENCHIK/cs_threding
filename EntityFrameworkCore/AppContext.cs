using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.Entities;

namespace EntityFrameworkCore
{
    class AppContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;

        public AppContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Sourse=sqlite.db");
        }
    }
}