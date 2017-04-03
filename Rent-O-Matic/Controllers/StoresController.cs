using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;
using System.Linq;
using System.Web.Mvc;

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
            if (User.IsInRole(RoleName.CanManageCars))
                return View("StoresList", stores);
            return View("ReadOnlyStoresList", stores);
        }

        [Authorize(Roles = RoleName.CanManageCars)]
        public ActionResult New()
        {
            var storeViewModel = new StoreViewModel();

            return View(storeViewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageCars)]
        public ActionResult Save(Store store)
        {
            ModelState.Remove("store.Id");
            if (!ModelState.IsValid)
            {
                var storeViewModel = new StoreViewModel()
                {
                    Store = store
                };
                return View("New", storeViewModel);
            }

            if (store.Id == 0)
                _context.Stores.Add(store);
            else
            {
                var storeInDb = _context.Stores.Single(c => c.Id == store.Id);
                storeInDb.City = store.City;
                storeInDb.Country = store.Country;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Stores");
        }

        [Authorize(Roles = RoleName.CanManageCars)]
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