using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Animal
    {
        [Key]
        public int AnimalId { get; set; }  // ID тварини
        public string AnimalName { get; set; } // Ім'я
        public string Kind { get; set; } // Вид
        public string Breed { get; set; } // Порода
        public DateTime Birth { get; set; } // Вік
        public string Gender { get; set; } // Стаь

        public string OwnerName { get; set; } // Власник
    }
}