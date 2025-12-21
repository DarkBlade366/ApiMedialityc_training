using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc_training.Features.Users.DTOs
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public List<string> Emails { get; set; } = new List<string>();
        public List<string> Phones { get; set; } = new List<string>();
        public string Role { get; set; } = "User";
    }
}