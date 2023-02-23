using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace App.Models
{
    interface IOperation
    {
        void AddOp(ClinicContext C, Object O);
        void DeleteOp(ClinicContext C, int Id);
        void UpdateOp(ClinicContext C, Object val, int Id);
    }

    class DiagnosOper : IOperation
    {
        public void AddOp(ClinicContext bd, Object O)
        {
            Diagnoses Diag = (Diagnoses)O;
            var Patient = bd.Animals.FirstOrDefault(r => r.AnimalId == Diag.AnimalId);
            bd.Diagnoses.Add(Diag);
            bd.SaveChanges();
        }
        public void DeleteOp(ClinicContext bd, int Id)
        {
            var Diag = bd.Diagnoses.FirstOrDefault(r => r.DiagnosisId == Id);
            bd.Diagnoses.Remove(Diag);
            bd.SaveChanges();
        }
        public void UpdateOp(ClinicContext bd, Object v, int Id)
        {
            string val = (string)v;
            var Diag = bd.Diagnoses.FirstOrDefault(r => r.DiagnosisId == Id);
            Diag.Information = val;
            bd.SaveChanges();
        }
    }

    class AppointOper : IOperation
    {
        public Animal NewAnimal(ClinicContext bd, string AnName, string OwnName)
        {
            var NewAn = new Animal
            {
                AnimalName = AnName,
                Kind = null,
                Breed = null,
                Birth = default(DateTime),
                Gender = null,
                OwnerName = OwnName,
            };
            bd.Animals.Add(NewAn);
            bd.SaveChanges();
            return NewAn;
        }
        public void AddOp(ClinicContext bd, Object O)
        {
            AppointmentData appdata = (AppointmentData)O;
            var An = bd.Animals.FirstOrDefault(s => s.AnimalName.Contains(appdata.AnName.ToString()) && s.OwnerName.Contains(appdata.ClName.ToString()));
            if (An == null)
            {
                An = NewAnimal(bd, appdata.AnName.ToString(), appdata.ClName);
            }
            var app = new Appointment
            {
                ClientName = appdata.ClName,
                AnimalId = An.AnimalId,
                VetId = bd.Vets.FirstOrDefault(s => s.VetName.Contains(appdata.VetName.ToString())).VetId,
                ServiceId = bd.Services.FirstOrDefault(s => s.ServiceName.Contains(appdata.ServName.ToString())).ServiceId,
                ATime = appdata.Time,
                AState = "Активний"
            };
            bd.Appointments.Add(app);
        }
        public void DeleteOp(ClinicContext bd, int Id)
        {
            var app = bd.Appointments.FirstOrDefault(r => r.AppointmentId == Id);
            bd.Appointments.Remove(app);
            bd.SaveChanges();
        }
        public void UpdateOp(ClinicContext bd, Object v, int Id)
        {
            string val =(string)v;
            var App = bd.Appointments.FirstOrDefault(r => r.AppointmentId == Id);
            App.AState = val;
            bd.SaveChanges();
        }
    }

    class OperDecorator : IOperation
    {
        protected IOperation Op;
        public OperDecorator(IOperation Op)
        {
            this.Op = Op;
        }
        public virtual void AddOp(ClinicContext C, Object O) {}
        public virtual void DeleteOp(ClinicContext bd, int Id) {}
        public virtual void UpdateOp(ClinicContext C, Object val, int Id) {}
    }

    class AdminAppointOper : OperDecorator
    {   
        public AdminAppointOper(AppointOper Op) : base(Op) { }
        public override void AddOp(ClinicContext bd, Object O)
        {
            this.Op.AddOp(bd, O);
        }
        public override void DeleteOp(ClinicContext bd, int Id)
        {
            this.Op.DeleteOp(bd, Id); 
        }

        public override void UpdateOp(ClinicContext bd, Object v, int Id)
        {
            string[] val = (string[])v;
            var App = bd.Appointments.FirstOrDefault(r => r.AppointmentId == Id);
            if (!String.IsNullOrEmpty(val[0]))
            {
                App.AState = val[0];
            }
            if (!String.IsNullOrEmpty(val[1]))
            {
                App.VetId = int.Parse(val[1]);
            }
            bd.SaveChanges();
        }
    }

    class UserAppointOper : OperDecorator
    {
        public bool Ind;
        public Appointment App;
        public Animal An;
        public UserAppointOper(AppointOper Op) : base(Op)
        {
            this.Ind = false;
            this.App = null;
            this.An = null;
        }
        public override void AddOp(ClinicContext bd, Object O)
        {
            AppointmentData appdata = (AppointmentData)O;
            var An = bd.Animals.FirstOrDefault(s => s.AnimalName.Contains(appdata.AnName.ToString()) && s.OwnerName.Contains(appdata.ClName.ToString()));
            if (An == null)
            {
                var Op2 = (AppointOper)Op;
                An = Op2.NewAnimal(bd, appdata.AnName.ToString(), appdata.ClName);
                this.Ind = true;
            }
            this.An = An;
            var app = new Appointment
            {
                ClientName = appdata.ClName,
                AnimalId = An.AnimalId,
                VetId = bd.Vets.FirstOrDefault(s => s.VetName.Contains(appdata.VetName.ToString())).VetId,
                ServiceId = bd.Services.FirstOrDefault(s => s.ServiceName.Contains(appdata.ServName.ToString())).ServiceId,
                ATime = appdata.Time,
                AState = "Активний"
            };
            this.App = app;
            bd.Appointments.Add(app);
        }
        public override void DeleteOp(ClinicContext bd, int Id)
        {
            this.Op.DeleteOp(bd, Id);
        }

        public override void UpdateOp(ClinicContext bd, Object v, int Id)
        {
            this.Op.UpdateOp(bd, v, Id);
        }
    }
}