using Microsoft.AspNet.Identity;
using Rent_O_Matic.GraphicModels;
using Rent_O_Matic.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                        PricePerDay = carEntry.FinalPrice,
                        Store = store.City + ", " + store.Country,
                        DateRented = carEntry.DateRented,
                        DateReturned = carEntry.DateReturned
                    };
                    carsRented.Add(carRented);
                }
                var homeCarModel = new HomeCarStatisticsViewModel()
                {
                    CarBrandStatistics = new List<CarBrandStatistics>(),
                    RentedCarsViewModel = carsRented
                };
                return View(homeCarModel);
            }
            catch
            {
                var homeCarModel = new HomeCarStatisticsViewModel()
                {
                    CarBrandStatistics = GetCarBrandStatistics(),
                    RentedCarsViewModel = new List<RentedCarsViewModel>()
                };
                return View(homeCarModel);
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

        public List<CarBrandStatistics> GetCarBrandStatistics()
        {
            var cars = _context.Cars.ToList();
            var brandStatistics = new List<CarBrandStatistics>();
            var brandList = new List<string>();
            var brandModelList = new Dictionary<string, List<CarModelStatistics>>();

            foreach (var car in cars)
            {
                if (!brandList.Contains(car.Brand))
                {
                    var modelStatistics = new List<CarModelStatistics>();
                    var carModels = _context.Cars.Where(c => c.Brand.Equals(car.Brand)).ToList();
                    brandList.Add(car.Brand);
                    var carModelsList = new List<string>();

                    foreach (var carModel in carModels)
                    {
                        carModelsList.Add(carModel.Model);
                    }

                    var modelFrequencyList = carModelsList.ToDictionary(model => model, frequency => 0);

                    var rentalHistoryForBrand = _context.RentalsHistories
                        .Include(c => c.Car)
                        .Where(c => c.Car.Brand == car.Brand)
                        .ToList();
                    var totalRentalsForBrand = rentalHistoryForBrand.Count;
                    foreach (var rental in rentalHistoryForBrand)
                    {
                        modelFrequencyList[rental.Car.Model] += 1;
                    }

                    foreach (var modelFrequency in modelFrequencyList)
                    {
                        var frequency = (double)modelFrequency.Value / (double)totalRentalsForBrand * 100;
                        var modelStat = new CarModelStatistics(modelFrequency.Key, Math.Round(frequency, 2));
                        modelStatistics.Add(modelStat);
                    }


                    brandModelList.Add(car.Brand, modelStatistics);
                }
            }

            var brandFrequencyList = brandList.ToDictionary(brand => brand, frequency => 0);

            var rentalHistory = _context.RentalsHistories.Include(c => c.Car).ToList();
            var totalRentals = rentalHistory.Count;
            foreach (var rental in rentalHistory)
            {
                brandFrequencyList[rental.Car.Brand] += 1;
            }


            foreach (var brandFrequency in brandFrequencyList)
            {
                var frequency = (double)brandFrequency.Value / (double)totalRentals * 100;
                if (brandFrequency.Value != 0)
                {
                    var brandStat = new CarBrandStatistics(brandFrequency.Key, Math.Round(frequency, 2));
                    brandStat.CarModelStatistics = brandModelList[brandFrequency.Key];
                    brandStatistics.Add(brandStat);
                }

            }
            brandStatistics.Sort();

            //var topFiveCarBrandStatistics = new List<CarBrandStatistics>();
            //double cumulativePercentage = 0;

            //if (brandStatistics.Count >= 5)
            //{
            //    for (var i = 0; i < 5; i++)
            //    {
            //        topFiveCarBrandStatistics.Add(brandStatistics[i]);
            //        cumulativePercentage += brandStatistics[i].Percentace;
            //    }
            //    topFiveCarBrandStatistics.Add(new CarBrandStatistics
            //    {
            //        CarModelStatistics = new List<CarModelStatistics>(),
            //        Name = "Others",
            //        Percentace = 100 - cumulativePercentage
            //    });
            //}
            //else
            //{
            //    return brandStatistics;
            //}

            return brandStatistics;
        }
    }
}