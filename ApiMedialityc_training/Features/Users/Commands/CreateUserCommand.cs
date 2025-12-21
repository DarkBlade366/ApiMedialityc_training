using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc_training.Features.Users.DTOs;

namespace ApiMedialityc_training.Features.Users.Commands
{
    public class CreateUserCommand
    {
        public CreateUserRequestDto Request { get; set; }
    
        public CreateUserCommand(CreateUserRequestDto request)
        {
            Request = request;
        }
    }
}