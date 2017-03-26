﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;

namespace Rent_O_Matic.Controllers
{
    public class CarsController : Controller
    {

        private ApplicationDbContext _context;

        public CarsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Cars
        public ActionResult Random()
        {
            var cars = _context.Cars.ToList();

            return View(cars);
        }

        [Authorize]
        public ActionResult New()
        {
            var stores = _context.Stores.ToList();
            var viewModel = new CarViewModel()
            {
                Stores = stores
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(CarViewModel carViewModel)
        {
            var stores = _context.Stores.ToList();
            carViewModel.Stores = stores;
            if (!ModelState.IsValid)
                return View("New", carViewModel);
            if (carViewModel.Car.Id == 0)
                _context.Cars.Add(carViewModel.Car);
            else
            {
                var carInDb = _context.Cars.Single(c => c.Id == carViewModel.Car.Id);
                carInDb.Model = carViewModel.Car.Model;
                carInDb.Brand = carViewModel.Car.Brand;
                carInDb.StoreId = carViewModel.Car.StoreId;
                carInDb.Price = carViewModel.Car.Price;
                carInDb.Year = carViewModel.Car.Year;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Cars");
        }

        [Route("cars/years/{year}")]
        public ActionResult ByYear(int year)
        {
            return Content(year.ToString());
        }


        //GET: Cars
        public ActionResult Index()
        {
            var cars = _context.Cars.Include(c => c.Store).ToList();
            return View(cars);
        }
        public ActionResult Edit(int id)
        {

            var car = _context.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
                return HttpNotFound();
            var carViewModel = new CarViewModel
            {
                Car = car,
                Stores = _context.Stores.ToList()
            };
            return View("New", carViewModel);
        }


    }
}