using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class VetController : Controller
    {
        ClinicContext bd = new ClinicContext();

        [HttpGet]
        public ActionResult ViewAnimals()
        {
            IEnumerable<Animal> animals = bd.Animals;
            ViewBag.Animals = animals;
            return View();
        }

        [HttpGet]
        public ActionResult AddDiagnosis(int PatientId)
        {
            IEnumerable<Animal> patients = bd.Animals;
            foreach (Animal an in patients)
            {
                if (an.AnimalId == PatientId)
                    ViewBag.Patient = an;
            }
            return View();
        }
        [HttpPost]
        public string AddDiagnosis(Diagnosis Diag)
        {
            IEnumerable<Animal> patients = bd.Animals;
            Animal Patient = null;
            foreach (Animal an in patients)
            {
                if (an.AnimalId == Diag.AnId)
                    Patient = an;
            }
            Diag.DiagnosisId = Patient.Diag.Count + 1;
            Patient.Diag.Add(Diag);
            bd.SaveChanges();
            return "Було додано діагноз: " + Diag.Information + " пацієнту: " + Patient.Kind + " " + Patient.Breed + " " + Patient.Name;
        }
    }
}