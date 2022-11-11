using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace App.Models
{
    public class ClinicDbInitializer : DropCreateDatabaseAlways<ClinicContext>
    {
        protected override void Seed(ClinicContext db)
        {
            // послуги
            db.Services.Add(new Serv
            {
                ServId = 1,
                Name = "Терапія",
                Description = "Терапія – область ветеринарної медицини, що займається безпосередньо лікуванням патологій, " +
                "полегшенням стану хворого та усуненням симптомів захворювань.",
                BasePrice = 500
            });
            db.Services.Add(new Serv
            {
                ServId = 2,
                Name = "Хірурія",
                Description = "Хірургія необхідна у тих випадках, коли терапевтичне лікування не допомагає або потрібне планове втручання.",
                BasePrice = 3000
            });
            db.Services.Add(new Serv
            {
                ServId = 3,
                Name = "Офтальмологія",
                Description = "Ветеринарний офтальмолог проводить обстеження, визначає діагноз і вибирає напрямок комплексного лікування.",
                BasePrice = 400
            });

            // лікарі
            db.Vets.Add(new Vet
            {
                VetId = 1,
                Name = "ім'я 1",
                Specialization = "Терапевт"
            });
            db.Vets.Add(new Vet
            {
                VetId = 2,
                Name = "ім'я 2",
                Specialization = "Хірург"
            });
            db.Vets.Add(new Vet
            {
                VetId = 2,
                Name = "ім'я 3",
                Specialization = "Офтальмолог"
            });

            // тварини
            db.Animals.Add(new Animal
            {
                AnimalId = 1,
                Name = "Барні",
                Owner = "Чиженко Євгенія",
                Kind = "Собака",
                Breed = "Вельш коргі",
                Age = 2,
                Gen = "Чоловіча"
            });
            db.Animals.Add(new Animal
            {
                AnimalId = 2,
                Name = "Ася",
                Owner = "Шевченко Володимир",
                Kind = "Собака",
                Breed = "Хаскі",
                Age = 4,
                Gen = "Жіноча"
            });
            db.Animals.Add(new Animal
            {
                AnimalId = 3,
                Name = "Рекс",
                Owner = "Тарасенко Анастасія",
                Kind = "Собака",
                Breed = "Німецька вівчарка",
                Age = 6,
                Gen = "Чоловіча"
            });

            base.Seed(db);
        }
    }
}