using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarDealer.Models.Attributes;

namespace CarDealer.Models.BindingModels
{
    public class EditCustomerBm : IValidatableObject
    {
        public int Id { get; set; }
        //[Admin]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Name.StartsWith("User"))
            {
                yield return new ValidationResult("The name should start with user", new[] {nameof(Name)});
            }
            if (BirthDate.Year == 2015)
            {
                yield return new ValidationResult("Best year for animation!", new[] {nameof(BirthDate)});
            }
           
        }
    }
}