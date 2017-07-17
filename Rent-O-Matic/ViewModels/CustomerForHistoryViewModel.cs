using Rent_O_Matic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rent_O_Matic.ViewModels
{
    public class CustomerForHistoryViewModel
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

        public IEnumerable<Car> Cars { get; set; }

        public IEnumerable<Store> Stores { get; set; }

        public Car Car { get; set; }

        public Store Store { get; set; }

        [Display(Name = "Showroom store")]
        [Required]
        public int? StoreId { get; set; }

        [Display(Name = "Car available")]
        [Required]
        public int? CarId { get; set; }

        [Display(Name = "Date to pick up the car")]
        [DateRentedOutsideOtherRentalsPeriod]
        public DateTime DateRented { get; set; }

        [Display(Name = "Date to return the car")]
        [DateReturnedBeforeOtherRentals]
        public DateTime DateReturned { get; set; }
    }
}