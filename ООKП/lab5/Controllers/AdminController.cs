using App.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class AdminController : Controller
    {
        ClinicContext bd = new ClinicContext();

        [HttpGet]
        public ActionResult AdminPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AdAnimals()
        {
            IEnumerable<Animal> animals = bd.Animals;
            ViewBag.Animals = animals;
            return View();
        }

        [HttpPost]
        public ActionResult AddAnimal(string name, string kind, string breed, string birth, string gen, string own)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var temp = birth.Split('.');
                    var animal = new Animal
                    {
                        AnimalName = name,
                        Kind = kind,
                        Breed = breed,
                        Birth = new DateTime(int.Parse(temp[0]), int.Parse(temp[1]), int.Parse(temp[2])),
                        Gender = gen,
                        OwnerName = own
                    };
                    bd.Animals.Add(animal);
                    bd.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("AdAnimals");
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return RedirectToAction("AdAnimals");
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteAnimal(int Id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var animal = bd.Animals.FirstOrDefault(r => r.AnimalId == Id);
                    bd.Animals.Remove(animal);
                    bd.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("AdAnimals");
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return RedirectToAction("AdAnimals");
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateAnimal(string newName, string newKind, string newBreed, string newBirth, string newGen, string newOwn, int Id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var animal = bd.Animals.FirstOrDefault(r => r.AnimalId == Id);
                    if (!String.IsNullOrEmpty(newName))
                    {
                        animal.AnimalName = newName;
                    }
                    if (!String.IsNullOrEmpty(newKind))
                    {
                        animal.Kind = newKind;
                    }
                    if (!String.IsNullOrEmpty(newBreed))
                    {
                        animal.Breed = newBreed;
                    }
                    if (!String.IsNullOrEmpty(newBirth))
                    {
                        var temp = newBirth.Split('.');
                        animal.Birth = new DateTime(int.Parse(temp[0]), int.Parse(temp[1]), int.Parse(temp[2]));
                    }
                    if (!String.IsNullOrEmpty(newGen))
                    {
                        animal.Gender = newGen;
                    }
                    if (!String.IsNullOrEmpty(newOwn))
                    {
                        animal.OwnerName = newOwn;
                    }
                    bd.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("AdAnimals");
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return RedirectToAction("AdAnimals");
                }
            }
        }

        [HttpGet]
        public ActionResult AdDiagnoses()
        {
            ViewBag.Diagnoses = bd.Diagnoses;
            return View();
        }

        [HttpGet]
        public ActionResult AdVets()
        {
            IEnumerable<Vet> vets = bd.Vets;
            ViewBag.Vets = vets;
            return View();
        }

        [HttpPost]
        public ActionResult AddVet(Vet vet)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    bd.Vets.Add(vet);
                    bd.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("AdVets");
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return RedirectToAction("AdVets");
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteVet(int Id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var vet = bd.Vets.FirstOrDefault(r => r.VetId == Id);
                    bd.Vets.Remove(vet);
                    bd.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("AdVets");
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return RedirectToAction("AdVets");
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateVet(string newName, string newSpec, int Id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var vet = bd.Vets.FirstOrDefault(r => r.VetId == Id);
                    if (!String.IsNullOrEmpty(newName))
                    {
                        vet.VetName = newName;
                    }
                    if (!String.IsNullOrEmpty(newSpec))
                    {
                        vet.Specialization = newSpec;
                    }
                    bd.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("AdVets");
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return RedirectToAction("AdVets");
                }
            }
        }

        [HttpGet]
        public ActionResult AdAppointments(string Filter, string SFilter, string sortOrder)
        {
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var appData = (from app in bd.Appointments
                           join an in bd.Animals on app.AnimalId equals an.AnimalId into Animals
                           from anim in Animals.DefaultIfEmpty()
                           join v in bd.Vets on app.VetId equals v.VetId into Vets
                           from vet in Vets.DefaultIfEmpty()
                           join s in bd.Services on app.ServiceId equals s.ServiceId into Servs
                           from serv in Servs.DefaultIfEmpty()
                           select new AppointmentData
                           {
                               Id = app.AppointmentId,
                               ClName = app.ClientName,
                               AnName = anim.AnimalName,
                               VetName = vet.VetName,
                               ServName = serv.ServiceName,
                               Time = app.ATime,
                               State = app.AState,
                           }).ToList();

            ViewData["VetFilter"] = Filter;

            if (!String.IsNullOrEmpty(Filter))
            {
                appData = appData.Where(s => s.VetName.Contains(Filter)).ToList();
            }

            ViewData["StateFilter"] = SFilter;

            if (!String.IsNullOrEmpty(SFilter))
            {
                appData = appData.Where(s => s.State == SFilter).ToList();
            }

            switch (sortOrder)
            {
                case "date_desc":
                    appData = appData.OrderBy(s => s.Time).ToList();
                    break;
                default:
                    appData = appData.OrderBy(s => s.ClName).ToList();
                    break;
            }

            ViewBag.Vets = bd.Vets;
            ViewBag.Appointments = appData;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateState(string val, int Id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var App = bd.Appointments.FirstOrDefault(r => r.AppointmentId == Id);
                    App.AState = val;
                    bd.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("ViewAppointments");
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return RedirectToAction("ViewAppointments");
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteApp(int Id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var app = bd.Appointments.FirstOrDefault(r => r.AppointmentId == Id);
                    bd.Appointments.Remove(app);
                    bd.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("AdAppointments");
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return RedirectToAction("AdAppointments");
                }
            }
        }
    }
}