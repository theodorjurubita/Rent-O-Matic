using System;

namespace Rent_O_Matic.Models
{
    public class RentalsHistory
    {
        public int Id { get; set; }

        public Car Car { get; set; }
        public int CarId { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public DateTime DateRented { get; set; }
        public DateTime DateReturned { get; set; }


    }
}