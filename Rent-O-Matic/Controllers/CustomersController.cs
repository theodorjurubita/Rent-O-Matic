using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("New", "Customers");
        }


        [HttpPost]
        public ActionResult GetCarsByStoreId(int storeId)
        {
            IEnumerable<Car> cars = new List<Car>();
            cars = _context.Cars.Where(c => c.StoreId == storeId).ToList();
            SelectList carsList = new SelectList(cars,"Id","Model",0);
            return Json(carsList);
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }
    }
}