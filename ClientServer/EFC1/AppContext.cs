using EFC1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFC1
{
    class AppContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;

        public AppContext() {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Warning);

            optionsBuilder.UseLazyLoadingProxies().UseSqlite(config.GetConnectionString("DefaultConnection"));
        }

        public void LoadData()
        {
            if (!Students.Any())
            {
                Student s1 = new Student { Name = "Vasya", Surname = "Black Hole Off", Age = 20, CreatedAt = DateTime.Now };
                Student s2 = new Student { Name = "Petya", Surname = "Sun Strike Off", Age = 14, CreatedAt = DateTime.Now };
                Student s3 = new Student { Name = "Danny", Surname = "Dream Coil Off", Age = 16, CreatedAt = DateTime.Now };
                Student s4 = new Student { Name = "Sanya", Surname = "Echo Slam Off", Age = 56, CreatedAt = DateTime.Now };

                Students.Add(s1);
                Students.Add(s2);
                Students.Add(s3);
                Students.Add(s4);

                SaveChanges();
            }
        }
    }
}