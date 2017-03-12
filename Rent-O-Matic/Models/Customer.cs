using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rent_O_Matic.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DrivingLiscense { get; set; }
        public int YearsOld { get; set; }
        public string Nationality { get; set; }

    }
}