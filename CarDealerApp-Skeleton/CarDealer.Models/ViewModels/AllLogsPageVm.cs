using System.Collections.Generic;

namespace CarDealer.Models.ViewModels
{
    public class AllLogsPageVm
    {
        public int CurrentPage { get; set; }

        public int TotalNumberOfPages { get; set; }

        public IEnumerable<AllLogVm> Logs { get; set; }

        public string WantedUsername { get; set; }
    }
}