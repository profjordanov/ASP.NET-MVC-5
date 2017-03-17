namespace CarDealer.Models.BindingModels
{
    public class AddSaleBm
    {
        public int CustomerId { get; set; }

        public int CarId { get; set; }

        public int Discount { get; set; }
    }
}