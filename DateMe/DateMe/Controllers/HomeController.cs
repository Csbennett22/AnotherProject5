using DateMe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace DateMe.Controllers
{
    public class HomeController : Controller
    {

        private DateApplicationContext daContext { get; set; }

        public HomeController(DateApplicationContext someName)
        {
            daContext = someName;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Movies ()
        {
            ViewBag.Majors = daContext.Majors.ToList();
            return View("MovieApp");
        }
        [HttpPost]
        public IActionResult Movies(ApplicationResponse ar)
        {
            if(ModelState.IsValid)
            {
                daContext.Add(ar);
                daContext.SaveChanges();
                return View("Confirmation", ar); // pass in the bundled up ar
            }
            else
            {
                ViewBag.Majors = daContext.Majors.ToList();

                return View("MovieApp");
            }
        }
        [HttpGet]
        public IActionResult MovieList()
        {
            var applications = daContext.Responses
                .Include(x => x.Major) // also include the major object
                .Where(x => x.Stalker == false) //c# uses double = sign
                .OrderBy(x => x.LastName) // filtering the responses that are returned to the website
                .ToList();
            return View("List", applications);
        }
        [HttpGet]
        public IActionResult Edit(int applicationid) // this name has to match what the view has for asp-route- ________
        {
            ViewBag.Majors = daContext.Majors.ToList();
            
            var application = daContext.Responses.Single(x => x.ApplicationId == applicationid); // find the single record where the app id is the same as one of the ones in the database
            
            return View("DatingApp", application);
        }
        [HttpPost]
        public IActionResult Edit(ApplicationResponse ar)
        {
            daContext.Update(ar);
            daContext.SaveChanges();
            
            return RedirectToAction("Waitlist"); // makes it so we don't have to load up all of the same information for that action
        }
        [HttpGet]
        public IActionResult Delete(int applicationid)
        {

            var application = daContext.Responses.Single(x => x.ApplicationId == applicationid);

            return View(application);
        }
        [HttpPost]
        public IActionResult Delete(ApplicationResponse ar)
        {
            daContext.Responses.Remove(ar);
            daContext.SaveChanges();
            return RedirectToAction("Waitlist");
        }
    }
}
