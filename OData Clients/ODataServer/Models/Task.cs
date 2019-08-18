using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ODataServer.Models
{
    [CustomValidation(typeof(Task), "ValidateDates")]
    public class Task
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public virtual TaskPriority Priority { get; set; }
        public virtual TaskStatus Status { get; set; }
        public virtual User AssignedTo { get; set; }

        public static ValidationResult  ValidateDates(object task)
        {
            Task t = task as Task;
            if (t == null)
                return new ValidationResult("Invalid object for validation");
            if (t.DueDate < t.StartDate)
                return new ValidationResult("Due date must not be before start date");
            else
                return ValidationResult.Success;

        }
    }
}