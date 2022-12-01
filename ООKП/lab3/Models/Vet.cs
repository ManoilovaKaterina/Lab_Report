using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Vet
    {
        [Key]
        public int VetId { get; set; }  // ID ветеринара
        public string VetName { get; set; } // Ім'я
        public string Specialization { get; set; } // Спеціалізація
    }
}