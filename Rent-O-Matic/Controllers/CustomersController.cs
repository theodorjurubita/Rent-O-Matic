using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;

namespace Rent_O_Matic.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var stores = _context.Stores.ToList();
            CustomerViewModel customer = new CustomerViewModel()
            {
                Stores = stores
            };


            return View(customer);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Nationality = customer.Nationality;
                customerInDb.DrivingLiscense = customer.DrivingLiscense;
                customerInDb.YearsOld = customer.YearsOld;
                customerInDb.CarId = customer.CarId;
                customerInDb.StoreId = customer.StoreId;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }


        [HttpPost]
        public ActionResult GetCarsByStoreId(int storeId)
        {
            IEnumerable<Car> cars = new List<Car>();
            cars = _context.Cars.Where(c => c.StoreId == storeId).ToList();
            SelectList carsList = new SelectList(cars, "Id", "Model", 0);
            return Json(carsList);
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Customer = customer,
                Stores = _context.Stores.ToList()
            };

            return View("New", customerViewModel);
        }

        [HttpPost]
        public ActionResult GetCarBrandById(int id)
        {
            IEnumerable<Car> car = new List<Car>();
            car = _context.Cars.Where(c => c.Id == id);
            SelectList carzz = new SelectList(car, "Id", "Model", 0);
            return Json(carzz);
        }
    }
}