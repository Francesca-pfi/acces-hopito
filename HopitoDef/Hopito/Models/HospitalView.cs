using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hopito.Models
{
    public class HospitalView
    {
        public int Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; }
        [Display(Name = "Adresse")]
        public string Adress { get; set; }
        [Display(Name = "Ville")]
        public string City { get; set; }
        [Display(Name = "Province")]
        public string Province { get; set; }
        [Display(Name = "Code postal")]
        public string Zip { get; set; }
        [Display(Name = "Numéro de téléphone")]
        public string Phone { get; set; }
        public Hopitaux ToHopital()
        {   // Attention cette méthode retourne une instance de User
            // qui ne sera pas utilisable en tant cible de modification
            // car il ne contiendra pas un object context
            // et l'instruction suivante générera une exception
            // DB.Entry(user).State = EntityState.Modified;
            return new Hopitaux()
            {
                id = Id,
                nom = Name,
                adresse = Adress,
                ville = City,
                province = Province,
                codePost = Zip,
                tel = Phone,
            };
        }
        public void CopyToHopitaux(Hopitaux hopital)
        { // Utilisez cette fonction pour copier un UserView dans en User
            hopital.id = Id;
            hopital.nom = Name;
            hopital.adresse = Adress;
            hopital.ville = City;
            hopital.province = Province;
            hopital.codePost = Zip;
            hopital.tel = Phone;
        }
        public void CopyToHospitalView(HospitalView hopital)
        { // Utilisez cette fonction pour copier un UserView dans un autre UserView
            Id = hopital.Id;
            Name = hopital.Name;
            Adress = hopital.Adress;
            City = hopital.City;
            Province = hopital.Province;
            Zip = hopital.Zip;
            Phone = hopital.Phone;
        }
    }
}