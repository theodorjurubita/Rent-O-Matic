using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rent_O_Matic.Models;

namespace Rent_O_Matic.Controllers
{
    public class CarsController : Controller
    {
        // GET: Cars
        public ActionResult Random()
        {
            var car=new Car()
            {
                Brand = "Mercedes",
                Model = "C-Klasse",
                Year = 2013,
                Price = 23500
            };
            return View(car);
        }

        [Route("cars/years/{year}")]
        public ActionResult ByYear(int year)
        {
            return Content(year.ToString());
        }
    }
}