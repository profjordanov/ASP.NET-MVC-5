using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarDealer.Models.ViewModels
{
    public class EditCustomerVm
    {
        [HiddenInput]
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Birthday")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime BirthDate { get; set; }
        [DisplayName("Is the driver young?")]
        public bool IsYoungDriver { get; set; }
    }
}