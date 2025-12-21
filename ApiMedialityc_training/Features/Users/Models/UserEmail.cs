using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc_training.Features.Users.Models
{
    public class UserEmail
    {
        public Guid Id { get; set; }
        public string Email { get; set; }  = string.Empty;
        public Guid UserId { get; set; }
    }
}