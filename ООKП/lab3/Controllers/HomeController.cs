using App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
            IEnumerable<ClinicService> services = bd.Services;
            ViewBag.Services = services;
            IEnumerable<Vet> vets = bd.Vets;
            ViewBag.Vets = vets;
            return View();
        }

        [HttpGet]
        public ActionResult ViewService()
        {
            IEnumerable<ClinicService> services = bd.Services;
            ViewBag.Services = services;
            return View();
        }

        [HttpGet]
        public ActionResult OurVets(string Filter)
        {
            IEnumerable<Vet> vets = bd.Vets;
            ViewData["CurrentFilter"] = Filter;

            if (!String.IsNullOrEmpty(Filter))
            {
                vets = vets.Where(s => s.Specialization.Contains(Filter)).ToList();
            }

            ViewBag.Vets = vets;
            return View();
        }

        [HttpGet]
        public ActionResult Appoint()
        {
            return View();
        }
        [HttpPost]
        public string Appoint(AppointmentData appdata)
        {
            var app = new Appointment
            {
                ClientName = appdata.ClName,
                AnimalId = bd.Animals.FirstOrDefault(s => s.AnimalName == appdata.AnName).AnimalId,
                VetId = bd.Vets.FirstOrDefault(s => s.VetName == appdata.VetName).VetId,
                ServiceId = bd.Services.FirstOrDefault(s => s.ServiceName == appdata.ServName).ServiceId,
                ATime = appdata.ATime,
                AState = null
            };
            bd.Appointments.Add(app);
            bd.SaveChanges();
            return app.ClientName + ", вас було записано на прийом";
        }

        [HttpGet]
        public ActionResult ViewPatients()
        {
            return View();
        }
    }
}