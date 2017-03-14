using System.Collections.Generic;

namespace CarDealer.Models.ViewModels
{
    public class AboutCarVm
    {
        public virtual CarVm Car { get; set; }

        public virtual IEnumerable<PartVm>  Parts { get; set; }

    }
}