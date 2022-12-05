using App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
            ViewBag.Animals = bd.Animals;
            ViewBag.Services = bd.Services;
            ViewBag.Vets = bd.Vets;
            return View();
        }
        [HttpPost]
        public ActionResult Appoint(AppointmentData appdata)
        {
            var app = new Appointment
            {
                ClientName = appdata.ClName,
                AnimalId = bd.Animals.FirstOrDefault(s => s.AnimalName.Contains(appdata.AnName.ToString())).AnimalId,
                VetId = bd.Vets.FirstOrDefault(s => s.VetName.Contains(appdata.VetName.ToString())).VetId,
                ServiceId = bd.Services.FirstOrDefault(s => s.ServiceName.Contains(appdata.ServName.ToString())).ServiceId,
                ATime = appdata.Time,
                AState = null
            };
            bd.Appointments.Add(app);
            bd.SaveChanges();
            return RedirectToAction("SuccessAppoint",
            new {
                ClName = appdata.ClName,
                AnName = appdata.AnName,
                VetName = appdata.VetName,
                ServName = appdata.ServName,
                Time = appdata.Time
            });
        }

        [HttpGet]
        public ActionResult SuccessAppoint(AppointmentData appd)
        {
            ViewBag.Appdata = appd;
            return View();
        }

        [HttpGet]
        public ActionResult ViewPatients()
        {
            return View();
        }
    }
}