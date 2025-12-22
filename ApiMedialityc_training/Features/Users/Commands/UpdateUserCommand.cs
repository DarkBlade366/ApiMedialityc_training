using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc_training.Features.Users.DTOs;

namespace ApiMedialityc_training.Features.Users.Commands
{
    public class UpdateUserCommand
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public List<UserEmailDto> Emails { get; set; }
        public List<UserPhoneDto> Phones { get; set; }

        public UpdateUserCommand(Guid userId, string fullName, List<UserEmailDto> emails, List<UserPhoneDto> phones)
        {
            UserId = userId;
            FullName = fullName;
            Emails = emails;
            Phones = phones;
        }
    }
}