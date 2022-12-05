using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; } // Id прийому
        public string ClientName { get; set; } // Id клієнта
        public int AnimalId { get; set; } // Id пацієнта
        public int VetId { get; set; } // Id лікаря
        public int ServiceId { get; set; } // Тип послуги
        public DateTime ATime { get; set; } // Час прийому
        public string AState { get; set; } // Стан прийому
    }

    public class AppointmentData
    {
        public string ClName { get; set; }
        public string AnName { get; set; }
        public string VetName { get; set; }
        public string ServName { get; set; }
        public DateTime Time { get; set; }
    }
}