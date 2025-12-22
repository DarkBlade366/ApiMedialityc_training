using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc_training.Features.Users.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }  = string.Empty;
        public bool IsActive { get; set; }  = true;
        public string Password { get; set; } = string.Empty; 

        public ICollection<UserEmail> Emails { get; set; } = new List<UserEmail>();
        public ICollection<UserPhone> Phones { get; set; } = new List<UserPhone>();
        public string Role { get; set; } = "User";

    }
}