using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rent_O_Matic.Models
{
    public class Store
    {
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

    }
}