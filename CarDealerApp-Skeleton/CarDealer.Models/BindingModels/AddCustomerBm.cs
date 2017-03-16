using System;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models.BindingModels
{
    public class AddCustomerBm
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}