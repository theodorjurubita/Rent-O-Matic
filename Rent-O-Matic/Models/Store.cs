using System.ComponentModel.DataAnnotations;

namespace Rent_O_Matic.Models
{
    public class Store
    {
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public bool HasCars { get; set; }

    }
}