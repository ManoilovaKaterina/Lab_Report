using App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        ClinicContext bd = new ClinicContext();
        public ActionResult Index()
        {
            IEnumerable<Serv> services = bd.Services;
            ViewBag.Services = services;
            IEnumerable<Vet> vets = bd.Vets;
            ViewBag.Vets = vets;
            return View();
        }

        [HttpGet]
        public ActionResult ViewService()
        {
            IEnumerable<Serv> services = bd.Services;
            ViewBag.Services = services;
            return View();
        }

        [HttpGet]
        public ActionResult OurVets()
        {
            IEnumerable<Vet> vets = bd.Vets;
            ViewBag.Vets = vets;
            return View();
        }

        [HttpGet]
        public ActionResult Appoint()
        {
            return View();
        }
        [HttpPost]
        public string Appoint(Appointment app)
        {
            bd.Appointments.Add(app);
            bd.SaveChanges();
            return app.ClientName + ", вас було записано на прийом";
        }

        [Authorize]
        [HttpGet]
        public ActionResult ViewPatients()
        {
            return View();
        }

        //private ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //}

        //public ActionResult Authorization()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<ActionResult> Authorization(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        IdentityResult result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }
        //        else
        //        {
        //            foreach (string error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error);
        //            }
        //        }
        //    }
        //    return View(model);
        //}
    }
}