using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class ClinicContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Serv> Services { get; set; }
        public DbSet<Vet> Vets { get; set; }
    }
}