using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;

namespace Rent_O_Matic.Controllers
{
    public class CarsController : Controller
    {

        private ApplicationDbContext _context;

        public CarsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Cars
        public ActionResult Random()
        {
            var cars = _context.Cars.ToList();
            
            return View(cars);
        }

        public ActionResult New()
        {
            var stores = _context.Stores.ToList();
            var viewModel = new NewCarViewModel()
            {
                Stores = stores
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            
            return RedirectToAction("New","Cars");
        }

        [Route("cars/years/{year}")]
        public ActionResult ByYear(int year)
        {
            return Content(year.ToString());
        }
    }
}