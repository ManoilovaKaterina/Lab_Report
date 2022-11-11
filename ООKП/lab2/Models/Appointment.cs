using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; } // Id прийому
        public string ClientName { get; set; } // Id клієнта
        public string Client { get; set; } // клієнт
        public string PatientName { get; set; } // Id пацієнта
        public Animal Patient { get; set; } // пацієнт
        public string Service { get; set; } // Тип послуги
        public int VetId { get; set; } // Id лікаря
        public string Time { get; set; } // Час прийому
        public string State { get; set; } // Стан прийому
    }
}