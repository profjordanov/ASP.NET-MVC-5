using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models.ViewModels
{
    public class EditPartVm
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:f1}", ApplyFormatInEditMode = true)]
        public double? Price { get; set; }

        public int Quantity { get; set; }
    }
}