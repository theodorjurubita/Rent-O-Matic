using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rent_O_Matic.Models;

namespace Rent_O_Matic.Controllers
{
    public class StoresController : Controller
    {

        private ApplicationDbContext _context;

        public StoresController()
        {
            _context=new ApplicationDbContext();
            
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Stores
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var store=new Store();

            return View(store);
        }

        [HttpPost]
        public ActionResult Create(Store store)
        {
            _context.Stores.Add(store);
            _context.SaveChanges();
            return RedirectToAction("New", "Stores");
        }
    }
}