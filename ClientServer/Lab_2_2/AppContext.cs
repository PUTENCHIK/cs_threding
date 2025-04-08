using Lab_2_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_2
{
    class AppContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;

        public DbSet<Course> Courses { get; set; } = null!;

        public AppContext()
        {
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
        }

        public void SeedData()
        {
            if (!Students.Any() && !Courses.Any())
            {
                var student1 = new Student { Name = "Petya", Surname = "Petrov", Birthday = new DateTime(1994, 5, 20) };
                var student2 = new Student { Name = "Danny", Surname = "Shelby", Birthday = new DateTime(2002, 3, 14) };

                Students.Add(student1);
                Students.Add(student2);

                var course1 = new Course { Name = "IoT", CreatedAt = DateTime.Now };
                var course2 = new Course { Name = "Computer Vision", CreatedAt = DateTime.Now };

                Courses.Add(course1);
                Courses.Add(course2);

                SaveChanges();
            }
        }
    }
}
