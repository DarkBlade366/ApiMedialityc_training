using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc_training.Data;
using ApiMedialityc_training.Features.Users.Commands;
using ApiMedialityc_training.Features.Users.DTOs;
using ApiMedialityc_training.Features.Users.Models;

namespace ApiMedialityc_training.Features.Users.Handlers
{
    public class CreateUserHandler
    {
        private readonly MedialitycDBContext _context;
    
        public CreateUserHandler(MedialitycDBContext context)
        {
            _context = context;
        }
    
        public async Task<UserResponseDto> HandleAsync(CreateUserCommand command)
        {
            var dto = command.Request;
    
            // Created user
            var user = new User
            {
                FullName = dto.FullName,
                Role = "User"
            };
    
            // Emails
            user.Emails = dto.Emails.Select(e => new UserEmail { Email = e }).ToList();
    
            // Phones
            user.Phones = dto.Phones.Select(p => new UserPhone { Phone = p }).ToList();
    
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
    
            // Return DTO
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Role = user.Role,
                IsActive = user.IsActive,
                Emails = user.Emails.Select(e => e.Email).ToList(),
                Phones = user.Phones.Select(p => p.Phone).ToList()
            };
        }
    }
}