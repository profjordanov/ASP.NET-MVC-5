using System;
using System.Collections.Generic;

namespace LearningSystem.Models.EntityModels
{
    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual  ApplicationUser Trainer { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual  ICollection<Student> Students { get; set; }
    }
}