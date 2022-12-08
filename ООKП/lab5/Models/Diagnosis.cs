using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Diagnoses
    {
        [Key]
        public int DiagnosisId { get; set; }
        public int AnimalId { get; set; }
        public string Information { get; set; }
    }
}