using System;

namespace CarDealer.Models.BindingModels
{
    public class EditCustomerBm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}