using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rent_O_Matic.Models;

namespace Rent_O_Matic.ViewModels
{
    public class NewCarViewModel
    {
        public IEnumerable<Store> Stores { get; set; }
        public Car Car { get; set; }
    }
}