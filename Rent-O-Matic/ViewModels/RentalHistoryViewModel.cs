using Rent_O_Matic.Models;
using System;
using System.Collections.Generic;

namespace Rent_O_Matic.ViewModels
{
    public class RentalHistoryViewModel
    {
        public int RentalHistoryId { get; set; }
        public string CustomerName { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime DateReturned { get; set; }
        public byte IncidentGravityId { get; set; }
        public IEnumerable<IncidentGravity> IncidentGravity { get; set; }
    }
}