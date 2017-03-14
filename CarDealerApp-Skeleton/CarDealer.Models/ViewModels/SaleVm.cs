namespace CarDealer.Models.ViewModels
{
    public class SaleVm
    {
        public virtual Car Car { get; set; }
        public virtual AllCustomerVm Customer { get; set; }
        public double Discount { get; set; }

        public double Price { get; set; }
    }
}