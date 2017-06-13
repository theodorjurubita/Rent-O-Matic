using Microsoft.AspNet.Identity;
using Rent_O_Matic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Rent_O_Matic.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var customerAssociatedWithUser = _context.Customers.Single(c => c.UserId == userId);
                var carEntriesFromHistory = _context.RentalsHistories
                    .Where(h => h.CustomerId == customerAssociatedWithUser.Id)
                    .Where(h => h.DateRented <= DateTime.Today && h.DateReturned >= DateTime.Today)
                    .ToList();

                var carsRented = new List<RentedCarsViewModel>();

                foreach (var carEntry in carEntriesFromHistory)
                {
                    var car = _context.Cars.Single(c => c.Id == carEntry.CarId);
                    var store = _context.Stores.Single(s => s.Id == car.StoreId);
                    var carRented = new RentedCarsViewModel()
                    {
                        Brand = car.Brand,
                        Model = car.Model,
                        PricePerDay = car.Price,
                        Store = store.City + ", " + store.Country,
                        DateRented = carEntry.DateRented,
                        DateReturned = carEntry.DateReturned
                    };
                    carsRented.Add(carRented);
                }
                return View(carsRented);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}