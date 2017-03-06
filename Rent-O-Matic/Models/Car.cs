using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rent_O_Matic.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public float Price { get; set; }

    }
}