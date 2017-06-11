﻿using Rent_O_Matic.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rent_O_Matic.Models
{
    public class DateRentedOutsideOtherRentalsPeriod : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentRentalHistory = (RentalsHistory)validationContext.ObjectInstance;

            var _context = new ApplicationDbContext();

            var carForRentHistory = _context.RentalsHistories
                .Where(h => h.CarId == currentRentalHistory.Car.Id)
                .Where(h => h.DateRented > DateTime.Today)
                .ToList();

            foreach (var history in carForRentHistory)
            {
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