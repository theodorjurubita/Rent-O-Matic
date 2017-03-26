using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;

namespace Rent_O_Matic.Controllers
{
    [Authorize]
    public class StoresController : Controller
    {

        private ApplicationDbContext _context;

        public StoresController()
        {
            _context = new ApplicationDbContext();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Stores
        public ActionResult Index()
        {
            var stores = _context.Stores.ToList();
            return View(stores);
        }

        public ActionResult New()
        {
            var storeViewModel = new StoreViewModel();

            return View(storeViewModel);
        }

        [HttpPost]
        public ActionResult Save(StoreViewModel storeViewModel)
        {

            if (!ModelState.IsValid)
                return View("New", storeViewModel);
            if (storeViewModel.Store.Id == 0)
                _context.Stores.Add(storeViewModel.Store);
            else
            {
                var storeInDb = _context.Stores.Single(c => c.Id == storeViewModel.Store.Id);
                storeInDb.City = storeViewModel.Store.City;
                storeInDb.Country = storeViewModel.Store.Country;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Stores");
        }

        public ActionResult Edit(int id)
        {
            var store = _context.Stores.SingleOrDefault(c => c.Id == id);
            if (store == null)
                return HttpNotFound();
            var storeViewModel = new StoreViewModel()
            {
                Store = store
            };

            return View("New", storeViewModel);
        }
    }
}