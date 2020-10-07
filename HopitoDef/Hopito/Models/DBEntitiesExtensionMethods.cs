using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hopito.Models
{
    public static class DBEntitiesExtensionMethods
    {
        public static UserView ToUserView(this User user)
        {
            return new UserView()
            {
                Id = user.id,
                Email = user.email,
                Password = user.password,
                FirstName = user.fname,
                LastName = user.name,
                Birthdate = user.dateNaissance,
                Phone = user.tel,
                NAM = user.assMaladie,
                Admin = user.admin
            };
        }
        public static bool EmailExist(this HopitoDBEntities DB, string userName)
        {
            User user = DB.Users.Where(u => u.email == userName).FirstOrDefault();
            return (user != null);
        }
        public static User FindByEmail(this HopitoDBEntities DB, string userName)
        {
            User user = DB.Users.Where(u => u.email == userName).FirstOrDefault();
            return user;
        }
        public static UserView AddUser(this HopitoDBEntities DB, UserView userView)
        {
            User user = userView.ToUser();
            user = DB.Users.Add(user);
            DB.SaveChanges();
            return user.ToUserView();
        }
        public static bool UpdateUser(this HopitoDBEntities DB, UserView userView)
        {
            User userToUpdate = DB.Users.Find(userView.Id);
            userView.CopyToUser(userToUpdate);
            DB.Entry(userToUpdate).State = EntityState.Modified;
            DB.SaveChanges();
            return true;
        }
        public static bool RemoveUser(this HopitoDBEntities DB, UserView userView)
        {
            User userToDelete = DB.Users.Find(userView.Id);
            DB.Users.Remove(userToDelete);
            DB.SaveChanges();
            return true;
        }
        public static PatientView ToPatientView(this Patient patient)
        {
            return new PatientView()
            {
                Id = patient.id,
                Name = patient.nom,
                ArrivalTime = patient.arrivalTime,
                Priority = patient.priorite,
                IdHospital = patient.idHopital,
                IdUser = (int)patient.idUser,
            };
        }
        
        public static List<PatientView> PatientsList(this HopitoDBEntities DB)
        {
            List<PatientView> patients = new List<PatientView>();
            if (DB.Patients != null)
            {
                foreach (Patient patient in DB.Patients)
                {
                    patients.Add(patient.ToPatientView());
                }
            }
            return patients.OrderBy(f => f.ArrivalTime).ToList();
        }
        public static List<UserView> UsersList(this HopitoDBEntities DB)
        {
            List<UserView> users = new List<UserView>();
            if (DB.Users != null)
            {
                foreach (User user in DB.Users)
                {
                    users.Add(user.ToUserView());
                }
            }
            return users;
        }
        public static bool IdUserExists(this HopitoDBEntities DB, int idUser)
        {
            Patient patient = DB.Patients.Where(u => u.idUser == idUser).FirstOrDefault();
            return (patient != null);
        }
        public static Patient FindIdUser(this HopitoDBEntities DB, int idUser)
        {
            Patient patient = DB.Patients.Where(u => u.idUser == idUser).FirstOrDefault();
            return patient;
        }
        public static PatientView AddPatient(this HopitoDBEntities DB, PatientView patientView)
        {
            Patient patient = patientView.ToPatient();
            patient = DB.Patients.Add(patient);
            DB.SaveChanges();
            return patient.ToPatientView();
        }
        public static bool UpdatePatient(this HopitoDBEntities DB, PatientView patientView)
        {
            Patient patientToUpdate = DB.Patients.Find(patientView.Id);
            patientView.CopyToPatient(patientToUpdate);
            DB.Entry(patientToUpdate).State = EntityState.Modified;
            DB.SaveChanges();
            return true;
        }
        public static bool RemovePatient(this HopitoDBEntities DB, PatientView patientView)
        {
            Patient patientToDelete = DB.Patients.Find(patientView.Id);
            DB.Patients.Remove(patientToDelete);
            DB.SaveChanges();
            return true;
        }
        public static bool UpdateHospital(this HopitoDBEntities DB, HospitalView hospitalView)
        {
            Hopitaux hospitalToUpdate = DB.Hopitauxes.Find(hospitalView.Id);
            hospitalView.CopyToHopitaux(hospitalToUpdate);
            DB.Entry(hospitalToUpdate).State = EntityState.Modified;
            DB.SaveChanges();
            return true;
        }
    }
}