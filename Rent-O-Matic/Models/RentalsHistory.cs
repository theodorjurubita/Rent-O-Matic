using System;
using System.ComponentModel.DataAnnotations;

namespace Rent_O_Matic.Models
{
    public class RentalsHistory
    {
        public int Id { get; set; }

        public Car Car { get; set; }
        public int CarId { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Date to pick up the car")]
        [DateRentedOutsideOtherRentalsPeriod]
        public DateTime DateRented { get; set; }

        [Display(Name = "Date to return the car")]
        [DateReturnedBeforeOtherRentals]
        public DateTime DateReturned { get; set; }


    }
}