using Rent_O_Matic.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Rent_O_Matic.ViewModels
{
    public class RentCarViewModel
    {

        public DateTime DateRented { get; set; }

        public DateTime DateReturned { get; set; }

        public Customer Customer { get; set; }

        public Car Car { get; set; }

        [Display(Name = "Store Location")]
        public string StoreName { get; set; }

        public float FinalPrice { get; set; }
    }
}