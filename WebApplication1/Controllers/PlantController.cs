using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PlantController : Controller
    {
        private ILogger<PlantController> _logger;
        private DatabaseContext database;
        public PlantController(ILogger<PlantController> logger, DatabaseContext context)
        { // Update this
            _logger = logger;
            database = context; // Add this line
        }

        public IActionResult Index()
        {
            return View((from Plant in database.Plants select Plant).ToList());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Plant obj = database.Plants.Find(id);
            if (obj==null)
            {
                obj = new Plant();
            }
            return View(obj);
        }
        public IActionResult Details(int id)
        {
            return View(database.Plants.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Plant obj)
        {
            bool isNew = false;
            try
            {
                Plant target = database.Plants.Find(obj.Id);
                if (target == null)
                {
                    target = new Plant();
                    isNew = true;
                }
                target.Name = obj.Name;
                target.Description = obj.Description;
                target.PricePerUnit = obj.PricePerUnit;
                target.MaxTemperature = obj.MaxTemperature;
                target.MinTemperature = obj.MinTemperature;
                target.Humidity = obj.Humidity;
                if (isNew)
                {
                    database.Plants.Add(target);
                }
                else
                {
                    database.Plants.Update(target);
                };
                database.SaveChanges();
            }
            catch (System.Exception)
            {
                return RedirectToAction("Error");
            }
            finally
            {
                database.Dispose();
            }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Remove(int id)
        {
            return View(database.Plants.Find(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(Plant obj)
        {
            try
            {
                Plant target = database.Plants.Find(obj.Id);
                if (target != null)
                {
                    database.Plants.Remove(target);
                    database.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                return RedirectToAction("Error");
            }
            finally { 
                database.Dispose();
            }
            
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
