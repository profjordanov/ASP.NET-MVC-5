using System;

namespace CarDealer.Models.ViewModels
{
    public class AllLogVm
    {
        public string Username { get; set; }

        public OperationLog Operation { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModifiedTable { get; set; }
    }
}