using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Vet
    {
        public int VetId { get; set; }  // ID ветеринара
        public string Name { get; set; } // Ім'я
        public string Specialization { get; set; } // Спеціалізація
    }
}