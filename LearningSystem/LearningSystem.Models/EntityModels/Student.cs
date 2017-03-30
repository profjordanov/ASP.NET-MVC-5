using System.Collections.Generic;

namespace LearningSystem.Models.EntityModels
{
    public class Student
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
        }
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual  ICollection<Course> Courses { get; set; }
    }
}