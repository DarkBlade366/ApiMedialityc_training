using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc_training.Features.Users.Models
{
    public class UserPhone
    {
        public Guid Id { get; set; }
        public string Phone { get; set; }  = string.Empty;
        public Guid UserId { get; set; }
    }
}