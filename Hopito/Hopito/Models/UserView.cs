using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Web;
using System.Threading.Tasks;

namespace Project_H.Models
{
    public class UserView
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Requis")]
        [Display(Name = "Adresse Courriel")]
        public string Email { get; set; }

        [Display(Name = "Mot de passe")]
        public string Password { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public string Phone { get; set; }

        public string NAM { get; set; }

        public bool Admin { get; set; }

        [Required(ErrorMessage = "Requis")]
        [Display(Name = "Nouveau mot de passe")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirmation")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "La confirmation ne correspond pas.")]
        public string Confirmation { get; set; }

        public User ToUser()
        {   // Attention cette méthode retourne une instance de User
            // qui ne sera pas utilisable en tant cible de modification
            // car il ne contiendra pas un object context
            // et l'instruction suivante générera une exception
            // DB.Entry(user).State = EntityState.Modified;
            return new User()
            {
                Id = this.Id,
                Email = this.Email,
                Password = this.Password,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Birthdate = this.Birthdate,
                Phone = this.Phone,
                NAM = this.NAM,
                Admin = this.Admin
            };
        }
        public void CopyToUser(User user)
        { // Utilisez cette fonction pour copier un UserView dans en User
            user.Id = Id;
            user.Email = Email;
            user.Password = Password;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Birthdate = Birthdate;
            user.Phone = Phone;
            user.NAM = NAM;
            user.Admin = Admin;
        }
        public void CopyToUserView(UserView user)
        { // Utilisez cette fonction pour copier un UserView dans un autre UserView
            user.Id = Id;
            user.Email = Email;
            user.Password = Password;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Birthdate = Birthdate;
            user.Phone = Phone;
            user.NAM = NAM;
            user.Admin = Admin;
        }
    }
    public class LoginView
    {
        [Required(ErrorMessage = "Requis")]
        [Display(Name = "Adresse Courriel")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Requis")]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
    public static class DBEntitiesExtensionsMethods
    {
        public static UserView ToUserView(this User user)
        {
            return new UserView()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthdate = user.Birthdate,
                Phone = user.Phone,
                NAM = user.NAM,
                Admin = user.Admin
            };
        }
        public static bool UserNameExist(this DBEntities DB, string userName)
        {
            User user = DB.Users.Where(u => u.UserName == userName).FirstOrDefault();
            return (user != null);
        }
        public static User FindByUserName(this DBEntities DB, string userName)
        {
            User user = DB.Users.Where(u => u.UserName == userName).FirstOrDefault();
            return user;
        }
        public static UserView AddUser(this DBEntities DB, UserView userView)
        {
            User user = userView.ToUser();
            user = DB.Users.Add(user);
            DB.SaveChanges();
            return user.ToUserView();
        }
        public static bool UpdateUser(this DBEntities DB, UserView userView)
        {
            User userToUpdate = DB.Users.Find(userView.Id);
            userView.CopyToUser(userToUpdate);
            DB.Entry(userToUpdate).State = EntityState.Modified;
            DB.SaveChanges();
            return true;
        }
        public static bool RemoveUser(this DBEntities DB, UserView userView)
        {
            User userToDelete = DB.Users.Find(userView.Id);
            DB.Users.Remove(userToDelete);
            DB.SaveChanges();
            return true;
        }
    }
}
