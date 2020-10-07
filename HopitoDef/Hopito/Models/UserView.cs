using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Web;
using System.Threading.Tasks;

namespace Hopito.Models
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
                id = this.Id,
                email = this.Email,
                password = this.Password,
                fname = this.FirstName,
                name = this.LastName,
                dateNaissance = this.Birthdate,
                tel = this.Phone,
                assMaladie = this.NAM,
                admin = this.Admin
            };
        }
        public void CopyToUser(User user)
        { // Utilisez cette fonction pour copier un UserView dans en User
            user.id = Id;
            user.email = Email;
            user.password = Password;
            user.fname = FirstName;
            user.name = LastName;
            user.dateNaissance = Birthdate;
            user.tel = Phone;
            user.assMaladie = NAM;
            user.admin = Admin;
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

    public static class OnlineUsers
    {
        public static List<UserView> Users
        {
            get
            {
                if (HttpRuntime.Cache["OnLineUsers"] == null)
                    HttpRuntime.Cache["OnLineUsers"] = new List<UserView>();
                return (List<UserView>)HttpRuntime.Cache["OnLineUsers"];
            }
        }

        public static UserView CurrentUser
        {
            get
            {
                try
                {
                    return (UserView)HttpContext.Current.Session["User"];
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    HttpContext.Current.Session.Timeout = 30;
                    HttpContext.Current.Session["User"] = value;
                }
                else
                {
                    if (HttpContext.Current != null)
                        HttpContext.Current.Session.Abandon();
                }
            }
        }
        public static void AddSessionUser(UserView user)
        {
            Users.Add(user);
            CurrentUser = user;
        }
        public static void RemoveSessionUser()
        {
            if (Users != null)
            {
                Users.Remove(CurrentUser);
                CurrentUser = null;
            }
        }
        public static void Remove(UserView userView)
        {
            if (Users != null)
            {
                if (CurrentUser.Id == userView.Id)
                {
                    RemoveSessionUser();
                }
                else
                {
                    Users.Remove(userView);
                }

            }
        }
        public static UserView GetSessionUser()
        {
            return CurrentUser;
        }

        public static UserView Find(int userId)
        {
            return Users.Where(u => u.Id == userId).FirstOrDefault();
        }
        public static bool CurrentUserIsAdmin()
        {
            UserView user = CurrentUser;
            if (user != null)
                return user.Admin;
            return false;
        }
        public static bool IsOnLine(int userId)
        {
            foreach (UserView user in Users)
            {
                if (user.Id == userId)
                    return true;
            }
            return false;
        }
    }
}
