using CarDealer.Data;

namespace CarDealer.Services
{
    public class Service
    {
        protected CarDealerContext Context;

        public Service()
        {
            this.Context = new CarDealerContext();
        }
    }
}