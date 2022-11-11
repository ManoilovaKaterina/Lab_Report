using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Diagnosis
    {
        public int DiagnosisId { get; set; }
        public int AnId { get; set; }
        public string Information { get; set; }
    }
    public class Animal
    {
        public int AnimalId { get; set; }  // ID тварини
        public string Name { get; set; } // Ім'я
        public string Kind { get; set; } // Вид
        public string Breed { get; set; } // Порода
        public int Age { get; set; } // Вік
        public string Gen { get; set; } // Стаь

        public string Owner { get; set; } // Власник
        public List<Diagnosis> Diag { get; set; } = new List<Diagnosis>();
    }
}