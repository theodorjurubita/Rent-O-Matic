using Microsoft.AspNet.Identity;
using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Rent_O_Matic.Controllers
{
    [Authorize]
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


        [Authorize(Roles = RoleName.CanManageCars)]
        public ActionResult New()
        {
            var stores = _context.Stores.ToList();
            var viewModel = new CarViewModel()
            {
                Stores = stores
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageCars)]
        public ActionResult Save(Car car)
        {
            ModelState.Remove("car.Id");
            if (!ModelState.IsValid)
            {
                var carViewModel = new CarViewModel()
                {
                    Car = car,
                    Stores = _context.Stores.ToList()
                };
                return View("New", carViewModel);
            }

            if (car.Id == 0)
                _context.Cars.Add(car);
            else
            {
                var carInDb = _context.Cars.Single(c => c.Id == car.Id);
                carInDb.Model = car.Model;
                carInDb.Brand = car.Brand;
                carInDb.StoreId = car.StoreId;
                carInDb.Price = car.Price;
                carInDb.Year = car.Year;
                var customerInDb = _context.Customers.Single(c => c.CarId == carInDb.Id);
                customerInDb.StoreId = carInDb.StoreId;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Cars");
        }


        //GET: Cars
        public ActionResult Index()
        {
            var cars = _context.Cars.Include(c => c.Store).ToList();
            if (User.IsInRole(RoleName.CanManageCars))
                return View("CarList", cars);
            return View("ReadOnlyCarList", cars);
        }

        [Authorize(Roles = RoleName.CanManageCars)]
        public ActionResult Edit(int id)
        {

            var car = _context.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
            {
                return HttpNotFound();
            }
            var carViewModel = new CarViewModel
            {
                Car = car,
                Stores = _context.Stores.ToList()
            };
            return View("New", carViewModel);
        }

        public ActionResult Rent(int id)
        {
            var car = _context.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
            {
                return HttpNotFound();
            }
            var carStore = _context.Stores.Single(c => c.Id == car.StoreId);

            var carViewModel = new RentCarViewModel()
            {
                Car = car,
                StoreName = carStore.City + ", " + carStore.Country
            };

            return View("Rent", carViewModel);
        }

        [HttpPost]
        public ActionResult SaveRent(RentalsHistory carRented)
        {
            if (!ModelState.IsValid)
            {
                var carStore = _context.Stores.Single(c => c.Id == carRented.Car.StoreId);
                var rentCarViewModel = new RentCarViewModel()
                {
                    Car = carRented.Car,
                    //StoreName = store.City + ", " + store.Country
                    StoreName = carStore.City + ", " + carStore.Country
                };
                return View("Rent", rentCarViewModel);
            }

            var userId = User.Identity.GetUserId();

            var carIsRented = _context.Cars.Single(c => c.Id == carRented.Car.Id);
            if (carRented.DateRented.Date == DateTime.Today)
            {
                carIsRented.IsRented = true;
            }
            carRented.Car = carIsRented;

            var customerAttachedToRental = _context.Customers.Single(c => c.UserId == userId);
            customerAttachedToRental.Store = _context.Stores.Single(c => c.Id == carRented.Car.StoreId);
            customerAttachedToRental.Car = carIsRented;
            carRented.Customer = customerAttachedToRental;
            carRented.IncidentGravity = _context.IncidentGravities.Single(g => g.Id == 5);
            carRented.IncidentGravityId = 5;
            _context.RentalsHistories.Add(carRented);
            _context.SaveChanges();

            return RedirectToAction("Index", "Cars");
        }


    }
}