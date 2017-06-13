using Rent_O_Matic.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Rent_O_Matic.Controllers.API
{
    public class CarsHistoryController : ApiController
    {
        public ApplicationDbContext _context;

        public CarsHistoryController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // /api/carsHistory
        public IEnumerable<RentalHistoryViewModel> GetRentalHistory()
        {
            var rentalHistory = _context.RentalsHistories.ToList();
            var rentalHistoryToShow = new List<RentalHistoryViewModel>();


            foreach (var rental in rentalHistory)
            {
                var carRented = _context.Cars.Single(c => c.Id == rental.CarId);
                var rentalTransaction = new RentalHistoryViewModel()
                {
                    RentalHistoryId = rental.Id,
                    CustomerName = _context.Customers.Single(c => c.Id == rental.CustomerId).Name,
                    CarBrand = carRented.Brand,
                    CarModel = carRented.Model,
                    DateRented = rental.DateRented,
                    DateReturned = rental.DateReturned,
                    IncidentGravityId = rental.IncidentGravityId,
                    IncidentGravity = _context.IncidentGravities.ToList()
                };

                rentalHistoryToShow.Add(rentalTransaction);
            }

            return rentalHistoryToShow;
        }
        // /api/carsHistory

        [Route("api/carsHistory/update/{rentalHistoryId}/{incidentGravityId}")]
        [HttpPut]
        public void UpdateRentalHistory(int rentalHistoryId, byte incidentGravityId)
        {
            var rentFromHistory = _context.RentalsHistories.SingleOrDefault(h => h.Id == rentalHistoryId);
            if (rentFromHistory == null) return;
            rentFromHistory.IncidentGravityId = incidentGravityId;
            _context.SaveChanges();
        }
    }
}