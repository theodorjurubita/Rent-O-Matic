using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Rent_O_Matic.DTOs;
using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;

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

        //POST /api/car
        [HttpPost]
        public IHttpActionResult CreateCustomer(CarDto carDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var car = Mapper.Map<CarDto, Car>(carDto);
            _context.Cars.Add(car);
            _context.SaveChanges();

            carDto.Id = car.Id;
            return Created(new Uri(Request.RequestUri + "/" + car.Id), carDto);
        }

        //PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CarDto carDto)
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
