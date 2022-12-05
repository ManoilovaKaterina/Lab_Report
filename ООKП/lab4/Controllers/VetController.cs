using App.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class VetController : Controller
    {
        ClinicContext bd = new ClinicContext();

        [HttpGet]
        public ActionResult VetPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewAnimals(string Filter, string sortOrder)
        {
            ViewBag.NameSortParm = sortOrder == "Kind" ? "kind_desc" : "Kind";
            IEnumerable<Animal> animals = bd.Animals;

            ViewData["CurrentFilter"] = Filter;

            if (!String.IsNullOrEmpty(Filter))
            {
                animals = animals.Where(s => s.Kind.Contains(Filter)).ToList();
            }

            switch (sortOrder)
            {
                case "kind_desc":
                    animals = animals.OrderBy(s => s.Kind).ToList();
                    break;
                default:
                    animals = animals.OrderBy(s => s.AnimalName).ToList();
                    break;
            }

            ViewBag.Animals = animals;

            return View();
        }
        [HttpGet]
        public ActionResult ViewAppointments(string Filter, string sortOrder)
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
                                       ClName = app.ClientName,
                                       AnName = anim.AnimalName,
                                       VetName = vet.VetName,
                                       ServName = serv.ServiceName,
                                       Time = app.ATime,
                                   }).ToList();

            ViewData["CurrentFilter"] = Filter;

            if (!String.IsNullOrEmpty(Filter))
            {
                appData = appData.Where(s => s.VetName.Contains(Filter)).ToList();
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

            ViewBag.Appointments = appData;
            return View();
        }

        [HttpGet]
        public ActionResult ViewDiagnosis(int PatientId)
        {
            Animal Patient = bd.Animals.FirstOrDefault(r => r.AnimalId == PatientId);
                ViewBag.Patient = Patient;
                ViewBag.Diagnoses = bd.Diagnoses.Where(r => r.AnimalId == PatientId);
                return View();
        }

        [HttpPost]
        public ActionResult AddDiagnosis(Diagnoses Diag)
        {
            var Patient = bd.Animals.FirstOrDefault(r => r.AnimalId == Diag.AnimalId);
            bd.Diagnoses.Add(Diag);
            bd.SaveChanges();
            return RedirectToAction("ViewDiagnosis", new { PatientId = Diag.AnimalId });
        }

        [HttpPost]
        public ActionResult DeleteDiagnosis(int Id, int pId)
        {
            var Diag = bd.Diagnoses.FirstOrDefault(r => r.DiagnosisId == Id);
            bd.Diagnoses.Remove(Diag);
            bd.SaveChanges();
            return RedirectToAction("ViewDiagnosis", new { PatientId = pId });
        }

        [HttpPost]
        public ActionResult UpdateDiagnosis(string val, int Id, int pId)
        {
            var Diag = bd.Diagnoses.FirstOrDefault(r => r.DiagnosisId == Id);
            Diag.Information = val;
            bd.SaveChanges();
            return RedirectToAction("ViewDiagnosis", new { PatientId = pId });
        }
    }
}