using Rent_O_Matic.Models;
using System.Collections.Generic;

namespace Rent_O_Matic.ViewModels
{
    public class CustomerViewModel
    {

        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Store> Stores { get; set; }
        public Customer Customer { get; set; }
    }
}