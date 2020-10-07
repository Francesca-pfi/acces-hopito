using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hopito.Models
{
    public class PatientView
    {
        public int Id { get; set; }
        public User Compte { get; set; }
        public string Name { get; set; }
        public bool Waiting { get; set; }
        [Display(Name = "Position dans la file d'attente")]
        public int Position { get; set; }
        [Display(Name = "Heure prévue")]
        public DateTime ArrivalTime { get; set; }
        [Display(Name = "Heure prévue")]
        public TimeSpan PlannedTimeDue { get; set; }
        [Display(Name = "Temps d'attente prévu")]
        public DateTime PlannedTimeLeft { get; set; }
        [Display(Name = "Niveau de priorité")]
        public int Priority { get; set; }
        public int IdUser { get; set; }
        public int IdHospital { get; set; }

        public Patient ToPatient()
        {   // Attention cette méthode retourne une instance de User
            // qui ne sera pas utilisable en tant cible de modification
            // car il ne contiendra pas un object context
            // et l'instruction suivante générera une exception
            // DB.Entry(user).State = EntityState.Modified;
            return new Patient()
            {
                id = this.Id,
                nom = this.Name,
                arrivalTime = this.ArrivalTime,
                priorite = this.Priority,
                idHopital = this.IdHospital,
                idUser = this.IdUser,
            };
        }
        public void CopyToPatient(Patient patient)
        { // Utilisez cette fonction pour copier un UserView dans en User
            patient.id = Id;
            patient.nom = Name;
            patient.arrivalTime = ArrivalTime;
            patient.priorite = Priority;
            patient.idHopital = IdHospital;
            patient.idUser = IdUser;
        }
        public void CopyToPatientView(PatientView patient)
        { // Utilisez cette fonction pour copier un UserView dans un autre UserView
            patient.Id = Id;
            patient.Name = Name;
            patient.ArrivalTime = ArrivalTime;
            patient.Priority = Priority;
            patient.IdHospital = IdHospital;
            patient.IdUser = IdUser;
        }

        public PatientView()
        {
            Id = 1;
            Waiting = true;
            Name = "Roger St - Germain";
            Position = 12;
            PlannedTimeDue = new TimeSpan(3, 0, 0);
            PlannedTimeLeft = new DateTime(2020, 9, 30, 14, 23, 0);
            Priority = 4;
        }
        
    }
}