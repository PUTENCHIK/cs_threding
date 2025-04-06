using EFC1.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EFC1
{
    class Program
    {
        static void Main()
        {
            using (AppContext db = new())
            {
                Student student1 = new Student { Name = "Vasya", Age = 20 };
                Student student2 = new Student { Name = "Petya", Age = 35 };

                db.Add(student1);
                db.Add(student2);
                db.SaveChanges();

                Console.WriteLine("Students added");

                var students = db.Students.ToList();

                foreach (var student in students)
                {
                    Console.WriteLine($"[{student.StudentId}] {student.Name}, {student.Age}");
                }
            }
        }
    }
}