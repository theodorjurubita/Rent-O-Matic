using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
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

        [Authorize]
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
        [Authorize]
        public ActionResult Save(Car car)
        {
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
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Cars");
        }

        [Route("cars/years/{year}")]
        public ActionResult ByYear(int year)
        {
            return Content(year.ToString());
        }


        //GET: Cars
        public ActionResult Index()
        {
            var cars = _context.Cars.Include(c => c.Store).ToList();
            return View(cars);
        }
        public ActionResult Edit(int id)
        {

            var car = _context.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
                return HttpNotFound();
            var carViewModel = new CarViewModel
            {
                Car = car,
                Stores = _context.Stores.ToList()
            };
            return View("New", carViewModel);
        }


    }
}