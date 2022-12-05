using App.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        public string AddAnimal(string name, string kind, string breed, string birth, string gen, string own)
        {
            var temp = birth.Split('.');
            var animal = new Animal 
            { AnimalName = name, 
                Kind = kind, 
                Breed = breed, 
                Birth = new DateTime(int.Parse(temp[0]), int.Parse(temp[1]), int.Parse(temp[2])), 
                Gender = gen, 
                OwnerName = own };
            bd.Animals.Add(animal);
            bd.SaveChanges();
            return "Було додано тварину: " + animal.AnimalName;
        }

        [HttpPost]
        public string DeleteAnimal(int Id)
        {
            var animal = bd.Animals.FirstOrDefault(r => r.AnimalId == Id);
            bd.Animals.Remove(animal);
            bd.SaveChanges();
            return "Було видалено тварину за індексом " + Id;
        }

        [HttpPost]
        public string UpdateAnimal(string newName, string newKind, string newBreed, string newBirth, string newGen, string newOwn, int Id)
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
            return "Було оновлено дані тварини за індексом " + Id;
        }

        [HttpGet]
        public ActionResult AdAppointments()
        {
            var appData = (from app in bd.Appointments
                           join an in bd.Animals on app.AnimalId equals an.AnimalId into Animals
                           from anim in Animals.DefaultIfEmpty()
                           join v in bd.Vets on app.VetId equals v.VetId into Vets
                           from vet in Vets.DefaultIfEmpty()
                           join s in bd.Services on app.ServiceId equals s.ServiceId into Servs
                           from serv in Servs.DefaultIfEmpty()
                           select new AppointmentData
                           {
                               ClName = app.ClientName,
                               AnName = anim.AnimalName,
                               VetName = vet.VetName,
                               ServName = serv.ServiceName,
                               Time = app.ATime,
                           }).ToList();

            ViewBag.Appointments = appData;
            return View();
        }

        [HttpPost]
        public string DeleteApp(int Id)
        {
            var app = bd.Appointments.FirstOrDefault(r => r.AppointmentId == Id);
            bd.Appointments.Remove(app);
            bd.SaveChanges();
            return "Було видалено запис за індексом " + Id;
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
        public string AddVet(Vet vet)
        {
            bd.Vets.Add(vet);
            bd.SaveChanges();
            return "Було додано лікаря: " + vet.VetName;
        }

        [HttpPost]
        public string DeleteVet(int Id)
        {
            var vet = bd.Vets.FirstOrDefault(r => r.VetId == Id);
            bd.Vets.Remove(vet);
            bd.SaveChanges();
            return "Було видалено лікаря за індексом " + Id;
        }

        [HttpPost]
        public string UpdateVet(string newName, string newSpec, int Id)
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
            return "Було оновлено дані ветеринара за індексом " + Id;
        }
    }
}