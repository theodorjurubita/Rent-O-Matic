using Rent_O_Matic.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rent_O_Matic.Models
{
    public class DateReturnedBeforeOtherRentals : ValidationAttribute
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

            if (currentRentalHistory.DateReturned < currentRentalHistory.DateRented)
            {
                return new ValidationResult("DateReturned must be greater or equal than DateRented!");
            }
            var carForRentHistory = _context.RentalsHistories
                .Where(h => h.CarId == currentRentalHistory.Car.Id)
                .Where(h => h.DateRented > DateTime.Today)
                .ToList();
            foreach (var history in carForRentHistory)
            {
                if (history.Id == currentRentalHistory.Id) continue;
                if (currentRentalHistory.DateRented < history.DateRented
                    && currentRentalHistory.DateReturned > history.DateRented)
                {
                    return new ValidationResult("DateReturned must be before "
                                                + history.DateRented.ToString("d")
                                                + " because the car is already booked from then until " + history.DateReturned.ToString("d"));
                }
            }

            return ValidationResult.Success;
        }
    }
}