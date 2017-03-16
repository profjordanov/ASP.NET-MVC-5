using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AdminAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string stringField = value as string;
            if (!stringField.StartsWith("Admin"))
            {
                return new ValidationResult("The given value does not start with admin");
            }

            return ValidationResult.Success;
        }
    }
}