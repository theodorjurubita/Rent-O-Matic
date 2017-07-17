using Rent_O_Matic.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rent_O_Matic.Models
{
    public class DateRentedOutsideOtherRentalsPeriod : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            RentalsHistory currentRentalHistory;
            if (validationContext.ObjectType.Name.Equals("CustomerForHistoryViewModel"))
            {
                var customerForRental = (CustomerForHistoryViewModel)validationContext.ObjectInstance;
                currentRentalHistory = new RentalsHistory()
                {
                    Id = customerForRental.Id,
                    CarId = customerForRental.CarId ?? default(int),
                    CustomerId = customerForRental.Id,
                    DateRented = customerForRental.DateRented,
                    DateReturned = customerForRental.DateReturned,
                    IncidentGravityId = 5

                };
            }
            else
            {
                currentRentalHistory = (RentalsHistory)validationContext.ObjectInstance;
            }


            var _context = new ApplicationDbContext();

            var laterEdit = _context.RentalsHistories.Include("Car").SingleOrDefault(c => c.Id == currentRentalHistory.Id);

            if (currentRentalHistory.DateRented < DateTime.Today && laterEdit == null)
            {
                return new ValidationResult("DateRented must be greater or equal than today!");
            }

            if (currentRentalHistory.CarId == 0)
            {
                if (currentRentalHistory.Car == null)
                {
                    return new ValidationResult("No car selected yet!");
                }
            }

            if (currentRentalHistory.Car == null)
            {
                var car = _context.Cars.Single(c => c.Id == currentRentalHistory.CarId);
                currentRentalHistory.Car = car;
            }
            var carForRentHistory = _context.RentalsHistories
                .Where(h => h.CarId == currentRentalHistory.Car.Id)
                .Where(h => h.DateRented > DateTime.Today)
                .ToList();

            foreach (var history in carForRentHistory)
            {
                if (history.Id == currentRentalHistory.Id) continue;
                if (currentRentalHistory.DateRented >= history.DateRented &&
                    currentRentalHistory.DateRented <= history.DateReturned)
                {
                    return new ValidationResult("This car is already booked in the interval between "
                                                + history.DateRented + " and " + history.DateReturned);
                }
            }
            return ValidationResult.Success;
        }
    }
}