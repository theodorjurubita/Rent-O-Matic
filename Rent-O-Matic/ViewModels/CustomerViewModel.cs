using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;

namespace Rent_O_Matic.ViewModels
{
    public class CustomerViewModel
    {

        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Store> Stores { get; set; }
        public Customer Customer { get; set; }

    }
}