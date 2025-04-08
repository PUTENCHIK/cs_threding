using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_2.Entities
{
    class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

        public override string ToString()
        {
            var timestamp = DateTime.Now.Subtract(Birthday);
            return $"[{Id}] {Name} {Surname}, ({timestamp})";
        }
    }
}
