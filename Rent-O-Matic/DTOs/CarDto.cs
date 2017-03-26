using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rent_O_Matic.ViewModels;

namespace Rent_O_Matic.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public float Price { get; set; }

        public StoreDto Store { get; set; }

        public int StoreId { get; set; }
    }
}