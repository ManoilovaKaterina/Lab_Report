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
using System.Transactions;
using System.Drawing;
using System.Xml.Linq;

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

        public Animal NewAnimal(string AnName, string OwnName)
        {
            var NewAn = new Animal
            {
                AnimalName = AnName,
                Kind = null,
                Breed = null,
                Birth = default(DateTime),
                Gender = null,
                OwnerName = OwnName,
            };
            bd.Animals.Add(NewAn);
            bd.SaveChanges();
            return NewAn;
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
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    bool Ind = false;
                    var An = bd.Animals.FirstOrDefault(s => s.AnimalName.Contains(appdata.AnName.ToString()) && s.OwnerName.Contains(appdata.ClName.ToString()));
                    if (An == null)
                    {
                        An = NewAnimal(appdata.AnName.ToString(), appdata.ClName);
                        Ind = true;
                    }
                    var app = new Appointment
                    {
                        ClientName = appdata.ClName,
                        AnimalId = An.AnimalId,
                        VetId = bd.Vets.FirstOrDefault(s => s.VetName.Contains(appdata.VetName.ToString())).VetId,
                        ServiceId = bd.Services.FirstOrDefault(s => s.ServiceName.Contains(appdata.ServName.ToString())).ServiceId,
                        ATime = appdata.Time,
                        AState = "Активний"
                    };
                    bd.Appointments.Add(app);
                    bd.SaveChanges();
                    scope.Complete();
                    if (Ind)
                    {
                        TempData["InInf"] = "Будь ласка, введіть дані про вашу тварину";
                    }
                    return RedirectToAction("SuccessAppoint",
                    new
                    {
                        Id = app.AppointmentId,
                        ClName = appdata.ClName,
                        AnName = appdata.AnName,
                        VetName = appdata.VetName,
                        ServName = appdata.ServName,
                        Time = appdata.Time,
                        State = app.AState,
                    });
                }
                catch (Exception)
                {
                    scope.Dispose();
                    TempData["message"] = "Невірно введені дані";
                    return RedirectToAction("Appoint");
                }
            }
        }

        [HttpGet]
        public ActionResult SuccessAppoint(AppointmentData appd)
        {
            ViewBag.Appdata = appd;
            return View();
        }

        [HttpPost]
        public ActionResult UpNewAnimal(string Name, string OwnName, string newKind, string newBreed, DateTime Date, string newGen)
        {
            var animal = bd.Animals.FirstOrDefault(s => s.AnimalName.Contains(Name) && s.OwnerName.Contains(OwnName));

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (!String.IsNullOrEmpty(newKind))
                    {
                        animal.Kind = newKind;
                    }
                    if (!String.IsNullOrEmpty(newBreed))
                    {
                        animal.Breed = newBreed;
                    }
                    if (Date != default(DateTime))
                    {
                        animal.Birth = Date;
                    }
                    if (!String.IsNullOrEmpty(newGen))
                    {
                        animal.Gender = newGen;
                    }
                    bd.SaveChanges();
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();
                }
            }
            return RedirectToAction("Index");
        }
    }

}