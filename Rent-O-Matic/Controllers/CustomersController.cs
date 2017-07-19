using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Rent_O_Matic.Controllers
{
    [Authorize(Roles = RoleName.CanManageCars)]
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
            var customer = new CustomerForHistoryViewModel()
            {
                Stores = stores
            };


            return View(customer);
        }

        [HttpPost]
        public ActionResult Save(CustomerForHistoryViewModel customer)
        {

            ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                var customerViewModel = new CustomerForHistoryViewModel()
                {
                    Stores = _context.Stores.ToList(),
                    Name = customer.Name,
                    CarId = customer.CarId,
                    DateRented = customer.DateRented,
                    DateReturned = customer.DateReturned,
                    DrivingLiscense = customer.DrivingLiscense,
                    Nationality = customer.Nationality,
                    StoreId = customer.StoreId,
                    YearsOld = customer.YearsOld

                };
                return View("New", customerViewModel);

            }

            if (customer.Id == 0)
            {
                var cust = new Customer()
                {
                    Name = customer.Name,
                    CarId = customer.CarId,
                    DrivingLiscense = customer.DrivingLiscense,
                    Nationality = customer.Nationality,
                    StoreId = customer.StoreId,
                    YearsOld = customer.YearsOld
                };
                var transaction = new RentalsHistory()
                {
                    DateRented = customer.DateRented,
                    DateReturned = customer.DateReturned,
                    CarId = customer.CarId ?? default(int),
                    IncidentGravityId = 5,
                    FinalPrice = _context.Cars.Single(c => c.Id == customer.CarId).Price

                };
                _context.RentalsHistories.Add(transaction);
                _context.Customers.Add(cust);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Nationality = customer.Nationality;
                customerInDb.DrivingLiscense = customer.DrivingLiscense;
                customerInDb.YearsOld = customer.YearsOld;

                var carRentedBefore = _context.Cars.Single(c => c.Id == customerInDb.CarId);
                carRentedBefore.IsRented = false;

                customerInDb.CarId = customer.CarId;
                customerInDb.StoreId = customer.StoreId;
                var carRentedNow = _context.Cars.Single(c => c.Id == customer.CarId);

                var rentalHist = new RentalsHistory()
                {
                    DateRented = customer.DateRented,
                    DateReturned = customer.DateReturned,
                    CarId = customer.CarId ?? default(int),
                    IncidentGravityId = 5,
                    FinalPrice = CarsController.GetFinalPrice(customer.Id, carRentedNow),
                    CustomerId = customerInDb.Id

                };
                _context.RentalsHistories.Add(rentalHist);
            }
            var carRented = _context.Cars.Single(c => c.Id == customer.CarId);
            carRented.IsRented = true;

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

            var customerViewModel = new CustomerForHistoryViewModel()
            {
                Id = id,
                Name = customer.Name,
                CarId = customer.CarId,
                DrivingLiscense = customer.DrivingLiscense,
                Nationality = customer.Nationality,
                StoreId = customer.StoreId,
                YearsOld = customer.YearsOld,
                Stores = _context.Stores.ToList(),
                Cars = _context.Cars.Where(c => c.StoreId == customer.StoreId)

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