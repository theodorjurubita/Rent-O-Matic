using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rent_O_Matic.ViewModels;

namespace Rent_O_Matic.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DrivingLiscense { get; set; }

        public int YearsOld { get; set; }

        public string Nationality { get; set; }

        public StoreDto Store { get; set; }

        public CarDto Car { get; set; }

    }
}