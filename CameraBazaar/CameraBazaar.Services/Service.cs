using CameraBazaar.Data;

namespace CameraBazaar.Services
{
    public class Service
    {
        public Service()
        {
            this.Context = new CameraBazaarContext();
        }
        protected CameraBazaarContext Context { get; }
    }
}
