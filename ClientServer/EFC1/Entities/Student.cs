﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFC1.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} {Surname}, {Age} y.o. ({CreatedAt})";
        }
    }
}
