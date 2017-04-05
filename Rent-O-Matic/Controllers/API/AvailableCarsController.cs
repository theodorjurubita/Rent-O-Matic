using AutoMapper;
using Rent_O_Matic.DTOs;
using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;
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
            return _context.Cars
                .Where(a => a.IsRented == false)
                .Include(a => a.Store)
                .ToList()
                .Select(Mapper.Map<Car, CarDto>);
        }

    }
}
