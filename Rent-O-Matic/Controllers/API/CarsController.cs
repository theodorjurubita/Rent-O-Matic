using AutoMapper;
using Rent_O_Matic.DTOs;
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
        [HttpPost]
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
        [HttpPut]
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
        [HttpDelete]
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
