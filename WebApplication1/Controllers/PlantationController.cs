using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PlantationController : Controller
    {
        private ILogger<PlantationController> _logger;
        private DatabaseContext database;
        public PlantationController(ILogger<PlantationController> logger, DatabaseContext context)
        {
            _logger = logger;
            database = context;
        }
        #region INDEX
        public IActionResult Index()
        {
            return View((from Plantation in database.Plantation select Plantation).ToList());
        }
        #endregion
        #region EDIT    
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Plantation obj = database.Plantation.Find(id);
            if (obj == null)
            {
                obj = new Plantation();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Plantation obj)
        {
            bool isNew = false;
            try
            {
                Plantation target = database.Plantation.Find(obj.Id);
                if (target == null)
                {
                    target = new Plantation();
                    isNew = true;
                }
                target.Name = obj.Name;
                target.Description = obj.Description;
                target.PlantId = obj.PlantId;
                if (isNew)
                {
                    database.Plantation.Add(target);
                }
                else
                {
                    database.Plantation.Update(target);
                };
                database.SaveChanges();
            }
            catch (System.Exception e)
            {
                return RedirectToAction("Error");
            }
            finally { database.Dispose(); };
            
            return RedirectToAction("Index");
        }
        #endregion
        #region DETAILS
        public IActionResult Details(int id)
        {
            return View(database.Plantation.Find(id));
        }
        #endregion
        #region REMOVE
        [HttpGet]
        public IActionResult Remove(int id)
        {
            return View(database.Plantation.Find(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(Plantation obj)
        {
            try
            {
                Plantation target = database.Plantation.Find(obj.Id);
                if (target != null)
                {
                    database.Plantation.Remove(target);
                    database.SaveChanges();
                }
                
            }
            catch (System.Exception e)
            {
                return RedirectToAction("Error");
            }
            finally
            {
                database.Dispose();
            }

            return RedirectToAction("Index");
        }
        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
