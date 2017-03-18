using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Rent_O_Matic.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Driving Liscense Number")]
        public string DrivingLiscense { get; set; }

        [Display(Name = "Years Old")]
        public int YearsOld { get; set; }

        public string Nationality { get; set; }

        public Car Car { get; set; }

        public Store Store { get; set; }

        [Display(Name = "Showroom store")]
        public int StoreId { get; set; }

        [Display (Name = "Car available")]
        public int CarId { get; set; }

    }
}