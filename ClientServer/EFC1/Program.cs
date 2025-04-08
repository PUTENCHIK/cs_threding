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
                db.LoadData();

                // Data display
                var students = db.Students.ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"{student}");
                }

                // Data updating
                var enigma = db.Students.OrderBy(s => s.Id).FirstOrDefault();
                if (enigma != null)
                {
                    enigma.Age = 13;
                    enigma.CreatedAt = DateTime.Now;
                    db.SaveChanges();
                    Console.WriteLine($"Updated student: {enigma}");
                }
            }
        }
    }
}