using System;

namespace Rent_O_Matic.ViewModels
{
    public class RentedCarsViewModel
    {
        public DateTime DateRented { get; set; }

        public DateTime DateReturned { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Store { get; set; }

        public float PricePerDay { get; set; }
    }
}