using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_2.Entities
{
    class TeacherCourse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
