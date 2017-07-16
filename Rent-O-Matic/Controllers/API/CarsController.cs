using AutoMapper;
using Rent_O_Matic.DTOs;
using Rent_O_Matic.GraphicModels;
using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Rent_O_Matic.Controllers.API
{
    public class CarsController : ApiController
    {
        private ApplicationDbContext _context;

        public CarsController()
        {
            _context = new ApplicationDbContext();

        }

        [Route("api/cars/getBrandStatistics")]
        [HttpGet]
        public IHttpActionResult GetBrandStatistics()
        {

            var cars = _context.Cars.ToList();
            var brandStatistics = new List<CarBrandStatistics>();
            var brandList = new List<string>();
            var brandModelList = new Dictionary<string, List<CarModelStatistics>>();
            var contor = 0;

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

            var topFiveCarBrandStatistics = new List<CarBrandStatistics>();
            double cumulativePercentage = 0;

            if (brandStatistics.Count >= 5)
            {
                for (var i = 0; i < 5; i++)
                {
                    topFiveCarBrandStatistics.Add(brandStatistics[i]);
                    cumulativePercentage += brandStatistics[i].Percentace;
                }
                topFiveCarBrandStatistics.Add(new CarBrandStatistics("Others", 100 - cumulativePercentage));
            }
            else
            {
                return Json(brandStatistics);
            }

            return Json(topFiveCarBrandStatistics);

        }

        // GET /api/cars
        public IEnumerable<CarDto> GetCars()
        {
            var pastRentals = _context.RentalsHistories.Where(h => h.DateReturned < DateTime.Today).ToList();
            foreach (var historyCar in pastRentals)
            {
                var car = _context.Cars.Single(c => c.Id == historyCar.CarId);
                if (car == null) continue;
                car.IsRented = false;
                _context.SaveChanges();
            }
            var carz = _context.RentalsHistories.Where(h => h.DateRented == DateTime.Today).ToList();
            foreach (RentalsHistory historyCar in carz)
            {
                var car = _context.Cars.Single(c => c.Id == historyCar.CarId);
                if (car == null) continue;
                car.IsRented = true;
                _context.SaveChanges();
            }
            return _context.Cars
                .Include(c => c.Store)
                .ToList()
                .Select(Mapper.Map<Car, CarDto>);
        }

        // GET /api/cars/1
        public IHttpActionResult GetCars(int id)
        {
            var car = _context.Cars.SingleOrDefault(c => c.Id == id);

            if (car == null)
                return NotFound();

            return Ok(Mapper.Map<Car, CarDto>(car));
        }

        //POST /api/cars
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateCar(CarDto carDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var car = Mapper.Map<CarDto, Car>(carDto);
            _context.Cars.Add(car);
            _context.SaveChanges();

            carDto.Id = car.Id;
            return Created(new Uri(Request.RequestUri + "/" + car.Id), carDto);
        }

        //PUT /api/cars/1
        [System.Web.Http.HttpPut]
        public void UpdateCar(int id, CarDto carDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var carInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (carInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(carDto, carInDb);

            _context.SaveChanges();
        }

        //DELETE /api/cars/1
        [System.Web.Http.HttpDelete]
        public void DeleteCar(int id)
        {

            var car = _context.Cars.SingleOrDefault(c => c.Id == id);

            if (car == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Cars.Remove(car);
            _context.SaveChanges();
        }
    }
}
