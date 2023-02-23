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
        readonly ClinicContext bd = new ClinicContext();
        readonly UserAppointOper AO = new UserAppointOper(new AppointOper());
        readonly DiagnosOper DO = new DiagnosOper();
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
                    AO.AddOp(bd, appdata);
                    scope.Complete();
                    if (AO.Ind)
                    {
                        TempData["InInf"] = "Будь ласка, введіть дані про вашу тварину";
                        TempData["Id"] = AO.An.AnimalId;
                    }
                    return RedirectToAction("SuccessAppoint",
                    new
                    {
                        Id = AO.App.AppointmentId,
                        ClName = appdata.ClName,
                        AnName = appdata.AnName,
                        VetName = appdata.VetName,
                        ServName = appdata.ServName,
                        Time = appdata.Time,
                        State = AO.App.AState,
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
        public ActionResult UpNewAnimal(int Id, string newKind, string newBreed, DateTime Date, string newGen)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var animal = bd.Animals.FirstOrDefault(s => s.AnimalId == Id);
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