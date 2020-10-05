using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_H.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Phone { get; set; }
        public string NAM { get; set; }
        public bool Admin { get; set; }
    }
}