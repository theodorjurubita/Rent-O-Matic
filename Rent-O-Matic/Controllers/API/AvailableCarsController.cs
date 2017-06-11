using AutoMapper;
using Rent_O_Matic.DTOs;
using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Rent_O_Matic.Controllers.API
{
    public class AvailableCarsController : ApiController
    {
        private ApplicationDbContext _context;

        public AvailableCarsController()
        {
            _context = new ApplicationDbContext();

        }

        // GET /api/availablecars
        public IEnumerable<CarDto> GetCars()
        {
            //var cars = _context.RentalsHistories.SqlQuery(@"Select a.Id, a.CarId, a.CustomerId, a.DateRented, a.DateReturned 
            //                                            FROM RentalsHistories a Where DateReturned=(SELECT MAX(b.DateReturned) 
            //                                            FROM RentalsHistories b Where b.CarId=a.CarId)
            //                                            ").ToList();
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
                .Where(a => a.IsRented == false)
                .Include(a => a.Store)
                .ToList()
                .Select(Mapper.Map<Car, CarDto>);
        }

    }
}
