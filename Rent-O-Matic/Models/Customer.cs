using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Rent_O_Matic.ViewModels;

namespace Rent_O_Matic.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Driving Liscense Number")]
        [Required]
        public string DrivingLiscense { get; set; }

        [Display(Name = "Years Old")]
        [Required]
        public int YearsOld { get; set; }

        [Required]
        public string Nationality { get; set; }

        public Car Car { get; set; }

        public Store Store { get; set; }

        [Display(Name = "Showroom store")]
        public int StoreId { get; set; }

        [Display(Name = "Car available")]
        public int CarId { get; set; }

    }
}