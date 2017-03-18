using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Models.ViewModels
{
    public class EditSupplierVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsImporter { get; set; }
    }
}
