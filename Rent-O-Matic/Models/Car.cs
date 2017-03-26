using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rent_O_Matic.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Display(Name = "Price per day")]
        [Required]
        public float Price { get; set; }

        public Store Store { get; set; }

        [Display(Name = "Showroom Store")]
        public int StoreId { get; set; }

    }
}